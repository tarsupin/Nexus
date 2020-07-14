using System;
using System.Collections.Generic;

namespace Nexus.Engine {

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
		public bool IsSelected { get { return UIComponent.ComponentSelected.id == this.id; } }
		public UIMouseOverState MouseOver;

		public UIComponent( UIComponent parent ) {
			this.id = UIComponent.NextID; // Attach an ID; useful to identify between components.

			// Verify that the parent is a valid UIComponent (not null).
			if(parent is UIComponent) {
				this.AssignComponentToParent(parent);
			}
		}

		// Sizing
		public void SetWidth(short width) { this.width = width; }
		public void SetHeight(short height) { this.height = height; }

		// Positioning
		public void SetRelativePosition( short xOffset, short yOffset, UIHorPosition xRel = UIHorPosition.Left, UIVertPosition yRel = UIVertPosition.Top ) {

			// Set X Position
			if(xRel == UIHorPosition.Left) {
				this.x = xOffset;
			} else if(xRel == UIHorPosition.Right) {
				this.x = (short)((this.Parent is UIComponent ? this.Parent.width : Systems.screen.windowWidth) - this.width - xOffset);
			} else if(xRel == UIHorPosition.Center) {
				this.x = (short)((this.Parent is UIComponent ? Math.Floor(this.Parent.width / 2f) : Systems.screen.windowHalfWidth) - Math.Floor(this.width / 2f) - xOffset);
			}
			
			// Set Y Position
			if(yRel == UIVertPosition.Top) {
				this.y = yOffset;
			} else if(yRel == UIVertPosition.Bottom) {
				this.y = (short)((this.Parent is UIComponent ? this.Parent.height : Systems.screen.windowHeight) - this.height - yOffset);
			} else if(yRel == UIVertPosition.Center) {
				this.y = (short)((this.Parent is UIComponent ? Math.Floor(this.Parent.height / 2f) : Systems.screen.windowHalfHeight) - Math.Floor(this.height / 2f) - yOffset);
			}

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

		// Mouse Detection
		public UIComponent GetHoverComponent() {
			if(this.MouseOver != UIMouseOverState.On) { return null; }

			// Loop through children and get more refined answer, if applicable.
			foreach(UIComponent child in this.Children) {
				UIComponent childComp = child.GetHoverComponent();
				if(childComp != null) { return childComp; }
			}

			return this;
		}

		public UIMouseOverState GetMouseOverState() {
			int mouseX = Cursor.MouseX;
			int mouseY = Cursor.MouseY;

			// If the Mouse is not over this component, set Mouse State to "Off" or "Exited"
			if(mouseX < this.trueX || mouseX > this.trueX + this.width || mouseY < this.trueY || mouseY > this.trueY + this.height) {
				return this.MouseOver == UIMouseOverState.On ? UIMouseOverState.Exited : UIMouseOverState.Off;
			}

			// Otherwise, set Mouse State to "On" or "Entered"
			return this.MouseOver == UIMouseOverState.Off ? UIMouseOverState.Entered : UIMouseOverState.On;
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
