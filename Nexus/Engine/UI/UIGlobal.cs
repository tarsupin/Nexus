
namespace Nexus.Engine {

	// UIGlobal contains the globally recognized UI features, such as:
	//		- Notification Popups
	//		- Tool Tips
	//		- Text Box and/or Message Box (Usable for NPCs, but also for other notices; can see Confirmation Box for basic structure)
	//		- Confirmation Boxes (May Lock Screen + Require Interaction)
	//		- Right Click Menu
	//		- Navigation Bars
	//		- Status Bar?
	public class UIGlobal : UIComponent {
		public UIContainNotifications notifyBox;
		public UIToolTip toolTip;
		public UIConfirmBox confirmBox;

		public UIGlobal() : base(null) {
			this.SetHeight(Systems.screen.viewHeight);
			this.SetWidth(Systems.screen.viewWidth);
			this.notifyBox = new UIContainNotifications(this);
			this.toolTip = new UIToolTip(this);
		}
	}
}
