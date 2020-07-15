
namespace Nexus.Engine {

	// GlobalUI Class provides a RunTick() that is designed to run over ALL scenes.
	// GlobalUI handles globally recognized UI features, including:
	//		- Notification Popups
	//		- Tool Tips
	//		- Alert + Confirmation Boxes (May Lock Screen + Require Interaction)
	//		- Right Click Menu
	// GlobalUI also has a .LocalSettings value that allows overwriting GlobalUI settings, positions, visibility, etc.
	public class GlobalUI : UIComponent {
		public UIContainNotifications notifyBox;
		public UIToolTip toolTip;

		public GlobalUI() : base(null) {
			this.SetHeight(Systems.screen.windowHeight);
			this.SetWidth(Systems.screen.windowWidth);
			this.notifyBox = new UIContainNotifications(this);
			this.toolTip = new UIToolTip(this);
			this.UpdateTheme();
		}

		public void UpdateTheme() {
			this.notifyBox.RunThemeUpdate();
			this.toolTip.RunThemeUpdate();
		}

		public void RunTick() {
			this.notifyBox.RunTick();
			this.toolTip.RunTick();
		}

		public void Draw() {
			this.notifyBox.Draw();
			this.toolTip.Draw();
		}
	}
}
