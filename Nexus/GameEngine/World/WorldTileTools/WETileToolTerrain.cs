using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolTerrain : WETileTool {

		public WETileToolTerrain() : base() {

			this.slotGroup = (byte) WorldSlotGroup.Terrain;

			// Primary Terrain
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Grass,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Desert,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Water,
				},
			});

			// Secondary Terrain
			this.placeholders.Add(new WEPlaceholder[] {
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
					tBase = (byte) OTerrain.Ice,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.DirtDark,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.WaterShallow,
				},
			});

			// Constructed Terrain
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Cobble,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Road,
				},
			});
		}
	}
}
