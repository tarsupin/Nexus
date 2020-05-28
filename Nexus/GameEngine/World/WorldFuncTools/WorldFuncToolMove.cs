using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class WorldFuncToolMove : WorldFuncTool {

		private bool activity;		// TRUE if the selection is active.
		private ushort xStart;		// X-Grid that selection started at.
		private ushort yStart;		// Y-Grid that selection started at.
		private ushort xEnd;		// X-Grid that selection ends at.
		private ushort yEnd;		// Y-Grid that selection ends at.

		public WorldFuncToolMove() : base() {
			this.spriteName = "Icons/Move";
			this.title = "Move Tool";
			this.description = "Drag and move objects.";
		}

		private byte BoxWidth { get { return (byte) (Math.Abs(this.xEnd - this.xStart) + 1); } }
		private byte BoxHeight { get { return (byte) (Math.Abs(this.yEnd - this.yStart) + 1); } }

		public void StartSelection(ushort gridX, ushort gridY) {
			this.activity = true;
			this.xStart = gridX;
			this.xEnd = gridX;
			this.yStart = gridY;
			this.yEnd = gridY;
		}
		
		private void EndSelection() {
			this.activity = false;

			// If we're currently using selection as a temporary tool, force it to be an active tool now that we have a selected area.
			if(WorldEditorTools.WorldTempTool is WorldFuncToolMove) {
				WorldEditorTools.SetWorldFuncTool(this);
			}
		}

		public override void RunTick(WorldEditorScene scene) {
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
					this.StartSelection(Cursor.MouseGridX, Cursor.MouseGridY);
				}
			}
		}

		public override void DrawWorldFuncTool() {

			ushort left = this.xStart <= this.xEnd ? this.xStart : this.xEnd;
			ushort top = this.yStart <= this.yEnd ? this.yStart : this.yEnd;
			int width = this.BoxWidth * (byte)WorldmapEnum.TileWidth;
			int height = this.BoxHeight * (byte)WorldmapEnum.TileHeight;

			// Draw Semi-Transparent Box over Selection
			if(this.activity != false) {
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(left * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, top * (byte)WorldmapEnum.TileHeight - Systems.camera.posY, width, height), Color.White * 0.25f);
			}

			// Draw Selection Icon
			this.atlas.Draw(this.spriteName, Cursor.MouseGridX * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, Cursor.MouseGridY * (byte)WorldmapEnum.TileHeight - Systems.camera.posY);
		}
	}
}
