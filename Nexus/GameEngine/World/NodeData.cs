using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public static class NodeData {

		// ----------------------- //
		// --- Level Detection --- //
		// ----------------------- //
		
		public static bool IsLevelValid( string levelId ) {

			// Make sure a Level ID is provided.
			if(levelId.Length == 0) { return false; }

			// Check if Level exists in file system.
			if(LevelContent.LevelExists(levelId)) { return true; }

			// If it doesn't exist in the file system, check if it's online:
			// TODO URGENT: Download Levels from Online.

			return false;
		}

		// ---------------------- //
		// --- Node Detection --- //
		// ---------------------- //

		public static bool IsNodeAtLocation(WorldContent worldContent, WorldZoneFormat zone, byte gridX, byte gridY, bool dotsCount = true, bool invisibleDotsCount = true, bool playableOnly = false) {

			// Check if a node is located here:
			byte[] wtData = worldContent.GetWorldTileData(zone, (byte)gridX, (byte)gridY);

			// If a node is not located here, continue.
			return NodeData.IsObjectANode(wtData[5], dotsCount, invisibleDotsCount, playableOnly);
		}

		public static byte GetNodeAtLocation(WorldContent worldContent, WorldZoneFormat zone, byte gridX, byte gridY, bool dotsCount = true, bool invisibleDotsCount = true, bool playableOnly = false) {

			// Check if a node is located here:
			byte[] wtData = worldContent.GetWorldTileData(zone, (byte)gridX, (byte)gridY);

			// If a node is not located here, continue.
			if(NodeData.IsObjectANode(wtData[5], dotsCount, invisibleDotsCount, playableOnly)) {
				return wtData[5];
			}

			return 0;
		}

		public static bool IsObjectANode(byte objectId, bool dotsCount = true, bool invisibleDotsCount = true, bool playableOnly = false) {

			// Check for Playable Nodes
			switch(objectId) {
				case (byte)OTerrainObjects.NodeStrict:
				case (byte)OTerrainObjects.NodeCasual:
				case (byte)OTerrainObjects.NodeWon:
					return true;
			}

			if(playableOnly) { return false; }
			
			if(dotsCount && NodeData.IsObjectADot(objectId, invisibleDotsCount)) { return true; }

			// Check for Non-Playable Nodes
			switch(objectId) {
				case (byte)OTerrainObjects.NodePoint:
				case (byte)OTerrainObjects.NodeMove:
				case (byte)OTerrainObjects.NodeWarp:
					return true;
			}

			return false;
		}

		public static bool IsObjectABlockingNode( byte objectId ) {
			return objectId == (byte)OTerrainObjects.NodeStrict || objectId == (byte)OTerrainObjects.NodeCasual;
		}

		public static bool IsObjectAnAutoTravelDot( byte objectId ) {
			return objectId >= (byte)OTerrainObjects.Dot_UL && objectId <= (byte)OTerrainObjects.Dot_RD;
		}

		public static bool IsObjectADot(byte objectId, bool invisibleDotsCount = true) {
			
			if(invisibleDotsCount) {
				if(IsObjectAnAutoTravelDot(objectId)) { return true; }
			}

			return objectId >= (byte)OTerrainObjects.Dot_All && objectId <= (byte)OTerrainObjects.Dot_LRD;
		}

		public static (bool up, bool left, bool right, bool down) GetDotDirections(byte objectId) {
			if(objectId == (byte)OTerrainObjects.Dot_All) { return (up: true, left: true, right: true, down: true); }
			if(objectId == (byte)OTerrainObjects.Dot_ULR) { return (up: true, left: true, right: true, down: false); }
			if(objectId == (byte)OTerrainObjects.Dot_ULD) { return (up: true, left: true, right: false, down: true); }
			if(objectId == (byte)OTerrainObjects.Dot_URD) { return (up: true, left: false, right: true, down: true); }
			if(objectId == (byte)OTerrainObjects.Dot_LRD) { return (up: false, left: true, right: true, down: true); }
			if(objectId == (byte)OTerrainObjects.Dot_UL) { return (up: true, left: true, right: false, down: false); }
			if(objectId == (byte)OTerrainObjects.Dot_UR) { return (up: true, left: false, right: true, down: false); }
			if(objectId == (byte)OTerrainObjects.Dot_UD) { return (up: true, left: false, right: false, down: true); }
			if(objectId == (byte)OTerrainObjects.Dot_LR) { return (up: false, left: true, right: true, down: false); }
			if(objectId == (byte)OTerrainObjects.Dot_LD) { return (up: false, left: true, right: false, down: true); }
			if(objectId == (byte)OTerrainObjects.Dot_RD) { return (up: false, left: false, right: true, down: true); }
			return (up: true, left: true, right: true, down: true);
		}

		public static bool IsDirectionAllowed(byte objectId, DirCardinal dir) {

			// All Level Nodes can move in all directions.
			if(NodeData.IsObjectANode(objectId, false)) { return true; }

			// Dots may or may not have direction allowance:
			if(NodeData.IsObjectADot(objectId)) {

				var dirsAllowed = NodeData.GetDotDirections(objectId);

				if(dir == DirCardinal.Up) { return dirsAllowed.up; }
				if(dir == DirCardinal.Down) { return dirsAllowed.down; }
				if(dir == DirCardinal.Left) { return dirsAllowed.left; }
				if(dir == DirCardinal.Right) { return dirsAllowed.right; }
			}

			return false;
		}

		public static (byte objectId, byte gridX, byte gridY) LocateNodeConnection(WorldContent worldContent, WorldZoneFormat zone, byte gridX, byte gridY, DirCardinal dir) {

			// Is the node allowed to connect? If not, return early.
			var baseNode = NodeData.LocateNearestNode(worldContent, zone, gridX, gridY, dir);

			if(dir == DirCardinal.Up) {
				if(!NodeData.IsDirectionAllowed(baseNode.objectId, DirCardinal.Down)) { return (0, 0, 0); }
				if(baseNode.objectId > 0) {
					var revNode = NodeData.LocateNearestNode(worldContent, zone, baseNode.gridX, baseNode.gridY, DirCardinal.Down);

					// If the discovered node has no alternative to connect to, it matches with this one.
					if(revNode.objectId == 0) { return baseNode; }

					// If alternative node is on the same X level, it matches this one.
					if(baseNode.gridX == gridX) { return baseNode; }

					// If alternative node is further away than this one, it matches this one.
					if(revNode.gridY > gridY) { return baseNode; }
				}
			}

			else if(dir == DirCardinal.Left) {
				if(!NodeData.IsDirectionAllowed(baseNode.objectId, DirCardinal.Right)) { return (0, 0, 0); }
				if(baseNode.objectId > 0) {
					var revNode = NodeData.LocateNearestNode(worldContent, zone, baseNode.gridX, baseNode.gridY, DirCardinal.Right);

					// If the discovered node has no alternative to connect to, it matches with this one.
					if(revNode.objectId == 0) { return baseNode; }

					// If alternative node is on the same Y level, it matches this one.
					if(baseNode.gridY == gridY) { return baseNode; }

					// If alternative node is further away than this one, it matches this one.
					if(revNode.gridX > gridX) { return baseNode; }
				}
			}

			else if(dir == DirCardinal.Right) {
				if(!NodeData.IsDirectionAllowed(baseNode.objectId, DirCardinal.Left)) { return (0, 0, 0); }
				if(baseNode.objectId > 0) {
					var revNode = NodeData.LocateNearestNode(worldContent, zone, baseNode.gridX, baseNode.gridY, DirCardinal.Left);

					// If the discovered node has no alternative to connect to, it matches with this one.
					if(revNode.objectId == 0) { return baseNode; }

					// If alternative node is on the same Y level, it matches this one.
					if(baseNode.gridY == gridY) { return baseNode; }

					// If alternative node is further away than this one, it matches this one.
					if(revNode.gridX < gridX) { return baseNode; }
				}
			}

			else if(dir == DirCardinal.Down) {
				if(!NodeData.IsDirectionAllowed(baseNode.objectId, DirCardinal.Up)) { return (0, 0, 0); }
				if(baseNode.objectId > 0) {
					var revNode = NodeData.LocateNearestNode(worldContent, zone, baseNode.gridX, baseNode.gridY, DirCardinal.Up);

					// If the discovered node has no alternative to connect to, it matches with this one.
					if(revNode.objectId == 0) { return baseNode; }

					// If alternative node is on the same X level, it matches this one.
					if(baseNode.gridX == gridX) { return baseNode; }

					// If alternative node is further away than this one, it matches this one.
					if(revNode.gridY < gridY) { return baseNode; }
				}
			}

			return (0, 0, 0);
		}

		public static (byte objectId, byte gridX, byte gridY) LocateNearestNode(WorldContent worldContent, WorldZoneFormat zone, byte gridX, byte gridY, DirCardinal dir, byte range = 5) {
			(byte objectId, byte gridX, byte gridY) tuple = (objectId: 0, gridX: 0, gridY: 0);

			// Vertical UP Node Scan - Scans for any nodes above this location.
			if(dir == DirCardinal.Up) {
				byte xRange = 1;

				// Do a first scan for vertical line:
				for(int y = gridY - 1; y >= gridY - range; y--) {
					byte objectId = NodeData.GetNodeAtLocation(worldContent, zone, gridX, (byte)y);
					if(objectId == 0) { continue; }
					return (objectId, gridX, (byte) y);
				}

				// Do a scan for angled versions:
				for(int y = gridY - 1; y >= gridY - range; y--) {
					for(int x = gridX - xRange; x <= gridX + xRange; x++) {

						// If a node is located, track it, and make sure it's more centered than any other by looping through X values again.
						byte objectId = NodeData.GetNodeAtLocation(worldContent, zone, (byte)x, (byte)y);
						if(objectId == 0) { continue; }

						// Keep the node closest to center:
						if(tuple.objectId != 0) {
							int dist1 = Math.Abs(gridX - tuple.gridX);
							int dist2 = Math.Abs(gridX - x);
							if(dist1 < dist2) { continue; } else { tuple.gridX = (byte)x; tuple.gridY = (byte)y; }
						} else {
							tuple.objectId = objectId;
							tuple.gridX = (byte)x;
							tuple.gridY = (byte)y;
						}
					}
					xRange++;

					// If a node is located, end test.
					if(tuple.objectId > 0) { return tuple; }
				}
			}

			// Vertical DOWN Node Scan - Scans for any nodes below this location.
			else if(dir == DirCardinal.Down) {
				byte xRange = 1;

				// Do a first scan for vertical line:
				for(int y = gridY + 1; y <= gridY + range; y++) {
					byte objectId = NodeData.GetNodeAtLocation(worldContent, zone, gridX, (byte)y);
					if(objectId == 0) { continue; }
					return (objectId, gridX, (byte)y);
				}

				// Do a scan for angled versions:
				for(int y = gridY + 1; y <= gridY + range; y++) {
					for(int x = gridX - xRange; x <= gridX + xRange; x++) {

						// If a node is located, track it, and make sure it's more centered than any other by looping through X values again.
						byte objectId = NodeData.GetNodeAtLocation(worldContent, zone, (byte)x, (byte)y);
						if(objectId == 0) { continue; }

						// Keep the node closest to center:
						if(tuple.objectId != 0) {
							int dist1 = Math.Abs(gridX - tuple.gridX);
							int dist2 = Math.Abs(gridX - x);
							if(dist1 < dist2) { continue; } else { tuple.gridX = (byte)x; tuple.gridY = (byte)y; }
						} else {
							tuple.objectId = objectId;
							tuple.gridX = (byte)x;
							tuple.gridY = (byte)y;
						}
					}
					xRange++;

					// If a node is located, end test.
					if(tuple.objectId > 0) { return tuple; }
				}
			}

			// Horizontal LEFT Node Scan - scans for any nodes left of this location.
			else if(dir == DirCardinal.Left) {
				byte yRange = 0;

				// Do a first scan for horizontal line:
				for(int x = gridX - 1; x >= gridX - range; x--) {
					byte objectId = NodeData.GetNodeAtLocation(worldContent, zone, (byte)x, gridY);
					if(objectId == 0) { continue; }
					return (objectId, (byte)x, gridY);
				}

				// Do a scan for angled versions:
				for(int x = gridX - 1; x >= gridX - range; x--) {
					for(int y = gridY - yRange; y <= gridY + yRange; y++) {

						// If a node was located, track it, then make sure it's more centered than any other by looping through Y values again.
						byte objectId = NodeData.GetNodeAtLocation(worldContent, zone, (byte)x, (byte)y);
						if(objectId == 0) { continue; }

						// Keep the node closest to center:
						if(tuple.objectId != 0) {
							int dist1 = Math.Abs(gridY - tuple.gridY);
							int dist2 = Math.Abs(gridY - y);
							if(dist1 < dist2) { continue; } else { tuple.gridY = (byte)y; tuple.gridX = (byte)x; }
						} else {
							tuple.objectId = objectId;
							tuple.gridX = (byte)x;
							tuple.gridY = (byte)y;
						}
					}
					yRange++;

					// If a node is located, end test.
					if(tuple.objectId > 0) { return tuple; }
				}
			}

			// Horizontal RIGHT Node Scan - scans for any nodes right of this location.
			else if(dir == DirCardinal.Right) {
				byte yRange = 0;

				// Do a first scan for horizontal line:
				for(int x = gridX + 1; x <= gridX + range; x++) {
					byte objectId = NodeData.GetNodeAtLocation(worldContent, zone, (byte)x, gridY);
					if(objectId == 0) { continue; }
					return (objectId, (byte)x, gridY);
				}

				// Do a scan for angled versions:
				for(int x = gridX + 1; x <= gridX + range; x++) {
					for(int y = gridY - yRange; y <= gridY + yRange; y++) {

						// If a node was located, track it, then make sure it's more centered than any other by looping through Y values again.
						byte objectId = NodeData.GetNodeAtLocation(worldContent, zone, (byte)x, (byte)y);
						if(objectId == 0) { continue; }

						// Keep the node closest to center:
						if(tuple.objectId != 0) {
							int dist1 = Math.Abs(gridY - tuple.gridY);
							int dist2 = Math.Abs(gridY - y);
							if(dist1 < dist2) { continue; } else { tuple.gridY = (byte)y; tuple.gridX = (byte)x; }
						} else {
							tuple.objectId = objectId;
							tuple.gridX = (byte)x;
							tuple.gridY = (byte)y;
						}
					}
					yRange++;

					// If a node is located, end test.
					if(tuple.objectId > 0) { return tuple; }
				}
			}

			// Return No Results
			return tuple;
		}

		// ---------------------- //
		// --- Tile Detection --- //
		// ---------------------- //

		public static DirCardinal RelativeDirectionOfTiles(sbyte relX, sbyte relY) {

			if(relX < 0) {
				if(relY < 0 && relY <= relX) { return DirCardinal.Up; }             // ex: -2, -3
				if(relY > 0 && relY >= 0 - relX) { return DirCardinal.Down; }       // ex: -2, 3
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

		public static void DrawDirectionTiles(byte gridX, byte gridY, byte range = 6, bool up = false, bool left = false, bool right = false, bool down = false) {
			for(int y = gridY - range; y < gridY + range + 1; y++) {
				for(int x = gridX - range; x < gridX + range + 1; x++) {

					DirCardinal dir = NodeData.RelativeDirectionOfTiles((sbyte)(x - gridX), (sbyte)(y - gridY));

					if((dir == DirCardinal.Up && up) || (dir == DirCardinal.Down && down)) {
						Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(x * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, y * (byte)WorldmapEnum.TileHeight - Systems.camera.posY, (byte)WorldmapEnum.TileWidth, (byte)WorldmapEnum.TileHeight), Color.White * 0.35f);
					} else if((dir == DirCardinal.Left && left) || (dir == DirCardinal.Right && right)) {
						Systems.spriteBatch.Draw(Systems.tex2dDarkGreen, new Rectangle(x * (byte)WorldmapEnum.TileWidth - Systems.camera.posX, y * (byte)WorldmapEnum.TileHeight - Systems.camera.posY, (byte)WorldmapEnum.TileWidth, (byte)WorldmapEnum.TileHeight), Color.White * 0.35f);
					}
				}
			}
		}

	}
}
