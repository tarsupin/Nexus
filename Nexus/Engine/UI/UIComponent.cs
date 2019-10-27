using System;
using System.Collections.Generic;

/*
 * Methods needed:
 * RenderChildren()
 * RenderSelf()
 * onClick/onActivate()
 * IsMouseOver() // only run from the base UI down, and only on click or hover or something.
 * GetComponentClicked()		// seeks down the children until it finds a component that returns true; or most true... or self.
 */

namespace Nexus.Engine {

	public class UIComponent {

		// Identification
		public uint id;

		// Parent & Children
		public UIComponent Parent { get; protected set; }
		public List<UIComponent> Children { get; protected set; }

		public bool hasParent;					// TRUE if the component has a parent set.

		// Positioning
		protected short x, y;					// The relative offset of its parent component.
		protected short trueX, trueY;			// The true position (on screen) of the UI Component.

		public short ParentX { get { return this.Parent.trueX; } }
		public short ParentY { get { return this.Parent.trueY; } }

		public short MidX { get { return (short) (this.trueX + Math.Floor(this.width / 2d)); } }
		public short MidY { get { return (short) (this.trueX + Math.Floor(this.width / 2d)); } }

		// Sizing
		public short width;
		public short height;

		public short ParentWidth { get { return this.Parent.width; } }
		public short ParentHeight { get { return this.Parent.height; } }

		// Interactive Properties
		public bool visible;
		public bool IsSelected { get { return UIData.ComponentSelected.id == this.id; } }

		public UIComponent( UIComponent parent ) {
			this.id = UIData.NextID; // Attach an ID; useful to identify between components.

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
			this.trueX = (short) (this.x + (this.hasParent ? this.Parent.trueX : 0));
			this.trueY = (short) (this.y + (this.hasParent ? this.Parent.trueY : 0));

			// Children Must Update Their Positions
			foreach(UIComponent child in this.Children) { child.UpdateTruePosition(); }
		}

		// Sizing
		public void SetWidth(short width) { this.width = width; this.TriggerChange( UIChangeFlag.Size ); }
		public void SetHeight(short height) { this.height = height; this.TriggerChange( UIChangeFlag.Size ); }

		public void SetWidthToParent() { if(this.x != 0) { this.SetRelativeX(0); }; this.SetWidth(this.Parent.width); }
		public void SetHeightToParent() { if(this.y != 0) { this.SetRelativeY(0); }; this.SetHeight(this.Parent.height); }

		// Loop through every child and run their OnParentResize() behavior.
		private void TriggerChange( UIChangeFlag flag ) {
			foreach(UIComponent child in this.Children) { child.OnParentChange( flag ); }
			if(this.hasParent) { this.Parent.OnChildChange( flag ); }
		}

		// Reactions
		public virtual void OnParentChange( UIChangeFlag flag ) { /* Method that triggers when the parent component changes. */ }
		public virtual void OnChildChange( UIChangeFlag flag ) { /* Method that triggers when a child component changes. */ }

		// Interactive Properties
		public void SetVisible( bool visible ) {
			if(this.visible == visible) { return; }
			this.visible = visible;
			this.TriggerChange( UIChangeFlag.IsVisible );
		}

		public void SetSelected( bool selected ) {
			if(this.IsSelected == selected) { return; }
			UIData.ComponentSelected.id = this.id;
			this.TriggerChange( UIChangeFlag.IsSelected );
		}

		// Attach a Component to a Parent.
		private void AssignComponentToParent(UIComponent parent) {
			this.Parent = parent;
			this.hasParent = true;
			parent.AssignChildToComponent(this);
		}

		private void AssignChildToComponent(UIComponent child) {
			this.Children.Add(child);
		}
	}
}
