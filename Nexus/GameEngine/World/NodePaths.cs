using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public static class NodePath {

		public static bool IsNodeAtLocation(WorldContent worldContent, WorldZoneFormat zone, byte gridX, byte gridY, bool dotsCount = true, bool invisibleDotsCount = true) {

			// Check if a node is located here:
			byte[] wtData = worldContent.GetWorldTileData(zone, (byte)gridX, (byte)gridY);

			// If a node is not located here, continue.
			return NodePath.IsObjectANode(wtData[5]);
		}

		public static bool IsObjectANode(byte objectId, bool dotsCount = true, bool invisibleDotsCount = true) {
			if(dotsCount && NodePath.IsObjectADot(objectId, invisibleDotsCount)) { return true; }

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

		public static bool IsObjectADot(byte objectId, bool invisibleDotsCount = true) {
			
			if(invisibleDotsCount) {
				if(objectId >= (byte)OTerrainObjects.Dot_UL && objectId <= (byte)OTerrainObjects.Dot_RD) {
					return true;
				}
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

		public static (bool hasNode, byte gridX, byte gridY) LocateNodeConnection(WorldContent worldContent, WorldZoneFormat zone, byte gridX, byte gridY, DirCardinal dir) {

			if(dir == DirCardinal.Up) {
				var upNode = NodePath.LocateNearestNode(worldContent, zone, gridX, gridY, DirCardinal.Up);
				if(upNode.hasNode) {
					var revNode = NodePath.LocateNearestNode(worldContent, zone, upNode.gridX, upNode.gridY, DirCardinal.Down);

					// If the discovered node has no alternative to connect to, it matches with this one.
					if(!revNode.hasNode) { return upNode; }

					// If alternative node is on the same X level, it matches this one.
					if(upNode.gridX == gridX) { return upNode; }

					// If alternative node is further away than this one, it matches this one.
					if(revNode.gridY > gridY) { return upNode; }
				}
			}

			else if(dir == DirCardinal.Left) {
				var leftNode = NodePath.LocateNearestNode(worldContent, zone, gridX, gridY, DirCardinal.Left);
				if(leftNode.hasNode) {
					var revNode = NodePath.LocateNearestNode(worldContent, zone, leftNode.gridX, leftNode.gridY, DirCardinal.Right);

					// If the discovered node has no alternative to connect to, it matches with this one.
					if(!revNode.hasNode) { return leftNode; }

					// If alternative node is on the same Y level, it matches this one.
					if(leftNode.gridY == gridY) { return leftNode; }

					// If alternative node is further away than this one, it matches this one.
					if(revNode.gridX > gridX) { return leftNode; }
				}
			}

			else if(dir == DirCardinal.Right) {
				var rightNode = NodePath.LocateNearestNode(worldContent, zone, gridX, gridY, DirCardinal.Right);
				if(rightNode.hasNode) {
					var revNode = NodePath.LocateNearestNode(worldContent, zone, rightNode.gridX, rightNode.gridY, DirCardinal.Left);

					// If the discovered node has no alternative to connect to, it matches with this one.
					if(!revNode.hasNode) { return rightNode; }

					// If alternative node is on the same Y level, it matches this one.
					if(rightNode.gridY == gridY) { return rightNode; }

					// If alternative node is further away than this one, it matches this one.
					if(revNode.gridX < gridX) { return rightNode; }
				}
			}

			else if(dir == DirCardinal.Down) {
				var downNode = NodePath.LocateNearestNode(worldContent, zone, gridX, gridY, DirCardinal.Down);
				if(downNode.hasNode) {
					var revNode = NodePath.LocateNearestNode(worldContent, zone, downNode.gridX, downNode.gridY, DirCardinal.Up);

					// If the discovered node has no alternative to connect to, it matches with this one.
					if(!revNode.hasNode) { return downNode; }

					// If alternative node is on the same X level, it matches this one.
					if(downNode.gridX == gridX) { return downNode; }

					// If alternative node is further away than this one, it matches this one.
					if(revNode.gridY < gridY) { return downNode; }
				}
			}

			return (false, 0, 0);
		}

		public static (bool hasNode, byte gridX, byte gridY) LocateNearestNode(WorldContent worldContent, WorldZoneFormat zone, byte gridX, byte gridY, DirCardinal dir, byte range = 4) {
			(bool hasNode, byte gridX, byte gridY) tuple = (hasNode: false, gridX: 0, gridY: 0);

			// Vertical UP Node Scan - Scans for any nodes above this location.
			if(dir == DirCardinal.Up) {
				byte xRange = 1;

				// Do a first scan for vertical line:
				for(int y = gridY - 1; y >= gridY - range; y--) {
					if(!NodePath.IsNodeAtLocation(worldContent, zone, gridX, (byte)y)) { continue; }
					return (true, gridX, (byte) y);
				}

				// Do a scan for angled versions:
				for(int y = gridY - 1; y >= gridY - range; y--) {
					for(int x = gridX - xRange; x <= gridX + xRange; x++) {

						// If a node is located, track it, and make sure it's more centered than any other by looping through X values again.
						if(!NodePath.IsNodeAtLocation(worldContent, zone, (byte)x, (byte)y)) { continue; }

						// Keep the node closest to center:
						if(tuple.hasNode) {
							int dist1 = Math.Abs(gridX - tuple.gridX);
							int dist2 = Math.Abs(gridX - x);
							if(dist1 < dist2) { continue; } else { tuple.gridX = (byte)x; tuple.gridY = (byte)y; }
						} else {
							tuple.hasNode = true;
							tuple.gridX = (byte)x;
							tuple.gridY = (byte)y;
						}
					}
					xRange++;

					// If a node is located, end test.
					if(tuple.hasNode) { return tuple; }
				}
			}

			// Vertical DOWN Node Scan - Scans for any nodes below this location.
			else if(dir == DirCardinal.Down) {
				byte xRange = 1;

				// Do a first scan for vertical line:
				for(int y = gridY + 1; y <= gridY + range; y++) {
					if(!NodePath.IsNodeAtLocation(worldContent, zone, gridX, (byte)y)) { continue; }
					return (true, gridX, (byte) y);
				}

				// Do a scan for angled versions:
				for(int y = gridY + 1; y <= gridY + range; y++) {
					for(int x = gridX - xRange; x <= gridX + xRange; x++) {

						// If a node is located, track it, and make sure it's more centered than any other by looping through X values again.
						if(!NodePath.IsNodeAtLocation(worldContent, zone, (byte)x, (byte)y)) { continue; }

						// Keep the node closest to center:
						if(tuple.hasNode) {
							int dist1 = Math.Abs(gridX - tuple.gridX);
							int dist2 = Math.Abs(gridX - x);
							if(dist1 < dist2) { continue; } else { tuple.gridX = (byte)x; tuple.gridY = (byte)y; }
						} else {
							tuple.hasNode = true;
							tuple.gridX = (byte)x;
							tuple.gridY = (byte)y;
						}
					}
					xRange++;

					// If a node is located, end test.
					if(tuple.hasNode) { return tuple; }
				}
			}

			// Horizontal LEFT Node Scan - scans for any nodes left of this location.
			else if(dir == DirCardinal.Left) {
				byte yRange = 0;

				// Do a first scan for horizontal line:
				for(int x = gridX - 1; x >= gridX - range; x--) {
					if(!NodePath.IsNodeAtLocation(worldContent, zone, (byte) x, gridY)) { continue; }
					return (true, (byte) x, gridY);
				}

				// Do a scan for angled versions:
				for(int x = gridX - 1; x >= gridX - range; x--) {
					for(int y = gridY - yRange; y <= gridY + yRange; y++) {

						// If a node was located, track it, then make sure it's more centered than any other by looping through Y values again.
						if(!NodePath.IsNodeAtLocation(worldContent, zone, (byte)x, (byte)y)) { continue; }

						// Keep the node closest to center:
						if(tuple.hasNode) {
							int dist1 = Math.Abs(gridY - tuple.gridY);
							int dist2 = Math.Abs(gridY - y);
							if(dist1 < dist2) { continue; } else { tuple.gridY = (byte)y; tuple.gridX = (byte)x; }
						} else {
							tuple.hasNode = true;
							tuple.gridX = (byte)x;
							tuple.gridY = (byte)y;
						}
					}
					yRange++;

					// If a node is located, end test.
					if(tuple.hasNode) { return tuple; }
				}
			}

			// Horizontal RIGHT Node Scan - scans for any nodes right of this location.
			else if(dir == DirCardinal.Right) {
				byte yRange = 0;

				// Do a first scan for horizontal line:
				for(int x = gridX + 1; x <= gridX + range; x++) {
					if(!NodePath.IsNodeAtLocation(worldContent, zone, (byte) x, gridY)) { continue; }
					return (true, (byte) x, gridY);
				}

				// Do a scan for angled versions:
				for(int x = gridX + 1; x <= gridX + range; x++) {
					for(int y = gridY - yRange; y <= gridY + yRange; y++) {

						// If a node was located, track it, then make sure it's more centered than any other by looping through Y values again.
						if(!NodePath.IsNodeAtLocation(worldContent, zone, (byte)x, (byte)y)) { continue; }

						// Keep the node closest to center:
						if(tuple.hasNode) {
							int dist1 = Math.Abs(gridY - tuple.gridY);
							int dist2 = Math.Abs(gridY - y);
							if(dist1 < dist2) { continue; } else { tuple.gridY = (byte)y; tuple.gridX = (byte)x; }
						} else {
							tuple.hasNode = true;
							tuple.gridX = (byte)x;
							tuple.gridY = (byte)y;
						}
					}
					yRange++;

					// If a node is located, end test.
					if(tuple.hasNode) { return tuple; }
				}
			}

			// Return No Results
			return tuple;
		}

		public static void DrawDirectionTiles(byte gridX, byte gridY, byte range = 6, bool up = false, bool left = false, bool right = false, bool down = false) {
			for(int y = gridY - range; y < gridY + range + 1; y++) {
				for(int x = gridX - range; x < gridX + range + 1; x++) {

					DirCardinal dir = NodePath.RelativeDirectionOfTiles((sbyte)(x - gridX), (sbyte)(y - gridY));

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
