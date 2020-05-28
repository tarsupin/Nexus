using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class WorldEditorScene : Scene {

		// References
		public readonly WorldEditorUI worldEditorUI;
		public readonly PlayerInput playerInput;
		public CampaignState campaign;
		public Atlas atlas;

		// Access to World Data
		public WorldContent worldContent;
		public WorldFormat worldData;		// worldContent.data

		// Grid Limits
		public byte xCount = 45;
		public byte yCount = 26;

		public WorldEditorScene() : base() {

			// Prepare Components
			this.worldEditorUI = new WorldEditorUI(this);
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.campaign = Systems.handler.campaignState;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];

			// Prepare World Content
			this.worldContent = Systems.handler.worldContent;
			this.worldData = this.worldContent.data;

			// Camera Update
			Systems.camera.UpdateScene(this);

			// Add Mouse Behavior
			Systems.SetMouseVisible(true);
			Cursor.UpdateMouseState();
		}

		public WorldZoneFormat currentZone { get { return this.worldContent.GetWorldZone(this.campaign.zoneId); } }

		public override void StartScene() {

			// Make sure that world data is available.
			if(this.worldData is WorldFormat == false) {
				throw new Exception("Unable to load world. No world data available.");
			}

			// Update Grid Limits
			this.xCount = this.worldContent.GetWidthOfZone(this.currentZone);
			this.yCount = this.worldContent.GetHeightOfZone(this.currentZone);
		}

		public override void RunTick() {

			// Update the Mouse State Every Tick
			Cursor.UpdateMouseState();

			// Run World UI Updates
			this.worldEditorUI.RunTick();

			// Debug Console (only runs if visible)
			Systems.worldEditConsole.RunTick();

			// Check Input Updates
			this.RunInputCheck();
		}

		public void RunInputCheck() {
			InputClient input = Systems.input;

			// Get the Local Keys Held Down
			//Keys[] localKeys = input.GetAllLocalKeysDown();
			//if(localKeys.Length == 0) { return; }

			// Menu-Specific Key Presses
			// if(this.game.menu.isOpen) { return this.game.menu.onKeyDown( key, iKeyPressed ); }

			// Open Menu
			if(input.LocalKeyPressed(Keys.Tab) || input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
				// TODO: Open a context menu here.
			}
		}

		public override void Draw() {
			Camera cam = Systems.camera;

			byte startX = (byte)Math.Max((byte)0, (byte)cam.GridX);
			byte startY = (byte)Math.Max((byte)0, (byte)cam.GridY);

			byte gridX = (byte)(startX + 45 + 1); // 26 is view size. +1 is to render the edge.
			byte gridY = (byte)(startY + 26 + 1); // 25.5 is view size. +1 is to render the edge.

			if(gridX > this.xCount) { gridX = (byte)(this.xCount); } // Must limit to room size.
			if(gridY > this.yCount) { gridY = (byte)(this.yCount); } // Must limit to room size.

			// Camera Position
			int camX = cam.posX;
			int camY = cam.posY;

			// Prepare Zone Data
			var WorldTerrain = Systems.mapper.WorldTerrain;
			var WorldTerrainCat = Systems.mapper.WorldTerrainCat;
			var WorldLayers = Systems.mapper.WorldLayers;
			var WorldObjects = Systems.mapper.WorldObjects;

			byte[][][] curTiles = this.currentZone.tiles;

			// Loop through the zone tile data:
			for(byte y = gridY; y-- > startY;) {
				ushort tileYPos = (ushort)(y * (byte)WorldmapEnum.TileHeight - camY);

				for(byte x = gridX; x-- > startX;) {
					byte[] wtData = curTiles[y][x];
					
					// Draw Base
					if(wtData[0] != 0) {

						// If there is a top layer:
						if(wtData[1] != 0) {

							// Draw a standard base tile with no varient, so that the top layer will look correct.
							this.atlas.Draw(WorldTerrain[wtData[0]] + "/b1", x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);

							// Draw the Top Layer
							this.atlas.Draw(WorldTerrain[wtData[1]] + "/" + WorldLayers[wtData[3]], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);
						}

						// If there is not a top layer:
						else {

							// If there is a category:
							if(wtData[2] != 0) {
								this.atlas.Draw(WorldTerrain[wtData[0]] + "/" + WorldTerrainCat[wtData[2]] + "/" + WorldLayers[wtData[3]], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);
							} else {
								this.atlas.Draw(WorldTerrain[wtData[0]] + "/" + WorldLayers[wtData[3]], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);
							}
						}
					}

					// Draw Top, with no base:
					else if(wtData[1] != 0) {
						this.atlas.Draw(WorldTerrain[wtData[1]] + "/" + WorldLayers[wtData[3]], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);
					}

					// Draw Object Layer
					if(wtData[4] != 0) {
						this.atlas.Draw("Objects/" + WorldObjects[wtData[4]], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);
					}
				};
			}

			// Draw UI
			this.worldEditorUI.Draw();
			Systems.worldEditConsole.Draw();
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
			this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);
			// TODO: Update the tile being drawn with.
		}

	}
}
