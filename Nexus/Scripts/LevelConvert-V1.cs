using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Scripts {

	class LevelConvertV1 : LevelConvert {

		public LevelConvertV1() : base() {

		}

		protected override void ProcessLayerData(Dictionary<string, Dictionary<string, ArrayList>> layerJson) {

			// Temporary Blockers - Testing Purposes.
			if(LevelConvert.curLevelId != "QCALQOD13") { return; }          // Specific Level Allowance
			//if(CalcRandom.IntBetween(0, 50) == 35) { return; }

			// Run Standard Layer Data Process
			base.ProcessLayerData(layerJson);
		}

		protected override void ProcessTileData( ArrayList tileJson ) {
			// tileJson[0], tileJson[1], tileJson[2]
			System.Console.WriteLine(tileJson[0] + ", " + tileJson[1]);
			//System.Console.WriteLine(tileJson[0] + ", " + tileJson[1] + ", " + tileJson[2]);
			this.ConvertGrassToMud(tileJson);
		}

		protected void ConvertGrassToMud(ArrayList tileJson) {

			// If the tile is Grass
			if(Byte.Parse(tileJson[0].ToString()) == 1) {

				// Convert the tile to Mud
				tileJson[0] = (byte)TileEnum.GroundMud;

				this.OverwriteTileData(tileJson);
			}
		}
	}
}
