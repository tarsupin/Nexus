
// UIData is a static class that tracks global information about the UI.

namespace Nexus.Engine {

	public enum UIChangeFlag : byte {
		Position,
		Size,
		IsVisible,
		IsSelected,
	}

	public static class UIData {

		private static uint _nextId = 0;
		public static uint NextID { get { UIData._nextId++; return UIData._nextId; } }

		public static UIComponent ComponentWithFocus;		// Component with current focus.
		public static UIComponent ComponentSelected;		// Component that is currently "selected" (like a button)
	}
}
