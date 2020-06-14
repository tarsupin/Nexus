using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class SportBall : Item {

		private float rotation = 0;
		private uint lastTouchFrame = 0;
		private uint lastTouchId = 0;
		private FInt bounceX = FInt.Create(0.95f);
		private FInt bounceY = FInt.Create(0.8f);
		private FInt decelGround = FInt.Create(0.95f);
		private FInt decelAir = FInt.Create(0.99f);

		public enum SportBallSubType : byte {
			Earth = 1,
			Fire = 2,
			Forest = 3,
			Water = 4,
		}

		public SportBall(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.SportBall].meta;

			// Physics
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.4));

			this.ThrowStrength = 4;
			this.KickStrength = 4;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);
		}

		public override void RunTick() {
			base.RunTick();

			this.physics.velocity.X *= this.decelAir;

			// Set Rotation based on Velocity.
			this.rotation += (float)(this.physics.velocity.X.ToDouble() * 0.05f);
		}

		public override void CollidePosDown(int posY) {
			FInt velY = this.physics.velocity.Y;

			this.physics.touch.TouchDown();
			this.physics.StopY();
			this.physics.MoveToPosY(posY);

			this.physics.velocity.X *= this.decelGround;
			this.physics.velocity.Y -= velY * bounceY;
		}

		public override void CollidePosLeft(int posX) {
			FInt velX = this.physics.velocity.X;

			this.physics.touch.TouchLeft();
			this.physics.StopX();
			this.physics.MoveToPosX(posX);

			// Bounce Back
			this.physics.velocity.X -= velX * bounceX;
		}

		public override void CollidePosRight(int posX) {
			FInt velX = this.physics.velocity.X;

			this.physics.touch.TouchRight();
			this.physics.StopX();
			this.physics.MoveToPosX(posX);

			// Bounce Back
			this.physics.velocity.X -= velX * bounceX;
		}

		public bool RunCharacterImpact(Character character) {

			// Don't allow interaction if it's too recent.
			if(this.lastTouchId == character.id && this.lastTouchFrame + 20 > Systems.timer.Frame) { return false; }
			else if(this.lastTouchFrame + 2 > Systems.timer.Frame) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, this);

			// Update Last Touch Details
			this.lastTouchFrame = Systems.timer.Frame;
			this.lastTouchId = character.id;

			FInt boost = FInt.Create(Math.Round(Math.Abs(character.physics.velocity.X.RoundInt) * 0.5f));

			byte kickBoost = (byte)(this.KickStrength + boost);
			byte throwBoost = (byte)(this.ThrowStrength + boost + (byte) Math.Round(Math.Abs(character.physics.velocity.Y.RoundInt) * 0.5f));

			// Facing the same direction as the sport ball increases strength, and vice versa.
			if(dir == DirCardinal.Right) {
				if(character.FaceRight) { kickBoost += 2; throwBoost += 1; }
				else { kickBoost -= 2; throwBoost -= 1; }
			}

			else if(dir == DirCardinal.Left) {
				if(character.FaceRight) { kickBoost -= 2; throwBoost -= 1; }
				else { kickBoost += 2; throwBoost += 1; }
			}

			// Holding Up increases throw strength.
			if(character.input.isDown(IKey.Up)) { throwBoost += 4; }
			else if(character.input.isDown(IKey.Down)) { throwBoost -= 4; }

			if(dir == DirCardinal.Up) { throwBoost += 4; }
			else if(dir == DirCardinal.Down) { throwBoost -= 4; }

			// Holding A Button and/or Y Button increases kick strength.
			if(character.input.isDown(IKey.AButton)) { kickBoost += 2; }
			if(character.input.isDown(IKey.YButton)) { kickBoost += 2; }

			//DebugConfig.AddDebugNote("throw: " + throwBoost + "  kick: " + kickBoost);

			// Horizontal Hits
			if(dir == DirCardinal.Right) {
				this.physics.velocity.X += kickBoost;
				this.physics.velocity.Y -= throwBoost;
				return false;
			} else if(dir == DirCardinal.Left) {
				this.physics.velocity.X -= kickBoost;
				this.physics.velocity.Y -= throwBoost;
				return false;
			}

			// TODO: Add Kick Boost based on character velocity, holding left or right, etc.
			// TODO: Add Kick Boost based on character velocity, holding left or right, etc.


			// Vertical Hits
			if(dir == DirCardinal.Down) {
				this.physics.velocity.Y += throwBoost;
			} else if(dir == DirCardinal.Up) {
				this.physics.velocity.Y -= throwBoost;
			}

			return false;
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawAdvanced(this.SpriteName, this.posX - camX, this.posY - camY, null, this.rotation);
		}

		private void AssignSubType(byte subType) {
			
			if(subType == (byte) SportBallSubType.Earth) {
				this.SpriteName = "Orb/Earth";
				this.decelGround = FInt.Create(0.89f);
				this.decelAir = FInt.Create(0.99f);
				this.bounceX = FInt.Create(0.85f);
				this.bounceY = FInt.Create(0.5f);
				this.ThrowStrength = 3;
				this.KickStrength = 2;
			}
			
			else if(subType == (byte) SportBallSubType.Fire) {
				this.SpriteName = "Orb/Fire";
				this.decelGround = FInt.Create(0.97f);
				this.decelAir = FInt.Create(0.998f);
				this.bounceX = FInt.Create(1);
				this.bounceY = FInt.Create(0.9f);
				this.ThrowStrength = 7;
				this.KickStrength = 6;
			}
			
			else if(subType == (byte) SportBallSubType.Forest) {
				this.SpriteName = "Orb/Forest";
				this.decelGround = FInt.Create(0.94f);
				this.decelAir = FInt.Create(0.995f);
				this.bounceX = FInt.Create(0.95f);
				this.bounceY = FInt.Create(0.8f);
				this.ThrowStrength = 6;
				this.KickStrength = 5;
			}

			else if(subType == (byte) SportBallSubType.Water) {
				this.SpriteName = "Orb/Water";
				this.decelGround = FInt.Create(0.92f);
				this.decelAir = FInt.Create(0.993f);
				this.bounceX = FInt.Create(0.9f);
				this.bounceY = FInt.Create(0.7f);
				this.ThrowStrength = 5;
				this.KickStrength = 4;
			}
		}
	}
}
