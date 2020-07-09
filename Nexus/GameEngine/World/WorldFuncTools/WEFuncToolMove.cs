using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class WEFuncToolMove : WEFuncTool {

		private bool activity;		// TRUE if the selection is active.
		private short xStart;		// X-Grid that selection started at.
		private short yStart;		// Y-Grid that selection started at.
		private short xEnd;			// X-Grid that selection ends at.
		private short yEnd;			// Y-Grid that selection ends at.

		public WEFuncToolMove() : base() {
			this.spriteName = "Small/Move";
			this.title = "Move Tool";
			this.description = "Drag and move objects.";
		}

		private byte BoxWidth { get { return (byte) (Math.Abs(this.xEnd - this.xStart) + 1); } }
		private byte BoxHeight { get { return (byte) (Math.Abs(this.yEnd - this.yStart) + 1); } }

		public void StartSelection(short gridX, short gridY) {
			this.activity = true;
			this.xStart = gridX;
			this.xEnd = gridX;
			this.yStart = gridY;
			this.yEnd = gridY;
		}
		
		private void EndSelection() {
			this.activity = false;

			// If we're currently using selection as a temporary tool, force it to be an active tool now that we have a selected area.
			if(WETools.WETempTool is WEFuncToolMove) {
				WETools.SetWorldFuncTool(this);
			}
		}

		public override void RunTick(WEScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

			if(this.activity == true) {

				// If left-mouse is released, end the selection:
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Released) {
					this.EndSelection();
				}
			}

			else {

				// If left-mouse clicks, start the selection or drag an existing one:
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					this.StartSelection(Cursor.MiniGridX, Cursor.MiniGridY);
				}
			}
		}

		public override void DrawWorldFuncTool() {

			short left = this.xStart <= this.xEnd ? this.xStart : this.xEnd;
			short top = this.yStart <= this.yEnd ? this.yStart : this.yEnd;
			int width = this.BoxWidth * (byte)WorldmapEnum.TileWidth;
			int height = this.BoxHeight * (byte)WorldmapEnum.TileHeight;

			// Draw Semi-Transparent Box over Selection
			if(this.activity != false) {
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(left * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, top * (byte)WorldmapEnum.TileHeight - Systems.camera.posY, width, height), Color.White * 0.25f);
			}

			// Draw Selection Icon
			UIHandler.atlas.Draw(this.spriteName, Cursor.MiniGridX * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, Cursor.MiniGridY * (byte)WorldmapEnum.TileHeight - Systems.camera.posY);
		}
	}
}
