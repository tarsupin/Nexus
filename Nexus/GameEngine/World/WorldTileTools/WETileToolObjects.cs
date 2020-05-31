using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolObjects : WETileTool {

		public WETileToolObjects() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Standard;

			// Objects, Vegetation
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Brush },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Rose2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Rose3 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Lily2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Lily3 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Blur },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Stump },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Cactus },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.CactusSmall },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.CactusBig },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Tree1 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Tree2 },
			});

			// Objects, Ground
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.DirtSpot },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dune },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Drift },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Snowman1 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Snowman2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Bones },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Pit },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Dungeon },
			});

			// Objects, Residence
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House1 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House3 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House4 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House5 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House6 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House7 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House8 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House9 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.House10 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Town1 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Town2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Town3 },
			});

			// Objects, Structure
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Castle1 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Castle2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Castle3 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Castle4 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Castle5 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Tower1 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Tower2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Tower3 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Tower4 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Pyramid1 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Pyramid2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Pyramid3 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.Stadium },
			});

			// Object, Bridges
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StoneBridge2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StoneBridge4 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StoneBridge6 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StoneBridge8 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StoneBridgeH },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.StoneBridgeV },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.WoodBridge2 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.WoodBridge4 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.WoodBridge6 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.WoodBridge8 },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.WoodBridgeH },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.WoodBridgeV },
			});

			// Object, Nodes
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeStrict },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeCasual },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodePoint },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeMove },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeWarp },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeWon },
				new WEPlaceholder() { obj = (byte) OTerrainObjects.NodeStart },
			});
		}
	}
}
