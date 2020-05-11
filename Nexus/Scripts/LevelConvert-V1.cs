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
			if(this.levelContent.levelId != "QCALQOD16") { return; }          // Specific Level Allowance
			//if(CalcRandom.IntBetween(0, 50) == 35) { return; }

			// Run Standard Layer Data Process
			base.ProcessLayerData(layerJson);
		}

		protected override void ProcessTileData( ArrayList tileJson ) {
			// tileJson[0], tileJson[1], tileJson[2]
			System.Console.WriteLine(tileJson[0] + ", " + tileJson[1]);
			//System.Console.WriteLine(tileJson[0] + ", " + tileJson[1] + ", " + tileJson[2]);
			//this.ConvertGrassToMud(tileJson);
			this.ConvertGroundSubTypesToHorizontal(tileJson);
		}

		protected void ConvertGrassToMud(ArrayList tileJson) {

			// If the tile is Grass
			if(Byte.Parse(tileJson[0].ToString()) == (byte) TileEnum.GroundGrass) {

				// Convert the tile to Mud
				tileJson[0] = (byte) TileEnum.GroundMud;

				this.OverwriteTileData(tileJson);
			}
		}
		
		protected void ConvertGroundSubTypesToHorizontal(ArrayList tileJson) {

			byte tileId = Byte.Parse(tileJson[0].ToString());
			byte subType = Byte.Parse(tileJson[1].ToString());

			if(
				tileId == (byte) TileEnum.PlatformFixed ||
				tileId == (byte) TileEnum.PlatformItem ||
				tileId == (byte) TileEnum.Log ||
				tileId == (byte) TileEnum.Wall
			) {

				// Convert Ground Types to Horizontal Types
				if(subType == (byte) GroundSubTypes.H1) {
					tileJson[1] = (byte) HorizontalSubTypes.H1;
				}

				else if(subType == (byte)GroundSubTypes.H2) {
					tileJson[1] = (byte)HorizontalSubTypes.H2;
				}

				else if(subType == (byte)GroundSubTypes.H3) {
					tileJson[1] = (byte)HorizontalSubTypes.H3;
				}

				this.OverwriteTileData(tileJson);
			}
		}
	}
}
