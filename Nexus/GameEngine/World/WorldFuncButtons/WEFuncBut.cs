using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WEFuncBut {

		public Atlas atlas;
		public string keyChar;
		public string spriteName;
		public string title;
		public string description;

		public enum WEFuncButEnum : byte {
			Info,
			Move,
			Eraser,
			Eyedrop,
			
			Wand,
			Settings,
			
			Undo,
			Redo,
			
			RoomLeft,
			Home,
			RoomRight,
			SwapRight,

			Save,
			Play,
		}

		public static Dictionary<byte, WEFuncBut> WEFuncButMap = new Dictionary<byte, WEFuncBut>() {
			{ (byte) WEFuncButEnum.Info, new WEFuncButInfo() },
			{ (byte) WEFuncButEnum.Move, new WEFuncButMove() },
			{ (byte) WEFuncButEnum.Eraser, new WEFuncButEraser() },
			{ (byte) WEFuncButEnum.Eyedrop, new WEFuncButEyedrop() },
			{ (byte) WEFuncButEnum.Wand, new WEFuncButWand() },
			{ (byte) WEFuncButEnum.Settings, new WEFuncButSettings() },
			{ (byte) WEFuncButEnum.Undo, new WEFuncButUndo() },
			{ (byte) WEFuncButEnum.Redo, new WEFuncButRedo() },
			{ (byte) WEFuncButEnum.RoomLeft, new WEFuncButZoneLeft() },
			{ (byte) WEFuncButEnum.Home, new WEFuncButHome() },
			{ (byte) WEFuncButEnum.RoomRight, new WEFuncButZoneRight() },
			{ (byte) WEFuncButEnum.SwapRight, new WEFuncButSwapRight() },
			{ (byte) WEFuncButEnum.Save, new WEFuncButSave() },
			{ (byte) WEFuncButEnum.Play, new WEFuncButPlay() },
		};

		public WEFuncBut() {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
		}

		public virtual void ActivateWorldFuncButton() {}

		public void DrawFunctionTile(int posX, int posY) {
			this.atlas.Draw(this.spriteName, posX, posY);

			if(keyChar.Length > 0) {
				Systems.fonts.baseText.Draw(keyChar, posX + 2, posY + (byte) TilemapEnum.TileHeight - 18, Color.DarkOrange);
			}
		}
	}
}
