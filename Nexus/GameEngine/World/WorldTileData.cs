using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldTileData {

		public OTerrain tile;           // Grass = 1, Desert = 2, etc..			// Saved
		public string tileFrame;

		public OTerrain layer;          // Grass = 1, Desert = 2, etc...		// Saved
		public string layerFrame;

		public string objectFrame;												// Saved
		public ushort nodeId = 0;		// Node ID at this Tile.				// Saved
	}
}
