using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WEFuncTool {

		public enum WEFuncToolEnum : byte {
			None,
			Eraser,
			Eyedrop,
			Move,
			Wand,
		}

		public static Dictionary<byte, WEFuncTool> WEFuncToolMap = new Dictionary<byte, WEFuncTool>() {
			{ (byte) WEFuncToolEnum.Eraser, new WEFuncToolEraser() },
			{ (byte) WEFuncToolEnum.Eyedrop, new WEFuncToolEyedrop() },
			{ (byte) WEFuncToolEnum.Move, new WEFuncToolMove() },
			{ (byte) WEFuncToolEnum.Wand, new WEFuncToolWand() },
		};

		public static Dictionary<Keys, byte> WEFuncToolKey = new Dictionary<Keys, byte>() {
			{ Keys.X, (byte) WEFuncToolEnum.Eraser },
			{ Keys.C, (byte) WEFuncToolEnum.Eyedrop },
			{ Keys.V, (byte) WEFuncToolEnum.Move },
			{ Keys.E, (byte) WEFuncToolEnum.Wand },
		};

		public string spriteName;
		public string title;
		public string description;

		public WEFuncTool() {
			
		}

		public virtual void RunTick(WEScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }
		}

		public virtual void DrawWorldFuncTool() {
			UIHandler.atlas.Draw(this.spriteName, Cursor.MiniGridX * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, Cursor.MiniGridY * (byte)WorldmapEnum.TileHeight - Systems.camera.posY);
		}
	}
}
