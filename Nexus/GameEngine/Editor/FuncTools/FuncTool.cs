using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class FuncTool {

		public enum FuncToolEnum : byte {
			None,
			Select,
			Eraser,
			Eyedrop,
			Move,
			Blueprint,
			Wand,
		}

		public static Dictionary<byte, FuncTool> funcToolMap = new Dictionary<byte, FuncTool>() {
			{ (byte) FuncToolEnum.Select, new FuncToolSelect() },
			{ (byte) FuncToolEnum.Eraser, new FuncToolEraser() },
			{ (byte) FuncToolEnum.Eyedrop, new FuncToolEyedrop() },
			{ (byte) FuncToolEnum.Blueprint, new FuncToolBlueprint() },
			{ (byte) FuncToolEnum.Wand, new FuncToolWand() },
		};

		public static Dictionary<Keys, byte> funcToolKey = new Dictionary<Keys, byte>() {
			{ Keys.X, (byte) FuncToolEnum.Eraser },
			{ Keys.C, (byte) FuncToolEnum.Eyedrop },
			{ Keys.V, (byte) FuncToolEnum.Select },
			{ Keys.E, (byte) FuncToolEnum.Wand },
		};

		public Atlas atlas;
		public string spriteName;
		public string title;
		public string description;

		public FuncTool() {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
		}

		public virtual void RunTick(EditorRoomScene scene) {}

		public virtual void DrawFuncTool() {
			this.atlas.Draw(this.spriteName, Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
		}
	}
}
