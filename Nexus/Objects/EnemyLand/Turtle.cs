using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum TurtleSubType : byte {
		Red,
	}

	public class Turtle : EnemyLand {

		public Turtle(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Turtle].meta;

			// Movement
			this.speed = FInt.Create(1.0);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(15, 2, -2);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) TurtleSubType.Red) {
				this.animate = new Animate(this, "Turtle/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Turtle/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle2, 15);
		}

		public override bool Die( DeathResult deathType ) {

			// Knockouts still create standard effect:
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
