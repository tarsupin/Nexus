﻿using Microsoft.Xna.Framework;
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
		public Dictionary<byte, string> WorldLayers;
		public Dictionary<byte, string> WorldObjects;

		// Local Mapping Rules
		private Dictionary<byte, OLayer> mapLayerRule = new Dictionary<byte, OLayer>() {
			{ (byte) AutoMapSequence.map_1001, OLayer.pv },
			{ (byte) AutoMapSequence.map_0110, OLayer.ph },

			{ (byte) AutoMapSequence.map_1010, OLayer.s1 },
			{ (byte) AutoMapSequence.map_1110, OLayer.s2 },
			{ (byte) AutoMapSequence.map_1100, OLayer.s3 },
			{ (byte) AutoMapSequence.map_1011, OLayer.s4 },
			{ (byte) AutoMapSequence.map_1111, OLayer.s5 },
			{ (byte) AutoMapSequence.map_1101, OLayer.s6 },
			{ (byte) AutoMapSequence.map_0011, OLayer.s7 },
			{ (byte) AutoMapSequence.map_0111, OLayer.s8 },
			{ (byte) AutoMapSequence.map_0101, OLayer.s9 },

			{ (byte) AutoMapSequence.map_0000, OLayer.e5 },
			{ (byte) AutoMapSequence.map_1000, OLayer.e2 },
			{ (byte) AutoMapSequence.map_0010, OLayer.e4 },
			{ (byte) AutoMapSequence.map_0100, OLayer.e6 },
			{ (byte) AutoMapSequence.map_0001, OLayer.e8 },
		};

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
				if(ph.obj > 0) {
					this.PlaceObject(ph.obj, (byte) gridX, (byte) gridY);
				}

				// Placing Tile Base
				else if(ph.tBase > 0) {
					this.PlaceTile(ph, (byte) gridX, (byte) gridY);
				}

				// Placing Top Tile
				else if(ph.top > 0) {
					this.PlaceTile(ph, (byte) gridX, (byte) gridY, WorldTileStack.Top);
				}

				// Placing Cover Tile
				else if(ph.cover > 0) {
					this.PlaceTile(ph, (byte) gridX, (byte) gridY, WorldTileStack.Cover);
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
		public void PlaceTile(WEPlaceholder ph, byte gridX, byte gridY, WorldTileStack tileStack = WorldTileStack.Base) {

			// Check Current Tile (might affect placement)
			byte[] wtData = this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);

			// Run Tile Placement (Base)
			if(tileStack == WorldTileStack.Base) {

				// If placing base water, remove any top layers. Otherwise, it conflicts with draw behaviors.
				if(ph.tBase == (byte) OTerrain.Water) {
					this.worldContent.SetTileTop(this.currentZone, gridX, gridY, 0, 0);
				}

				this.worldContent.SetTileBase(this.currentZone, gridX, gridY, ph.tBase);
			}
			
			// Run Tile Placement (Top)
			else if(tileStack == WorldTileStack.Top) {
				
				// Don't place any top pieces on water, unless it meets the above criteria.
				if(wtData[0] == (byte) OTerrain.Water) { return; }

				this.worldContent.SetTileTop(this.currentZone, gridX, gridY, ph.top, ph.topLay);
			}
			
			// Run Tile Placemenet (Cover)
			else if(tileStack == WorldTileStack.Cover) {
				if(wtData[0] == (byte)OTerrain.Water) { return; } // Skip cover placements on water.
				this.worldContent.SetTileCover(this.currentZone, gridX, gridY, ph.cover, ph.coverLay);
			}

			// Run Auto-Tiling
			if(ph.auto) {

				// Auto-Tile the Cover Terrain
				if(ph.cover > 0) {
					this.RunAutoTileCover(ph.cover, (byte)(gridX - 1), (byte)(gridY - 1));
					this.RunAutoTileCover(ph.cover, (byte)(gridX), (byte)(gridY - 1));
					this.RunAutoTileCover(ph.cover, (byte)(gridX + 1), (byte)(gridY - 1));
					this.RunAutoTileCover(ph.cover, (byte)(gridX - 1), (byte)(gridY));
					this.RunAutoTileCover(ph.cover, (byte)(gridX), (byte)(gridY));
					this.RunAutoTileCover(ph.cover, (byte)(gridX + 1), (byte)(gridY));
					this.RunAutoTileCover(ph.cover, (byte)(gridX - 1), (byte)(gridY + 1));
					this.RunAutoTileCover(ph.cover, (byte)(gridX), (byte)(gridY + 1));
					this.RunAutoTileCover(ph.cover, (byte)(gridX + 1), (byte)(gridY + 1));
					return;
				}

				// Auto-Tile the Base/Top Terrain
				this.RunAutoTile((byte) (gridX - 1), (byte) (gridY - 1));
				this.RunAutoTile((byte) (gridX), (byte) (gridY - 1));
				this.RunAutoTile((byte) (gridX + 1), (byte) (gridY - 1));
				this.RunAutoTile((byte) (gridX - 1), (byte) (gridY));
				this.RunAutoTile((byte) (gridX), (byte) (gridY));
				this.RunAutoTile((byte) (gridX + 1), (byte) (gridY));
				this.RunAutoTile((byte) (gridX - 1), (byte) (gridY + 1));
				this.RunAutoTile((byte) (gridX), (byte) (gridY + 1));
				this.RunAutoTile((byte) (gridX + 1), (byte) (gridY + 1));
				return;
			}
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

			// Draw Water / Coastline (special behavior)
			if(wtData[0] == (byte) OTerrain.Water) {

				// For Coastlines, we switch the "Base" and "Top" terrains, and draw the "Water/" coastline layers.
				// We're drawing a water varient if the "Base" and "Top" are both water.
				if(wtData[1] != 0) {
					if(wtData[1] != (byte) OTerrain.Water) { this.atlas.Draw(WorldTerrain[wtData[1]] + "/b1", posX, posY); }
					this.atlas.Draw("Water/" + WorldLayers[wtData[2]], posX, posY);
				} else {
					this.atlas.Draw(WorldTerrain[wtData[0]] + "/b1", posX, posY);
				}

			} else {

				// Draw Base, unless the top layer is identical.
				if(wtData[0] != 0 && wtData[0] != wtData[1]) {
					this.atlas.Draw(WorldTerrain[wtData[0]] + "/b1", posX, posY);
				}

				// Draw Top [1], [2]
				if(wtData[1] != 0) {
					this.atlas.Draw(WorldTerrain[wtData[1]] + "/" + WorldLayers[wtData[2]], posX, posY);
				}
			}

			// Draw Cover [3], [4]
			if(wtData[3] != 0) {
				this.atlas.Draw(WorldTerrain[wtData[3]] + "/" + WorldLayers[wtData[4]], posX, posY);
			}

			// Draw Object Layer [5]
			if(wtData[5] != 0) {
				this.atlas.Draw("Objects/" + WorldObjects[wtData[5]], posX, posY);
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

		// --------------------- //
		// --- Node Handling --- //
		// --------------------- //

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
			byte objectId = wtData[5];

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

		// -------------------------- //
		// --- Terrain Generation --- //
		// -------------------------- //

		// Automatically updates the terrain at a given grid location.
		private void RunAutoTile( byte gridX, byte gridY ) {

			if(gridX > this.xCount) { return; }
			if(gridY > this.yCount) { return; }

			byte[] tData = this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);

			// Determine which terrain tile type (e.g. OTerrain.Grass, OTerrain.Snow, etc) is most common nearby.
			byte neighborType = this.GetNeighborType(gridX, gridY, tData[0]);

			// If there is no neighbor type discovered, all types are of the placed terrain.
			// ALSO: Make sure the order of terrain tiling is determined, otherwise both tiles will try to layer the other.
			// ALSO: Base Terrain above Snow ID can only auto-tile with water.
			if(neighborType == 0 || neighborType > tData[0] || (neighborType > (byte) OTerrain.Snow && tData[0] != (byte)OTerrain.Water)) {

				// Can remove the layers from this tile:
				this.worldContent.SetTile(this.currentZone, gridX, gridY, tData[0], 0, 0, tData[3], tData[4], tData[5], tData[6]);
				return;
			}

			// Neighbor type was discovered. Run the AutoTiler.
			byte mapSeq = this.GetAutoMap(gridX, gridY, tData[0]);
			bool isStandard = this.IsStandardMapSequence(mapSeq);
			OLayer mapRule = this.mapLayerRule[mapSeq];

			// Check if the map sequence is a "standard" piece that may have corners.
			if(isStandard) {

				// If working with 's5' sequence, it might need additional layering:
				if(mapRule == OLayer.s5) {
					bool c1 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY + 1), neighborType);
					bool c3 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY + 1), neighborType);
					bool c7 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY - 1), neighborType);
					bool c9 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY - 1), neighborType);

					if(c1) {
						if(c3) {
							if(c7 && c9) { mapRule = OLayer.c5; }
							else if(c7) { mapRule = OLayer.v1; }
							else if(c9) { mapRule = OLayer.v3; }
							else { mapRule = OLayer.c8; }
						}
						else if(c7) {
							if(c9) { mapRule = OLayer.v7; }
							else { mapRule = OLayer.c6; }
						}
						else if(c9) { mapRule = OLayer.cr; }
						else { mapRule = OLayer.c1; }
					}
					else if(c3) {
						if(c7 && c9) { mapRule = OLayer.v9; }
						else if(c7) { mapRule = OLayer.cl; }
						else if(c9) { mapRule = OLayer.c4; }
						else { mapRule = OLayer.c3; }
					}
					else if(c7) {
						if(c9) { mapRule = OLayer.c2; }
						else { mapRule = OLayer.c7; }
					}
					else if(c9) {
						mapRule = OLayer.c9;
					}

					// If no rules were detected, then it's an empty square. Can remove its layer.
					else {
						this.worldContent.SetTile(this.currentZone, gridX, gridY, tData[0], 0, tData[2], 0, tData[4], tData[5]);
						return;
					}
				}

				else if(mapRule == OLayer.s1) {
					if(this.TerrainMatch((byte)(gridX + 1), (byte)(gridY - 1), neighborType)) { mapRule = OLayer.p1; }
				}

				else if(mapRule == OLayer.s3) {
					if(this.TerrainMatch((byte)(gridX - 1), (byte)(gridY - 1), neighborType)) { mapRule = OLayer.p3; }
				}

				else if(mapRule == OLayer.s7) {
					if(this.TerrainMatch((byte)(gridX + 1), (byte)(gridY + 1), neighborType)) { mapRule = OLayer.p7; }
				}

				else if(mapRule == OLayer.s9) {
					if(this.TerrainMatch((byte)(gridX - 1), (byte)(gridY + 1), neighborType)) { mapRule = OLayer.p9; }
				}

				else if(mapRule == OLayer.s2) {
					bool c7 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY - 1), neighborType);
					bool c9 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY - 1), neighborType);
					if(c7 && c9) { mapRule = OLayer.t2; }
					else if(c7) { mapRule = OLayer.r7; }
					else if(c9) { mapRule = OLayer.l9; }
				}

				else if(mapRule == OLayer.s4) {
					bool c3 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY + 1), neighborType);
					bool c9 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY - 1), neighborType);
					if(c3 && c9) { mapRule = OLayer.t4; }
					else if(c3) { mapRule = OLayer.l3; }
					else if(c9) { mapRule = OLayer.r9; }
				}
					
				else if(mapRule == OLayer.s6) {
					bool c1 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY + 1), neighborType);
					bool c7 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY - 1), neighborType);
					if(c1 && c7) { mapRule = OLayer.t6; }
					else if(c1) { mapRule = OLayer.r1; }
					else if(c7) { mapRule = OLayer.l7; }
				}

				else if(mapRule == OLayer.s8) {
					bool c1 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY + 1), neighborType);
					bool c3 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY + 1), neighborType);
					if(c1 && c3) { mapRule = OLayer.t8; }
					else if(c1) { mapRule = OLayer.l1; }
					else if(c3) { mapRule = OLayer.r3; }
				}
			}

			// Check if the map layering rule was set as a path ('ph' or 'pv'). May want to ignore nearby non-neighbor tiles.
			else if(mapRule == OLayer.ph) {
				bool c2 = this.TerrainMatch(gridX, (byte)(gridY + 1), neighborType);
				bool c8 = this.TerrainMatch(gridX, (byte)(gridY - 1), neighborType);
				if(!c2) { mapRule = OLayer.s8; }
				else if(!c8) { mapRule = OLayer.s2; }
			}
				
			else if(mapRule == OLayer.pv) {
				bool c4 = this.TerrainMatch((byte)(gridX - 1), gridY, neighborType);
				bool c6 = this.TerrainMatch((byte)(gridX + 1), gridY, neighborType);
				if(!c4) { mapRule = OLayer.s6; }
				else if(!c6) { mapRule = OLayer.s4; }
			}

			// Special Rules for Water Cliffs
			if(tData[0] == (byte)OTerrain.Water || neighborType == (byte) OTerrain.Water) {
				switch(mapRule) {
					case OLayer.c1:
					case OLayer.c3:
					case OLayer.c7:
					case OLayer.c9:
					case OLayer.s:
					case OLayer.s1:
					case OLayer.s2:
					case OLayer.s3:
					case OLayer.s4:
					case OLayer.s5:
					case OLayer.s6:
					case OLayer.s7:
					case OLayer.s8:
					case OLayer.s9:
					case OLayer.w1:
					case OLayer.w3:
					case OLayer.w7:
					case OLayer.w9:
						break;
					default:
						mapRule = OLayer.s5;
						break;
				}
			}

			// Apply the Layer to the Tile
			this.worldContent.SetTileTop(this.currentZone, gridX, gridY, neighborType, (byte)mapRule);
		}

		// Automatically updates terrain cover at a given grid location.
		private void RunAutoTileCover(byte cover, byte gridX, byte gridY) {

			// Only process this terrain update if the tile matches the cover being auto-tiled.
			byte[] tData = this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);
			if(tData[3] != cover) { return; }

			byte mapSeq = this.GetAutoMap(gridX, gridY, cover, 3);
			bool isStandard = this.IsStandardMapSequence(mapSeq);

			// Check if the map rule detected "standard" edges.
			// If it didn't, there's no chance the cover terrain can connect, and it should remain as a solo block.
			if(!isStandard) {
				this.worldContent.SetTileCover(this.currentZone, gridX, gridY, cover, (byte) OLayer.s);
				return;
			}

			// Determine Unrefined Map Rule based on Edges
			OLayer finalRule = OLayer.s;

			// Determine Sides (which will help to refine the map rule)
			bool s2 = this.TerrainMatch((byte)(gridX), (byte)(gridY + 1), cover, 3);
			bool s4 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY), cover, 3);
			bool s6 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY), cover, 3);
			bool s8 = this.TerrainMatch((byte)(gridX), (byte)(gridY - 1), cover, 3);

			// Determine Corners (which will help to refine the map rule)
			bool c1 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY + 1), cover, 3);
			bool c3 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY + 1), cover, 3);
			bool c7 = this.TerrainMatch((byte)(gridX - 1), (byte)(gridY - 1), cover, 3);
			bool c9 = this.TerrainMatch((byte)(gridX + 1), (byte)(gridY - 1), cover, 3);

			// Center Piece
			if(s2 && s4 && s6 && s8) {

				// Some Terrain Cover doesn't have these corner pieces allowed.
				if(cover == (byte)OTerrain.TreesPine || cover == (byte)OTerrain.TreesPineSnow) {
					finalRule = OLayer.s5;
				}

				else if(c1) {
					if(c3) {
						if(c7) {
							if(c9) { finalRule = OLayer.s5; }
							else { finalRule = OLayer.c9; }
						} else if(c9) {
							finalRule = OLayer.c7;
						}
					} else if(c7) {
						if(c9) { finalRule = OLayer.c3; }
					}
				} else if(c3) {
					if(c7 && c9) { finalRule = OLayer.c1; }
				}
			}

			// Side Pieces
			else if(s4 && s6 && s8) { if(c7 && c9) { finalRule = OLayer.s2; } }
			else if(s2 && s6 && s8) { if(c3 && c9) { finalRule = OLayer.s4; } }
			else if(s2 && s4 && s8) { if(c1 && c7) { finalRule = OLayer.s6; } }
			else if(s2 && s4 && s6) { if(c1 && c3) { finalRule = OLayer.s8; } }
			
			// Corner Pieces
			else if (s6 && s8) { if(c9) { finalRule = OLayer.s1; } }
			else if (s4 && s8) { if(c7) { finalRule = OLayer.s3; } }
			else if (s2 && s6) { if(c3) { finalRule = OLayer.s7; } }
			else if (s2 && s4) { if(c1) { finalRule = OLayer.s9; } }

			// Apply the Layer to the Tile
			this.worldContent.SetTileCover(this.currentZone, gridX, gridY, cover, (byte) finalRule);
		}

		// Returns TRUE if the tile at gridX, gridY has the Terrain Type (tData[0], tBase) that matches matchVal.
		private bool TerrainMatch( byte gridX, byte gridY, byte matchVal, byte pos = 0 ) {
			byte[] tData = this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);
			return (tData[pos] != 0 && tData[pos] == matchVal);
		}

		// Returns TRUE if the AutoMapSequence value is a "standard" map rule: s1, s2, s3, s4, etc...
		private bool IsStandardMapSequence( byte mapSequence ) {
			switch(mapSequence) {
				case (byte)AutoMapSequence.map_1010:	// s1
				case (byte)AutoMapSequence.map_1110:	// s2
				case (byte)AutoMapSequence.map_1100:	// s3
				case (byte)AutoMapSequence.map_1011:	// s4
				case (byte)AutoMapSequence.map_1111:	// s5
				case (byte)AutoMapSequence.map_1101:	// s6
				case (byte)AutoMapSequence.map_0011:	// s7
				case (byte)AutoMapSequence.map_0111:	// s8
				case (byte)AutoMapSequence.map_0101:    // s9
					return true;
			}
			return false;
		}

		// Identifies neighbor tile OTerrain type (e.g. OTerrain.Grass, Oterrain.Snow, etc) with the most matches.
		private byte GetNeighborType( byte gridX, byte gridY, byte ignoreType = 0 ) {
			WorldZoneFormat zone = this.currentZone;
			byte[][][] tiles = zone.tiles;
			byte[] tTracker = new byte[22];

			byte maxX = (byte)Math.Min(this.xCount, gridX + 1);
			byte maxY = (byte)Math.Min(this.yCount, gridY + 1);

			// Loop through neighboring tiles.
			for(byte y = (byte)Math.Max(0, gridY - 1); y <= maxY; y++) {
				if(y >= tiles.Length) { continue; }
				byte[][] yData = tiles[y];
				
				for(byte x = (byte)Math.Max(0, gridX - 1); x <= maxX; x++) {
					if(x >= yData.Length) { continue; }

					// Retrieve the Terrain Data at this x, y position.
					byte[] tData = this.worldContent.GetWorldTileData(zone, x, y);

					if(tData[0] == ignoreType) { continue; }

					// Coastline tiles are treated as water:
					if(tData[1] == 4 && ignoreType != 4) { tTracker[4]++; continue; }

					// The terrain is valid. Add it to the tracker to determine the count.
					tTracker[tData[0]]++;
				}
			}

			// Identify the terrain type with the most neighbor matches:
			byte terrainWithMost = 0;
			byte terrainIndex = 0;

			for(byte i = 0; i < tTracker.Length; i++) {
				if(terrainWithMost < tTracker[i]) {
					terrainIndex = i;
					terrainWithMost = tTracker[i];
				}
			}

			return terrainIndex;
		}

		// Retrieves AutoMapSequence enum value (WorldTypes.cs), such as "1001" to identify what auto-tiling should occur.
		// 'placedType' is an OTerrain value, such as OTerrain.Grass
		private byte GetAutoMap( byte gridX, byte gridY, byte placedType, byte pos = 0 ) {
			WorldZoneFormat zone = this.currentZone;

			byte[] top = this.worldContent.GetWorldTileData(zone, gridX, (byte)(gridY - 1));
			byte[] left = this.worldContent.GetWorldTileData(zone, (byte)(gridX - 1), gridY);
			byte[] right = this.worldContent.GetWorldTileData(zone, (byte)(gridX + 1), gridY);
			byte[] bottom = this.worldContent.GetWorldTileData(zone, gridX, (byte)(gridY + 1));

			// Top == Placed
			if(top[pos] == placedType) {

				// Left == Placed
				if(left[pos] == placedType) {

					// Right == Placed
					if(right[pos] == placedType) {
						if(bottom[pos] == placedType) { return (byte) AutoMapSequence.map_1111; }
						return (byte)AutoMapSequence.map_1110;
					}

					if(bottom[pos] == placedType) { return (byte)AutoMapSequence.map_1101; }
					return (byte)AutoMapSequence.map_1100;

				}

				// Right == Placed
				if(right[pos] == placedType) {
					if(bottom[pos] == placedType) { return (byte)AutoMapSequence.map_1011; }
					return (byte)AutoMapSequence.map_1010;
				}

				if(bottom[pos] == placedType) { return (byte)AutoMapSequence.map_1001; }
				return (byte)AutoMapSequence.map_1000;
			}

			// Left == Placed
			if(left[pos] == placedType) {

				// Right == Placed
				if(right[pos] == placedType) {
					if(bottom[pos] == placedType) { return (byte)AutoMapSequence.map_0111; }
					return (byte)AutoMapSequence.map_0110;
				}

				if(bottom[pos] == placedType) { return (byte)AutoMapSequence.map_0101; }
				return (byte)AutoMapSequence.map_0100;

			}

			// Right == Placed
			if(right[pos] == placedType) {
				if(bottom[pos] == placedType) { return (byte)AutoMapSequence.map_0011; }
				return (byte)AutoMapSequence.map_0010;
			}

			if(bottom[pos] == placedType) { return (byte)AutoMapSequence.map_0001; }
			return (byte)AutoMapSequence.map_0000;
		}
	}
}
