using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldScene : Scene {

		// TODO: The IDs here match OTerrain exactly, so this whole thing seems like it's useless.
		// TODO: Remove this when we can.
		public static Dictionary<byte, object> OverTerrain = new Dictionary<byte, object> {
			{ 1, new RandTypeCur(OTerrain.Grass, "g") },
			{ 2, new RandTypeCur(OTerrain.Desert, "d") },
			{ 3, new RandTypeCur(OTerrain.Snow, "s") },
			{ 4, new RandTypeCur(OTerrain.Water, "w") },
			{ 5, new RandTypeCur(OTerrain.Water, "w") },
			{ 6, new RandTypeCur(OTerrain.Mud, "u") },
			{ 7, new RandTypeCur(OTerrain.Dirt, "d") },
			{ 8, new RandTypeCur(OTerrain.Cobble, "c") },
			{ 9, new RandTypeCur(OTerrain.Road, "r") },
			{ 10, new RandTypeCur(OTerrain.Ice, "i") },
			{ 11, new RandTypeCur(OTerrain.GrassDeep, "x") },
		};

		// References
		public readonly WorldUI worldUI;
		public readonly PlayerInput playerInput;
		public WorldChar character;
		public CampaignState campaign;
		public Atlas atlas;

		// World Metadata
		public string id = "";
		public string name = "";
		public string description = "";
		public string author = "";
		public ushort version = 0;
		public byte score = 0;

		// Access to World Data
		public WorldFormat worldData;

		// Zones
		//public Dictionary<ushort, WorldZone> zones;			// TODO: REMOVE

		// Grid Limits
		// TODO: UPDATE THESE VALUES
		public byte xCount = 20;
		public byte yCount = 20;

		public WorldScene() : base() {

			// Prepare Components
			this.worldUI = new WorldUI(this);
			this.character = new WorldChar(this);
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.campaign = Systems.handler.campaignState;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];

			// Prepare World Content
			this.worldData = Systems.handler.worldContent.data;

			// Camera Update
			Systems.camera.UpdateScene(this);
		}

		public WorldZoneFormat currentZone { get { return this.worldData.zones[this.campaign.zoneId]; } }

		public override void StartScene() {

			// TODO: Make sure that world data is available.
			//if(this.data) {
			//	throw new Exception("Unable to load world. No world data available.");
			//}

			// Assign Default Zone
			//if(Systems.campaign.zone != null) {
			//	Systems.campaign.zone = 0;
			//}

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

			byte gridX = (byte)(startX + 29 + 1 + 1); // 29 is view size. +1 is to render the edge. +1 is to deal with --> operator in loop.
			byte gridY = (byte)(startY + 18 + 1 + 1); // 18 is view size. +1 is to render the edge. +1 is to deal with --> operator in loop.

			if(gridX > this.xCount) { gridX = (byte)(this.xCount + 1); } // Must limit to room size. +1 is to deal with --> operator in loop.
			if(gridY > this.yCount) { gridY = (byte)(this.yCount + 1); } // Must limit to room size. +1 is to deal with --> operator in loop.

			// Camera Position
			int camX = cam.posX;
			int camY = cam.posY;

			// TODO: REMOVE. THIS IS TEMPORARY.
			// TODO: REMOVE. THIS IS TEMPORARY.
			// TODO: REMOVE. THIS IS TEMPORARY.
			gridX = 40;
			gridY = 40;

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
			this.worldUI.Draw();
			Systems.worldConsole.Draw();
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
