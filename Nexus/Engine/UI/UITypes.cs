
namespace Nexus.Engine {

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

	public enum UIAlertType : byte {
		Normal,
		Error,
		Warning,
		Success,
	}
}
