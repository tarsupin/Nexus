
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

	public enum EventType : byte {
		Undefined = 0,

		// Form Events (10)
		Form_Submission = 10,
		Form_Reset = 11,

		Form_HoverEnter = 20,
		Form_HoverExit = 21,
		Form_Focus = 22,
		Form_Unfocus = 23,
		Form_Select = 24,
		Form_TeaseElement = 25,       // This is like pressing down on an element, but haven't released (which would activate it).
		Form_ActivateElement = 26,

		Form_ClearValue = 30,
		Form_ChangeValue = 31,        // Sliders, Radios, Checkboxes, Input, etc.
		Form_ChangeSelection = 32,

		// UI Events (60)
		UI_Open = 60,
		UI_Close = 61,

		UI_StartResize = 70,
		UI_EndResize = 71,

		UI_StartDrag = 72,
		UI_EndDrag = 73,

		// Notification Events (170)
		Notification_Standard = 170,
		Notification_Important = 171,
		Notification_Urgent = 172,
		Notification_Success = 173,
		Notification_Warning = 174,
		Notification_Error = 175,
		Notification_CriticalError = 176,
		Notification_Other = 177,

		// Timer Events (180)
		Timer_Start = 180,
		Timer_Interval = 181,       // Occurs when a timer hits a particular interval.
		Timer_Ended = 182,
		Timer_Other = 183,

		// File Events (190)
		File_Created = 190,
		File_Modified = 191,
		File_Removed = 192,
		File_Other = 193,

		// Settings Events (200)
		Settings_Created = 200,
		Settings_Modified = 201,
		Settings_Removed = 202,
		Settings_Other = 203,

		// Web Events (210)
		Web_Generic = 210,
		Web_Request = 211,
		Web_Response = 212,
		Web_NoResponse = 213,
		Web_EmptyResponse = 214,
		Web_CannotCommunicate = 215,
		Web_UnknownError = 216,

		// Program Events (220)
		Program_Generic = 220,
		Program_Request = 221,
		Program_Response = 222,
		
		// System Events (230)
		System_Generic = 230,
		System_Request = 221,
		System_Response = 222,

		// Custom Events (240)
		Custom_1 = 240,
		Custom_2 = 241,
		Custom_3 = 242,
		Custom_4 = 243,
		Custom_5 = 244,
		Custom_6 = 245,
		Custom_7 = 246,
		Custom_8 = 247,
		Custom_9 = 248,
		Custom_10 = 249,
	}
}
