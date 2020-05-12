
using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections;

namespace Nexus.GameEngine {

	// This class allows the user to highlight a grid up to 20x20 squares.
	public class FuncToolSelect : FuncTool {

		private enum SelectActivity : byte {
			None,
			Working,
			HasSelection,
		}

		private SelectActivity activity;	// TRUE if the selection is active.
		private ushort xStart;				// X-Grid that selection started at.
		private ushort yStart;				// Y-Grid that selection started at.
		private ushort xEnd;				// X-Grid that selection ends at.
		private ushort yEnd;				// Y-Grid that selection ends at.

		public FuncToolSelect() : base() {
			this.spriteName = "Icons/Move";
			this.title = "Select";
			this.description = "No behavior at this time.";
		}

		private byte BoxWidth { get { return (byte) (Math.Abs(this.xEnd - this.xStart) + 1); } }
		private byte BoxHeight { get { return (byte) (Math.Abs(this.yEnd - this.yStart) + 1); } }

		public void StartSelection(ushort gridX, ushort gridY) {
			this.activity = SelectActivity.Working;
			this.xStart = gridX;
			this.xEnd = gridX;
			this.yStart = gridY;
			this.yEnd = gridY;
		}

		public void UpdatePosition(ushort gridX, ushort gridY) {
			this.xEnd = gridX;
			this.yEnd = gridY;
		}

		public void EndSelection() { this.activity = SelectActivity.HasSelection; }
		public void ClearSelection() { this.activity = SelectActivity.None; }

		public override void RunTick(EditorRoomScene scene) {

			if(this.activity == SelectActivity.Working) {

				// If left-mouse is held down, update the selection:
				if(Cursor.LeftMouseState == Cursor.MouseDownState.HeldDown) {
					this.UpdatePosition(Cursor.MouseGridX, Cursor.MouseGridY);
				}

				// If left-mouse is released, end the selection:
				else if(Cursor.LeftMouseState == Cursor.MouseDownState.Released) {
					this.UpdatePosition(Cursor.MouseGridX, Cursor.MouseGridY);
					this.EndSelection();
				}
			}

			else {

				// If left-mouse clicks, start the selection:
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					this.StartSelection(Cursor.MouseGridX, Cursor.MouseGridY);
				}
			}
		}

		public override void DrawFuncTool() {

			ushort left = this.xStart <= this.xEnd ? this.xStart : this.xEnd;
			ushort top = this.yStart <= this.yEnd ? this.yStart : this.yEnd;
			int width = this.BoxWidth * (byte)TilemapEnum.TileWidth;
			int height = this.BoxHeight * (byte)TilemapEnum.TileHeight;

			// Draw Semi-Transparent Box over Selection
			Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(left * (byte)TilemapEnum.TileWidth - Systems.camera.posX, top * (byte)TilemapEnum.TileHeight - Systems.camera.posY, width, height), Color.White * 0.25f);

			// Draw Selection Icon
			this.atlas.Draw(this.spriteName, Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
		}
	}
}
