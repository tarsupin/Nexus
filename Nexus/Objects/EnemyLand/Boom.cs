using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;
using static Nexus.Objects.Bomb;

namespace Nexus.Objects {

	public enum BoomSubType : byte {
		Boom = 0,
	}

	public class Boom : EnemyLand {

		public Boom(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Boom].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));

			// Sub-Types
			this.AssignSubType(subType);

			// Speed Handling
			this.speed = FInt.Create(1.2);

			this.physics.velocity.X = (FInt)(0 - this.speed);

			// Bounds
			this.AssignBoundsByAtlas(8, 4, -4);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) BoomSubType.Boom) {
				this.animate = new Animate(this, "Boom/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.physics.velocity.X = this.speed * (this.FaceRight ? 1 : -1);
			this.animate.SetAnimation("Boom/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 15);
		}

		public override bool GetJumpedOn( Character character, sbyte bounceStrength = 0 ) {
			this.Destroy(); // Can automatically destroy Boom, since it gets replaced with an item.
			Systems.sounds.thudWhop.Play();

			// Create Bomb in Boom's Place
			Bomb bomb = new Bomb(this.room, (byte) BombSubType.Bomb, FVector.Create(this.posX, this.posY), new Dictionary<string, short>() { { "on", 1 } });
			room.AddToScene(bomb, true);

			ActionMap.Jump.StartAction(character, 0, 0, 2, true, false);

			return true;
		}
	}
}
