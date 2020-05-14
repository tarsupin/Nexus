﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ParamMenu : UIComponent {

		private static byte SlotHeight = 30;        // The height of each slot.

		private Params paramSet;		// The parameter set to use.
		private ushort splitPos;		// The "central" section in the middle that splits the left and right sides.
		private ushort leftWidth;		// The amount of width for the left side (titles)
		private ushort rightWidth;		// The amount of width for the right side (integers, percents, labels, etc)
		private static FontClass font;

		// Tile Details
		private EditorRoomScene scene;
		private Dictionary<string, Dictionary<string, ArrayList>> layerData;		// The LevelContent layer data (main) for this tile.
		private ushort tileX;		// The tile's GridX position.
		private ushort tileY;		// The tile's GridY position.

		public ParamMenu( UIComponent parent ) : base(parent) {

			// Ensure Font has been set.
			if(ParamMenu.font is FontClass == false) {
				ParamMenu.font = Systems.fonts.console;
			}

			this.SetVisible(false);
		}

		// Rebuild the ParamMenu design:
		public void LoadParamMenu(EditorRoomScene scene, ushort gridX, ushort gridY) {

			// Get Tile Information
			this.scene = ((EditorScene)Systems.scene).CurrentRoom;
			this.layerData = this.scene.levelContent.data.rooms[this.scene.roomID].main;

			// Verify that Tile is Valid:
			if(!LevelContent.VerifyTiles(this.layerData, gridX, gridY)) { return; }

			this.tileX = gridX;
			this.tileY = gridY;

			// Get the Tile Class
			ArrayList tileObj = LevelContent.GetTileDataWithParams(this.layerData, this.tileX, this.tileY);
			TileGameObject tile = Systems.mapper.TileDict[byte.Parse(tileObj[0].ToString())];

			// If the tile has param sets, it can be used here. Otherwise, return.
			if(tile.paramSets == null) { return; }

			// Get Parameter Set
			this.paramSet = tile.paramSets[0];		// TODO: Either remove multiple param sets, or update this to accomodate multiple

			// Get Sizing Details
			this.leftWidth = this.GetLeftWidth();
			this.rightWidth = this.GetRightWidth();

			this.width = (short)(this.leftWidth + 20 + this.rightWidth);
			this.height = (short)(this.paramSet.rules.Length * ParamMenu.SlotHeight - 1);

			// posX, posY describes the center of the context menu.
			// x, y describes the top-left corner of the context menu.
			this.x = (short)((gridX * (byte) TilemapEnum.TileWidth) - Systems.camera.posX - this.width);		// Only need to readjust x coord, since we're right-aligning it.
			this.y = (short)((gridY * (byte) TilemapEnum.TileHeight) - Systems.camera.posY);

			// Reposition the menu if it would overlap other content:
			if(this.x < 100) { this.x = 100; }
			if(this.y < 50) { this.y = 50; }
			if(this.y + this.height > Systems.screen.windowHeight - 50) { this.y = (short) (Systems.screen.windowHeight - 50 - this.height); }

			this.splitPos = (ushort) (this.x + this.leftWidth + 10);

			this.SetVisible(true);
		}

		// Identifies the width that the left side should be by determining the width of the largest string.
		private ushort GetLeftWidth() {
			var rules = this.paramSet.rules;
			ushort largestWidth = 0;

			for(byte i = 0; i < rules.Length; i++) {
				ushort w = (ushort) ParamMenu.font.font.MeasureString(rules[i].name).X;
				if(w > largestWidth) { largestWidth = w; }
			}

			return (ushort) (largestWidth + 50);
		}

		// Identifies the width that the left side should be by determining the width of the largest string.
		private ushort GetRightWidth() {
			var rules = this.paramSet.rules;
			ushort rightWidth = 140;

			foreach(ParamGroup group in rules) {
				if(group is LabeledParam) {
					rightWidth += 80;
					break;
				}
			}

			return rightWidth;
		}

		public void RunTick() {

			// End method if the context menu isn't visible, or if the tab key was released.
			if(!this.visible) { return; }

			// If the current tool switches away from the wand, close the menu:
			if(EditorTools.tempTool is FuncToolWand == false && EditorTools.funcTool is FuncToolWand == false) {
				this.CloseMenu();
				return;
			}

			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				sbyte curSelection = this.GetMenuSelection(Cursor.MouseY);
				if(curSelection < 0) { return; }

				// Cycle through parameter using mouse scroll:
				if(Cursor.MouseScrollDelta != 0) {

					// Get the parameter key (e.g. "speed", "count", etc)
					string paramKey = this.paramSet.GetParamKey((byte)curSelection);

					// Increment the parameter value by 1 step:
					this.paramSet.CycleParam(this.layerData, this.tileX, this.tileY, paramKey, Cursor.MouseScrollDelta < 0);
				}
			}
		}

		// Identify which menu option is at a specific location:
		public sbyte GetMenuSelection( int posY ) {
			if(UIComponent.ComponentWithFocus is ParamMenu == false) { return -1; }
			short distFromTop = (short) (posY - this.y);
			if(distFromTop <= ParamMenu.SlotHeight) { return 0; }
			return (sbyte) Math.Floor((decimal) (distFromTop / ParamMenu.SlotHeight));
		}

		public void OpenMenu() {
			this.SetVisible(true);
		}

		public void CloseMenu() {
			this.SetVisible(false);
		}

		public void Draw() {
			if(!this.visible) { return; }

			ParamGroup[] rules = this.paramSet.rules;
			ArrayList tileObj = LevelContent.GetTileDataWithParams(this.layerData, this.tileX, this.tileY);

			// Draw White Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y, this.width, this.height), Color.White * 0.6f);

			// Draw Line Divisions & Menu Options
			byte count = (byte) rules.Length;
			sbyte curSelection = this.GetMenuSelection(Cursor.MouseY);

			// Loop through vertical set:
			for(byte i = 0; i < count; i++) {

				// Draw Line
				Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * i, this.width, 2), Color.Black);

				// Add Title
				string name = rules[i].name;
				Vector2 textSize = ParamMenu.font.font.MeasureString(name);
				ParamMenu.font.Draw(name, this.splitPos - 10 - (byte) Math.Floor(textSize.X), this.y + ParamMenu.SlotHeight * i + 8, Color.Black);

				// Draw Current Value
				string paramKey = rules[i].key;
				bool isDefault = true;

				// If the LevelContent Tile exists and has data, draw it:
				if(tileObj.Count > 2 && tileObj[2] is JObject) {
					JObject paramList = (JObject) tileObj[2];
					
					if(paramList.ContainsKey(paramKey)) {
						isDefault = false;

						// Set this line as green to indicate that it's not a default value (unless currently being highlighted):
						if(curSelection != i) {
							Systems.spriteBatch.Draw(Systems.tex2dDarkGreen, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * i, this.width, ParamMenu.SlotHeight), Color.White * 0.2f);
						}

						// Draw the Text
						if(rules[i] is LabeledParam) {
							ParamMenu.font.Draw(((LabeledParam)(rules[i])).labels[short.Parse(paramList[paramKey].ToString())], this.splitPos + 10, this.y + ParamMenu.SlotHeight * i + 8, Color.Black);
						} else {
							ParamMenu.font.Draw(paramList[paramKey].ToString() + rules[i].unitName, this.splitPos + 10, this.y + ParamMenu.SlotHeight * i + 8, Color.Black);
						}
					}
				}

				// Draw the default value if a value wasn't provided:
				if(isDefault) {
					ParamMenu.font.Draw(rules[i].defStr, this.splitPos + 10, this.y + ParamMenu.SlotHeight * i + 8, Color.Black);
				}
			}

			// Draw Bottom Line
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * count, this.width, 2), Color.Black);

			// Hovering Visual
			if(curSelection > -1) {
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * curSelection, this.width, ParamMenu.SlotHeight), Color.White * 0.5f);
			}
		}
	}
}
