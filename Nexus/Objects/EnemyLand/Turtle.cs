using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum TurtleSubType : byte {
		Red = 0,
	}

	public class Turtle : EnemyLand {

		public Turtle(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Turtle].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(1.0);

			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(15, 2, -2);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) TurtleSubType.Red) {
				this.animate = new Animate(this, "Turtle/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Turtle/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle2, 12);
		}

		public override bool GetJumpedOn(Character character, sbyte bounceStrength = 3) {
			Systems.sounds.shellBoop.Play(0.3f, 0f, 0f);
			ActionMap.Jump.StartAction(character, bounceStrength, 0, 4);
			return this.ReceiveWound();
		}

		public override bool ReceiveWound() {
			return this.Die(DeathResult.Squish);
		}

		public override bool Die( DeathResult deathType ) {

			// Knockouts and TNT can still occur, but squishing will cause other behaviors.
			if(deathType == DeathResult.Knockout) {
				return base.Die(deathType);
			}

			this.Destroy();

			// TODO HIGH PRIORITY: Uncomment once "Shell" item is created.
			// Create Shell In Turtle's Place
			//Shell shell = new Shell(this.scene, ShellSubType.Red, this.posX, this.posY);
			//this.scene.AddToObjects(shell);

			return true;
		}

	}
}
