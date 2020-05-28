using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WEAutoTiles : WETileTool {

		public WEAutoTiles() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Standard;
			
			// Bases / Terrain
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Water,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.WaterShallow,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Mud,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Dirt,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Cobble,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Road,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Ice,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.DirtDark,
				},
			});

			// Grass Terrain (Trees, Mountains, etc.)
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Grass,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Grass,
					tCat = (byte) OTerrainCat.Trees,
					tLayer = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Grass,
					tCat = (byte) OTerrainCat.Mountains,
					tLayer = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Grass,
					tCat = (byte) OTerrainCat.Field,
					tLayer = (byte) OLayer.s5,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Grass,
					tCat = (byte) OTerrainCat.Veg,
					tLayer = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Grass,
					tCat = (byte) OTerrainCat.Water,
					tLayer = (byte) OLayer.c5,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Grass,
					tCat = (byte) OTerrainCat.Field2,
					tLayer = (byte) OLayer.s5,
				},
			});

			// Desert Terrain (Trees, Mountains, etc.)
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Desert,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Desert,
					tCat = (byte) OTerrainCat.Mountains,
					tLayer = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Desert,
					tCat = (byte) OTerrainCat.Veg,
					tLayer = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Desert,
					tCat = (byte) OTerrainCat.Water,
					tLayer = (byte) OLayer.c5,
				},
			});

			// Snow Terrain (Trees, Mountains, etc.)
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
					tCat = (byte) OTerrainCat.Trees,
					tLayer = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
					tCat = (byte) OTerrainCat.Mountains,
					tLayer = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
					tCat = (byte) OTerrainCat.Field,
					tLayer = (byte) OLayer.s5,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
					tCat = (byte) OTerrainCat.Veg,
					tLayer = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
					tCat = (byte) OTerrainCat.Water,
					tLayer = (byte) OLayer.c5,
				},
			});

			// Object, Nodes
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.NodeStrict },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.NodeCasual },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.NodePoint },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.NodeMove },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.NodeWarp },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.NodeWon },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.NodeStart },
			});

			// Objects, Ground
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Bones },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Cactus },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Stump },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Snowman1 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Snowman2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Tree1 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Tree2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Pit },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Dungeon },
			});

			// Objects, Residence
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House1 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House3 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House4 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House5 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House6 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House7 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House8 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House9 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.House10 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Town1 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Town2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Town3 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Tent },
			});

			// Objects, Structure
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Castle1 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Castle2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Castle3 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Castle4 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Castle5 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Tower1 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Tower2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Tower3 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Tower4 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Pyramid1 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Pyramid2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Pyramid3 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.Stadium },
			});

			// Object, Bridges
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.StoneBridge2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.StoneBridge4 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.StoneBridge6 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.StoneBridge8 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.StoneBridgeH },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.StoneBridgeV },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.WoodBridge2 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.WoodBridge4 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.WoodBridge6 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.WoodBridge8 },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.WoodBridgeH },
				new WEPlaceholder() { tObj = (byte) OTerrainObjects.WoodBridgeV },
			});
			
		}
	}
}
