﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Newtonsoft.Json.Linq;

namespace Nexus.Objects {

	public enum OctoSubType : byte {
		Octo,
	}

	public class Octo : EnemyLand {

		public Octo(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyLand];

			// Movement
			this.speed = FInt.Create(0.6);

			// Physics, Collisions, etc.
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
			this.physics.velocity.X = (FInt)(0 - this.speed);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 6, -4, -4);
		}

		private void AssignSubType( byte subType ) {
			if(subType == (byte) OctoSubType.Octo) {
				this.animate = new Animate(this, "Octo/");
				this.OnDirectionChange();
			}
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Octo/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 15);
		}
	}
}