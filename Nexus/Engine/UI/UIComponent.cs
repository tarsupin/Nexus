using System;
using System.Collections.Generic;

namespace Nexus.Engine {

	// Mouse Overlay State
	public enum UIMouseOverState {
		Off,
		Entered,
		On,
		Exited,
	}

	public class UIComponent {

		// Static Settings
		private static int _nextId = 0;
		public static int NextID { get { UIComponent._nextId++; return UIComponent._nextId; } }

		public static UIComponent ComponentWithFocus;       // Component with current focus.
		public static UIComponent ComponentSelected;        // Component that is currently "selected" (like a button)

		// Identification
		public int id;

		// Parent & Children
		public UIComponent Parent { get; protected set; }
		public List<UIComponent> Children { get; protected set; }

		// Positioning
		public short x { get; protected set; }			// The relative offset of its parent component.
		public short y { get; protected set; }			// The relative offset of its parent component.
		public short trueX { get; protected set; }		// The true position (on screen) of the UI Component.
		public short trueY { get; protected set; }		// The true position (on screen) of the UI Component.

		public short MidX { get { return (short) (this.trueX + Math.Floor(this.width / 2d)); } }
		public short MidY { get { return (short) (this.trueX + Math.Floor(this.width / 2d)); } }

		// Sizing
		public short width;
		public short height;

		// Interactive Properties
		public bool visible;
		public bool IsSelected { get { return UIComponent.ComponentSelected.id == this.id; } }
		public UIMouseOverState MouseOver;

		public UIComponent( UIComponent parent ) {
			this.id = UIComponent.NextID; // Attach an ID; useful to identify between components.

			// Verify that the parent is a valid UIComponent (not null).
			if(parent is UIComponent) {
				this.AssignComponentToParent(parent);
			}
		}

		// Positioning
		public void SetRelativeX(short x) { if(this.x != x) { this.x = x; this.UpdateTruePosition(); }; }
		public void SetRelativeY(short y) { if(this.y != y) { this.y = y; this.UpdateTruePosition(); }; }

		public void SetRelativePosition( short x, short y ) {
			this.x = x;
			this.y = y;
			this.UpdateTruePosition();
		}

		public void UpdateTruePosition() {
			this.trueX = (short) (this.x + (this.Parent is UIComponent ? this.Parent.trueX : 0));
			this.trueY = (short) (this.y + (this.Parent is UIComponent ? this.Parent.trueY : 0));

			// Children Must Update Their Positions
			if(this.Children is List<UIComponent>) {
				foreach(UIComponent child in this.Children) { child.UpdateTruePosition(); }
			}
		}

		// Sizing
		public void SetWidth(short width) { this.width = width; }
		public void SetHeight(short height) { this.height = height; }

		// Interactive Properties
		public void SetVisible( bool visible ) {
			if(this.visible == visible) { return; }
			this.visible = visible;
		}

		// Mouse Detection
		public UIComponent GetHoverComponent() {
			if(!this.IsMouseOver()) { return null; }

			// Loop through children and get more refined answer, if applicable.
			foreach(UIComponent child in this.Children) {
				UIComponent childComp = child.GetHoverComponent();
				if(childComp != null) { return childComp; }
			}

			return this;
		}

		public bool IsMouseOver() {
			int mouseX = Cursor.MouseX;
			int mouseY = Cursor.MouseY;

			if(mouseX < this.trueX || mouseX > this.trueX + this.width || mouseY < this.trueY || mouseY > this.trueY + this.height) {

				// Update Mouse State to "Off" or "Exited"
				if(this.MouseOver == UIMouseOverState.On) { this.MouseOver = UIMouseOverState.Exited; } else { this.MouseOver = UIMouseOverState.Off; }

				return false;
			}

			// Update Mouse State to "On" or "Entered"
			if(this.MouseOver == UIMouseOverState.Off) { this.MouseOver = UIMouseOverState.Entered; } else { this.MouseOver = UIMouseOverState.On; }

			return true;
		}

		// Attach a Component to a Parent.
		private void AssignComponentToParent(UIComponent parent) {
			this.Parent = parent;
			parent.AssignChildToComponent(this);
		}

		private void AssignChildToComponent(UIComponent child) {

			if(this.Children is List<UIComponent> == false) {
				this.Children = new List<UIComponent>();
			}

			this.Children.Add(child);
		}
	}
}
