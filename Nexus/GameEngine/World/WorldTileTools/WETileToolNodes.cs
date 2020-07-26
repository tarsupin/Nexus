using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolNodes : WETileTool {

		// WARNING: DO NOT CHANGE THIS FILE WITHOUT THE FOLLOWING:
		// WARNING: DO NOT CHANGE THIS FILE WITHOUT THE FOLLOWING:
		// WARNING: DO NOT CHANGE THIS FILE WITHOUT THE FOLLOWING:
		// The "TutorialWorldEdit.cs" file relies on the index of this.placeholders being in specific positions for Level Nodes and Characters.
		//		e.g. WETools.WETileTool.index == 0
		// So either add a const value that can track this information, or keep them consistent.

		public WETileToolNodes() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Nodes;

			// Object, Nodes
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeStrict },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeCasual },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeWarp },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeWon },
			});

			// Travel Forks
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_All },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_ULR },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_ULD },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_URD },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_LRD },
			});

			// Auto-Travel Dots, Two Directions
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_UD },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_LR },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_UR },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_RD },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_LD },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dot_UL },
			});

			// Character Start
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StartRyu },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StartPoo },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StartCarl },
			});
		}
	}
}
