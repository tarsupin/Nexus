using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum BouncerSubType : byte { Normal }

	public class Bouncer : EnemyFlight {

		public float rotation;

		public Bouncer(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Bouncer].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, -2);

			// Initialize Bounce-Like Movement
			this.physics.velocity.X = FInt.Create(paramList == null || !paramList.ContainsKey("x") ? 2 : paramList["x"]);
			this.physics.velocity.Y = FInt.Create(paramList == null || !paramList.ContainsKey("y") ? 2 : paramList["y"]);
			this.UpdateRotation();
		}

		public override bool RunCharacterImpact(Character character) {
			character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			return false;
		}

		public override void RunTick() {
			this.physics.RunPhysicsTick();
		}

		public override void CollidePosRight(int posX) {
			var bounceX = this.physics.velocity.X.Inverse;
			base.CollidePosRight(posX);
			this.physics.velocity.X = bounceX;
			this.UpdateRotation();
		}
		
		public override void CollidePosLeft(int posX) {
			var bounceX = this.physics.velocity.X.Inverse;
			base.CollidePosLeft(posX);
			this.physics.velocity.X = bounceX;
			this.UpdateRotation();
		}
		
		public override void CollidePosDown(int posY) {
			var bounceY = this.physics.velocity.Y.Inverse;
			base.CollidePosDown(posY);
			this.physics.velocity.Y = bounceY;
			this.UpdateRotation();
		}
		
		public override void CollidePosUp(int posY) {
			var bounceY = this.physics.velocity.Y.Inverse;
			base.CollidePosUp(posY);
			this.physics.velocity.Y = bounceY;
			this.UpdateRotation();
		}

		private void UpdateRotation() {
			this.rotation = Radians.GetRadiansBetweenCoords(0, 0, this.physics.velocity.X.RoundInt, this.physics.velocity.Y.RoundInt);
		}

		public override void Draw(int camX, int camY) {
			//this.Meta.Atlas.DrawAdvanced(this.SpriteName, this.posX - camX, this.posY - camY, null, this.rotation);
			this.Meta.Atlas.DrawAdvanced(this.SpriteName, this.posX - camX, this.posY - camY, null, this.rotation);
		}

		private void AssignSubType( byte subType ) {
			//this.SpriteName = "Bouncer/Norm";
			this.SpriteName = "Shroom/Black/Right2";
		}
	}
}
