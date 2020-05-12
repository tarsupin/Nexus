
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

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
			this.title = "Selection Tool";
			this.description = "Drag a selection. Ctrl+C will copy, Ctrl+X will cut, Delete will end the selection.";
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
			if(Math.Abs(gridX - this.xStart) < 20) { this.xEnd = gridX; }
			if(Math.Abs(gridY - this.yStart) < 20) { this.yEnd = gridY; }
		}

		public void EndSelection() { this.activity = SelectActivity.HasSelection; }
		public void ClearSelection() { this.activity = SelectActivity.None; this.xEnd = this.xStart; this.yEnd = this.yStart; }

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

			if(this.activity != SelectActivity.None) {

				// If Delete is pressed:
				if(Systems.input.LocalKeyPressed(Keys.Delete)) {
					this.ClearSelection();
				}

				// If Control is being held down:
				else if(Systems.input.LocalKeyDown(Keys.LeftControl) || Systems.input.LocalKeyDown(Keys.RightControl)) {

					// Copy + Cut
					bool pressC = Systems.input.LocalKeyPressed(Keys.C);
					bool pressX = Systems.input.LocalKeyPressed(Keys.X);

					// Set the Blueprint FuncTool as active.
					if(pressC || pressX) {
						FuncToolBlueprint bpFunc = (FuncToolBlueprint) FuncTool.funcToolMap[(byte)FuncToolEnum.Blueprint];
						EditorTools.SetFuncTool(bpFunc);

						// Assign Grid Tiles
						bpFunc.PrepareBlueprint(scene, this.xStart, this.yStart, this.xEnd, this.yEnd);

						// If the selection was cut, remove the tiles:
						if(pressX) {
							this.CutTiles(scene);
						}

						this.ClearSelection();
					}
				}
			}
		}

		public void CutTiles(EditorRoomScene scene) {

			ushort left = this.xStart <= this.xEnd ? this.xStart : this.xEnd;
			ushort top = this.yStart <= this.yEnd ? this.yStart : this.yEnd;
			ushort right = this.xStart <= this.xEnd ? this.xEnd : this.xStart;
			ushort bottom = this.yStart <= this.yEnd ? this.yEnd : this.yStart;

			for(ushort y = top; y <= bottom; y++) {
				for(ushort x = left; x <= right; x++) {
					scene.DeleteTile(x, y);
				}
			}
		}

		public override void DrawFuncTool() {

			ushort left = this.xStart <= this.xEnd ? this.xStart : this.xEnd;
			ushort top = this.yStart <= this.yEnd ? this.yStart : this.yEnd;
			int width = this.BoxWidth * (byte)TilemapEnum.TileWidth;
			int height = this.BoxHeight * (byte)TilemapEnum.TileHeight;

			// Draw Semi-Transparent Box over Selection
			if(this.activity != SelectActivity.None) {
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(left * (byte)TilemapEnum.TileWidth - Systems.camera.posX, top * (byte)TilemapEnum.TileHeight - Systems.camera.posY, width, height), Color.White * 0.25f);
			}

			// Draw Selection Icon
			this.atlas.Draw(this.spriteName, Cursor.MouseGridX * (byte)TilemapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
		}
	}
}
