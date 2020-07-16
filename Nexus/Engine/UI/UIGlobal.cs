
namespace Nexus.Engine {

	// UIGlobal contains the globally recognized UI features, such as:
	//		- Notification Popups
	//		- Tool Tips
	//		- Alert + Confirmation Boxes (May Lock Screen + Require Interaction)
	//		- Right Click Menu
	//		- Navigation Bars
	public class UIGlobal : UIComponent {
		public UIContainNotifications notifyBox;
		public UIToolTip toolTip;
		public UIConfirmBox confirmBox;

		public UIGlobal() : base(null) {
			this.SetHeight(Systems.screen.windowHeight);
			this.SetWidth(Systems.screen.windowWidth);
			this.notifyBox = new UIContainNotifications(this);
			this.toolTip = new UIToolTip(this);
			this.confirmBox = new UIConfirmBox(this);
		}
	}
}
