using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum SnailSubType : byte {
		Snail = 0,
	}

	public class Snail : EnemyLand {

		public Snail(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Snail].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(0.6);

			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(10, 4, -4);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) SnailSubType.Snail) {
				this.animate = new Animate(this, "Snail/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Snail/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 15);
		}

		public override bool GetJumpedOn(Character character, sbyte bounceStrength = 3) {
			Systems.sounds.shellBoop.Play(0.3f, 0f, 0f);
			ActionMap.Jump.StartAction(character, bounceStrength, 0, 4);
			return this.ReceiveWound();
		}

		public override bool ReceiveWound() {
			return this.Die(DeathResult.Squish);
		}

		public override bool Die(DeathResult deathType) {

			// Knockouts and TNT can still occur, but squishing will cause other behaviors.
			if(deathType == DeathResult.Knockout) {
				return base.Die(deathType);
			}

			this.Destroy();

			// TODO HIGH PRIORITY: Uncomment once "Shell" item is created.
			// Create Shell In Snail's Place
			//Shell shell = new Shell(this.scene, ShellSubType.Red, this.posX, this.posY);
			//this.scene.AddToObjects(shell);

			return true;
		}

	}
}
