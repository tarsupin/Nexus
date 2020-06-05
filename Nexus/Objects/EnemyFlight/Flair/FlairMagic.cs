using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum FlairMagicSubType : byte { Normal };

	public class FlairMagic : Elemental {

		public FlairMagic(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.FlairMagic].meta;
			this.AssignSubType(subType);
		}

		private void AssignSubType(byte subType) {
			this.animate = new Animate(this, "Flair/Magic/");
			this.animate.SetAnimation("Flair/Magic/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 12);
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Flair/Magic/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3Reverse, 12);
		}
	}
}
