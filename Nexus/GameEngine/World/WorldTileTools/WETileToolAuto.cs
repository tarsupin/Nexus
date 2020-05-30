using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolAuto : WETileTool {

		public WETileToolAuto() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Standard;

			// Miscellaneous Terrain
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Coast,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.MountainBrown,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.MountainGray,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.MountainIce,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.TreesPine,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.TreesPineSnow,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.TreesPalm,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.TreesOak,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.TreesOakSnow,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.FieldGrass,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.FieldHay,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.FieldSnow,
				},
			});

			// Miscellaneous Terrain
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

		}
	}
}
