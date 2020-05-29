using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WEScene : Scene {

		// References
		public readonly WE_UI weUI;
		public readonly PlayerInput playerInput;
		public CampaignState campaign;
		public Atlas atlas;

		// Access to World Data
		public WorldContent worldContent;
		public WorldFormat worldData;       // worldContent.data

		// Mapper Data
		public Dictionary<byte, string> WorldTerrain;
		public Dictionary<byte, string> WorldTerrainCat;
		public Dictionary<byte, string> WorldLayers;
		public Dictionary<byte, string> WorldObjects;

		// Grid Limits
		public byte xCount = 45;		// 1400 / 32 = 45
		public byte yCount = 29;		// 900 / 32 = 28.125
		public int mapWidth = 0;
		public int mapHeight = 0;

		public WEScene() : base() {

			// Prepare Components
			this.weUI = new WE_UI(this);
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.campaign = Systems.handler.campaignState;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];

			// Prepare World Content
			this.worldContent = Systems.handler.worldContent;
			this.worldData = this.worldContent.data;

			// Prepare Mapper Data
			this.WorldTerrain = Systems.mapper.WorldTerrain;
			this.WorldTerrainCat = Systems.mapper.WorldTerrainCat;
			this.WorldLayers = Systems.mapper.WorldLayers;
			this.WorldObjects = Systems.mapper.WorldObjects;

			// Camera Update
			Systems.camera.SetInputMoveSpeed(15);

			// Add Mouse Behavior
			Systems.SetMouseVisible(true);
			Cursor.UpdateMouseState();
		}

		public WorldZoneFormat currentZone { get { return this.worldContent.GetWorldZone(this.campaign.zoneId); } }

		public override int Width { get { return this.mapWidth; } }
		public override int Height { get { return this.mapHeight; } }

		public override void StartScene() {

			// Make sure that world data is available.
			if(this.worldData is WorldFormat == false) {
				throw new Exception("Unable to load world. No world data available.");
			}

			// Start the Default WETool
			WETools.SetWorldTileTool(WETileTool.WorldTileToolMap[(byte) WorldSlotGroup.Standard], 0);

			// Update Grid Limits
			this.xCount = this.worldContent.GetWidthOfZone(this.currentZone);
			this.yCount = this.worldContent.GetHeightOfZone(this.currentZone);

			// Prepare Map Size
			this.mapWidth = this.xCount * (byte)WorldmapEnum.TileWidth;
			this.mapHeight = this.yCount * (byte)WorldmapEnum.TileHeight;

			// Update Camera Bounds
			Systems.camera.UpdateScene(this);
		}

		public override void RunTick() {

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				player.Value.input.UpdateKeyStates(0);
			}

			// Update the Mouse State Every Tick
			Cursor.UpdateMouseState();

			// Run World UI Updates
			this.weUI.RunTick();

			// Debug Console (only runs if visible)
			Systems.worldEditConsole.RunTick();

			// Check Input Updates
			this.WorldEditorInput();

			// A right click will clone the current tile.
			if(Cursor.mouseState.RightButton == ButtonState.Pressed) {
				this.CloneTile((byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
				return;
			}

			// Update Tools
			if(WETools.WETempTool is WEFuncTool) {
				WETools.WETempTool.RunTick(this);
			} else if(WETools.WEFuncTool is WEFuncTool) {
				WETools.WEFuncTool.RunTick(this);
			} else {
				this.TileToolTick((byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
			}
		}

		public void WorldEditorInput() {
			InputClient input = Systems.input;

			// If holding shift down, increase camera movement speed by 3.
			byte moveMult = (input.LocalKeyDown(Keys.LeftShift) || input.LocalKeyDown(Keys.RightShift)) ? (byte)3 : (byte)1;

			// Camera Movement
			Systems.camera.MoveWithInput(Systems.localServer.MyPlayer.input, moveMult);

			// Release TempTool Control every tick:
			if(WETools.WETempTool != null) {
				WETools.ClearWorldTempTool();
			}

			// Get the Local Keys Held Down
			Keys[] localKeys = input.GetAllLocalKeysDown();
			if(localKeys.Length == 0) { return; }

			// Key Presses that AREN'T using control keys:
			if(!input.LocalKeyDown(Keys.LeftControl) && !input.LocalKeyDown(Keys.RightControl)) {

				// Func Tool Key Binds
				if(WEFuncTool.WEFuncToolKey.ContainsKey(localKeys[0])) {
					WETools.SetWorldTempTool(WEFuncTool.WEFuncToolMap[WEFuncTool.WEFuncToolKey[localKeys[0]]]);
				}

				// Tile Tool Key Binds
				else if(WE_UI.curWESlotGroup > 0) {
					this.CheckTileToolKeyBinds(localKeys[0]);
				}
			}

			// Open Wheel Menu
			if(input.LocalKeyPressed(Keys.Tab)) { this.weUI.contextMenu.OpenMenu(); }
		}
		
		public void CheckTileToolKeyBinds(Keys keyPressed) {
			if(keyPressed == Keys.D1) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 0); }
			else if(keyPressed == Keys.D2) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 1); }
			else if(keyPressed == Keys.D3) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 2); }
			else if(keyPressed == Keys.D4) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 3); }
			else if(keyPressed == Keys.D5) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 4); }
			else if(keyPressed == Keys.D6) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 5); }
			else if(keyPressed == Keys.D7) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 6); }
			else if(keyPressed == Keys.D8) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 7); }
			else if(keyPressed == Keys.D9) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 8); }
			else if(keyPressed == Keys.D0) { WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup, 9); }
		}

		public void TileToolTick(ushort gridX, ushort gridY) {

			// Make sure placement is in valid location:
			if(gridY < 0 || gridY > this.yCount) { return; }
			if(gridX < 0 || gridX > this.xCount) { return; }

			WETileTool tool = WETools.WETileTool;

			// Make sure the tile tool is set, or placement cannot occur:
			if(tool == null) { return; }

			// Left Mouse Button (Overwrite Current Tile)
			if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {

				// Prevent Placement Outside of Bounds
				if(gridX > this.xCount) { return; }
				if(gridY > this.yCount) { return; }

				// Prevent repeat-draws on the same tile (e.g. within the last 100ms).
				if(!DrawTracker.AttemptDraw(gridX, gridY)) { return; }

				// Prevent drawing when a component is selected.
				if(UIComponent.ComponentWithFocus != null) { return; }

				WEPlaceholder ph = tool.CurrentPlaceholder;

				// Placing an Object
				if(ph.tObj > 0) {
					this.PlaceObject(ph.tObj, (byte) gridX, (byte) gridY);
				}

				// Placing a Standard Tile
				else {
					this.PlaceTile(ph, (byte) gridX, (byte) gridY);
				}

				return;
			}
		}

		public override void Draw() {
			Camera cam = Systems.camera;

			byte startX = (byte)Math.Max((byte)0, (byte)cam.MiniX - 1);
			byte startY = (byte)Math.Max((byte)0, (byte)cam.MiniY - 1);

			byte gridX = (byte)(startX + 45 + 1); // 45 is view size. +1 is to render the edge.
			byte gridY = (byte)(startY + 29 + 1); // 28.125 is view size. +1 is to render the edge.

			if(gridX > this.xCount) { gridX = (byte)(this.xCount); } // Must limit to room size.
			if(gridY > this.yCount) { gridY = (byte)(this.yCount); } // Must limit to room size.

			// Camera Position
			int camX = cam.posX;
			int camY = cam.posY;

			byte[][][] curTiles = this.currentZone.tiles;

			// Loop through the zone tile data:
			for(byte y = gridY; y --> startY;) {
				int tileYPos = y * (byte)WorldmapEnum.TileHeight - camY;

				for(byte x = gridX; x --> startX;) {
					this.DrawWorldTile(curTiles[y][x], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);
				};
			}

			// Draw UI
			this.weUI.Draw();
			Systems.worldEditConsole.Draw();

			// Draw Directional Tile Overlap when path detection is required.
			//this.DrawDirectionTiles((byte) Cursor.MiniGridX, (byte) Cursor.MiniGridY);
		}

		public void DrawDirectionTiles( byte gridX, byte gridY, byte range = 5 ) {
			for(int y = gridY - range; y < gridY + range + 1; y++) {
				for(int x = gridX - range; x < gridX + range + 1; x++) {

					DirCardinal dir = WEScene.RelativeDirectionOfTiles((sbyte) (x - gridX), (sbyte) (y - gridY));

					if(dir == DirCardinal.Up || dir == DirCardinal.Down) {
						Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(x * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, y * (byte)WorldmapEnum.TileHeight - Systems.camera.posY, (byte)WorldmapEnum.TileWidth, (byte)WorldmapEnum.TileHeight), Color.White * 0.35f);
					} else {
						Systems.spriteBatch.Draw(Systems.tex2dDarkGreen, new Rectangle(x * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, y * (byte)WorldmapEnum.TileHeight - Systems.camera.posY, (byte)WorldmapEnum.TileWidth, (byte)WorldmapEnum.TileHeight), Color.White * 0.35f);
					}
				}
			}
		}

		// Resize Map
		public void ResizeWidth(byte newWidth = 0) {
			this.xCount = this.worldContent.SetZoneWidth(this.currentZone, newWidth);
			this.mapWidth = this.xCount * (byte)WorldmapEnum.TileWidth;
			Systems.camera.UpdateScene(this);
		}

		public void ResizeHeight(byte newHeight = 0) {
			this.yCount = this.worldContent.SetZoneHeight(this.currentZone, newHeight);
			this.mapHeight = this.yCount * (byte)WorldmapEnum.TileHeight;
			Systems.camera.UpdateScene(this);
		}

		// Place World Tile
		public void PlaceTile(WEPlaceholder ph, byte gridX, byte gridY) {

			// Run Auto-Tiling
			if(ph.auto) {

			}

			// Run Tile Placement
			this.worldContent.SetTile(this.currentZone, gridX, gridY, ph.tBase, ph.tTop, ph.tCat, ph.tLayer, ph.tObj, ph.tNodeId);
		}

		// Place World Object (Can include Nodes)
		public void PlaceObject(byte objectId, byte gridX, byte gridY) {

			// Start Node Behavior
			if(objectId == (byte) OTerrainObjects.NodeStart) {

				// TODO: SPECIAL START BEHAVIOR
				return;
			}

			// If we're placing a node, we don't change the node mechanics.
			// However, if we're placing an object, we need to remove the original node mechanics correctly.
			if(!WEScene.IsObjectANode(objectId)) {

				// Delete Node at Location
				// This handles special node deletions.
				this.DeleteNodeIfPresent((byte)gridX, (byte)gridY);
			}


			this.worldContent.SetTileObject(this.currentZone, gridX, gridY, objectId);
		}

		public void DrawWorldTile(byte[] wtData, int posX, int posY) {

			// Draw Base
			if(wtData[0] != 0) {

				// If there is a top layer:
				if(wtData[1] != 0) {

					// Draw a standard base tile with no varient, so that the top layer will look correct.
					this.atlas.Draw(WorldTerrain[wtData[0]] + "/b1", posX, posY);

					// Draw the Top Layer
					this.atlas.Draw(WorldTerrain[wtData[1]] + "/" + WorldLayers[wtData[3]], posX, posY);
				}

				// If there is not a top layer:
				else {

					// If there is a category:
					if(wtData[2] != 0) {
						this.atlas.Draw(WorldTerrain[wtData[0]] + "/" + WorldTerrainCat[wtData[2]] + "/" + WorldLayers[wtData[3]], posX, posY);
					} else {
						this.atlas.Draw(WorldTerrain[wtData[0]] + "/" + WorldLayers[wtData[3]], posX, posY);
					}
				}
			}

			// Draw Top, with no base:
			else if(wtData[1] != 0) {
				this.atlas.Draw(WorldTerrain[wtData[1]] + "/" + WorldLayers[wtData[3]], posX, posY);
			}

			// Draw Object Layer
			if(wtData[4] != 0) {
				this.atlas.Draw("Objects/" + WorldObjects[wtData[4]], posX, posY);
			}
		}

		public void SwitchZone(byte zoneId) {
			if(this.worldData.zones.Length > zoneId) {
				this.campaign.zoneId = zoneId;
			}
		}
		
		public void SwapZoneOrder() {
			byte curZoneId = this.campaign.zoneId;

			// Make sure Swapping this zone is legal.
			if(curZoneId > 8) { return; }
			if(this.worldData.zones.Length <= curZoneId) { return; }
			if(this.worldData.zones[curZoneId+1] is WorldZoneFormat == false) { return; }
			if(this.worldData.zones[curZoneId+1].tiles is byte[][][] == false) { return; }

			// Swap the Zone Order
			var temp = this.currentZone;
			this.worldData.zones[curZoneId] = this.worldData.zones[curZoneId + 1];
			this.worldData.zones[curZoneId + 1] = temp;
		}

		public static bool IsObjectANode( byte objectId ) {
			switch(objectId) {
				case (byte)OTerrainObjects.NodeStrict:
				case (byte)OTerrainObjects.NodeCasual:
				case (byte)OTerrainObjects.NodePoint:
				case (byte)OTerrainObjects.NodeMove:
				case (byte)OTerrainObjects.NodeWarp:
				case (byte)OTerrainObjects.NodeWon:
					return true;
			}

			return false;
		}

		public void DeleteNodeIfPresent(byte gridX, byte gridY) {

			// Get Object at Location
			byte[] wtData = this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);
			byte objectId = wtData[4];

			// Determine if the Object is a Node
			bool isNode = WEScene.IsObjectANode(objectId);

			// If the object is a node:
			if(isNode) {

				// TODO URGENT: Move node mechanics as required.

				// Delete the Object and Node Reference on the Tile
				this.worldContent.SetTileObject(this.currentZone, gridX, gridY, 0);
				this.worldContent.SetTileNodeId(this.currentZone, gridX, gridY, 0);
			}
		}

		public void CloneTile(byte gridX, byte gridY) {
			byte[] tileData = this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);

			// Identify the tile, and set it as the current editing tool (if applicable)
			WETileTool clonedTool = WETileTool.GetWorldTileToolFromTileData(tileData);

			if(clonedTool is WETileTool == true) {
				byte subIndex = clonedTool.subIndex; // Need to save this value to avoid subIndexSaves[] tracking.
				WETools.SetWorldTileTool(clonedTool, (byte)clonedTool.index);
				clonedTool.SetSubIndex(subIndex);
			}
		}

		public static DirCardinal RelativeDirectionOfTiles( sbyte relX, sbyte relY ) {

			if(relX < 0) {
				if(relY < 0 && relY <= relX) { return DirCardinal.Up; }			// ex: -2, -3
				if(relY > 0 && relY >= 0 - relX) { return DirCardinal.Down; }		// ex: -2, 3
				return DirCardinal.Left;
			}

			if(relX > 0) {
				if(relY < 0 && 0 - relY >= relX) { return DirCardinal.Up; }         // ex: 2, -3
				if(relY > 0 && relY >= relX) { return DirCardinal.Down; }           // ex: 2, 3
				return DirCardinal.Right;
			}
			
			if(relY < 0) { return DirCardinal.Up; }
			if(relY > 0) { return DirCardinal.Down; }

			return DirCardinal.None;
		}

		public (byte nodeId, byte gridX, byte gridY) LocateNearestNode( WorldContent worldContent, byte gridX, byte gridY, DirCardinal dir, byte range = 6 ) {
			(byte nodeId, byte gridX, byte gridY) tuple = (nodeId: 0, gridX: 0, gridY: 0);

			// Vertical Node Scan - scans for any nodes above or below this location.
			if(dir == DirCardinal.Down || dir == DirCardinal.Up) {
				sbyte incY = dir == DirCardinal.Down ? (sbyte) 1 : (sbyte) -1;
				sbyte xRange = 1;

				for(int y = gridY + incY; y < gridY + range * incY; y += incY) {

					for(int x = gridX - xRange; x < gridY + xRange; x++) {

						// If Node is located, make sure it's more centered than any other:
						if(true) { continue; } // TODO: IF NODE IS LOCATED HERE.

						if(tuple.gridX == 0 && tuple.gridY == 0) {
							int dist1 = Math.Abs(gridX - tuple.gridX);
							int dist2 = Math.Abs(gridX - x);
							
							// Keep the smallest version:
							if(dist1 < dist2) { continue; }
							else { tuple.gridX = (byte) x; tuple.gridY = (byte) y; }

						}
						
						else {
							tuple.gridX = (byte) x;
							tuple.gridY = (byte) y;
						}
					}

					xRange += 1;

					// If a node is located:
					if(tuple.gridX > 0 && tuple.gridY > 0) {
						tuple.nodeId = 0;   // TODO: Set the Node ID that applies here.
						return tuple;
					}
				}
			}

			// Horizontal Node Scan - scans for any nodes above or below this location.
			else if(dir == DirCardinal.Left || dir == DirCardinal.Right) {
				sbyte incX = dir == DirCardinal.Left ? (sbyte) 1 : (sbyte) -1;
				sbyte yRange = 0;

				for(int x = gridX + incX; x < gridX + range * incX; x += incX) {

					for(int y = gridY - yRange; y < gridY + yRange; y++) {

						// If Node is located, make sure it's more centered than any other:
						if(true) { continue; } // TODO: IF NODE IS LOCATED HERE.

						if(tuple.gridY == 0 && tuple.gridX == 0) {
							int dist1 = Math.Abs(gridY - tuple.gridY);
							int dist2 = Math.Abs(gridY - y);
							
							// Keep the smallest version:
							if(dist1 < dist2) { continue; }
							else { tuple.gridY = (byte) y; tuple.gridX = (byte) x; }

						}
						
						else {
							tuple.gridX = (byte)x;
							tuple.gridY = (byte) y;
						}
					}

					yRange += 1;

					// If a node is located:
					if(tuple.gridX > 0 && tuple.gridY > 0) {
						tuple.nodeId = 0;   // TODO: Set the Node ID that applies here.
						return tuple;
					}
				}
			}

			// Return No Results
			return tuple;
		}
	}
}
