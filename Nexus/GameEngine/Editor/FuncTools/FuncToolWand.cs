using Newtonsoft.Json.Linq;
using Nexus.Engine;
using System.Collections;

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

		// Essential Tile Data
		public static ArrayList wandTileData;
		public static TileGameObject wandTile;
		public static JObject wandTileParams;

		public static ushort gridX;
		public static ushort gridY;

		// Option-Specific Data
		// Data that pertains to the menu option being currently selected or focused on.
		public static string curParamKey;

		// Essential References to Wand and Parameter Tools
		public static Params paramSet;                  // Current parameter set (e.g. ParamsCollectable, ParamsContent, ParamsFireBurst, etc).
		public static ParamGroup[] paramRules;          // Shorthand for paramSet.rules

		// Basic Menu Information
		public static byte optionsToShow = 1;           // Number of Menu Options to Show
		public static byte optionHighlighted = 0;       // Current Menu Option Highlighted

		// The Menu Options Displayed
		// Stored for speed, as well as because it can change dynamically.
		public static string[] menuOptionLabels = new string[10] { "", "", "", "", "", "", "", "", "", "" };
		public static short[] menuOptionNumbers = new short[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		public static string[] menuOptionText = new string[10] { "", "", "", "", "", "", "", "", "", "" };

		// Rules Currently Loaded
		// This is important because some rules might not be loaded at times, such as with the "Flight" wand menu - rules change based on the Flight type.
		public static byte[] menuOptionRule = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	}
}
