using System.Collections.Generic;
using System.Linq;

namespace Nexus.Engine {

	public class UIContainNotifications : UIComponent {

		public const byte MaxNumberOfNotifications = 8;

		// List of Notifications
		private LinkedList<UINotification> notifications = new LinkedList<UINotification>();

		// Draw Positions
		private int startY;
		private short incomingMoved;			// Tracks how much the Incoming Notification has moved.

		// Incoming Notification
		private UINotification incomingNotif;
		private bool comesFromTop;				// TRUE means the notifications come from above. FALSE means come from below.

		public UIContainNotifications(UIComponent parent) : base(parent) {
			this.RunThemeUpdate();

			// TODO: TEMP. REMOVE
			// TODO: TEMP. REMOVE
			// TODO: TEMP. REMOVE
			// TODO: TEMP. REMOVE
			// TODO: TEMP. REMOVE
			this.AddIncomingNotification(UIAlertType.Success, "Success", "some text for you", 4000);
			this.AddIncomingNotification(UIAlertType.Error, "Error", "Some ERROR text for you. Normal is as normal does. Normal is as normal does. some text for you some text for you", 5000);
			this.AddIncomingNotification(UIAlertType.Warning, "Warning", "A quick warning.", 2000);
			this.AddIncomingNotification(UIAlertType.Normal, "Normal", "Normal is as normal does. But now let's see what else happens.", 3000);
		}

		public void AddIncomingNotification(UIAlertType type, string title, string text, int duration = 0) {

			// If there is already an incoming notification, we need to click it into place.
			this.SendIncomingNotificationToList();

			// Begin Incoming Notification
			this.incomingNotif = new UINotification(this, type, title, text, (duration > 0 ? Systems.timer.UniFrame + duration : 0));
			this.incomingMoved = 0; // Reset the Y notification movement.
		}

		public void SendIncomingNotificationToList() {
			if(this.incomingNotif is UINotification) {
				this.AddNotificationToList(this.incomingNotif);
				this.incomingNotif = null;
			}
		}

		public void AddNotificationToList(UINotification newNotif) {

			// Prepend the Notification
			this.notifications.AddFirst(newNotif);

			// Can't overwhelm the number of notifications.
			if(this.notifications.Count >= UIContainNotifications.MaxNumberOfNotifications) {
				this.notifications.RemoveLast();
			}
		}

		public void RunTick() {
			if(this.notifications.Count == 0 && this.incomingNotif is UINotification == false) { return; }
			
			UINotification delNotif = null;

			// Update the Incoming Notification Position, if applicable.
			if(this.incomingNotif is UINotification) {

				// Update the Incoming Notification's Y Total Movement.
				this.incomingMoved += 2;

				// We need to snap the Incoming Notification into place once it arrives.
				if(this.incomingMoved > this.incomingNotif.height) {
					this.SendIncomingNotificationToList();
				}
			}

			// Loop through all notifications and process transitions, exit frames.
			foreach(var notif in this.notifications) {

				// Check Notification Exit Mechanics
				if(notif.exitFrame > 0 && notif.exitFrame <= Systems.timer.UniFrame) {
					int finalFrame = notif.exitFrame + UIHandler.theme.notifs.exitDuration;

					// Draw Fade Effect during the fade itself.
					notif.alpha = 1 - Spectrum.GetPercentFromValue(Systems.timer.UniFrame, notif.exitFrame, finalFrame);

					// Delete the notification if their exit has finalized.
					if(Systems.timer.UniFrame > finalFrame) {
						delNotif = notif;
					}
				}
			}

			// Remove a notification that was marked for deletion this frame.
			if(delNotif is UINotification) {
				this.notifications.Remove(delNotif);
			}
		}

		// Draw All Notifications
		public void Draw() {
			if(this.notifications.Count == 0 && this.incomingNotif is UINotification == false) { return; }

			UIThemeNotifications theme = UIHandler.theme.notifs;

			int posY = this.startY;

			// Update starting position if notifications start at the bottom and there's at least one notification.
			if(!this.comesFromTop && this.notifications.Count > 0) {
				posY -= this.notifications.First.Value.height;
			}

			// Draw Incoming Notification (if applicable)
			if(this.incomingNotif is UINotification) {

				// Determine the starting position for the incoming notification:
				int incPosY = this.startY + (this.comesFromTop ? -this.incomingNotif.height + this.incomingMoved : -this.incomingMoved);

				// Draw the Incoming Notification
				this.incomingNotif.Draw(incPosY);

				// Adjust the position of the next notification in line.
				if(this.notifications.Count > 0) {
					if(this.comesFromTop) {
						posY = incPosY + this.incomingNotif.height + theme.NotifGap;
					} else {
						posY = incPosY - this.notifications.First.Value.height - theme.NotifGap;
					}
				}
			}

			// Loop through Notifications
			for(var i = 0; i < this.notifications.Count; i++) {
				UINotification notif = this.notifications.ElementAt(i);

				notif.Draw(posY);

				// Update the position of the next notification.
				if(this.comesFromTop) {
					posY = posY + notif.height + theme.NotifGap;
				} else {
					if(i + 1 < this.notifications.Count) {
						UINotification next = this.notifications.ElementAt(i + 1);
						posY = posY - next.height - theme.NotifGap;
					}
				}
			}
		}

		public void RunThemeUpdate() {
			UIThemeNotifications notifTheme = UIHandler.theme.notifs;

			this.SetWidth(notifTheme.ItemWidth);
			this.SetHeight(notifTheme.ContainerHeight);
			this.SetRelativePosition(notifTheme.xOffset, notifTheme.yOffset, notifTheme.xRel, notifTheme.yRel);

			// Set notifications to move from bottom to top if the container is linked to the bottom of parent.
			this.comesFromTop = notifTheme.yRel != UIVertPosition.Bottom;

			// Determine the starting position for each notification:
			this.startY = this.trueY + (this.comesFromTop == true ? 0 : this.height);
		}
	}
}
