using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonFixed : ButtonTile {

		public ButtonFixed() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ButtonFixed];
			this.title = "Fixed Button";
			this.description = "Toggles colors.";
			this.CreateTextures();
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[8];
			this.Texture[(byte)ButtonSubTypes.BR] = "Button/Fixed/BR";
			this.Texture[(byte)ButtonSubTypes.BRDown] = "Button/Fixed/BRDown";
			this.Texture[(byte)ButtonSubTypes.BROff] = "Button/Fixed/BROff";
			this.Texture[(byte)ButtonSubTypes.BROffDown] = "Button/Fixed/BROffDown";
			this.Texture[(byte)ButtonSubTypes.GY] = "Button/Fixed/GY";
			this.Texture[(byte)ButtonSubTypes.GYDown] = "Button/Fixed/GYDown";
			this.Texture[(byte)ButtonSubTypes.GYOff] = "Button/Fixed/GYOff";
			this.Texture[(byte)ButtonSubTypes.GYOffDown] = "Button/Fixed/GYOffDown";
		}
	}
}
