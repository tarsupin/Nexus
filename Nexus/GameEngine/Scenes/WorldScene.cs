﻿using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Nexus.Engine.UIHandler;

namespace Nexus.GameEngine {

	public class WorldScene : Scene {

		// References
		public readonly WorldUI worldUI;
		public readonly PlayerInput playerInput;
		public WorldChar character;
		public CampaignState campaign;
		public Atlas atlas;

		// Access to World Data
		public WorldContent worldContent;
		public WorldFormat worldData;       // worldContent.data

		// Mapper Data
		public Dictionary<byte, string> WorldTerrain;
		public Dictionary<byte, string> WorldLayers;
		public Dictionary<byte, string> WorldObjects;
		public Dictionary<byte, string> WorldCharacters;

		// Grid Limits
		public byte xCount = (byte)WorldmapEnum.MinWidth;
		public byte yCount = (byte)WorldmapEnum.MinHeight;

		public WorldScene() : base() {

			// UI State
			UIHandler.SetUIOptions(false, false);
			UIHandler.SetMenu(null, false);

			// Prepare Components
			this.worldUI = new WorldUI(this);
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.campaign = Systems.handler.campaignState;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];

			// Prepare Mapper Data
			this.WorldTerrain = Systems.mapper.WorldTerrain;
			this.WorldLayers = Systems.mapper.WorldLayers;
			this.WorldObjects = new Dictionary<byte, string>();
			this.WorldCharacters = Systems.mapper.WorldCharacters;

			this.PrepareWorldObjects();

			// Prepare World Content
			this.worldContent = Systems.handler.worldContent;
			this.worldData = this.worldContent.data;

			// Prepare Campaign Details
			this.campaign.LoadCampaign(Systems.handler.worldContent.worldId, this.worldData.start);

			// Load World Character
			this.character = new WorldChar(this);
		}

		public WorldZoneFormat currentZone { get { return this.worldContent.GetWorldZone(this.campaign.zoneId); } }

		public override void StartScene() {

			// Make sure that world data is available.
			if(this.worldData is WorldFormat == false) {
				throw new Exception("Unable to load world. No world data available.");
			}

			this.ResetZone();

			// Reset Timer
			Systems.timer.ResetTimer();

			// Play or Stop Music
			Systems.music.Play(this.worldData.music);
		}

		private void ResetZone() {

			// Update Grid Limits
			this.xCount = this.worldContent.GetWidthOfZone(this.currentZone);
			this.yCount = this.worldContent.GetHeightOfZone(this.currentZone);

			// Camera Update
			Systems.camera.UpdateScene(this, 0, 0, this.xCount * (byte)WorldmapEnum.TileWidth, this.yCount * (byte)WorldmapEnum.TileHeight);

			// Update Character
			this.character.SetCharacter(this.campaign);
		}

		public override void EndScene() {
			//if(Systems.music.whatever) { Systems.music.SomeTrack.Stop(); }
		}

		// In the World Scene, World Objects needs to draw certain objects differently.
		// The Auto-Travel Dots, for example, need to be invisible.
		// Therefore, we must rebuild the WorldObjects property to be a clone, and then update appropriately.
		private void PrepareWorldObjects() {

			// Clone WorldObjects
			foreach(KeyValuePair<byte, string> entry in Systems.mapper.WorldObjects) {
				this.WorldObjects.Add(entry.Key, (string)entry.Value.Clone());
			}

			// Dots
			this.WorldObjects[(byte)OTerrainObjects.Dot_All] = "NodePoint";
			this.WorldObjects[(byte)OTerrainObjects.Dot_ULR] = "NodePoint";
			this.WorldObjects[(byte)OTerrainObjects.Dot_ULD] = "NodePoint";
			this.WorldObjects[(byte)OTerrainObjects.Dot_URD] = "NodePoint";
			this.WorldObjects[(byte)OTerrainObjects.Dot_LRD] = "NodePoint";
			this.WorldObjects.Remove((byte)OTerrainObjects.Dot_UL);
			this.WorldObjects.Remove((byte)OTerrainObjects.Dot_UR);
			this.WorldObjects.Remove((byte)OTerrainObjects.Dot_UD);
			this.WorldObjects.Remove((byte)OTerrainObjects.Dot_LR);
			this.WorldObjects.Remove((byte)OTerrainObjects.Dot_LD);
			this.WorldObjects.Remove((byte)OTerrainObjects.Dot_RD);
		}

		public override void RunTick() {

			// Update Timer
			Systems.timer.RunTick();

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				//player.Value.input.UpdateKeyStates(Systems.timer.Frame);
				player.Value.input.UpdateKeyStates(0); // TODO: Update LocalServer so frames are interpreted and assigned here.
			}

			// Update UI
			UIComponent.ComponentWithFocus = null;
			Cursor.UpdateMouseState();
			UIHandler.cornerMenu.RunTick();

			// Menu State
			if(UIHandler.uiState == UIState.Menu) {
				UIHandler.menu.RunTick();
				return;
			}

			// Play UI is active:

			// Open Menu (Start)
			InputClient input = Systems.input;

			// Open Menu
			if(input.LocalKeyPressed(Keys.Tab) || input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
				UIHandler.SetMenu(UIHandler.mainMenu, true);
			}

			// Open Console (Tilde)
			else if(Systems.input.LocalKeyPressed(Keys.OemTilde)) {
				UIHandler.worldConsole.Open();
			}

			// Run World UI Updates
			this.worldUI.RunTick();

			// Update Character
			this.character.RunTick();

			// Update Camera
			Systems.camera.Follow(this.character.posX, this.character.posY, 100);
			Systems.camera.StayBounded(0, this.xCount * (byte)WorldmapEnum.TileWidth, 0, this.yCount * (byte)WorldmapEnum.TileHeight);

			// Check Input Updates
			this.RunInputCheck();
		}

		public void RunInputCheck() {

			// Get the Local Keys Held Down
			//Keys[] localKeys = input.GetAllLocalKeysDown();
			//if(localKeys.Length == 0) { return; }

			// Movement
			if(playerInput.isDown(IKey.Up)) { this.TryTravel(DirCardinal.Up); }
			else if(playerInput.isDown(IKey.Down)) { this.TryTravel(DirCardinal.Down); }
			else if(playerInput.isDown(IKey.Left)) { this.TryTravel(DirCardinal.Left); }
			else if(playerInput.isDown(IKey.Right)) { this.TryTravel(DirCardinal.Right); }

			// Activate Node
			else if(playerInput.isPressed(IKey.AButton) == true) {
				this.ActivateNode();
			}
		}

		// Run this method when the character has arrived at a new location:
		public void ArriveAtLocation( byte gridX, byte gridY ) {

			// Get Current Tile Data
			byte[] wtData = this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);
			
			bool isNode = NodeData.IsObjectANode(wtData[5]);
			bool isAutoDot = NodeData.IsObjectAnAutoTravelDot(wtData[5]);

			// If a node is not located here, something is wrong.
			if(!isNode) { throw new Exception("Arrived at a destination that was not indicated as a node. That should not be possible."); }

			// Update the Campaign's Position
			this.campaign.SetPosition(gridX, gridY, (byte) this.campaign.lastDir);
			this.campaign.SaveCampaign();

			// Check if Node type is Automatic Travel Dot.
			if(isAutoDot) {

				// We need to automatically travel. Take the route that wasn't taken last time:
				byte lastDir = this.campaign.lastDir;
				DirCardinal nextDir = DirCardinal.None;

				// Determine the next intended route:
				var nodeDirs = NodeData.GetDotDirections(wtData[5]);

				if(nodeDirs.up && lastDir != (byte) DirCardinal.Down) { nextDir = DirCardinal.Up; }
				else if(nodeDirs.down && lastDir != (byte) DirCardinal.Up) { nextDir = DirCardinal.Down; }
				else if(nodeDirs.right && lastDir != (byte) DirCardinal.Left) { nextDir = DirCardinal.Right; }
				else if(nodeDirs.left && lastDir != (byte) DirCardinal.Right) { nextDir = DirCardinal.Left; }

				// Attempt to travel in that direction:
				bool success = this.TryTravel(nextDir);

				// If the Auto-Travel fails, we need to return back.
				if(!success) {
					if(lastDir == (byte) DirCardinal.Left) { this.TryTravel(DirCardinal.Right); }
					else if(lastDir == (byte) DirCardinal.Right) { this.TryTravel(DirCardinal.Left); }
					else if(lastDir == (byte) DirCardinal.Up) { this.TryTravel(DirCardinal.Down); }
					else if(lastDir == (byte) DirCardinal.Down) { this.TryTravel(DirCardinal.Up); }
				}

				return;
			}

			bool isWarp = NodeData.IsObjectAWarp(wtData[5]);

			// Check for Auto-Warps (to new World Zones)
			if(isWarp) {
				string curStr = Coords.MapToInt(gridX, gridY).ToString();
				string origNodeVal = this.currentZone.nodes[curStr];

				// Scan for any warp that has the same warp link:
				for(byte zoneID = 0; zoneID < this.worldData.zones.Count; zoneID++) {
					WorldZoneFormat zone = this.worldData.zones[zoneID];
					var nodes = zone.nodes;

					foreach(var node in nodes) {

						// If we have a warp that matches the current warp link ID:
						if(node.Value == origNodeVal) {
							var grid = Coords.GetFromInt(int.Parse(node.Key));

							// Make sure the warp we found isn't referencing itself:
							if(grid.x == gridX && grid.y == gridY) { continue; }

							// We located a separate node to link to:
							this.ActivateWarp(zoneID, (byte) grid.x, (byte) grid.y);
						}
					}
				}
			}
		}

		public bool TryTravel( DirCardinal dir = DirCardinal.None) {

			// Can only move if the character is at a Node.
			if(!this.character.IsAtNode) { return false; }

			// Get Current Tile Data
			byte[] wtData = this.worldContent.GetWorldTileData(this.currentZone, this.character.curX, this.character.curY);

			bool isNode = NodeData.IsObjectANode(wtData[5]);
			bool isBlocking = NodeData.IsObjectABlockingNode(wtData[5]);

			// If a node is not located here, continue.
			if(!isNode) { return false; }

			// If the node is blocking (level unfinished), only the path back is allowed:
			if(isBlocking) {

				// Identify Level Data at this node:
				int coordId = Coords.MapToInt(this.character.curX, this.character.curY);
				string levelId = this.currentZone.nodes.ContainsKey(coordId.ToString()) ? this.currentZone.nodes[coordId.ToString()] : "";

				// Check if this level has been completed (or isn't marked as one)
				if(levelId != "" && !this.campaign.IsLevelWon(this.campaign.zoneId, levelId)) {

					// The level hasn't been completed, so it is restricted in all directions except from where you came.
					var lastDir = this.campaign.lastDir;
					if(lastDir == (byte) DirCardinal.Left && dir != DirCardinal.Right) { return false; }
					if(lastDir == (byte) DirCardinal.Right && dir != DirCardinal.Left) { return false; }
					if(lastDir == (byte) DirCardinal.Up && dir != DirCardinal.Down) { return false; }
					if(lastDir == (byte) DirCardinal.Down && dir != DirCardinal.Up) { return false; }
				}
			}

			// Make sure that direction is allowed from current Node.
			if(!NodeData.IsDirectionAllowed(wtData[5], dir)) { return false; }

			// Check for a connecting Node (one with a return connection).
			var connectNode = NodeData.LocateNodeConnection(this.worldContent, this.currentZone, this.character.curX, this.character.curY, dir);

			// Verify that a connection node exists:
			if(connectNode.objectId == 0) { return false; }

			// Perform Movement
			this.character.TravelPath(connectNode.gridX, connectNode.gridY);
			this.campaign.lastDir = (byte) dir;

			return true;
		}

		public void ActivateWarp( byte zoneId, byte gridX, byte gridY ) {

			// Ignore the warp if it goes to its own zone.
			if(zoneId == this.campaign.zoneId) { return; }

			// Update Campaign Positions
			this.campaign.lastDir = (byte) DirCardinal.None;
			this.campaign.curX = gridX;
			this.campaign.curY = gridY;
			this.campaign.zoneId = zoneId;

			this.ResetZone();
		}

		public async Task<bool> ActivateNode() {

			// Can only activate if the character is at a Node.
			if(!this.character.IsAtNode) { return false; }

			// Get Current Tile Data
			byte[] wtData = this.worldContent.GetWorldTileData(this.currentZone, this.character.curX, this.character.curY);

			bool isPlayableNode = NodeData.IsObjectANode(wtData[5], false, false, true);

			// If a node is not playable, continue.
			if(!isPlayableNode) { return false; }

			// Identify Level Data at this node:
			int coordId = Coords.MapToInt(this.character.curX, this.character.curY);
			string levelId = this.currentZone.nodes.ContainsKey(coordId.ToString()) ? this.currentZone.nodes[coordId.ToString()] : "";

			if(levelId.Length == 0) { return false; }

			// Check if Level exists in file system.
			if(!LevelContent.LevelExists(levelId)) {
				bool success = await WebHandler.LevelRequest(levelId);
				
				// If the level failed to be located (including online), delete the reference to it from the zone.
				if(!success) {
					this.currentZone.nodes.Remove(coordId.ToString());
					this.campaign.SaveCampaign();
					return false;
				}
			}

			// If the level is valid, we can enter the level.
			bool isWon = this.campaign.IsLevelWon(this.campaign.zoneId, levelId);

			// Grant Character Their World Equipment On Casual or Beaten Nodes (after scene generated)
			if(!isWon && (wtData[5] != (byte)OTerrainObjects.NodeCasual && wtData[5] != (byte)OTerrainObjects.NodeWon)) {
				CampaignState campaign = Systems.handler.campaignState;
				campaign.SetUpgrades(0, 0, 0, 0, 0, 0, 0);
				campaign.SaveCampaign();
			}

			SceneTransition.ToLevel(this.worldData.id, levelId, true);
			return true;
		}

		public override void Draw() {
			Camera cam = Systems.camera;

			byte startX = (byte)Math.Max((byte)0, (byte)cam.WorldX);
			byte startY = (byte)Math.Max((byte)0, (byte)cam.WorldY);

			byte gridX = (byte)(startX + (byte)WorldmapEnum.MinWidth + 1); // +1 is to render the edge.
			byte gridY = (byte)(startY + (byte)WorldmapEnum.MinHeight + 1); // +1 is to render the edge.

			if(gridX > this.xCount) { gridX = (byte)(this.xCount); } // Must limit to room size.
			if(gridY > this.yCount) { gridY = (byte)this.yCount; } // Must limit to room size.

			// Camera Position
			int camX = cam.posX;
			int camY = cam.posY;

			byte[][][] curTiles = this.currentZone.tiles;

			// Loop through the zone tile data:
			for(byte y = gridY; y-- > startY;) {
				int tileYPos = y * (byte)WorldmapEnum.TileHeight - camY;

				for(byte x = gridX; x-- > startX;) {
					this.DrawWorldTile(curTiles[y][x], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos, x, y);
				};
			}

			// Draw World Character
			this.character.Draw(camX, camY);

			// Draw UI
			if(UIHandler.uiState == UIState.Playing) { this.worldUI.Draw(); }
			UIHandler.cornerMenu.Draw();
			UIHandler.menu.Draw();
		}

		// NOTE: This is ROUGHLY a duplicate of World Editor (probably). Just need to add "Atlas".
		public void DrawWorldTile(byte[] wtData, int posX, int posY, byte gridX, byte gridY) {

			// Draw Water / Coastline (special behavior)
			if(wtData[0] == (byte)OTerrain.Water) {

				// For Coastlines, we switch the "Base" and "Top" terrains, and draw the "Water/" coastline layers.
				// We're drawing a water varient if the "Base" and "Top" are both water.
				if(wtData[1] != 0) {
					if(wtData[1] != (byte)OTerrain.Water) { this.atlas.Draw(WorldTerrain[wtData[1]] + "/b1", posX, posY); }
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
				if(WorldObjects.ContainsKey(wtData[5])) {

					// If a level is marked as completed, draw a "NodeWon" instead of "NodeCasual" or "NodeStrict"
					if(wtData[5] == (byte)OTerrainObjects.NodeCasual || wtData[5] == (byte)OTerrainObjects.NodeStrict) {

						// Identify Level Data at this node:
						int coordId = Coords.MapToInt(gridX, gridY);
						string levelId = this.currentZone.nodes.ContainsKey(coordId.ToString()) ? this.currentZone.nodes[coordId.ToString()] : "";
						
						if(this.campaign.IsLevelWon(this.campaign.zoneId, levelId)) {
							this.atlas.Draw("Objects/" + WorldObjects[(byte)OTerrainObjects.NodeWon], posX, posY);
							return;
						}
					}

					this.atlas.Draw("Objects/" + WorldObjects[wtData[5]], posX, posY);
				}
			}
		}
	}
}
