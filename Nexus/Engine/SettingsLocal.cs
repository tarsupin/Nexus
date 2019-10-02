using System;
using System.Collections.Generic;

namespace Nexus.Engine {
	class SettingsLocal {

		public FilesLocal filesLocal = new FilesLocal();

		public Dictionary<byte, string> keyBinds;		// Tracks Keybinds.

		public SettingsLocal() {
			
		}
	}
}
