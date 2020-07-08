using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum HoveringEyeSubType : byte { Eye }

	public class HoveringEye : EnemyFlight {

		private Character charWatched;

		private const int ViewDistance = 16 * (byte)TilemapEnum.TileWidth;
		private const byte BaseAttackSpeed = 6;

		public float rotation;

		public AttackSequence attack;
		public FInt attSpeed;
		public FInt attSpread;
		public byte attCount = 1;

		public HoveringEye(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.HoveringEye].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.shellCollision = true;
			this.SetCollide(CollideEnum.NoTileCollide);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			// Attack Details
			this.attack = new AttackSequence(paramList);
			this.attSpeed = FInt.Create(paramList == null || !paramList.ContainsKey("speed") ? BaseAttackSpeed : paramList["speed"] * 0.01 * BaseAttackSpeed);
			this.attSpread = FInt.Create(paramList == null || !paramList.ContainsKey("spread") ? 0.3f : paramList["spread"] * 0.01f * 0.3f);
			this.attCount = (byte)(paramList == null || !paramList.ContainsKey("count") ? 1 : paramList["count"]);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(6, 6, -6, -6);
		}

		public override void RunTick() {
			base.RunTick();

			// Get Center Bounds
			int midX = this.posX + this.bounds.MidX;
			int midY = this.posY + this.bounds.MidY;

			// Only change behaviors every 16 frames.
			if(Systems.timer.frame16Modulus == 15) {
				this.WatchForCharacter(midX, midY);
			}

			// Check for any attack to make this round.
			if(this.charWatched is Character && this.attack.AttackThisFrame()) {

				// Get distance from Character, if applicable.
				int destX = this.charWatched is Character ? this.charWatched.posX + this.charWatched.bounds.MidX - 10 : midX;
				int destY = this.charWatched is Character ? this.charWatched.posY + this.charWatched.bounds.MidY - 10 : midY;
				int distance = FPTrigCalc.GetDistance(FVector.Create(midX, midY), FVector.Create(destX, destY)).RoundInt;

				if(distance < ViewDistance) {

					FInt rotation = FPRadians.GetRadiansBetweenCoords(midX, midY, destX, destY);
					this.rotation = (float) rotation.ToDouble();

					if(this.attCount != 2) {
						this.ShootBolt(rotation, midX, midY);
					}

					if(this.attCount > 1) {
						this.ShootBolt(rotation + attSpread, midX, midY);
						this.ShootBolt(rotation - attSpread, midX, midY);
					}

					this.room.PlaySound(Systems.sounds.bolt, 0.6f, this.posX + 16, this.posY + 16);
				}
			}
		}

		private void ShootBolt(FInt rotation, int midX, int midY) {
			FInt r = FPRadians.Normalize(rotation);
			FInt velX = FInt.Create(Radians.GetXFromRotation((float) r.ToDouble(), (float) this.attSpeed.ToDouble()));
			FInt velY = FInt.Create(Radians.GetYFromRotation((float) r.ToDouble(), (float) this.attSpeed.ToDouble()));

			ProjectileEnemy projectile = ProjectileEnemy.Create(room, (byte)ProjectileEnemySubType.BoltBlue, FVector.Create(midX - 10, midY + 4), FVector.Create(velX, velY));
			projectile.physics.SetGravity(FInt.Create(0));
			projectile.rotation = (float) r.ToDouble();
		}

		private void WatchForCharacter(int midX, int midY) {

			// If there is no character in sight, check for a new one:
			if(this.charWatched is Character == false) {

				int objectId = CollideRect.FindOneObjectTouchingArea(
					this.room.objects[(byte)LoadOrder.Character],
					Math.Max(0, midX - ViewDistance),
					Math.Max(0, midY - ViewDistance),
					(ViewDistance * 2), // View Distance (Width)
					(ViewDistance * 2) // View Height
				);

				if(objectId > 0) {
					this.charWatched = (Character)this.room.objects[(byte)LoadOrder.Character][objectId];
				}
			}
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawAdvanced(this.SpriteName, this.posX - camX, this.posY - camY, null, this.rotation);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Eye/Eye";
		}

		public override bool RunCharacterImpact(Character character) {
			character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			return true;
		}
	}
}
