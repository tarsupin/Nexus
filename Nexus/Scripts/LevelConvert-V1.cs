using Newtonsoft.Json;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.Scripts {

	class LevelConvertV1 : LevelConvert {

		public LevelConvertV1() : base() {
			System.Console.WriteLine("--------------------------------------");
			System.Console.WriteLine("----- LEVEL CONVERSION - Version 1.0");
			System.Console.WriteLine("--------------------------------------");
		}

		protected override void ProcessLayerData(Dictionary<string, Dictionary<string, ArrayList>> layerJson) {

			// Temporary Blockers - Testing Purposes.
			//if(this.levelContent.levelId != "QCALQOD16") { return; }          // Specific Level Allowance
			//if(CalcRandom.IntBetween(0, 50) == 35) { return; }

			// Run Standard Layer Data Process
			base.ProcessLayerData(layerJson);
		}

		protected override void ProcessTileData( ArrayList tileJson ) {
			// tileJson[0], tileJson[1], tileJson[2]
			//System.Console.WriteLine(tileJson[0] + ", " + tileJson[1]);
			//System.Console.WriteLine(tileJson[0] + ", " + tileJson[1] + ", " + tileJson[2]);

			// Prepare Tile Data
			byte tileId = Byte.Parse(tileJson[0].ToString());
			byte subTypeId = Byte.Parse(tileJson[1].ToString());
			Dictionary<string, short> paramList = null;

			// Param-Specific Conversions
			if(tileJson.Count >= 3) {
				this.ConvertStringParams(tileJson, tileId, subTypeId);
			}

			// Retrieve Parameter List
			//if(tileJson.Count >= 3) {
			//	paramList = (Dictionary<string, short>) tileJson[2];
			//}

			// Run Conversions
			//this.ConvertGrassToMud(tileJson, tileId, subTypeId);
			this.ConvertGroundSubTypesToHorizontal(tileJson, tileId, subTypeId);

		}

		// This method will look for params that are still stored as string values, so that we can identify what needs to be changed to 'short' numbers.
		protected void ConvertStringParams(ArrayList tileJson, byte tileId, byte subTypeId) {
			// {{ "suit": "WhiteNinja", "powerAtt": "Shuriken" }}

			Dictionary<string, short> newParams = new Dictionary<string, short>();

			// We'll need to just look at tileJson, since the cast will be inaccurate.
			Dictionary<string, object> paramsOld = JsonConvert.DeserializeObject<Dictionary<string, object>>(tileJson[2].ToString());
			//Dictionary<string, object> paramsOld = (Dictionary<string, object>) tileJson[2];


			bool changed = false;

			// Loop through each param
			foreach(var param in paramsOld) {

				// Convert any broken parameters:
				var paramVal = param.Value;
				short newValue = 0;

				if(param.Key == "suit") {
					switch(paramVal) {
						case "WhiteNinja": changed = true; newValue = (byte)SuitSubType.WhiteNinja; break;
						case "BlueNinja": changed = true; newValue = (byte)SuitSubType.BlueNinja; break;
						case "GreenNinja": changed = true; newValue = (byte)SuitSubType.GreenNinja; break;
						case "RedNinja": changed = true; newValue = (byte)SuitSubType.RedNinja; break;
						case "BlackNinja": changed = true; newValue = (byte)SuitSubType.BlackNinja; break;
						case "BlueWizard": changed = true; newValue = (byte)SuitSubType.BlueWizard; break;
						case "WhiteWizard": changed = true; newValue = (byte)SuitSubType.WhiteWizard; break;
						case "RedWizard": changed = true; newValue = (byte)SuitSubType.RedWizard; break;
						case "GreenWizard": changed = true; newValue = (byte)SuitSubType.GreenWizard; break;
					}
				}
				
				else if(param.Key == "hat") {
					switch(paramVal) {
						case "WizBlueHat": changed = true; newValue = (byte)HatSubType.WizardBlueHat; break;
						case "WizGreenHat": changed = true; newValue = (byte)HatSubType.WizardGreenHat; break;
						case "WizRedHat": changed = true; newValue = (byte)HatSubType.WizardRedHat; break;
						case "WizWhiteHat": changed = true; newValue = (byte)HatSubType.WizardWhiteHat; break;
						case "FeatheredHat": changed = true; newValue = (byte)HatSubType.FeatheredHat; break;
					}
				}

				else if(param.Key == "powerAtt") {
					switch(paramVal) {
						case "Shuriken": changed = true; newValue = (byte) PowerSubType.Shuriken; break;
						case "BoxingRed": changed = true; newValue = (byte) PowerSubType.BoxingRed; break;
					}
				}
				
				else if(param.Key == "powerMob") {
					switch(paramVal) {
						case "BurstPower": changed = true; newValue = (byte) PowerSubType.Burst; break;
						case "SlowFall": changed = true; newValue = (byte) PowerSubType.SlowFall; break;
						case "LeapPower": changed = true; newValue = (byte) PowerSubType.Leap; break;
					}
				}

				else if(param.Key == "reverse" || param.Key == "attAllow" || param.Key == "beginFall" || param.Key == "perm") {
					switch(paramVal) {
						case "False": changed = true; newValue = 0; break;
						case "True": changed = true; newValue = 1; break;
						case true: changed = true; newValue = 1; break;
					}
				}

				if(changed) {
					//System.Console.WriteLine("Param Located :  { " + param.Key + ",  " + paramVal.ToString() + " }  -->  " + newValue);
					newParams[param.Key] = newValue;
				}
				
				else {
					bool isNumeric = short.TryParse(paramVal.ToString(), out _);

					if(!isNumeric) {
						System.Console.WriteLine("Param Located :  { " + param.Key + ",  " + paramVal.ToString() + " }  -->  UNRESOLVED");
					}
				}
			}

			if(changed) {
				this.OverwriteTileData(tileJson, tileId, subTypeId, newParams);
			}
		}

		protected void ConvertGrassToMud(ArrayList tileJson, byte tileId, byte subTypeId) {

			// If the tile is Grass, convert it to Mud
			if(tileId == (byte) TileEnum.GroundGrass) {
				tileId = (byte) TileEnum.GroundMud;
				this.OverwriteTileData(tileJson, tileId, subTypeId);
			}
		}
		
		protected void ConvertGroundSubTypesToHorizontal(ArrayList tileJson, byte tileId, byte subTypeId) {

			if(
				tileId == (byte) TileEnum.PlatformFixed ||
				tileId == (byte) TileEnum.PlatformItem ||
				tileId == (byte) TileEnum.Log ||
				tileId == (byte) TileEnum.Wall
			) {

				// Convert Ground Types to Horizontal Types
				if(subTypeId == (byte) GroundSubTypes.H1) {
					subTypeId = (byte) HorizontalSubTypes.H1;
				}

				else if(subTypeId == (byte)GroundSubTypes.H2) {
					subTypeId = (byte)HorizontalSubTypes.H2;
				}

				else if(subTypeId == (byte)GroundSubTypes.H3) {
					subTypeId = (byte)HorizontalSubTypes.H3;
				}

				this.OverwriteTileData(tileJson, tileId, subTypeId);
			}
		}
	}
}
