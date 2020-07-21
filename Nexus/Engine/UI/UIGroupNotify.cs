
namespace Nexus.Engine {

	public class UIGroupNotify : UINotification {

		public UIGroupNotify(UIComponent parent, UIAlertType type, string title, string text, int exitFrame) : base(parent, type, title, text, exitFrame, parent.width) {}

		// Group Notifies have an additional param option for the Draw method.
		public void Draw(int posY) {
			this.DrawBackground(this.Parent.trueX, posY);
			this.DrawText(this.Parent.trueX, posY);
		}
	}
}
