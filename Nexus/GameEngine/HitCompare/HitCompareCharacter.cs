﻿using Microsoft.Xna.Framework;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class HitCompareCharacter : IHitCompare {

		public bool RunImpact( GameObject character, GameObject obj ) {

			Character ch = (Character) character;

			// Specific Impact Types
			if(obj is Enemy) { return ((Enemy)obj).RunCharacterImpact(ch); }
			if(obj is Item) { return this.CharHitsItem(ch, (Item) obj); }
			if(obj is Platform) { return this.CharHitsPlatform(ch, (Platform) obj); }
			if(obj is Projectile) { return this.CharHitsProjectile(ch, (Projectile) obj); }
			if(obj is Block) { return this.CharHitsBlock(ch, (Block)obj); }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(ch, obj);

			// Standard Collision
			return Impact.StandardImpact(ch, obj, dir);
		}

		public bool CharHitsItem( Character character, Item item ) {

			// Sport Ball
			if(item is SportBall) {
				return ((SportBall)item).RunCharacterImpact(character);
			}

			// If this item is being held, skip.
			if(item.isHeld) { return false; }

			// If the entity is intangible, don't collide with the Character.
			if(item.intangible > Systems.timer.Frame) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, item);

			// If item is hit from side:
			if(dir == DirCardinal.Left || dir == DirCardinal.Right) {
				
				// Check if Grabbing Item
				if(character.heldItem.WillPickupAttemptWork(item, dir)) {
					character.heldItem.PickUpItem(item);
					return true;
				}
			}

			// If item is hit from above:
			else if(dir == DirCardinal.Up) {

				// Check if Grabbing Item
				if(character.heldItem.WillPickupAttemptWork(item, character.FaceRight ? DirCardinal.Right : DirCardinal.Left)) {
					character.heldItem.PickUpItem(item);
					return true;
				}
			}

			// If we've made it this far, run standard collision with item:
			return Impact.StandardImpact(character, item, dir);
		}

		public bool CharHitsPlatform( Character character, Platform platform ) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, platform);
			if(dir != DirCardinal.Down) { return false; }

			// Character is "Dropping" through platforms:
			if(character.status.action is DropdownAction) {
				character.physics.touch.TouchMover(null);
				return false;
			}

			// Activate the Platform
			platform.ActivatePlatform();
			character.physics.touch.TouchMover(platform);

			// Assign the Character with the "OnMover" action, which will maintain their momentum after leaving the platform.
			ActionMap.OnMover.StartAction(character);

			// Special Character Collision for Platforms.
			// Only the Character should run collision. Platform has no need to be touched, pushed, aligned, etc.
			// Additionally, it needs to skip the intend.Y test normally associated with CollideObjDown(platform).
			character.physics.touch.TouchDown();
			character.physics.AlignUp(platform);
			character.physics.StopY();

			return true;
		}

		public bool CharHitsBlock( Character character, Block block ) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, block);
			return Impact.StandardImpact(character, block, dir);
		}

		public bool CharHitsProjectile( Character character, Projectile projectile ) {

			// Skip Character if they cast the projectile.
			if(projectile.ByCharacterId == character.id) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, projectile);

			// Set the projectile to bounce off of the screen:
			EndBounceParticle.SetParticle(character.room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], projectile.SpriteName, new Vector2(projectile.posX, projectile.posY), Systems.timer.Frame + 40, 1, 0.5f, 0f);

			// Destroy the Projectile
			projectile.Destroy();

			// If this projectile can be jumped on:
			if(projectile.SafelyJumpOnTop) {

				// Hit from above (projectile is below):
				if(dir == DirCardinal.Down) {
					//character.BounceUp(projectile.posX + projectile.bounds.MidX, 1, 0, 0);
					ActionMap.Jump.StartAction(character, 1, 0, 0, true, false);
					Systems.sounds.bulletJump.Play();
					return true;
				}

				// Hit from below (projectile is above):
				else if(dir == DirCardinal.Up && character.status.action is Action) {
					character.status.action.EndAction(character);
				}
			}

			// If wearing a hard hat, can ignore damage from above.
			if(dir == DirCardinal.Up) {
				if(character.hat is HardHat) { return true; }
			}

			character.wounds.ReceiveWoundDamage(projectile.Damage);

			return true;
		}
	}
}
