using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldFuncTool {

		public enum WorldFuncToolEnum : byte {
			None,
			Select,
			Eraser,
			Eyedrop,
			Move,
			Blueprint,
			Wand,
		}

		public static Dictionary<byte, WorldFuncTool> WorldFuncToolMap = new Dictionary<byte, WorldFuncTool>() {
			{ (byte) WorldFuncToolEnum.Eraser, new WorldFuncToolEraser() },
			{ (byte) WorldFuncToolEnum.Eyedrop, new WorldFuncToolEyedrop() },
			{ (byte) WorldFuncToolEnum.Wand, new WorldFuncToolWand() },
		};

		public static Dictionary<Keys, byte> WorldfuncToolKey = new Dictionary<Keys, byte>() {
			{ Keys.X, (byte) WorldFuncToolEnum.Eraser },
			{ Keys.C, (byte) WorldFuncToolEnum.Eyedrop },
			{ Keys.V, (byte) WorldFuncToolEnum.Select },
			{ Keys.E, (byte) WorldFuncToolEnum.Wand },
		};

		public Atlas atlas;
		public string spriteName;
		public string title;
		public string description;

		public WorldFuncTool() {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
		}

		public virtual void RunTick(WorldEditorScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }
		}

		public virtual void DrawWorldFuncTool() {
			this.atlas.Draw(this.spriteName, Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
		}
	}
}
