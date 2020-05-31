using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolCoverage : WETileTool {

		public WETileToolCoverage() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Coverage;

			// Mountains
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
			});

			// Trees
			this.placeholders.Add(new WEPlaceholder[] {
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
			});

			// Fields
			this.placeholders.Add(new WEPlaceholder[] {
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
