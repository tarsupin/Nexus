using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using static Nexus.GameEngine.FuncTool;

namespace Nexus.GameEngine {

	public class EditorRoomScene : Scene {

		// References
		public readonly EditorScene scene;
		public LevelContent levelContent;
		public string roomID;

		// Editor Data
		public short xCount { get; private set; }
		public short yCount { get; private set; }
		private int mapWidth;
		private int mapHeight;

		public EditorRoomScene(EditorScene scene, string roomID) : base() {

			// References
			this.scene = scene;
			this.levelContent = this.scene.levelContent;
			this.roomID = roomID;

			// Build Tilemap with Correct Dimensions
			short xCount, yCount;
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
				this.CloneTile(Cursor.TileGridX, Cursor.TileGridY);
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
				this.TileToolTick(Cursor.TileGridX, Cursor.TileGridY);
			}
		}

		public override void Draw() {
			RoomFormat roomData = this.levelContent.data.rooms[this.roomID];

			//Systems.timer.stopwatch.Start();

			if(roomData.bg != null) { DrawLayer(roomData.bg); }
			if(roomData.main != null) { DrawLayer(roomData.main); }
			if(roomData.fg != null) { DrawLayer(roomData.fg); }
			if(roomData.obj != null) { DrawObjectLayer(roomData.obj); }

			// Debugging
			//Systems.timer.stopwatch.Stop();
			//System.Console.WriteLine("Benchmark: " + Systems.timer.stopwatch.ElapsedTicks + ", " + Systems.timer.stopwatch.ElapsedMilliseconds);
		}

		private void DrawLayer(Dictionary<string, Dictionary<string, ArrayList>> layerData) {
			Camera cam = Systems.camera;

			short startX = Math.Max((short)0, cam.GridX);
			short startY = Math.Max((short)0, cam.GridY);

			short gridX = (short)(startX + (byte)TilemapEnum.MinWidth + 1 + 1); // 30 is view size. +1 is to render the edge. +1 is to deal with --> operator in loop.
			short gridY = (short)(startY + (byte)TilemapEnum.MinHeight + 1 + 1); // 18 is view size. +1 is to render the edge. +1 is to deal with --> operator in loop.

			if(gridX > this.xCount) { gridX = this.xCount; } // Must limit to room size. +1 is to deal with --> operator in loop.
			if(gridY > this.yCount) { gridY = this.yCount; } // Must limit to room size. +1 is to deal with --> operator in loop.

			// Camera Position
			bool isShaking = cam.IsShaking();
			int camX = cam.posX + (isShaking ? cam.GetCameraShakeOffsetX() : 0);
			int camY = cam.posY + (isShaking ? cam.GetCameraShakeOffsetY() : 0);

			var tileDict = Systems.mapper.TileDict;

			// Loop through the tilemap data:
			for(short y = gridY; y --> startY; ) {
				short tileYPos = (short)(y * (byte)TilemapEnum.TileHeight - camY);

				string yStr = y.ToString();

				// Make sure this Y-line exists, or skip further review:
				if(!layerData.ContainsKey(yStr)) { continue; }
				var yData = layerData[yStr];

				for(short x = gridX; x --> startX; ) {

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
			
			short startX = Math.Max((short)0, cam.GridX);
			short startY = Math.Max((short)0, cam.GridY);

			// Must adjust for the World Gaps (Resistance Barrier)
			if(startX < (byte)TilemapEnum.GapLeft) { startX = (byte)TilemapEnum.GapLeft; }
			if(startY < (byte)TilemapEnum.GapUp) { startY = (byte)TilemapEnum.GapUp; }

			short gridX = (short)(startX + (byte)TilemapEnum.MinWidth + 1); // 30 is view size. +1 is to render the edge.
			short gridY = (short)(startY + (byte)TilemapEnum.MinHeight + 1); // 18 is view size. +1 is to render the edge.

			if(gridX > this.xCount) { gridX = this.xCount; } // Must limit to room size.
			if(gridY > this.yCount) { gridY = this.yCount; } // Must limit to room size.

			// Camera Position
			bool isShaking = cam.IsShaking();
			int camX = cam.posX + (isShaking ? cam.GetCameraShakeOffsetX() : 0);
			int camY = cam.posY + (isShaking ? cam.GetCameraShakeOffsetY() : 0);

			// Loop through the tilemap data:
			for(short y = gridY; y --> startY; ) {
				short tileYPos = (short)(y * (byte)TilemapEnum.TileHeight - camY);

				string yStr = y.ToString();

				// Make sure this Y-line exists, or skip further review:
				if(!layerData.ContainsKey(yStr)) { continue; }
				var yData = layerData[yStr];

				for(short x = gridX; x --> startX; ) {

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

		public bool ConfirmDraw(short gridX, short gridY) {

			// Make sure deletion is in valid location:
			if(gridY < 0 || gridY > this.yCount) { return false; }
			if(gridX < 0 || gridX > this.xCount) { return false; }

			// Prevent repeat-draws on the same tile (e.g. within the last 100ms).
			if(!DrawTracker.AttemptDraw(gridX, gridY)) { return false; }

			return true;
		}

		public void TileToolTick(short gridX, short gridY) {

			// Prevent drawing when a component is selected.
			if(UIComponent.ComponentWithFocus != null) { return; }

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
					RoomFormat roomData = this.levelContent.data.rooms[this.roomID];

					if(ph.layerEnum == LayerEnum.main) {
						this.PlaceTile(roomData.main, ph.layerEnum, gridX, gridY, ph.tileId, ph.subType, null);
					} else  if(ph.layerEnum == LayerEnum.bg) {
						this.PlaceTile(roomData.bg, ph.layerEnum, gridX, gridY, ph.tileId, ph.subType, null);
					} else  if(ph.layerEnum == LayerEnum.fg) {
						this.PlaceTile(roomData.fg, ph.layerEnum, gridX, gridY, ph.tileId, ph.subType, null);
					}
				}

				// Place Object
				else if(ph.objectId > 0) {
					this.PlaceTile(this.levelContent.data.rooms[this.roomID].obj, LayerEnum.obj, gridX, gridY, ph.objectId, ph.subType, null);
				}

				return;
			}
		}

		public void DeleteTile(short gridX, short gridY) {
			if(!this.ConfirmDraw(gridX, gridY)) { return; }
			this.levelContent.DeleteTile(this.roomID, gridX, gridY);
		}

		public void DeleteTileOnLayer(LayerEnum layerEnum, short gridX, short gridY) {
			if(!this.ConfirmDraw(gridX, gridY)) { return; }
			this.levelContent.DeleteTileOnLayer(layerEnum, this.roomID, gridX, gridY);
		}

		public void PlaceTile(Dictionary<string, Dictionary<string, ArrayList>> layerData, LayerEnum layerEnum, short gridX, short gridY, byte tileId, byte subType, Dictionary<string, object> paramList = null) {

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

			// If placing on 'obj' or 'main' layer, delete the other:
			if(layerEnum == LayerEnum.main) {
				this.DeleteTileOnLayer(LayerEnum.obj, gridX, gridY);
			} else if(layerEnum == LayerEnum.obj) {
				this.DeleteTileOnLayer(LayerEnum.main, gridX, gridY);
			}
		}

		public void CloneTile(short gridX, short gridY) {
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

		// Resize Map
		public void ResizeWidth(short newWidth = 0) {

			// Delete all tiles that got resized:
			if(newWidth < this.xCount) {
				for(short gridX = newWidth; gridX <= (short)(this.xCount + 1); gridX++) {
					for(short gridY = 0; gridY <= (short)(this.yCount + 1); gridY++) {
						this.levelContent.DeleteTile(this.roomID, gridX, gridY);
					}
				}
			}

			this.xCount = newWidth;
			this.mapWidth = this.xCount * (byte)TilemapEnum.TileWidth;
			Systems.camera.UpdateScene(this);
		}

		public void ResizeHeight(short newHeight = 0) {

			// Delete all tiles that got resized:
			if(newHeight < this.yCount) {
				for(short gridY = newHeight; gridY <= (short)(this.yCount + 1); gridY++) {
					for(short gridX = 0; gridX <= (short)(this.xCount + 1); gridX++) {
						this.levelContent.DeleteTile(this.roomID, gridX, gridY);
					}
				}
			}

			this.yCount = newHeight;
			this.mapHeight = this.yCount * (byte)TilemapEnum.TileHeight;
			Systems.camera.UpdateScene(this);
		}
	}
}
