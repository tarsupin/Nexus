using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	// This class tracks a grid up to 20x20 blueprint of tiles, allowing user to place them all at once.
	public class FuncToolBlueprint : FuncTool {

		private ArrayList[,] gridTrack = new ArrayList[20, 20];

		private bool isActive;				// TRUE if the blueprint is active.
		private ushort blueprintHeight;		// Width of the blueprint.
		private ushort blueprintWidth;      // Height of the blueprint.
		private sbyte xOffset;               // X-offset to drag the blueprint at, respective to the cursor.
		private sbyte yOffset;				// Y-offset to drag the blueprint at, respective to the cursor.

		public FuncToolBlueprint() : base() {
			this.spriteName = "Icons/Blueprint";
			this.title = "Blueprint Tool";
			this.description = "Click to place the selected blueprint. Cancel with delete or by changing tools.";
		}

		public void PrepareBlueprint(EditorRoomScene scene, ushort xStart, ushort yStart, ushort xEnd, ushort yEnd, sbyte xOffset = 0, sbyte yOffset = 0) {
			
			this.isActive = true;

			ushort left = xStart <= xEnd ? xStart : xEnd;
			ushort top = yStart <= yEnd ? yStart : yEnd;

			this.blueprintWidth = (ushort)(Math.Abs(xEnd - xStart) + 1);
			this.blueprintHeight = (ushort)(Math.Abs(yEnd - yStart) + 1);
			this.xOffset = xOffset;
			this.yOffset = yOffset;

			var layerData = scene.levelContent.data.rooms[scene.roomID].main;

			// Loop through the blueprint:
			for(ushort y = 0; y < this.blueprintHeight; y++) {
				for(ushort x = 0; x < this.blueprintWidth; x++) {

					// Make sure tile exists:
					string yPos = (top + y).ToString();
					string xPos = (left + x).ToString();

					if(!layerData.ContainsKey(yPos) || !layerData[yPos].ContainsKey(xPos)) {
						this.gridTrack[y, x] = null;
						continue;
					}

					// Save the value stored in this blueprint at correct tile position:
					var tileData = layerData[yPos][xPos];

					this.gridTrack[y, x] = tileData;
				}
			}
		}

		private void ClearBlueprint() {
			this.isActive = false;
		}

		private void SwitchToSelectTool() {
			this.ClearBlueprint();
			FuncToolSelect selectFunc = (FuncToolSelect)FuncTool.funcToolMap[(byte)FuncToolEnum.Select];
			EditorTools.SetFuncTool(selectFunc);
		}

		public override void RunTick(EditorRoomScene scene) {
			if(!this.isActive || UIComponent.ComponentWithFocus != null) { return; }

			// If Delete is pressed:
			if(Systems.input.LocalKeyPressed(Keys.Delete)) {
				this.SwitchToSelectTool();
			}

			// If left-mouse clicks, start the selection:
			else if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.PasteBlueprint(scene, Cursor.MouseGridX, Cursor.MouseGridY);
				this.SwitchToSelectTool();
			}
		}

		public void PasteBlueprint(EditorRoomScene scene, ushort xStart, ushort yStart) {
			if(this.isActive == false) { return; }

			xStart = (ushort) (xStart + this.xOffset < 0 ? 0 : xStart + this.xOffset);
			yStart = (ushort) (yStart + this.yOffset < 0 ? 0 : yStart + this.yOffset);

			var layerData = scene.levelContent.data.rooms[scene.roomID].main;

			// Loop through the blueprint:
			for(ushort y = 0; y < this.blueprintHeight; y++) {
				for(ushort x = 0; x < this.blueprintWidth; x++) {

					// Get the value stored in this blueprint at correct tile position:
					ArrayList bpData = this.gridTrack[y, x];

					if(bpData == null) { continue; }

					// Copy the blueprint at correct tile position in level editor:
					if(bpData.Count > 2) {
						scene.PlaceTile(layerData, LayerEnum.main, (ushort)(xStart + x), (ushort)(yStart + y), byte.Parse(bpData[0].ToString()), byte.Parse(bpData[1].ToString()), (Dictionary<string, object>)bpData[2]);
					} else {
						scene.PlaceTile(layerData, LayerEnum.main, (ushort)(xStart + x), (ushort)(yStart + y), byte.Parse(bpData[0].ToString()), byte.Parse(bpData[1].ToString()));
					}
				}
			}
		}

		public override void DrawFuncTool() {
			var tileDict = Systems.mapper.TileDict;

			ushort xStart = (ushort)(Cursor.MouseGridX + this.xOffset < 0 ? 0 : Cursor.MouseGridX + this.xOffset);
			ushort yStart = (ushort)(Cursor.MouseGridY + this.yOffset < 0 ? 0 : Cursor.MouseGridY + this.yOffset);

			// Loop through the blueprint:
			for(ushort y = 0; y < this.blueprintHeight; y++) {
				for(ushort x = 0; x < this.blueprintWidth; x++) {

					// Get the value stored in this blueprint at correct tile position:
					ArrayList bpData = this.gridTrack[y, x];

					if(bpData == null) { continue; }

					byte tileId = byte.Parse(bpData[0].ToString());

					// Draw the Blueprint Tile at the correct coordinate:
					if(tileDict.ContainsKey(tileId)) {
						TileObject tgo = tileDict[tileId];
						tgo.Draw(null, byte.Parse(bpData[1].ToString()), (xStart + x) * (byte)TilemapEnum.TileWidth - Systems.camera.posX, (yStart + y) * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
					}
				}
			}

			// Draw Semi-Transparent Box over Selection
			Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(xStart * (byte)TilemapEnum.TileWidth - Systems.camera.posX, yStart * (byte)TilemapEnum.TileHeight - Systems.camera.posY, this.blueprintWidth * (byte)TilemapEnum.TileWidth, this.blueprintHeight * (byte)TilemapEnum.TileHeight), Color.White * 0.25f);
		}
	}
}
