using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WETileToolTerrain : WETileTool {

		public WETileToolTerrain() : base() {

			this.slotGroup = (byte)WorldSlotGroup.Standard;

			// Grass Base
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.b1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.b2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.b3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.b4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.b5,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Desert,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.b2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.b3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.b4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.b5,
				},
				new WEPlaceholder() {
					auto = true,
					tBase = (byte) OTerrain.Snow,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.b2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.b3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.b4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.b5,
				},
			});

			// Grass Standard, Sides
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s8,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.s9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.p1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.p3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.p7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.ph,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.pv,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.p9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.l1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.l3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.l7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.l9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.r1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.r3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.r7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.r9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.e2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.e4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.e5,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.e6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.e8,
				},
			});

			// Grass Corners
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c5,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c8,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.c9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.cl,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.cr,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.v1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.v3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.v7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Grass,
					coverLay = (byte) OLayer.v9,
				},
			});

			// Desert Standard, Sides
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s8,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.s9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.p1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.p3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.p7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.ph,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.pv,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.p9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.l1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.l3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.l7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.l9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.r1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.r3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.r7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.r9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.e2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.e4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.e5,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.e6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.e8,
				},
			});

			// Desert Corners
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c5,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c8,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.c9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.cl,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.cr,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.v1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.v3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.v7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Desert,
					coverLay = (byte) OLayer.v9,
				},
			});

			// Snow Standard, Sides
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s8,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.s9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.p1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.p3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.p7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.ph,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.pv,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.p9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.l1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.l3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.l7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.l9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.r1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.r3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.r7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.r9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.e2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.e4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.e5,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.e6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.e8,
				},
			});

			// Snow Corners
			this.placeholders.Add(new WEPlaceholder[] {
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c2,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c4,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c5,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c6,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c8,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.c9,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.cl,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.cr,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.v1,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.v3,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.v7,
				},
				new WEPlaceholder() {
					tBase = (byte) OTerrain.Snow,
					coverLay = (byte) OLayer.v9,
				},
			});
			
		}
	}
}
