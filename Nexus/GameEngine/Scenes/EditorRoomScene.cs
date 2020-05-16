using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static Nexus.GameEngine.FuncTool;

namespace Nexus.GameEngine {

	public class EditorRoomScene : Scene {

		// References
		public readonly EditorScene scene;
		public LevelContent levelContent;
		public string roomID;

		Stopwatch stopwatch = new Stopwatch();

		// Editor Data
		private readonly ushort xCount;
		private readonly ushort yCount;
		private readonly int mapWidth;
		private readonly int mapHeight;

		public EditorRoomScene(EditorScene scene, string roomID) : base() {

			// References
			this.scene = scene;
			this.levelContent = this.scene.levelContent;
			this.roomID = roomID;

			// Build Tilemap with Correct Dimensions
			ushort xCount, yCount;
			RoomGenerate.DetectRoomSize(this.levelContent.data.rooms[roomID], out xCount, out yCount);

			// Sizing
			this.xCount = xCount;
			this.yCount = yCount;
			this.mapWidth = xCount * (byte)TilemapEnum.TileWidth;
			this.mapHeight = yCount * (byte)TilemapEnum.TileHeight;
		}

		public override int Width { get { return this.mapWidth; } }
		public override int Height { get { return this.mapHeight; } }

		public override void RunTick() {

			// A right click will clone the current tile.
			if(Cursor.mouseState.RightButton == ButtonState.Pressed) {
				this.CloneTile(Cursor.MouseGridX, Cursor.MouseGridY);
				return;
			}

			// Update Tools
			if(EditorTools.tempTool is FuncTool) {
				EditorTools.tempTool.RunTick(this);
			}
			
			else if(EditorTools.funcTool is FuncTool) {
				EditorTools.funcTool.RunTick(this);
			}

			else {
				this.TileToolTick(Cursor.MouseGridX, Cursor.MouseGridY);
			}

			// Faster Camera Movement (with arrow keys)
			InputClient input = Systems.input;

			// If holding shift down, increase camera movement speed by 3.
			byte moveMult = (input.LocalKeyDown(Keys.LeftShift) || input.LocalKeyDown(Keys.RightShift)) ? (byte) 3 : (byte) 1;

			// Camera Movement
			Systems.camera.MoveWithInput(Systems.localServer.MyPlayer.input, moveMult);
		}

		public override void Draw() {
			RoomFormat roomData = this.levelContent.data.rooms[this.roomID];

			//this.stopwatch.Start();

			if(roomData.bg != null) { DrawLayer(roomData.bg); }
			if(roomData.main != null) { DrawLayer(roomData.main); }
			if(roomData.fg != null) { DrawLayer(roomData.fg); }
			if(roomData.obj != null) { DrawObjectLayer(roomData.obj); }

			// Debugging
			//this.stopwatch.Stop();
			//System.Console.WriteLine("Benchmark: " + this.stopwatch.ElapsedTicks + ", " + this.stopwatch.ElapsedMilliseconds);
		}

		private void DrawLayer(Dictionary<string, Dictionary<string, ArrayList>> layerData) {
			Camera cam = Systems.camera;

			ushort startX = (ushort)Math.Max((ushort)0, (ushort)cam.GridX);
			ushort startY = (ushort)Math.Max((ushort)0, (ushort)cam.GridY);

			ushort gridX = (ushort)(startX + 29 + 1 + 1); // 29 is view size. +1 is to render the edge. +1 is to deal with --> operator in loop.
			ushort gridY = (ushort)(startY + 18 + 1 + 1); // 18 is view size. +1 is to render the edge. +1 is to deal with --> operator in loop.

			if(gridX > this.xCount) { gridX = (ushort) (this.xCount + 1); } // Must limit to room size. +1 is to deal with --> operator in loop.
			if(gridY > this.yCount) { gridY = (ushort) (this.yCount + 1); } // Must limit to room size. +1 is to deal with --> operator in loop.

			// Camera Position
			bool isShaking = cam.IsShaking();
			int camX = cam.posX + (isShaking ? cam.GetCameraShakeOffsetX() : 0);
			int camY = cam.posY + (isShaking ? cam.GetCameraShakeOffsetY() : 0);

			var tileDict = Systems.mapper.TileDict;

			// Loop through the tilemap data:
			for(ushort y = gridY; y --> startY; ) {
				ushort tileYPos = (ushort)(y * (byte)TilemapEnum.TileHeight - camY);

				string yStr = y.ToString();

				// Make sure this Y-line exists, or skip further review:
				if(!layerData.ContainsKey(yStr)) { continue; }
				var yData = layerData[yStr];

				for(ushort x = gridX; x --> startX; ) {

					// Verify Tile Data exists at this Grid Square:
					if(!yData.ContainsKey(x.ToString())) { continue; }
					var xData = yData[x.ToString()];
					byte index = byte.Parse(xData[0].ToString());
					byte subType = byte.Parse(xData[1].ToString());

					// Draw Layer
					TileObject tileObj = tileDict[index];

					// Render the tile with its designated Class Object:
					if(tileObj is TileObject) {
						tileObj.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - camX, tileYPos);
					};
				};
			}
		}

		private void DrawObjectLayer(Dictionary<string, Dictionary<string, ArrayList>> layerData) {
			Camera cam = Systems.camera;

			ushort startX = (ushort)Math.Max((ushort)0, (ushort)cam.GridX);
			ushort startY = (ushort)Math.Max((ushort)0, (ushort)cam.GridY);

			ushort gridX = (ushort)(startX + 29 + 1 + 1); // 29 is view size. +1 is to render the edge. +1 is to deal with --> operator in loop.
			ushort gridY = (ushort)(startY + 18 + 1 + 1); // 18 is view size. +1 is to render the edge. +1 is to deal with --> operator in loop.

			if(gridX > this.xCount) { gridX = (ushort) (this.xCount + 1); } // Must limit to room size. +1 is to deal with --> operator in loop.
			if(gridY > this.yCount) { gridY = (ushort) (this.yCount + 1); } // Must limit to room size. +1 is to deal with --> operator in loop.

			// Camera Position
			bool isShaking = cam.IsShaking();
			int camX = cam.posX + (isShaking ? cam.GetCameraShakeOffsetX() : 0);
			int camY = cam.posY + (isShaking ? cam.GetCameraShakeOffsetY() : 0);

			// Loop through the tilemap data:
			for(ushort y = gridY; y --> startY; ) {
				ushort tileYPos = (ushort)(y * (byte)TilemapEnum.TileHeight - camY);

				string yStr = y.ToString();

				// Make sure this Y-line exists, or skip further review:
				if(!layerData.ContainsKey(yStr)) { continue; }
				var yData = layerData[yStr];

				for(ushort x = gridX; x --> startX; ) {

					// Verify Tile Data exists at this Grid Square:
					if(!yData.ContainsKey(x.ToString())) { continue; }
					var xData = yData[x.ToString()];
					byte index = byte.Parse(xData[0].ToString());
					byte subType = byte.Parse(xData[1].ToString());

					// Draw Layer
					ShadowTile.Draw(index, subType, null, x * (byte)TilemapEnum.TileWidth - camX, tileYPos);
				};
			}
		}

		public void TileToolTick(ushort gridX, ushort gridY) {

			// Make sure placement is in valid location:
			if(gridY < 0 || gridY > this.yCount) { return; }
			if(gridX < 0 || gridX > this.xCount) { return; }

			TileTool tool = EditorTools.tileTool;

			// Make sure the tile tool is set, or placement cannot occur:
			if(tool == null) { return; }

			// Check if AutoTile Tool is intended. Requires Control to be held down.
			if(Systems.input.LocalKeyDown(Keys.LeftControl)) {

				// If left mouse button was just clicked, AutoTile is being activated.
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					EditorTools.StartAutoTool(gridX, gridY);
				}
			}

			// If AutoTile Tool is active, run it's behavior:
			if(EditorTools.autoTool.IsActive) {

				// If left mouse was just released and AutoTile is active, place AutoTiles.
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Released) {
					EditorTools.autoTool.PlaceAutoTiles(this);
				}

				// If Control key is not held down, auto-tiles must be deactivated.
				else if(!Systems.input.LocalKeyDown(Keys.LeftControl)) {
					EditorTools.autoTool.ClearAutoTiles();
				}

				return;
			}

			// Left Mouse Button (Overwrite Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {

				// Prevent repeat-draws on the same tile (e.g. within the last 100ms).
				if(!DrawTracker.AttemptDraw(gridX, gridY)) { return;}

				EditorPlaceholder ph = tool.CurrentPlaceholder;

				// Place Tile
				if(ph.tileId > 0) {
					this.PlaceTile(this.levelContent.data.rooms[this.roomID].main, gridX, gridY, ph.tileId, ph.subType, null);
				}

				// Place Object
				else if(ph.objectId > 0) {
					this.PlaceTile(this.levelContent.data.rooms[this.roomID].obj, gridX, gridY, ph.objectId, ph.subType, null);
				}

				return;
			}
		}

		public void DeleteTile(ushort gridX, ushort gridY) {

			// Make sure deletion is in valid location:
			if(gridY < 0 || gridY > this.yCount) { return; }
			if(gridX < 0 || gridX > this.xCount) { return; }

			// Prevent repeat-draws on the same tile (e.g. within the last 100ms).
			if(!DrawTracker.AttemptDraw(gridX, gridY)) { return; }

			this.levelContent.DeleteTile(this.roomID, gridX, gridY);
		}

		public void PlaceTile(Dictionary<string, Dictionary<string, ArrayList>> layerData, ushort gridX, ushort gridY, byte tileId, byte subType, Dictionary<string, object> paramList = null) {

			// Check Tiles with special requirements (such as being restricted to one):
			//if(type == ObjectEnum.Character) {
			//	let tileLoc = Tile.scanLayerForTile(roomData, "mainLayer", "Character/Ryu");

			//	// Delete the existing version:
			//	if(tileLoc) { this.deleteTileLayer(roomData, "mainLayer", tileLoc.x, tileLoc.y); }

			//	// Update the character's starting point:
			//	roomData.charStart = {
			//	x: gridX * this.tilemap.tileWidth,
			//		y: gridY * this.tilemap.tileHeight
			//	};
			//}

			this.levelContent.SetTile(layerData, gridX, gridY, tileId, subType, null);
		}

		public void CloneTile(ushort gridX, ushort gridY) {
			RoomFormat roomData = this.levelContent.data.rooms[this.roomID];
			Dictionary<string, Dictionary<string, ArrayList>> layerData = null;
			bool isObject = false;

			if(LevelContent.VerifyTiles(roomData.obj, gridX, gridY)) { layerData = roomData.obj; isObject = true; }
			else if(LevelContent.VerifyTiles(roomData.main, gridX, gridY)) { layerData = roomData.main; }
			else if(LevelContent.VerifyTiles(roomData.fg, gridX, gridY)) { layerData = roomData.fg; }
			else if(LevelContent.VerifyTiles(roomData.bg, gridX, gridY)) { layerData = roomData.bg; }

			// If no tile is cloned, set the current Function Tool to "Select"
			if(layerData == null) {
				FuncToolSelect selectFunc = (FuncToolSelect)FuncTool.funcToolMap[(byte)FuncToolEnum.Select];
				EditorTools.SetFuncTool(selectFunc);
				selectFunc.ClearSelection();
				return;
			}

			// Get the Object from the Highlighted Tile (Search Front to Back until a tile is identified)
			byte[] tileData = LevelContent.GetTileData(layerData, gridX, gridY);

			// Identify the tile, and set it as the current editing tool (if applicable)
			TileTool clonedTool = TileTool.GetTileToolFromTileData(tileData, isObject);

			if(clonedTool is TileTool == true) {
				byte subIndex = clonedTool.subIndex; // Need to save this value to avoid subIndexSaves[] tracking.
				EditorTools.SetTileTool(clonedTool, (byte)clonedTool.index);
				clonedTool.SetSubIndex(subIndex);
			}
		}
	}
}
