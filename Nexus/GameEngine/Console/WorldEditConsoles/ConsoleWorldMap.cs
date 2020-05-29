using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleWorldMap {

		public static void ResizeMap() {
			string currentIns = ConsoleTrack.NextArg();
			int curVal = ConsoleTrack.NextArgAsInt();

			ConsoleTrack.PrepareTabLookup(resizeOpts, currentIns, "Resize World Map");

			// Width Option
			if(currentIns == "width") {
				ConsoleTrack.possibleTabs = "Example: resize width 60";
				ConsoleTrack.helpText = "Choose a width between " + (byte)WorldmapEnum.MinWidth + " and " + (byte)WorldmapEnum.MaxWidth;
			}

			// Height Option
			else if(currentIns == "height") {
				ConsoleTrack.possibleTabs = "Example: resize height 60";
				ConsoleTrack.helpText = "Choose a height between " + (byte)WorldmapEnum.MinHeight + " and " + (byte)WorldmapEnum.MaxHeight;
			}

			else { return; }

			// Activate Resize
			if(ConsoleTrack.activate && curVal > 0) {
				WEScene scene = (WEScene)Systems.scene;

				if(currentIns == "width" && curVal >= (byte)WorldmapEnum.MinWidth && curVal <= (byte)WorldmapEnum.MaxWidth) {
					scene.ResizeWidth((byte) curVal);
				}

				else if(currentIns == "height" && curVal >= (byte)WorldmapEnum.MinHeight && curVal <= (byte)WorldmapEnum.MaxHeight) {
					scene.ResizeHeight((byte)curVal);
				}
			}
		}

		public static readonly Dictionary<string, object> resizeOpts = new Dictionary<string, object>() {
			{ "width", "Change the world map's width." },
			{ "height", "Change the world map's height." },
		};
	}
}
