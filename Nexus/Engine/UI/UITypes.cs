
namespace Nexus.Engine {

	public enum UIState : byte { Playing, Menu }

	public enum UIMouseOverState {
		Off,
		Entered,
		On,
		Exited,
	}

	public enum UIHorPosition : byte {
		Left,
		Center,
		Right
	}

	public enum UIVertPosition : byte {
		Top,
		Center,
		Bottom,
	}

	public enum UIPrimaryDirection : byte {
		None,
		Top,
		Left,
		Center,
		Right,
		Bottom,
	}

	public enum UIAlertType : byte {
		Normal,
		Error,
		Warning,
		Success,
	}
}
