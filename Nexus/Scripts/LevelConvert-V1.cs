using Nexus.Engine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Scripts {

	class LevelConvertV1 : LevelConvert {

		public LevelConvertV1() : base() {

		}

		protected override void ProcessLayerData(Dictionary<string, Dictionary<string, ArrayList>> layerJson) {

			// Temporary Blocker - Testing Purposes.
			if(CalcRandom.IntBetween(0, 50) == 35) { return; }

			// Run Standard Layer Data Process
			base.ProcessLayerData(layerJson);
		}

		protected override void ProcessTileData( ArrayList tileJson ) {
			// tileJson[0], tileJson[1], tileJson[2]
			System.Console.WriteLine(tileJson[0] + ", " + tileJson[1]);
			//System.Console.WriteLine(tileJson[0] + ", " + tileJson[1] + ", " + tileJson[2]);
		}
	}
}
