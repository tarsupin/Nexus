using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public enum PokeSubType : byte {
		Poke,
	}

	public class Poke : EnemyLand {

		public Poke(RoomScene room, byte subType, FVector pos, JObject paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Movement
			this.speed = FInt.Create(0.8);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 8, -8, -2);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) PokeSubType.Poke) {
				this.animate = new Animate(this, "Poke/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Poke/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 15);
		}
	}
}
