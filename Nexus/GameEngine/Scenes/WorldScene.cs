using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
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

		// Grid Limits
		public byte xCount = 20;
		public byte yCount = 20;

		public WorldScene() : base() {

			// Prepare Components
			this.worldUI = new WorldUI(this);
			this.character = new WorldChar(this);
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.campaign = Systems.handler.campaignState;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];

			// Prepare Mapper Data
			this.WorldTerrain = Systems.mapper.WorldTerrain;
			this.WorldLayers = Systems.mapper.WorldLayers;
			this.WorldObjects = Systems.mapper.WorldObjects;

			// Prepare World Content
			this.worldContent = Systems.handler.worldContent;
			this.worldData = this.worldContent.data;

			// Camera Update
			Systems.camera.UpdateScene(this);
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

			// Generate Character
			this.character = new WorldChar(this, HeadSubType.RyuHead);

			// Reset Timer
			Systems.timer.ResetTimer();

			// Begin New Music Track
			// TODO: MUSIC HERE
			//Systems.music.SomeTrack.Play();
		}

		public override void EndScene() {
			//if(Systems.music.whatever) { Systems.music.SomeTrack.Stop(); }
		}

		public override void RunTick() {

			// Update Timer
			Systems.timer.RunTick();

			// Run World UI Updates
			this.worldUI.RunTick();

			// Debug Console (only runs if visible)
			Systems.worldConsole.RunTick();

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
			else if(playerInput.isPressed(IKey.Up)) { this.TryTravel(DirCardinal.Up); }
			else if(playerInput.isPressed(IKey.Down)) { this.TryTravel(DirCardinal.Down); }
			else if(playerInput.isPressed(IKey.Left)) { this.TryTravel(DirCardinal.Left); }
			else if(playerInput.isPressed(IKey.Right)) { this.TryTravel(DirCardinal.Right); }

			// Activate Node
			else if(playerInput.isPressed(IKey.AButton) == true) {
				// TODO: ACTIVATE NODE HERE
			}
		}

		public void TryTravel( DirCardinal dir = DirCardinal.Center, bool leaveSpot = false ) {

			// Can only move if the character is at a Node.
			if(this.character.IsAtNode) { return; }

			// Get Current Node
			NodeData curNode = this.GetNode(this.campaign.currentNodeId);

			// AUTO-TRAVEL : Attempt to automatically determine a direction when one is not provided.
			if(dir == DirCardinal.Center) {

				// Check for Auto-Warps (to new World Zones)
				if(curNode.type >= NodeType.Warp && curNode.warp > 0) {

					// If we're supposed to leave the warp (after an arrival)
					if(!leaveSpot) {
						dir = this.GetAutoDir(curNode, 0);
					} else {
						this.ActivateWarp(curNode.zone, curNode.warp);
						return;
					}
				}

				// Check for auto-move travel nodes:
				else {
					if(curNode.type != NodeType.TravelMove && curNode.type != NodeType.TravelPoint) { return; }

					// Identify the Remaining Travel Direction(s):
					dir = this.GetAutoDir(curNode, this.campaign.lastNodeId);
				}
			}

			// Make sure the direction indicated is a valid option:
			ushort nextId = curNode.GetIdOfNodeDir(dir);
			if(nextId == 0) { return; }

			// Get Last Node
			NodeData nextNode = this.GetNode(nextId);
			if(nextNode == null) { return; }

			// If the current level / node hasn't been completed, prevent movement if an open path hasn't been declared.
			// However, you can always move to the last node ID you came from.
			if(curNode.type < NodeType.TravelPoint && !this.campaign.IsLevelWon(this.campaign.currentNodeId)) {
				if(nextId != campaign.lastNodeId) { return; }
			}

			// Have Character Travel Path
			this.campaign.SetNode(nextId, this.campaign.currentNodeId);
			this.character.TravelPath(nextNode);
		}

		public DirCardinal GetAutoDir( NodeData node, ushort lastNodeId = 0, byte countReq = 1 ) {
			byte count = 0;

			// Identify the remaining travel direction(s):
			DirCardinal lastDir = lastNodeId > 0 ? this.FindDirToNode(node, lastNodeId) : DirCardinal.None;

			DirCardinal dir = DirCardinal.None;
			if(node.Down > 0 && lastDir != DirCardinal.Down) { dir = DirCardinal.Down; count++; }
			if(node.Left > 0 && lastDir != DirCardinal.Left) { dir = DirCardinal.Left; count++; }
			if(node.Right > 0 && lastDir != DirCardinal.Right) { dir = DirCardinal.Right; count++; }
			if(node.Up > 0 && lastDir != DirCardinal.Up) { dir = DirCardinal.Up; count++; }

			return count != countReq ? DirCardinal.None : dir;
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

			byte startX = (byte)Math.Max((byte)0, (byte)cam.GridX);
			byte startY = (byte)Math.Max((byte)0, (byte)cam.GridY);

			byte gridX = (byte)(startX + 45 + 1); // 26 is view size. +1 is to render the edge.
			byte gridY = (byte)(startY + 26 + 1); // 25.5 is view size. +1 is to render the edge.

			if(gridX > this.xCount) { gridX = (byte)(this.xCount); } // Must limit to room size.
			if(gridY > this.yCount) { gridY = (byte)(this.yCount); } // Must limit to room size.

			// Camera Position
			int camX = cam.posX;
			int camY = cam.posY;

			byte[][][] curTiles = this.currentZone.tiles;

			// Loop through the zone tile data:
			for(byte y = gridY; y-- > startY;) {
				ushort tileYPos = (ushort)(y * (byte)WorldmapEnum.TileHeight - camY);

				for(byte x = gridX; x-- > startX;) {
					this.DrawWorldTile(curTiles[y][x], x * (byte)WorldmapEnum.TileWidth - camX, tileYPos);
				};
			}

			// Draw UI
			this.worldUI.Draw();
			Systems.worldConsole.Draw();
		}

		// NOTE: This is a duplicate of World Editor.
		public void DrawWorldTile(byte[] wtData, int posX, int posY) {

			// Draw Base, unless the top layer is identical.
			if(wtData[0] != 0 && wtData[0] != wtData[1]) {
				this.atlas.Draw(WorldTerrain[wtData[0]] + "/b1", posX, posY);
			}

			// Draw Top [1], [2]
			if(wtData[1] != 0) {
				this.atlas.Draw(WorldTerrain[wtData[1]] + "/" + WorldLayers[wtData[2]], posX, posY);
			}

			// Draw Cover [3], [4]
			if(wtData[3] != 0) {
				this.atlas.Draw(WorldTerrain[wtData[3]] + "/" + WorldLayers[wtData[4]], posX, posY);
			}

			// Draw Object Layer [5]
			if(wtData[5] != 0) {
				this.atlas.Draw("Objects/" + WorldObjects[wtData[4]], posX, posY);
			}
		}






		public NodeData GetNode(ushort nodeId) {
			return null;
			//return this.nodes.ContainsKey(nodeId) ? this.nodes[nodeId] : null;
		}

		public ushort GetNodeIdByGrid(byte gridX, byte gridY) {

			// TODO: FIX THIS ONCE WORLD FORMAT IS CORRECT.
			//if(!this.tiles.ContainsKey(gridY)) { return 0; }
			//if(!this.tiles[gridY].ContainsKey(gridX)) { return 0; }

			//return this.tiles[gridY][gridX].nodeId;
			return 0;
		}

		public NodeData GetZoneStartNode() {
			ushort startNodeId = this.FindZoneStartNodeId();
			return this.GetNode(startNodeId);
		}

		public ushort FindZoneStartNodeId() {
			//foreach(var result in this.nodes) {
			//	if(result.Value.start) {
			//		return result.Key;
			//	}
			//}
			return 0;
		}

		public ushort FindWarpDestinationNode(byte zoneId, ushort warpId) {
			var zones = Systems.handler.worldContent.data.zones;

			// Make sure the zone contains the Zone ID assigned from the warp.
			if(zones.Length < zoneId) { return 0; }

			WorldZoneFormat zone = zones[zoneId];
			var tiles = zone.tiles;

			// TODO: REST OF THIS
			// Loop through tiles:
			//foreach(var result in tiles) {
			//	var row = result.Value;

			//	foreach()
			//}

			//NodeData[] nodes = zone.

			//let find = zone !== null ? this.worldData.zones[zone].nodes : this.nodes;
			//for(let id in find) {
			//		let node = find[id];
			//		if(node.warp === warpId) { return parseInt(id); }
			//	}
			//	return null;
			return 0;
		}

		public DirCardinal FindDirToNode(NodeData startNode, ushort endId) {
			if(startNode.Up == endId) { return DirCardinal.Up; }
			if(startNode.Right == endId) { return DirCardinal.Right; }
			if(startNode.Down == endId) { return DirCardinal.Down; }
			if(startNode.Left == endId) { return DirCardinal.Left; }
			return DirCardinal.None;
		}



		// TODO: Do these when we need to edit the world.
		// TODO: Do these when we need to edit the world.

		// ----------------------------
		//   Editing Methods
		// ----------------------------

		public void AssignStartNode() {

		}
	}
}
