using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Shell : Item {

		public enum ShellSubType : byte {
			Green,
			GreenWing,
			Heavy,
			Red,
		}

		public Shell(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Shell].meta;
			this.ThrowStrength = 14;
			this.KickStrength = 7;

			// Grip Points (When Held)
			this.gripLeft = -31;
			this.gripRight = 21;
			this.gripLift = -8;

			// Physics
			this.physics.SetGravity(FInt.Create(0.4));

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(10, 2, -2, 0);

			// Handle Params
			if(paramList != null && paramList.ContainsKey("x")) { this.physics.velocity.X = FInt.Create(paramList["x"]); }
			if(paramList != null && paramList.ContainsKey("y")) { this.physics.velocity.Y = FInt.Create(paramList["y"]); }
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ShellSubType.Green) {
				this.SpriteName = "Shell/Green/Spin1";
				this.animate = new Animate(this, "Shell/Green/Spin");
			} else if(subType == (byte) ShellSubType.GreenWing) {
				this.physics.SetGravity(FInt.Create(0.35));
				this.SpriteName = "Shell/GreenWing/Spin1";
				this.animate = new Animate(this, "Shell/GreenWing/Spin");
			} else if(subType == (byte) ShellSubType.Heavy) {
				this.physics.SetGravity(FInt.Create(0.6));
				this.KickStrength = 5;
				this.SpriteName = "Shell/Heavy/Spin1";
				this.animate = new Animate(this, "Shell/Heavy/Spin");
			} else if(subType == (byte) ShellSubType.Red) {
				this.SpriteName = "Shell/Red/Spin1";
				this.animate = new Animate(this, "Shell/Red/Spin");
			}
		}

		public override void Destroy() {
			EndBounceParticle.SetParticle(this.room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], this.SpriteName, new Vector2(this.posX + this.bounds.Left - 2, this.posY + this.bounds.Top - 10), Systems.timer.Frame + 40);
			Systems.sounds.shellBoop.Play(0.15f, 0, 0);
			base.Destroy();
		}

		public void BounceBack(int posX) {
			FInt velX = this.physics.velocity.X.Inverse;
			this.physics.StopX();
			this.physics.MoveToPosX(posX);
			this.physics.velocity.X = velX;
			this.animate.SetAnimation(null, velX > 0 ? AnimCycleMap.Cycle4 : AnimCycleMap.Cycle4Reverse, 7, 1);
		}

		public void HorizontalCollide(GameObject obj, int bouncePosX, DirCardinal dir) {

			// Enemy Collision
			if(obj is EnemyLand) {
				if(this.physics.velocity.X != 0) {
					((Enemy)obj).ReceiveWound();
				}
			}
			
			// Character Collision
			else if(obj is Character) {
				Character character = (Character)obj;

				// If the Shell is moving toward the character:
				if(this.physics.velocity.X != 0) {

					// Damages the character, unlesss they're wearing the Bamboo Hat.
					if(!(character.hat is BambooHat)) {
						((Character)obj).wounds.ReceiveWoundDamage(DamageStrength.Standard);
						this.BounceBack(bouncePosX);
						return;
					}
				}

				// Kick Shell
				character.heldItem.KickItem(this, dir == DirCardinal.Left ? DirCardinal.Right : DirCardinal.Left);
				return;
			}
			
			// Collides with another shell.
			else if(obj is Shell) {
				if(obj.physics.velocity.X == 0) { obj.Destroy(); }
				else { this.Destroy(); }
			}
			
			// Other objects.
			else {
				this.BounceBack(bouncePosX);
			}
		}

		public override bool CollideObjLeft(GameObject obj) {
			this.HorizontalCollide(obj, obj.posX + obj.bounds.Right - this.bounds.Left, DirCardinal.Left);
			return true;
		}

		public override bool CollideObjRight(GameObject obj) {
			this.HorizontalCollide(obj, obj.posX + obj.bounds.Left - this.bounds.Right, DirCardinal.Right);
			return true;
		}

		// Shell is Below
		public override bool CollideObjUp(GameObject obj) {

			// Collides with another shell.
			if(obj is Shell) {
				if(obj.physics.velocity.X == 0 && obj.physics.velocity.Y == 0) { obj.Destroy(); return false; }
				else { this.Destroy(); return false; }
			}
			
			// If the Shell is stationary:
			if(this.physics.velocity.X == 0 && this.physics.velocity.Y == 0) {

				if(obj is Character) {
					Character character = (Character)obj;
					character.heldItem.KickItem(this, character.FaceRight ? DirCardinal.Right : DirCardinal.Left);
					ActionMap.Jump.StartAction(character, 2, 0, 4, true);
					return true;
				}

				if(obj is Enemy) {
					Enemy enemy = (Enemy)obj;
					enemy.BounceUp(enemy.posX + enemy.bounds.MidX, 2);
				}

				// Start Shell Movingd
				int xDiff = CollideDetect.GetRelativeX(this, obj);
				this.physics.velocity.X = FInt.Create(xDiff > 0 ? this.KickStrength : -this.KickStrength);
				Systems.sounds.shellBoop.Play(0.2f, 0, 0);
				this.animate.SetAnimation(null, this.physics.velocity.X > 0 ? AnimCycleMap.Cycle4 : AnimCycleMap.Cycle4Reverse, 7, 1);

				return base.CollideObjUp(obj);
			}

			// If the Shell is moving:

			// Character will end the shell's movement and jump upward.
			if(obj is Character) {
				ActionMap.Jump.StartAction((Character)obj, 4, 0, 4, true);
				this.physics.StopX();
				this.animate.DisableAnimation();
				return base.CollideObjUp(obj);
			}

			// Damage Enemies
			if(obj is Enemy) {
				((Enemy)obj).ReceiveWound();
			}

			return false;
		}

		public override void CollidePosLeft(int posX) {
			this.BounceBack(posX);
			this.physics.touch.TouchLeft();
			Systems.sounds.shellThud.Play(0.6f, 0, 0);
		}

		public override void CollidePosRight(int posX) {
			this.BounceBack(posX);
			this.physics.touch.TouchRight();
			Systems.sounds.shellThud.Play(0.6f, 0, 0);
		}

		public override void CollidePosDown(int posY) {
			this.physics.touch.TouchUp();
			this.physics.StopY();
			this.physics.MoveToPosY(posY);

			// Only stop the shell's X momentum if it was equal to released momentum during a throw.
			if(this.physics.velocity.X.RoundInt == releasedMomentum) {
				this.physics.StopX();
				releasedMomentum = 0;
			}
		}

		public override bool CollideObjDown(GameObject obj) {

			// Damage Enemies
			if(obj is Enemy) {
				((Enemy)obj).ReceiveWound();
			}

			// Verify the object is moving Up. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.intend.Y >= 0) { return false; }

			this.physics.touch.TouchUp();
			this.physics.AlignDown(obj);
			this.physics.StopY();

			// Only stop the shell's X momentum if it was equal to released momentum during a throw.
			if(this.physics.velocity.X.RoundInt == releasedMomentum) {
				this.physics.StopX();
				releasedMomentum = 0;
			}

			return true;
		}
	}
}
