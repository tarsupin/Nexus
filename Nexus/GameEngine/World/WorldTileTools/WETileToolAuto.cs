using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolAuto : WETileTool {

		public WETileToolAuto() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Standard;

			// Auto Terrain
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

			// Miscellaneous Terrain
			this.placeholders.Add(new WEPlaceholder[] {
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
		}
	}
}
