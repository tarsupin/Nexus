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

			// Camera Movement
			Systems.camera.MoveWithInput(Systems.localServer.MyPlayer.input);
		}

		public void WorldEditorInput() {
			InputClient input = Systems.input;

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
				else if(WE_UI.currentSlotGroup > 0) {
					this.CheckTileToolKeyBinds(localKeys[0]);
				}
			}

			// Open Wheel Menu
			if(input.LocalKeyPressed(Keys.Tab)) { this.weUI.contextMenu.OpenMenu(); }
		}
		
		public void CheckTileToolKeyBinds(Keys keyPressed) {
			if(keyPressed == Keys.D1) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 0); }
			else if(keyPressed == Keys.D2) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 1); }
			else if(keyPressed == Keys.D3) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 2); }
			else if(keyPressed == Keys.D4) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 3); }
			else if(keyPressed == Keys.D5) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 4); }
			else if(keyPressed == Keys.D6) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 5); }
			else if(keyPressed == Keys.D7) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 6); }
			else if(keyPressed == Keys.D8) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 7); }
			else if(keyPressed == Keys.D9) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 8); }
			else if(keyPressed == Keys.D0) { WETools.SetWorldTileToolBySlotGroup(WE_UI.currentSlotGroup, 9); }
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

				// Prevent repeat-draws on the same tile (e.g. within the last 100ms).
				if(!DrawTracker.AttemptDraw(gridX, gridY)) { return; }

				// Prevent drawing when a component is selected.
				if(UIComponent.ComponentWithFocus != null) { return; }

				WEPlaceholder ph = tool.CurrentPlaceholder;

				// Placing an Object
				if(ph.tObj > 0) {
					this.worldContent.SetTileObject(this.currentZone, (byte)gridX, (byte)gridY, ph.tObj);
				}

				// Placing a Standard Tile
				else {
					this.worldContent.SetTile(this.currentZone, (byte)gridX, (byte)gridY, ph.tBase, ph.tTop, ph.tCat, ph.tLayer, ph.tObj, ph.tNodeId);
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
	}
}
