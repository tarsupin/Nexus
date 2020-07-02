using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEMenu : ContextMenu {

		public WEMenu( UIComponent parent, short posX, short posY, byte xCount = 4, byte yCount = 4 ) : base(parent, posX, posY, xCount, yCount) {
			
		}

		public override void OnClick() {
			byte menuOpt = this.GetContextOpt(Cursor.MouseX, Cursor.MouseY);

			// World Tile Tool Options
			if(menuOpt <= 5) {
				WE_UI.curWESlotGroup = menuOpt;
				WETools.SetWorldTileToolBySlotGroup(WE_UI.curWESlotGroup);
				WETools.UpdateHelperText();
			}

			// Resize Option
			else if(menuOpt == 6) {
				UIHandler.worldEditConsole.Open();
				UIHandler.worldEditConsole.SetInstructionText("resize ");
				ChatConsole.SendMessage("--------------------", Color.White);
				ChatConsole.SendMessage("Resize the World Map", Color.Red);
				ChatConsole.SendMessage("--------------------", Color.White);
			}

			this.CloseMenu();
		}
	}
}
