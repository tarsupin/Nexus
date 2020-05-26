using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class NodeData {

		public NodeType type;			// LevelStrict, LevelCasual, TravelPoint, TravelMove, Warp, Warp Auto
		public bool start = false;		// If `true`, this is where the character starts.
		public string level;			// The ID of the level to play (when the node is activated).

		public ushort gridX;
		public ushort gridY;

		public ushort Left = 0;			// Node ID
		public ushort Right = 0;		// Node ID
		public ushort Up = 0;			// Node ID
		public ushort Down = 0;			// Node ID

		public byte zone = 0;			// The Zone # to warp to (applies to warps).
		public ushort warp = 0;			// The ID of the Warp to warp to. (1 to 4).			// TODO: Change to Node ID?

		public NodeData( NodeType type, ushort gridX, ushort gridY ) {
			this.type = type;
			this.gridX = gridX;
			this.gridY = gridY;
		}

		public ushort GetIdOfNodeDir( DirCardinal dir ) {
			if(dir == DirCardinal.Up) { return this.Up; }
			if(dir == DirCardinal.Down) { return this.Down; }
			if(dir == DirCardinal.Left) { return this.Left; }
			if(dir == DirCardinal.Right) { return this.Right; }
			return 0;
		}
	}

}
