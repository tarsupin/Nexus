using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class FuncToolWand : FuncTool {

		public FuncToolWand() : base() {
			this.spriteName = "Icons/Wand";
			this.title = "Wand";
			this.description = "An advanced tool that allows you to modify properties on game objects.";
		}

		public override void RunTick(EditorRoomScene scene) {

			// Left Mouse Button
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				scene.scene.editorUI.paramMenu.LoadParamMenu(scene, Cursor.MouseGridX, Cursor.MouseGridY);
			}

			// Right Mouse Button (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile(Cursor.MouseGridX, Cursor.MouseGridY);
			}
		}
	}

	// Stores data related to a selected object and the wand param menu currently open.
	public static class WandData {

		public static bool validTile = false;

		// References to Scene and Layer Info
		public static EditorRoomScene editorScene;
		public static LevelContent levelContent;
		public static Dictionary<string, Dictionary<string, ArrayList>> layerData;

		// Tile Grid Data
		public static ushort gridX;
		public static ushort gridY;

		// Essential Tile Data
		public static ArrayList wandTileData;
		public static TileGameObject wandTile;

		// Option-Specific Data
		// Data that pertains to the menu option being currently selected or focused on.
		public static string curParamKey;

		// Essential References to Wand and Parameter Tools
		public static Params paramSet;					// Current parameter set (e.g. ParamsCollectable, ParamsContent, ParamsFireBurst, etc).
		public static ParamGroup[] paramRules;			// Shorthand for paramSet.rules

		// Basic Menu Information
		public static byte optionsToShow = 1;			// Number of Menu Options to Show
		public static sbyte optionSelected = 0;			// Current Menu Option Highlighted

		// The Menu Options Displayed
		// Stored for speed, as well as because it can change dynamically.
		public static string[] menuOptionLabels = new string[10] { "", "", "", "", "", "", "", "", "", "" };
		public static short[] menuOptionNumbers = new short[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		public static string[] menuOptionText = new string[10] { "", "", "", "", "", "", "", "", "", "" };

		// Rules Currently Loaded
		// This is important because some rules might not be loaded at times, such as with the "Flight" wand menu - rules change based on the Flight type.
		public static byte[] menuOptionRule = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


		// Update Menu Options (run when menu changes)
		public static void UpdateMenuOptions() {

			// Get Param List
			ArrayList tileObj = WandData.wandTileData;
			JObject paramList = null;

			if(tileObj.Count > 2 && tileObj[2] is JObject) {
				paramList = (JObject) tileObj[2];
			}

			// Get Rules
			ParamGroup[] rules = WandData.paramRules;
			byte ruleCount = (byte) rules.Length;

			// Prepare Menu Options
			WandData.optionsToShow = ruleCount;

			// Loop through each rule:
			for(byte i = 0; i < ruleCount; i++) {
				WandData.menuOptionRule[i] = i;
				WandData.menuOptionLabels[i] = rules[i].name;

				ParamGroup rule = rules[i];

				// Determine the Text
				if(paramList != null && paramList.ContainsKey(rule.key)) {

					if(rule is LabeledParam) {
						WandData.menuOptionText[i] = ((LabeledParam)(rule)).labels[short.Parse(paramList[rule.key].ToString())];
					} else {
						WandData.menuOptionText[i] = paramList[rule.key].ToString() + rule.unitName;
					}
				} else {
					WandData.menuOptionText[i] = rules[i].defStr;
				}
			}

			//WandData.menuOptionNumbers;
		}

		// Initialize Wand Data
		public static bool InitializeWandData(EditorRoomScene scene, ushort gridX, ushort gridY) {

			// Get Scene References
			WandData.editorScene = scene;
			WandData.levelContent = WandData.editorScene.levelContent;
			WandData.layerData = WandData.levelContent.data.rooms[WandData.editorScene.roomID].main;

			// Verify that Tile is Valid:
			WandData.validTile = LevelContent.VerifyTiles(WandData.layerData, gridX, gridY);
			if(WandData.validTile == false) { return false; }

			WandData.gridX = gridX;
			WandData.gridY = gridY;

			// Get the Tile Class
			WandData.wandTileData = LevelContent.GetTileDataWithParams(WandData.layerData, WandData.gridX, WandData.gridY);
			WandData.wandTile = Systems.mapper.TileDict[byte.Parse(WandData.wandTileData[0].ToString())];

			// If the tile has param sets, it can be used here. Otherwise, return.
			WandData.validTile = WandData.wandTile.paramSet != null;
			if(WandData.validTile == false) { return false; }

			WandData.paramSet = WandData.wandTile.paramSet;
			WandData.paramRules = WandData.paramSet.rules;

			// Reset Menu Information
			WandData.optionsToShow = 1;
			WandData.optionSelected = -1;

			return true;
		}
	}
}
