using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.GameEngine {

	public class FuncToolWand : FuncTool {

		public FuncToolWand() : base() {
			this.spriteName = "Icons/Wand";
			this.title = "Wand";
			this.description = "An advanced tool that allows you to modify properties on game objects.";
		}

		public override void RunTick(EditorRoomScene scene) {
			if(UIComponent.ComponentWithFocus != null) { return; }

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

		// Essential References to Wand and Parameter Tools
		public static Params paramSet;					// Current parameter set (e.g. ParamsCollectable, ParamsContent, ParamsFireBurst, etc).
		public static ParamGroup[] paramRules;          // Shorthand for paramSet.rules

		// Basic Menu Information
		public static bool menuOptsChanged = false;			// Sets to true if the menu changes, and needs a display update and/or resize.
		public static byte numberOptsToShow = 1;		// Number of Menu Options to Show
		public static sbyte optionSelected = 0;			// Current Menu Option Highlighted

		// The Menu Options Displayed
		// Stored for speed, as well as because it can change dynamically.
		public static string[] menuOptLabels = new string[10] { "", "", "", "", "", "", "", "", "", "" };
		public static string[] menuOptText = new string[10] { "", "", "", "", "", "", "", "", "", "" };

		// Rules Currently Loaded
		// This is important because some rules might not be loaded at times, such as with the "Flight" wand menu - rules change based on the Flight type.
		public static byte[] menuOptRuleIds = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

		// Retrieve Param List (the 3rd tile data value, which may be null)
		public static Dictionary<string, short> GetParamList() {
			ArrayList tileObj = WandData.wandTileData;

			if(tileObj.Count > 2 && tileObj[2] is Dictionary<string, short>) {
				return (Dictionary<string, short>) tileObj[2];
			}

			return null;
		}

		// Retrieve Param Value from Param List (the JSON for the specific param key)
		public static short GetParamVal(string paramKey) {
			Dictionary<string, short> paramList = WandData.GetParamList();

			// Get the default value if the tile data does not have one saved:
			if(paramList == null || !paramList.ContainsKey(paramKey)) {
				ParamGroup rule = WandData.paramSet.GetParamRule(paramKey);
				return rule.defValue;
			}

			return (short) paramList[paramKey];
		}

		// Update Menu Options (run when menu changes)
		public static void UpdateMenuOptions(byte numOptsToShow = 0, byte[] optRuleIds = null) {
			Dictionary<string, short> paramList = WandData.GetParamList();

			// Get Rules
			ParamGroup[] rules = WandData.paramRules;

			// Prepare Menu Options
			if(numOptsToShow == 0) {
				WandData.numberOptsToShow = (byte) rules.Length;
			} else {
				WandData.numberOptsToShow = numOptsToShow;
				WandData.menuOptsChanged = true;
			}

			// Determine the Rule IDs that appear in the Option List. The default is just to list each rule in order.
			// Some wand menus (such as Flight and Chest) will have different sequences that must be sent to this method.
			// The reason for this is because there are certain rules that will affect others. Such as Flight Type of Rotation making the rotating diameter value visible.
			for(byte i = 0; i < WandData.numberOptsToShow; i++) {
				WandData.menuOptRuleIds[i] = optRuleIds == null ? i : optRuleIds[i];
			}

			// Loop through each rule:
			for(byte i = 0; i < WandData.numberOptsToShow; i++) {
				byte ruleId = WandData.menuOptRuleIds[i];
				ParamGroup rule = rules[ruleId];

				WandData.menuOptLabels[i] = rule.name;

				// Determine the Text
				if(paramList != null && paramList.ContainsKey(rule.key)) {

					// Labeled Params
					if(rule is LabeledParam) {
						WandData.menuOptText[i] = ((LabeledParam)(rule)).labels[short.Parse(paramList[rule.key].ToString())];
					}
					
					// Dictionary Params
					else if(rule is DictParam) {
						DictParam dictRule = (DictParam)(rule);
						byte[] contentKeys = dictRule.dict.Keys.ToArray<byte>();
						byte paramVal = byte.Parse(paramList[rule.key].ToString());
						WandData.menuOptText[i] = dictRule.dict[contentKeys[paramVal]];
					}
					
					// Frame Params (show them as milliseconds, rather than by frames)
					else if(rule is FrameParam) {
						int newVal = byte.Parse(paramList[rule.key].ToString()) * 1000 / 60;
						WandData.menuOptText[i] = newVal.ToString() + " ms";
					}
					
					// Default Numeric Params
					else {
						WandData.menuOptText[i] = paramList[rule.key].ToString() + rule.unitName;
					}
				} else {
					WandData.menuOptText[i] = rule.defStr;
				}
			}
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
			WandData.numberOptsToShow = 1;
			WandData.optionSelected = -1;

			return true;
		}
	}
}
