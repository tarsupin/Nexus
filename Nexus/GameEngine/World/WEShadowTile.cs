using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	// This class is designed to data about World Tiles.
	public static class WEShadowTile {

		public static Dictionary<byte, string[]> HelpText = new Dictionary<byte, string[]> {
			
			// Nodes
			{ (byte) OTerrainObjects.NodeStrict, new string[2] { "Strict Level", "Cannot start this level with upgrades until beaten." } },
			{ (byte) OTerrainObjects.NodeCasual, new string[2] { "Casual Level", "Can begin this level with upgrades from other levels." } },
			{ (byte) OTerrainObjects.NodePoint, new string[2] { "Movement Node", "Can travel along this node, and connect it to levels." } },
			{ (byte) OTerrainObjects.NodeMove, new string[2] { "Movement Node", "Can travel along this node, and connect it to levels." } },
			{ (byte) OTerrainObjects.NodeWarp, new string[2] { "Warp Node", "Will move the player to a new zone when touched." } },
			{ (byte) OTerrainObjects.NodeWon, new string[2] { "Optional Level", "A level that is considered beaten. Won't obstruct movement." } },
			{ (byte) OTerrainObjects.NodeStart, new string[2] { "Starting Position", "The location the player starts. Can place on any level." } },

			// Movement Nodes
			{ (byte) OTerrainObjects.Dot_All, new string[2] { "Fork Node, All", "This node is for movement only. It forks in all four directions." } },

			{ (byte) OTerrainObjects.Dot_ULR, new string[2] { "Fork Node, Up-Left-Right", "This node is for movement only. It forks up, left, and right." } },
			{ (byte) OTerrainObjects.Dot_ULD, new string[2] { "Fork Node, Up-Left-Down", "This node is for movement only. It forks up, left, and down." } },
			{ (byte) OTerrainObjects.Dot_URD, new string[2] { "Fork Node, Up-Right-Down", "This node is for movement only. It forks up, right, and down." } },
			{ (byte) OTerrainObjects.Dot_LRD, new string[2] { "Fork Node, Left-Right-Down", "This node is for movement only. It forks left, right, and down." } },
			
			// Auto-Travel Dot
			{ (byte) OTerrainObjects.Dot_UD, new string[2] { "Auto-Travel Dot", "This node is invisible and auto-travels. Travel is up and down." } },
			{ (byte) OTerrainObjects.Dot_LR, new string[2] { "Auto-Travel Dot", "This node is invisible and auto-travels. Travel is left and right." } },
			{ (byte) OTerrainObjects.Dot_UL, new string[2] { "Auto-Travel Dot", "This node is invisible and auto-travels. Travel is up and left." } },
			{ (byte) OTerrainObjects.Dot_UR, new string[2] { "Auto-Travel Dot", "This node is invisible and auto-travels. Travel is up and right." } },
			{ (byte) OTerrainObjects.Dot_RD, new string[2] { "Auto-Travel Dot", "This node is invisible and auto-travels. Travel is right and down." } },
			{ (byte) OTerrainObjects.Dot_LD, new string[2] { "Auto-Travel Dot", "This node is invisible and auto-travels. Travel is left and down." } },
		};
	}
}
