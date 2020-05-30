using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolTerrain : WETileTool {

		public WETileToolTerrain() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Standard;

			// Grass Base
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.b2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.b3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.b4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.b5,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Desert,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.b2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.b3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.b4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.b5,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.b2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.b3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.b4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.b5,
				},
			});

			// Grass Standard, Sides
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s8,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.s9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.p1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.p3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.p7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.ph,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.pv,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.p9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.l1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.l3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.l7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.l9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.r1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.r3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.r7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.r9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.e2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.e4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.e5,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.e6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.e8,
				},
			});

			// Grass Corners
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c5,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c8,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.c9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.cl,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.cr,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.v1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.v3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.v7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Grass,
					topLay = (byte) OLayer.v9,
				},
			});

			// Desert Standard, Sides
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s8,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.s9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.p1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.p3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.p7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.ph,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.pv,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.p9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.l1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.l3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.l7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.l9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.r1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.r3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.r7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.r9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.e2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.e4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.e5,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.e6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.e8,
				},
			});

			// Desert Corners
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c5,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c8,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.c9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.cl,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.cr,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.v1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.v3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.v7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Desert,
					topLay = (byte) OLayer.v9,
				},
			});

			// Snow Standard, Sides
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s8,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.s9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.p1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.p3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.p7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.ph,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.pv,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.p9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.l1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.l3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.l7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.l9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.r1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.r3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.r7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.r9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.e2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.e4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.e5,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.e6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.e8,
				},
			});

			// Snow Corners
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c2,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c4,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c5,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c6,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c8,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.c9,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.cl,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.cr,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					topLay = (byte) OLayer.v1,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.v3,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.v7,
				},
				new WEPlaceholder() {
					top = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.v9,
				},
			});
			
		}
	}
}
