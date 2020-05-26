using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldZone {

		// Essential Data
		public WorldScene scene;
		public byte zoneId;
		public Dictionary<ushort, NodeData> nodes;
		//public Dictionary<ushort, Dictionary<ushort, WorldTileData>> tiles;
		public WorldTileFormat tiles;

		public byte gridWidth = (byte) WorldmapEnum.MinWidth;
		public byte gridHeight = (byte) WorldmapEnum.MinHeight;

		public WorldZone(byte zoneId) {

		}

		public NodeData GetNode( ushort nodeId ) {
			return this.nodes.ContainsKey(nodeId) ? this.nodes[nodeId] : null;
		}

		public ushort GetNodeIdByGrid( byte gridX, byte gridY ) {

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
			foreach(var result in this.nodes) {
				if(result.Value.start) {
					return result.Key;
				}
			}
			return 0;
		}

		public ushort FindWarpDestinationNode(byte zoneId, ushort warpId) {
			var zones = Systems.handler.worldContent.data.zones;
			
			// Make sure the zone contains the Zone ID assigned from the warp.
			if(!zones.ContainsKey(zoneId.ToString())) { return 0; }

			WorldZoneFormat zone = zones[zoneId.ToString()];
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

		public DirCardinal FindDirToNode( NodeData startNode, ushort endId ) {
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
