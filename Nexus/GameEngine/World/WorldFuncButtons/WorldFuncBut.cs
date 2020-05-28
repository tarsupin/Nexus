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

		public static Dictionary<byte, WEFuncBut> WorldFuncButMap = new Dictionary<byte, WEFuncBut>() {
			{ (byte) WEFuncButEnum.Info, new WorldFuncButInfo() },
			{ (byte) WEFuncButEnum.Move, new WorldFuncButMove() },
			{ (byte) WEFuncButEnum.Eraser, new WorldFuncButEraser() },
			{ (byte) WEFuncButEnum.Eyedrop, new WorldFuncButEyedrop() },
			{ (byte) WEFuncButEnum.Wand, new WorldFuncButWand() },
			{ (byte) WEFuncButEnum.Settings, new WorldFuncButSettings() },
			{ (byte) WEFuncButEnum.Undo, new WorldFuncButUndo() },
			{ (byte) WEFuncButEnum.Redo, new WorldFuncButRedo() },
			{ (byte) WEFuncButEnum.RoomLeft, new WorldFuncButRoomLeft() },
			{ (byte) WEFuncButEnum.Home, new WorldFuncButHome() },
			{ (byte) WEFuncButEnum.RoomRight, new WorldFuncButRoomRight() },
			{ (byte) WEFuncButEnum.SwapRight, new WorldFuncButSwapRight() },
			{ (byte) WEFuncButEnum.Save, new WorldFuncButSave() },
			{ (byte) WEFuncButEnum.Play, new WorldFuncButPlay() },
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
