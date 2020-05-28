using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldFuncBut {

		public Atlas atlas;
		public string keyChar;
		public string spriteName;
		public string title;
		public string description;

		public enum WorldFuncButEnum : byte {
			Info,
			Select,
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

		public static Dictionary<byte, WorldFuncBut> WorldFuncButMap = new Dictionary<byte, WorldFuncBut>() {
			{ (byte) WorldFuncButEnum.Info, new WorldFuncButInfo() },
			{ (byte) WorldFuncButEnum.Select, new WorldFuncButSelect() },
			{ (byte) WorldFuncButEnum.Eraser, new WorldFuncButEraser() },
			{ (byte) WorldFuncButEnum.Eyedrop, new WorldFuncButEyedrop() },
			{ (byte) WorldFuncButEnum.Wand, new WorldFuncButWand() },
			{ (byte) WorldFuncButEnum.Settings, new WorldFuncButSettings() },
			{ (byte) WorldFuncButEnum.Undo, new WorldFuncButUndo() },
			{ (byte) WorldFuncButEnum.Redo, new WorldFuncButRedo() },
			{ (byte) WorldFuncButEnum.RoomLeft, new WorldFuncButRoomLeft() },
			{ (byte) WorldFuncButEnum.Home, new WorldFuncButHome() },
			{ (byte) WorldFuncButEnum.RoomRight, new WorldFuncButRoomRight() },
			{ (byte) WorldFuncButEnum.SwapRight, new WorldFuncButSwapRight() },
			{ (byte) WorldFuncButEnum.Save, new WorldFuncButSave() },
			{ (byte) WorldFuncButEnum.Play, new WorldFuncButPlay() },
		};

		public WorldFuncBut() {
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
