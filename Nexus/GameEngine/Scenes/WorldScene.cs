using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

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
		public byte xCount = 45;
		public byte yCount = 28;

		public WorldScene() : base() {

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

			// Camera Update
			//Systems.camera.UpdateScene(this, 0, 0, xCount * (byte) WorldmapEnum.TileWidth, yCount * (byte)WorldmapEnum.TileHeight);
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

			// Camera Update
			Systems.camera.UpdateScene(this, 0, 0, this.xCount * (byte)WorldmapEnum.TileWidth, this.yCount * (byte)WorldmapEnum.TileHeight);

			// Update Character
			this.character.SetCharacter(this.campaign);

			// Reset Timer
			Systems.timer.ResetTimer();

			// Begin New Music Track
			// TODO: MUSIC HERE
			//Systems.music.SomeTrack.Play();
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

			// Update Values that must be changed from the original Mapper version:

			// Nodes
			//this.WorldObjects[(byte)OTerrainObjects.NodePoint] = "NodePoint";
			//this.WorldObjects[(byte)OTerrainObjects.NodeMove] = "NodeMove";

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

			// Update Character
			this.character.RunTick();

			// Run World UI Updates
			this.worldUI.RunTick();

			// Update Camera
			Systems.camera.Follow(this.character.posX, this.character.posY, 100);

			// Debug Console (only runs if visible)
			Systems.worldConsole.RunTick();

			// Prevent other interactions if the console is visible.
			if(Systems.worldConsole.visible) { return; }

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

			// Movement
			else if(playerInput.isDown(IKey.Up)) { this.TryTravel(DirCardinal.Up); }
			else if(playerInput.isDown(IKey.Down)) { this.TryTravel(DirCardinal.Down); }
			else if(playerInput.isDown(IKey.Left)) { this.TryTravel(DirCardinal.Left); }
			else if(playerInput.isDown(IKey.Right)) { this.TryTravel(DirCardinal.Right); }

			// Activate Node
			else if(playerInput.isPressed(IKey.AButton) == true) {
				// TODO: ACTIVATE NODE HERE
			}
		}

		// Run this method when the character has arrived at a new location:
		public void ArriveAtLocation( byte gridX, byte gridY ) {

			// Get Current Tile Data
			byte[] wtData = this.worldContent.GetWorldTileData(this.currentZone, gridX, gridY);
			
			bool isNode = NodePath.IsObjectANode(wtData[5]);
			bool isAutoDot = NodePath.IsObjectAnAutoTravelDot(wtData[5]);

			// If a node is not located here, something is wrong.
			if(!isNode) { throw new Exception("Arrived at a destination that was not indicated as a node. That should not be possible."); }

			// Check if Node type is Automatic Travel Dot.
			if(isAutoDot) {

				// We need to automatically travel. Take the route that wasn't taken last time:
				DirCardinal lastDir = this.character.lastDir;
				DirCardinal nextDir = DirCardinal.None;

				// Determine the next intended route:
				var nodeDirs = NodePath.GetDotDirections(wtData[5]);

				if(nodeDirs.up && lastDir != DirCardinal.Down) { nextDir = DirCardinal.Up; }
				else if(nodeDirs.down && lastDir != DirCardinal.Up) { nextDir = DirCardinal.Down; }
				else if(nodeDirs.right && lastDir != DirCardinal.Left) { nextDir = DirCardinal.Right; }
				else if(nodeDirs.left && lastDir != DirCardinal.Right) { nextDir = DirCardinal.Left; }

				// Attempt to travel in that direction:
				bool success = this.TryTravel(nextDir);

				// If the Auto-Travel fails, we need to return back.
				if(!success) {
					if(lastDir == DirCardinal.Left) { this.TryTravel(DirCardinal.Right); }
					else if(lastDir == DirCardinal.Right) { this.TryTravel(DirCardinal.Left); }
					else if(lastDir == DirCardinal.Up) { this.TryTravel(DirCardinal.Down); }
					else if(lastDir == DirCardinal.Down) { this.TryTravel(DirCardinal.Up); }
				}

				return;
			}

			// Check for Auto-Warps


			//// AUTO-TRAVEL : Attempt to automatically determine a direction when one is not provided.
			//if(dir == DirCardinal.Center) {

			//	// Check for Auto-Warps (to new World Zones)
			//	if(curNode.type >= NodeType.Warp && curNode.warp > 0) {

			//		// If we're supposed to leave the warp (after an arrival)
			//		if(!leaveSpot) {
			//			dir = this.GetAutoDir(curNode, 0);
			//		} else {
			//			this.ActivateWarp(curNode.zone, curNode.warp);
			//			return;
			//		}
			//	}

		}

		public bool TryTravel( DirCardinal dir = DirCardinal.Center ) {

			// Can only move if the character is at a Node.
			if(!this.character.IsAtNode) { return false; }

			// Get Current Tile Data
			byte[] wtData = this.worldContent.GetWorldTileData(this.currentZone, this.character.curX, this.character.curY);

			bool isNode = NodePath.IsObjectANode(wtData[5]);
			bool isBlocking = NodePath.IsObjectABlockingNode(wtData[5]);

			// If a node is not located here, continue.
			if(!isNode) { return false; }

			// If the node is blocking (level unfinished), only the path back is allowed:
			if(isBlocking) {

				// Identify Level Data at this node:
				uint coordId = Coords.MapToInt(this.character.curX, this.character.curY);
				string levelId = this.currentZone.nodes.ContainsKey(coordId.ToString()) ? this.currentZone.nodes[coordId.ToString()] : "";

				// Check if this level has been completed (or isn't marked as one)
				if(levelId != "" && !this.campaign.IsLevelWon(this.campaign.zoneId, levelId)) {

					// The level hasn't been completed, so it is restricted in all directions except from where you came.
					var lastDir = this.character.lastDir;
					if(lastDir == DirCardinal.Left && dir != DirCardinal.Right) { return false; }
					if(lastDir == DirCardinal.Right && dir != DirCardinal.Left) { return false; }
					if(lastDir == DirCardinal.Up && dir != DirCardinal.Down) { return false; }
					if(lastDir == DirCardinal.Down && dir != DirCardinal.Up) { return false; }
				}
			}

			// Make sure that direction is allowed from current Node.
			if(!NodePath.IsDirectionAllowed(wtData[5], dir)) { return false; }

			// Check for a connecting Node (one with a return connection).
			var connectNode = NodePath.LocateNodeConnection(this.worldContent, this.currentZone, this.character.curX, this.character.curY, dir);

			// Verify that a connection node exists:
			if(!connectNode.hasNode) { return false; }

			// Perform Movement
			this.character.TravelPath(connectNode.gridX, connectNode.gridY, dir);

			return true;
		}

		public void ActivateWarp( byte zoneId, ushort warpId ) {

			// Ignore the warp if it goes to its own zone.
			if(zoneId == this.campaign.zoneId) { return; }

			// TODO: FINISH
			// Find the warp in the designated zone:
			//ushort nodeId = this.zones.();
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
			for(byte y = gridY; y-- > startY;) {
				int tileYPos = y * (byte)WorldmapEnum.TileHeight - camY;

				for(byte x = gridX; x-- > startX;) {
					this.DrawWorldTile(curTiles[y][x], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);
				};
			}

			// Draw World Character
			this.character.Draw(camX, camY);

			// Draw UI
			this.worldUI.Draw();
			Systems.worldConsole.Draw();
		}

		// NOTE: This is a duplicate of World Editor.
		public void DrawWorldTile(byte[] wtData, int posX, int posY) {

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
					this.atlas.Draw("Objects/" + WorldObjects[wtData[5]], posX, posY);
				}
			}
		}
	}
}
