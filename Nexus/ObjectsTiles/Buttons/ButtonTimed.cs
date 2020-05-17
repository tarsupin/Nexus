using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonTimed : ButtonFixed {

		public ButtonTimed() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ButtonFixed];
			this.title = "Fixed Button";
			this.description = "Toggles colors, but the effect only lasts for 10 seconds.";
			this.CreateTextures();
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[8];
			this.Texture[(byte)ButtonSubTypes.BR] = "Button/Timed/BR";
			this.Texture[(byte)ButtonSubTypes.BRDown] = "Button/Timed/BRDown";
			this.Texture[(byte)ButtonSubTypes.BROff] = "Button/Timed/BROff";
			this.Texture[(byte)ButtonSubTypes.BROffDown] = "Button/Timed/BROffDown";
			this.Texture[(byte)ButtonSubTypes.GY] = "Button/Timed/GY";
			this.Texture[(byte)ButtonSubTypes.GYDown] = "Button/Timed/GYDown";
			this.Texture[(byte)ButtonSubTypes.GYOff] = "Button/Timed/GYOff";
			this.Texture[(byte)ButtonSubTypes.GYOffDown] = "Button/Timed/GYOffDown";
		}
	}
}
