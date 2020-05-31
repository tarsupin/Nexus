using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEMenu : ContextMenu {

		public WEMenu( UIComponent parent, short posX, short posY, byte xCount = 4, byte yCount = 4 ) : base(parent, posX, posY, xCount, yCount) {

		}

		public override void OnClick() {
			EditorUI.currentSlotGroup = this.GetContextOpt(Cursor.MouseX, Cursor.MouseY);
			EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup);
			EditorTools.UpdateHelperText();
			this.CloseMenu();
		}
	}
}
