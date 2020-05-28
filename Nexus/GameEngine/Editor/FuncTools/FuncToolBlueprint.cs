﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	// This class tracks a grid up to 20x20 blueprint of tiles, allowing user to place them all at once.
	public class FuncToolBlueprint : FuncTool {

		public static byte BPMaxWidth = 16;
		public static byte BPMaxHeight = 16;

		private ArrayList[,,] gridTrack = new ArrayList[4, FuncToolBlueprint.BPMaxWidth, FuncToolBlueprint.BPMaxHeight];

		private bool isActive;				// TRUE if the blueprint is active.
		private ushort blueprintHeight;		// Width of the blueprint.
		private ushort blueprintWidth;		// Height of the blueprint.
		private sbyte xOffset;				// X-offset to drag the blueprint at, respective to the cursor.
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

			RoomFormat roomData = scene.levelContent.data.rooms[scene.roomID];

			var mainData = roomData.main;
			var objData = roomData.obj;
			var bgData = roomData.bg;
			var fgData = roomData.fg;

			// Loop through the blueprint:
			for(ushort y = 0; y < this.blueprintHeight; y++) {
				string yPos = (top + y).ToString();

				for(ushort x = 0; x < this.blueprintWidth; x++) {
					string xPos = (left + x).ToString();

					this.AddToBlueprintTile(mainData, LayerEnum.main, x, y, xPos, yPos);
					this.AddToBlueprintTile(objData, LayerEnum.obj, x, y, xPos, yPos);
					this.AddToBlueprintTile(bgData, LayerEnum.bg, x, y, xPos, yPos);
					this.AddToBlueprintTile(fgData, LayerEnum.fg, x, y, xPos, yPos);
				}
			}
		}

		public void AddToBlueprintTile(Dictionary<string, Dictionary<string, ArrayList>> layerData, LayerEnum layerEnum, ushort x, ushort y, string xPos, string yPos) {

			if(!layerData.ContainsKey(yPos) || !layerData[yPos].ContainsKey(xPos)) {
				this.gridTrack[(byte) layerEnum, y, x] = null;
				return;
			}

			// Save the value stored in this blueprint at correct tile position:
			var tileData = layerData[yPos][xPos];

			this.gridTrack[(byte) layerEnum, y, x] = tileData;
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
				this.PasteBlueprint(scene, Cursor.TileGridX, Cursor.TileGridY);
				this.SwitchToSelectTool();
			}
		}

		public void PasteBlueprint(EditorRoomScene scene, ushort xStart, ushort yStart) {
			if(this.isActive == false) { return; }

			xStart = (ushort) (xStart + this.xOffset < 0 ? 0 : xStart + this.xOffset);
			yStart = (ushort) (yStart + this.yOffset < 0 ? 0 : yStart + this.yOffset);

			RoomFormat roomData = scene.levelContent.data.rooms[scene.roomID];

			var mainData = roomData.main;
			var objData = roomData.obj;
			var bgData = roomData.bg;
			var fgData = roomData.fg;

			// Loop through the blueprint:
			for(ushort y = 0; y < this.blueprintHeight; y++) {
				for(ushort x = 0; x < this.blueprintWidth; x++) {
					this.PasteBlueprintByLayer(scene, objData, LayerEnum.obj, x, y, xStart, yStart);
					this.PasteBlueprintByLayer(scene, mainData, LayerEnum.main, x, y, xStart, yStart);
					this.PasteBlueprintByLayer(scene, bgData, LayerEnum.bg, x, y, xStart, yStart);
					this.PasteBlueprintByLayer(scene, fgData, LayerEnum.fg, x, y, xStart, yStart);
				}
			}
		}

		public void PasteBlueprintByLayer(EditorRoomScene scene, Dictionary<string, Dictionary<string, ArrayList>> layerData, LayerEnum layerEnum, ushort x, ushort y, ushort xStart, ushort yStart) {

			// Get the value stored in this blueprint at correct tile position:
			ArrayList bpData = this.gridTrack[(byte) layerEnum, y, x];

			if(bpData == null) { return; }

			// Copy the blueprint at correct tile position in level editor:
			if(bpData.Count > 2) {
				scene.PlaceTile(layerData, layerEnum, (ushort)(xStart + x), (ushort)(yStart + y), byte.Parse(bpData[0].ToString()), byte.Parse(bpData[1].ToString()), (Dictionary<string, object>)bpData[2]);
			} else {
				scene.PlaceTile(layerData, layerEnum, (ushort)(xStart + x), (ushort)(yStart + y), byte.Parse(bpData[0].ToString()), byte.Parse(bpData[1].ToString()));
			}
		}

		public override void DrawFuncTool() {

			ushort xStart = (ushort)(Cursor.TileGridX + this.xOffset < 0 ? 0 : Cursor.TileGridX + this.xOffset);
			ushort yStart = (ushort)(Cursor.TileGridY + this.yOffset < 0 ? 0 : Cursor.TileGridY + this.yOffset);

			// Loop through the blueprint:
			for(ushort y = 0; y < this.blueprintHeight; y++) {
				for(ushort x = 0; x < this.blueprintWidth; x++) {
					DrawBlueprintByLayer(LayerEnum.bg, x, y, xStart, yStart);
					DrawBlueprintByLayer(LayerEnum.main, x, y, xStart, yStart);
					DrawBlueprintByLayer(LayerEnum.obj, x, y, xStart, yStart);
					DrawBlueprintByLayer(LayerEnum.fg, x, y, xStart, yStart);
				}
			}

			// Draw Semi-Transparent Box over Selection
			Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(xStart * (byte)TilemapEnum.TileWidth - Systems.camera.posX, yStart * (byte)TilemapEnum.TileHeight - Systems.camera.posY, this.blueprintWidth * (byte)TilemapEnum.TileWidth, this.blueprintHeight * (byte)TilemapEnum.TileHeight), Color.White * 0.25f);
		}

		public void DrawBlueprintByLayer(LayerEnum layerEnum, ushort x, ushort y, ushort xStart, ushort yStart) {

			// Get the value stored in this blueprint at correct tile position:
			ArrayList bpData = this.gridTrack[(byte) layerEnum, y, x];
			if(bpData == null) { return; }

			// Verify that a TileID (or ObjectID) is present
			byte objOrTileID = byte.Parse(bpData[0].ToString());
			if(objOrTileID == 0) { return; }

			// Drawing Objects
			if(layerEnum == LayerEnum.obj) {
				ShadowTile.Draw(objOrTileID, byte.Parse(bpData[1].ToString()), null, (xStart + x) * (byte)TilemapEnum.TileWidth - Systems.camera.posX, (yStart + y) * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
			}

			// Drawing Tiles
			else {
				var tileDict = Systems.mapper.TileDict;

				if(tileDict.ContainsKey(objOrTileID)) {
					TileObject tgo = tileDict[objOrTileID];
					tgo.Draw(null, byte.Parse(bpData[1].ToString()), (xStart + x) * (byte)TilemapEnum.TileWidth - Systems.camera.posX, (yStart + y) * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
				}
			}
		}
	}
}
