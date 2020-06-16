using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum FlairNormalSubType : byte { Normal };

	public class FlairNormal : Flair {

		public FlairNormal(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.FlairNormal].meta;
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 4, -4, -10);
		}

		private void AssignSubType( byte subType ) {
			this.animate = new Animate(this, "Flair/Norm/");
			this.animate.SetAnimation("Flair/Norm/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Flair/Norm/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}
	}
}
