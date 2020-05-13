using System.Text.RegularExpressions;

namespace Nexus.Engine {

	public static class Sanitize {

		private static string invalidFilenameChars = new string (System.IO.Path.GetInvalidFileNameChars());

		public static string Title(string content) { return Regex.Replace(content, @"[^\w\s]", ""); }
		public static string Digits(string content) { return Regex.Replace(content, @"[^\d]", ""); }
		public static string SafeWord(string content) { return Regex.Replace(content, @"[^\w]", ""); }
		public static string Coordinates(string content) { return Regex.Replace(content, @"[^0-9\,]", ""); }

		public static string IsMultiLingualAlphabet(string content) {
			return Regex.Replace(content, @"[^\w\s\-\+ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]", "");
		}

		public static string Filename(string content) {
			string invalidChars = Regex.Escape(invalidFilenameChars);
			string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
			return Regex.Replace(content, invalidRegStr, "_");
		}

	}
}
