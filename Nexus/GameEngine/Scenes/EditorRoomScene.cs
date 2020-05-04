using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class EditorRoomScene : Scene {

		// References
		public readonly EditorScene scene;

		// Editor Data
		public TilemapBool tilemap;

		public EditorRoomScene(EditorScene scene, string roomID) : base() {

			// References
			this.scene = scene;

			// Build Tilemap with Correct Dimensions
			ushort xCount, yCount;
			EditorRoomGenerate.DetermineRoomSize(Systems.handler.levelContent.data.room[roomID], out xCount, out yCount);

			this.tilemap = new TilemapBool(xCount, yCount);

			// Generate Room Content (Tiles, Objects)
			EditorRoomGenerate.GenerateRoom(this, Systems.handler.levelContent, roomID);
		}

		public override int Width { get { return this.tilemap.Width; } }
		public override int Height { get { return this.tilemap.Height; } }

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
			Camera cam = Systems.camera;

			int startX = Math.Max(0, cam.GridX);
			int startY = Math.Max(0, cam.GridY);

			ushort gridX = (ushort) (startX + 29 + 1); // 29 is view size. +1 is to render the edge.
			ushort gridY = (ushort) (startY + 18 + 1); // 18 is view size. +1 is to render the edge.

			if(gridX > this.tilemap.XCount) { gridX = this.tilemap.XCount; } // Must limit to room size (due to the +1)
			if(gridY > this.tilemap.YCount) { gridY = this.tilemap.YCount; } // Must limit to room size (due to the +1)

			// Camera Position
			bool isShaking = cam.IsShaking();
			int camX = cam.posX + (isShaking ? cam.GetCameraShakeOffsetX() : 0);
			int camY = cam.posY + (isShaking ? cam.GetCameraShakeOffsetY() : 0); ;
			int camRight = camX + cam.width;
			int camBottom = camY + cam.height;

			var tileDict = Systems.mapper.TileDict;

			// Loop through the tilemap data:
			for(int y = startY; y <= gridY; y++) {
				int tileYPos = y * (byte)TilemapEnum.TileHeight - camY;

				for(int x = startX; x <= gridX; x++) {

					// Scan the Tiles Data at this grid square:
					uint gridId = this.tilemap.GetGridID((ushort)x, (ushort)y);

					byte[] tileData = this.tilemap.GetTileDataAtGridID(gridId);

					if(tileData == null) {
						// TODO HIGH PRIOIRTY: This block should never run; tileData should never be null.
						// Remove any invalid options here.
						// Delete this block once the TileMap has been completed.
						continue;
					}

					TileGameObject tileObj = tileDict[tileData[0]];

					// Render the tile with its designated Class Object:
					if(tileObj is TileGameObject) {
						tileObj.Draw(null, tileData[1], x * (byte)TilemapEnum.TileWidth - camX, tileYPos);
					};
				};
			}
		}

		public void PlaceTile( LayerEnum layer, ushort gridX, ushort gridY, byte tileId, byte subType, JObject paramList = null) {

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

			// Retrieve the Grid ID
			uint gridId = this.tilemap.GetGridID(gridX, gridY);

			//byte[] tileData = this.tilemap.GetTileDataAtGridID(gridId);

			// Place the Tile
			if(layer == LayerEnum.main) {
				this.tilemap.AddTile(gridId, tileId, subType);
				System.Console.WriteLine("Tile ID: " + tileId + ", " + subType);
			} else if(layer == LayerEnum.fg) {
				this.tilemap.AddTile(gridId, 0, 0, tileId, subType);
			} else if(layer == LayerEnum.obj) {

				// TODO: Handle Obj Tiles and Params when Adding Tiles
				//if(params && typeof(params) === "object") {
				//	for(let p in params ) {
				//		if(p === "id") { continue; }
				//		yData[gridX][p] = params[p];
				//	}
				//}
			}
		}

		public void DeleteTile(ushort gridX, ushort gridY) {
			this.tilemap.RemoveTileByGrid(gridX, gridY);
		}

		public void TileToolTick(ushort gridX, ushort gridY) {
			
			// Left Mouse Button (Overwrite Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {

				// Prevent repeat-draws on the same tile (e.g. within the last 100ms).
				if(!DrawTracker.AttemptDraw(gridX, gridY)) { return;}

				TileTool tool = EditorTools.tileTool;
				EditorPlaceholder ph = tool.CurrentPlaceholder;
				LayerEnum layer = LayerEnum.main;

				// Place Tile
				this.PlaceTile(layer, gridX, gridY, ph.tileId, ph.subType, null);

				// Auto-Tile if shift is being held (and tile can auto-tile).
				bool autoTileRunning = false;

				// TODO HIGH PRIORITY: AUTO-TILING AFTER PLACEMENTE
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
				this.CloneTile(Cursor.MouseGridX, Cursor.MouseGridY);
			}
		}

		public void CloneTile(ushort gridX, ushort gridY) {

			// Get the Object from the Highlighted Tile (Search Front to Back until a tile is identified)
			byte[] tileData = this.tilemap.GetTileDataAtGrid(gridX, gridY);

			if(tileData == null) { return; }

			// Identify the tile, and set it as the current editing tool (if applicable)
			TileTool clonedTool = TileTool.GetTileToolFromTileData(tileData);

			if(clonedTool is TileTool == true) {
				EditorTools.SetTileTool(clonedTool);
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
