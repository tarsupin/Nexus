using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class FuncButton {

		public Atlas atlas;
		public string keyChar;
		public string spriteName;
		public string title;
		public string description;

		public enum FuncButtonEnum : byte {
			Info,
			Eraser,
			Move,
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

		public static Dictionary<byte, FuncButton> funcButtonMap = new Dictionary<byte, FuncButton>() {
			{ (byte) FuncButtonEnum.Info, new FuncButtonInfo() },
			{ (byte) FuncButtonEnum.Eraser, new FuncButtonEraser() },
			{ (byte) FuncButtonEnum.Move, new FuncButtonMove() },
			{ (byte) FuncButtonEnum.Eyedrop, new FuncButtonEyedrop() },
			{ (byte) FuncButtonEnum.Wand, new FuncButtonWand() },
			{ (byte) FuncButtonEnum.Settings, new FuncButtonSettings() },
			{ (byte) FuncButtonEnum.Undo, new FuncButtonUndo() },
			{ (byte) FuncButtonEnum.Redo, new FuncButtonRedo() },
			{ (byte) FuncButtonEnum.RoomLeft, new FuncButtonRoomLeft() },
			{ (byte) FuncButtonEnum.Home, new FuncButtonHome() },
			{ (byte) FuncButtonEnum.RoomRight, new FuncButtonRoomRight() },
			{ (byte) FuncButtonEnum.SwapRight, new FuncButtonSwapRight() },
			{ (byte) FuncButtonEnum.Save, new FuncButtonSave() },
			{ (byte) FuncButtonEnum.Play, new FuncButtonPlay() },
		};

		public FuncButton() {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
		}

		public virtual void ActivateFuncButton() {}

		public void DrawFunctionTile(int posX, int posY) {
			this.atlas.Draw(this.spriteName, posX, posY);

			if(keyChar.Length > 0) {
				Systems.fonts.baseText.Draw(keyChar, posX + 2, posY + (byte) TilemapEnum.TileHeight - 18, Color.DarkOrange);
			}
		}
	}
}
