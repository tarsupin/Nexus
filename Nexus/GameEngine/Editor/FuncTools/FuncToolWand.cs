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

				// Initialize the Wand Data for this Tile
				bool validWand = WandData.InitializeWandData(scene, Cursor.TileGridX, Cursor.TileGridY);
				if(validWand == false) { return; }

				scene.scene.editorUI.moveParamMenu.LoadParamMenu();
				scene.scene.editorUI.actParamMenu.LoadParamMenu();
			}

			// Right Mouse Button (Clone Current Tile)
			else if(Cursor.RightMouseState == Cursor.MouseDownState.Clicked) {
				scene.CloneTile(Cursor.TileGridX, Cursor.TileGridY);
			}
		}
	}

	// Stores data related to a selected object and the wand param menu currently open.
	public static class WandData {

		public static bool validTile = false;
		public static bool isObject = false;

		// References to Scene and Layer Info
		public static EditorRoomScene editorScene;
		public static ParamMenu moveParamMenu;
		public static ParamMenu actParamMenu;
		public static LevelContent levelContent;
		public static Dictionary<string, Dictionary<string, ArrayList>> layerData;

		// Tile Grid Data
		public static short gridX;
		public static short gridY;

		// Essential Tile Data
		public static ArrayList wandTileData;

		// Essential References to Wand and Parameter Tools
		public static Params moveParamSet;				// Current Move parameter set (e.g. ParamsFlight, ParamsMoveChase, etc).
		public static Params actParamSet;				// Current Act parameter set (e.g. ParamsFireSpit, ParamsElemental, etc).

		// Retrieve Param List (the 3rd tile data value, which may be null)
		public static Dictionary<string, short> GetAllParamsOnTile() {
			ArrayList tileObj = WandData.wandTileData;

			if(tileObj.Count > 2 && tileObj[2] is Dictionary<string, short>) {
				return (Dictionary<string, short>) tileObj[2];
			}

			return null;
		}

		// Retrieve Param Value from Param List (the JSON for the specific param key)
		public static short GetParamVal(Params paramSet, string paramKey) {
			Dictionary<string, short> paramList = WandData.GetAllParamsOnTile();

			// Get the default value if the tile data does not have one saved:
			if(paramList == null || !paramList.ContainsKey(paramKey)) {
				ParamGroup rule = paramSet.GetParamRule(paramKey);
				return rule.defValue;
			}

			return (short) paramList[paramKey];
		}

		// Initialize Wand Data
		public static bool InitializeWandData(EditorRoomScene scene, short gridX, short gridY) {

			// Get Scene References
			WandData.editorScene = scene;
			WandData.moveParamMenu = WandData.editorScene.scene.editorUI.moveParamMenu;
			WandData.actParamMenu = WandData.editorScene.scene.editorUI.actParamMenu;
			WandData.levelContent = WandData.editorScene.levelContent;

			RoomFormat roomData = WandData.levelContent.data.rooms[WandData.editorScene.roomID];

			// Verify that Tile is Valid:
			WandData.validTile = false;

			if(LevelContent.VerifyTiles(roomData.main, gridX, gridY)) {
				WandData.layerData = roomData.main;
				WandData.isObject = false;
			}
			
			else if(LevelContent.VerifyTiles(roomData.fg, gridX, gridY)) {
				WandData.layerData = roomData.fg;
				WandData.isObject = false;
			}
			
			else if(LevelContent.VerifyTiles(roomData.obj, gridX, gridY)) {
				WandData.layerData = roomData.obj;
				WandData.isObject = true;
			}

			else { return false; }

			WandData.validTile = true;

			WandData.gridX = gridX;
			WandData.gridY = gridY;

			// Get Tile Content Data
			WandData.wandTileData = LevelContent.GetTileDataWithParams(WandData.layerData, WandData.gridX, WandData.gridY);

			// Get the Param Set Used
			Params moveParamSet = null;
			Params actParamSet = null;

			if(WandData.isObject) {
				moveParamSet = Systems.mapper.ObjectMetaData[byte.Parse(WandData.wandTileData[0].ToString())].moveParamSet;
				actParamSet = Systems.mapper.ObjectMetaData[byte.Parse(WandData.wandTileData[0].ToString())].actParamSet;
			} else {
				TileObject wandTile = Systems.mapper.TileDict[byte.Parse(WandData.wandTileData[0].ToString())];
				moveParamSet = wandTile.moveParamSet;
				actParamSet = wandTile.actParamSet;
			}

			// If the tile has param sets, it can be used here. Otherwise, return.
			WandData.validTile = (moveParamSet != null || actParamSet != null);
			if(WandData.validTile == false) { return false; }

			WandData.moveParamSet = moveParamSet;
			WandData.actParamSet = actParamSet;

			return true;
		}
	}
}
