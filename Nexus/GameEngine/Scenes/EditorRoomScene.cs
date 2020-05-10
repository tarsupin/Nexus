using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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
			EditorDetection.DetectRoomSize(this.levelContent.data.rooms[roomID], out xCount, out yCount);

			// Sizing
			this.xCount = xCount;
			this.yCount = yCount;
			this.mapWidth = xCount * (byte)TilemapEnum.TileWidth;
			this.mapHeight = yCount * (byte)TilemapEnum.TileHeight;
		}

		public override int Width { get { return this.mapWidth; } }
		public override int Height { get { return this.mapHeight; } }

		public override void RunTick() {

			// Update Tools
			if(EditorTools.tempTool is FuncTool) {
				EditorTools.tempTool.RunTick(this.scene);
			}
			
			else if(EditorTools.funcTool is FuncTool) {
				EditorTools.funcTool.RunTick(this.scene);
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

			var tileMap = Systems.mapper.TileDict;

			// Loop through the tilemap data:
			for(ushort y = gridY; y --> startY; ) {
				ushort tileYPos = (ushort)(y * (byte)TilemapEnum.TileHeight - camY);

				// Make sure this Y-line exists, or skip further review:
				if(!layerData.ContainsKey(y.ToString())) { continue; }
				var yData = layerData[y.ToString()];

				for(ushort x = gridX; x --> startX; ) {

					// Verify Tile Data exists at this Grid Square:
					if(!yData.ContainsKey(x.ToString())) { continue; }
					var xData = yData[x.ToString()];
					byte index = byte.Parse(xData[0].ToString());
					byte subIndex = byte.Parse(xData[1].ToString());

					// Draw Layer
					TileGameObject tileObj = tileMap[index];

					// Render the tile with its designated Class Object:
					if(tileObj is TileGameObject) {
						tileObj.Draw(null, subIndex, x * (byte)TilemapEnum.TileWidth - camX, tileYPos);
					};
				};
			}
		}

		public void TileToolTick(byte gridX, byte gridY) {
			
			// Left Mouse Button (Overwrite Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {

				// Prevent repeat-draws on the same tile (e.g. within the last 100ms).
				if(!DrawTracker.AttemptDraw(gridX, gridY)) { return;}

				TileTool tool = EditorTools.tileTool;

				// Make sure the tile tool is set, or placement cannot occur:
				if(tool == null) { return; }

				EditorPlaceholder ph = tool.CurrentPlaceholder;
				LayerEnum layer = LayerEnum.main;		// TODO: Change this. Needs to be based on the actual tile or object.

				// Place Tile
				this.levelContent.SetTile(this.roomID, layer, gridX, gridY, ph.tileId, ph.subType, null);

				// Auto-Tile if shift is being held (and tile can auto-tile).
				bool autoTileRunning = false;

				// TODO HIGH PRIORITY: AUTO-TILING AFTER PLACEMENT
				//if(Systems.input.LocalKeyDown(Keys.LeftShift)) {

				//	// Attempt to run AutoTile (and trace success)
				//	autoTileRunning = this.autoTile.runAutoTile(tool.tile);
				//}

				//// Place Tile
				//if(!autoTileRunning) {

				//	// Check if the auto-neighbor process is handling the placement:
				//	bool autoNeighborRan = this.autoNeighbor.runAutoNeighbor(tool.tile, gridX, gridY);

				//	if(!autoNeighborRan) {

				//		// TODO UI
				//		// If there are alterations to the main layer, test for special alteration rules.
				//		// if(tool.tile.layerName === "mainLayer") {
				//		// 	this.alterTile( "mainLayer", this.scene.MouseGridX, this.scene.MouseGridY, false, true );
				//		// }

				//		this.PlaceTile(tool.tile.layerName, gridX, gridY, tool.tile.id, tool.tile.subType, tool.tile.paramList);
				//	}
				//}

				return;
			}

			// A right click will clone the current tile.
			if(Cursor.mouseState.RightButton == ButtonState.Pressed) {
				this.CloneTile(this.roomID, Cursor.MouseGridX, Cursor.MouseGridY);
			}
		}

		public void CloneTile(string roomID, byte gridX, byte gridY) {

			//// Get the Object from the Highlighted Tile (Search Front to Back until a tile is identified)
			byte[] tileData = LevelContent.GetTileData(this.levelContent.data.rooms[roomID].main, gridX, gridY);

			if(tileData == null) { return; }

			// Identify the tile, and set it as the current editing tool (if applicable)
			TileTool clonedTool = TileTool.GetTileToolFromTileData(tileData);

			if(clonedTool is TileTool == true) {
				byte subIndex = clonedTool.subIndex; // Need to save this value to avoid subIndexSaves[] tracking.
				EditorTools.SetTileTool(clonedTool, (byte)clonedTool.index);
				clonedTool.SetSubIndex(subIndex);
			}
		}

		//alterTile( layer: RoomLayer, gridX: number, gridY: number, split: boolean = false, del: boolean = false ) {

		//	// Determine Tile to identify if there are special alteration cases.
		//	let tile: Tile = this.getTileFromGrid( gridX, gridY, layer );

		//	// If there is no tile to alter, end here.
		//	if(!tile) { return; }

		//	// Platforms have special alteration cases:
		//	if(tile.objClass.meta.archetype === Arch.Platform) {
		//		let chainCursor = this.autoNeighbor.chainCursor;
		//		chainCursor.loadExtendedChain( tile, gridX, gridY );

		//		switch( tile.objClass ) {

		//			case PlatSolid:
		//				chainCursor.smoothChain( 50, del );
		//				break;

		//			case PlatDip:
		//			case PlatDelay:
		//			case PlatFall:
		//				chainCursor.smoothChain( 3, del );
		//				break;
		//		}
		//	}
		//}
	}
}
