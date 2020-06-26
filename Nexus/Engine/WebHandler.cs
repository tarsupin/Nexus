
namespace Nexus.Engine {

	public static class WebHandler {

		public static string LaunchURL(string url) {

			// Attempt to Start the URL Process
			try {
				System.Diagnostics.Process.Start(url);
			}
			
			// If there is no browser that opens:
			catch (System.ComponentModel.Win32Exception exception) {
				if(exception.ErrorCode == -2147467259) {
					return exception.Message;
				}
			}

			// Other Issue
			catch(System.Exception other) {
				return other.Message;
			}

			return "";
		}
	}
}
