using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class SpringSide : SpringTile {

		public enum SpringSideSubType : byte {
			Left = 0,
			Right = 1,
		}

		public SpringSide() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Block];
			this.title = "Side Spring";
			this.description = "A spring that things bounce off of.";
			this.CreateTextures();
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)SpringSideSubType.Left] = "Spring/Left";
			this.Texture[(byte)SpringSideSubType.Right] = "Spring/Right";
		}
	}
}
