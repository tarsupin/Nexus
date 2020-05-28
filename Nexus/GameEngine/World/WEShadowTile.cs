using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	// This class is designed to data about World Tiles.
	public static class WEShadowTile {

		public static Dictionary<byte, string> HelpText = new Dictionary<byte, string> {
			
			// Nodes
			{ (byte) OTerrainObjects.NodeStrict, "Strict Level - Cannot start this level with upgrades until beaten." },
			{ (byte) OTerrainObjects.NodeCasual, "Casual Level - Can begin this level with upgrades from other levels." },
			{ (byte) OTerrainObjects.NodePoint, "Movement Node - Can travel along this node, and connect it to levels." },
			{ (byte) OTerrainObjects.NodeMove, "Movement Node - Can travel along this node, and connect it to levels." },
			{ (byte) OTerrainObjects.NodeWarp, "Warp Node - Will move the player to a new zone when touched." },
			{ (byte) OTerrainObjects.NodeWon, "Optional Level - A level that is considered beaten. Won't obstruct movement." },
			{ (byte) OTerrainObjects.NodeStart, "Starting Position - The location the player starts. Can place on any level." },

		};
	}
}
