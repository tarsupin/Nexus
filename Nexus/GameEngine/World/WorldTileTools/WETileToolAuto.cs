using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolAuto : WETileTool {

		public WETileToolAuto() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Standard;

			// Miscellaneous Terrain
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.Coast,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.MountainBrown,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.MountainGray,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.MountainIce,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.TreesPine,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.TreesPineSnow,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.TreesPalm,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.TreesOak,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.TreesOakSnow,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.FieldGrass,
					coverLay = (byte) OLayer.s5,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.FieldHay,
					coverLay = (byte) OLayer.s5,
				},
				new WEPlaceholder() {
					auto = true,
					cover = (byte) OTerrain.FieldSnow,
					coverLay = (byte) OLayer.s5,
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
