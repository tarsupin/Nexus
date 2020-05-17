using Newtonsoft.Json;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.Scripts {

	class LevelConvertV1 : LevelConvert {

		public LevelConvertV1() : base() {
			System.Console.WriteLine("--------------------------------------");
			System.Console.WriteLine("----- LEVEL CONVERSION - Version 1.0");
			System.Console.WriteLine("--------------------------------------");
		}

		protected override void ProcessLayerData(Dictionary<string, Dictionary<string, ArrayList>> layerJson, bool isObjectLayer = false) {

			// Temporary Blockers - Testing Purposes.
			//if(CalcRandom.IntBetween(0, 50) == 35) { return; }

			// Run Standard Layer Data Process
			base.ProcessLayerData(layerJson, isObjectLayer);
		}

		protected override void ProcessTileData( ArrayList tileJson, bool isObject = false ) {
			// tileJson[0], tileJson[1], tileJson[2]
			//System.Console.WriteLine(tileJson[0] + ", " + tileJson[1]);
			//System.Console.WriteLine(tileJson[0] + ", " + tileJson[1] + ", " + tileJson[2]);

			// Prepare Tile Data
			byte tileId = Byte.Parse(tileJson[0].ToString());
			byte subTypeId = Byte.Parse(tileJson[1].ToString());
			Dictionary<string, short> paramList = null;

			// Objects to Move to Tiles
			if(isObject) {
				if(
					tileId == (byte)TileEnum.PlatformFixed ||
					tileId == (byte)TileEnum.PlatformItem
				) {
					this.MoveTileDataToLayer(LayerEnum.main, tileId, subTypeId, paramList);
					isObject = false;
				}

				// Fixed Button ID - from object ID 71 to tile ID 80
				if(tileId == 71) { this.MoveTileDataToLayer(LayerEnum.main, 80, subTypeId, paramList); return; }

				// Timed Button ID - from object ID 92 to tile ID 81
				if(tileId == 71) { this.MoveTileDataToLayer(LayerEnum.main, 81, subTypeId, paramList); return; }
			}

			// Param Conversions
			if(tileJson.Count >= 3) {
				this.ParamConversions(tileJson, tileId, subTypeId);
			}

			// Retrieve Parameter List
			//if(tileJson.Count >= 3) {
			//	paramList = (Dictionary<string, short>) tileJson[2];
			//}

			// Objects
			if(isObject) {

				// Run Conversions
				this.ConvertObjPlatformSubTypes(tileId, subTypeId);
			}

			// Tiles
			else {

				// Run Conversions
				//this.ConvertGrassToMud(tileJson, tileId, subTypeId);
				this.ConvertGroundSubTypesToHorizontal(tileId, subTypeId);
				this.ConvertTilePlatformSubTypes(tileId, subTypeId);
				this.ConvertFlagData(tileId, subTypeId);
			}
		}

		// This method will look for params that are still stored as string values, so that we can identify what needs to be changed to 'short' numbers.
		protected void ParamConversions(ArrayList tileJson, byte tileId, byte subTypeId) {
			// {{ "suit": "WhiteNinja", "powerAtt": "Shuriken" }}

			Dictionary<string, short> newParams = new Dictionary<string, short>();

			// We'll need to just look at tileJson, since the cast will be inaccurate.
			Dictionary<string, object> paramsOld = JsonConvert.DeserializeObject<Dictionary<string, object>>(tileJson[2].ToString());
			//Dictionary<string, object> paramsOld = (Dictionary<string, object>) tileJson[2];

			bool anyChange = false;

			// Loop through each param
			foreach(var param in paramsOld) {

				// Convert any broken parameters:
				var paramVal = param.Value;
				short newValue = 0;
				bool changed = false;

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
						case "TopHat": changed = true; newValue = (byte)HatSubType.TopHat; break;
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
						case false: changed = true; newValue = 0; break;
					}
				}

				// Looking for "ms" settings, regardless of tileId type
				if(changed == false && (param.Key == "duration" || param.Key == "offset" || param.Key == "durationOffset" || param.Key == "delay" || param.Key == "retDelay" || param.Key == "cycle")) {
					changed = true;
					newValue = (short)(int.Parse(param.Value.ToString()) * 60 / 1000);
					//System.Console.WriteLine("Param Located :  { " + param.Key + ",  " + param.Value.ToString() + " }  -->  " + newParams[param.Key]);
				}

				// Looking for "bool" sets, regardless of type:
				if(changed == false && paramVal is bool) {
					changed = true;
					if((bool) paramVal == true) { newValue = 1; }
				}

				if(changed) {
					anyChange = true;
					newParams[param.Key] = newValue;
					//System.Console.WriteLine("Param Located :  { " + param.Key + ",  " + paramVal.ToString() + " }  -->  " + newValue);
				} else {
					bool isNumeric = short.TryParse(paramVal.ToString(), out short newShort);

					if(!isNumeric) {
						System.Console.WriteLine("Param Located :  { " + param.Key + ",  " + paramVal.ToString() + " }  -->  UNRESOLVED");
					} else {
						newParams[param.Key] = newShort;
					}
				}
			}

			if(anyChange) {
				this.OverwriteTileData(tileId, subTypeId, newParams);
			}
		}

		protected void ConvertGrassToMud(byte tileId, byte subTypeId) {

			// If the tile is Grass, convert it to Mud
			if(tileId == (byte) TileEnum.GroundGrass) {
				tileId = (byte) TileEnum.GroundMud;
				this.OverwriteTileData(tileId, subTypeId);
			}
		}
		
		protected void ConvertGroundSubTypesToHorizontal(byte tileId, byte subTypeId) {

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

				this.OverwriteTileData(tileId, subTypeId);
			}
		}
		
		protected void ConvertObjPlatformSubTypes(byte objectId, byte subTypeId) {

			if(
				objectId == (byte) ObjectEnum.PlatformDelay ||
				objectId == (byte) ObjectEnum.PlatformDip ||
				objectId == (byte) ObjectEnum.PlatformFall ||
				objectId == (byte) ObjectEnum.PlatformMove
			) {

				// Convert Ground Types to Horizontal Types
				if(subTypeId == 6) { subTypeId = (byte) HorizontalSubTypes.H1; }
				else if(subTypeId == 7) { subTypeId = (byte)HorizontalSubTypes.H2; }
				else if(subTypeId == 8) { subTypeId = (byte)HorizontalSubTypes.H3; }

				this.OverwriteTileData(objectId, subTypeId);
			}
		}
		
		protected void ConvertTilePlatformSubTypes(byte tileId, byte subTypeId) {

			if(
				tileId == (byte)TileEnum.PlatformFixed ||
				tileId == (byte)TileEnum.PlatformItem ||
				tileId == (byte)TileEnum.Wall ||
				tileId == (byte)TileEnum.Log
			) {

				// Convert Ground Types to Horizontal Types
				if(subTypeId == 6) { subTypeId = (byte) HorizontalSubTypes.H1; }
				else if(subTypeId == 7) { subTypeId = (byte)HorizontalSubTypes.H2; }
				else if(subTypeId == 8) { subTypeId = (byte)HorizontalSubTypes.H3; }

				this.OverwriteTileData(tileId, subTypeId);
			}
		}
		
		protected void ConvertFlagData(byte tileId, byte subTypeId) {

			if(tileId == 150) {

				byte newTileId = 0;

				// Convert Ground Types to Horizontal Types
				if(subTypeId == 3) { subTypeId = 0; newTileId = (byte) TileEnum.CheckFlagFinish; }
				else if(subTypeId == 2) { subTypeId = 0; newTileId = (byte) TileEnum.CheckFlagCheckpoint; }
				else if(subTypeId == 0) { subTypeId = 0; newTileId = (byte) TileEnum.CheckFlagPass; }
				else if(subTypeId == 1) { subTypeId = 0; newTileId = (byte) TileEnum.CheckFlagRetry; }

				this.OverwriteTileData(newTileId, subTypeId);
			}
		}
		
	}
}
