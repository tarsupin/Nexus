
namespace Nexus.Engine {

	public enum EventCategory : byte {
		Form,			// Form Events (Buttons, Inputs, etc)
		UI,				// UI Events
		Notification,   // Notification Events
		Timer,          // Timer Events
		File,           // [AlterEvents] File Watcher
		Settings,       // [AlterEvents] Settings, Configurations
		Web,            // [ExternalEvents] Web Connection
		Program,        // [ExternalEvents] External Program
		System,         // System Events
		Custom,         // Custom Events
	}

	public enum FormEvents : byte {
		Undefined,
		Submission,
		Hover,
		Focus,
		Unfocus,
		ChangeValue,		// Sliders, Radios, Checkboxes, Input, etc.
		TeaseElement,		// This is like pressing down on an element, but haven't released (which would activate it).
		ActivateElement,	
	}

	public enum UIEvents : byte {
		Undefined,
		Open,
		Close,
		StartResize,
		EndResize,
		StartDrag,
		EndDrag,
	}

	public enum NotificationEvents : byte {
		Undefined,
		Notice,
		Success,
		Warning,
		Error,
		CriticalError,
	}

	public enum TimerEvents : byte {
		Undefined,
		Start,
		Interval,		// Occurs when a timer hits a particular interval.
		Ended,
	}
	
	// Files, Settings, Configs, etc.
	public enum AlterEvents : byte {
		Undefined,
		Created,
		Modified,
		Removed,
	}

	public enum ExternalEvents : byte {
		Undefined,
		Request,
		Response,
		NoResponse,
		EmptyResponse,
		CannotCommunicate,
		UnknownError,
	}

	public enum CustomEvents : byte {
		Undefined,
		Custom1,
		Custom2,
		Custom3,
		Custom4,
		Custom5,
		Custom6,
		Custom7,
		Custom8,
		Custom9,
		Custom10,
	}
}
