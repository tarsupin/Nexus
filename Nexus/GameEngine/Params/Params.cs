
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class Params {

	}

	public class WholeRangeParam {
		public string name;
		public short min;
		public short max;
		public short increment;
		public short defValue;
		public string unitName;

		public WholeRangeParam( string name, short min, short max, short increment, short defValue, string unitName = "" ) {
			this.name = name;
			this.min = min;
			this.max = max;
			this.increment = increment;
			this.defValue = defValue;
			this.unitName = unitName;
		}

		public static string Validate( short value, WholeRangeParam rule) {

			if(value < rule.min) {
				return "The minimum for `" + rule.name + "` is " + rule.min.ToString() + rule.unitName + ". " + value + rule.unitName + " is too low.";
			}

			if(value > rule.max) {
				return "The maximum for `" + rule.name + "` is " + rule.max.ToString() + rule.unitName + ". " + value + rule.unitName + " is too high.";
			}

			return null;
		}
	}

	public class LabeledParam {
		public string name;
		public string[] labels;
		public short defValue;
		public string unitName;

		public LabeledParam( string name, string[] labels, short defValue, string unitName = "" ) {
			this.name = name;
			this.labels = labels;
			this.defValue = defValue;
			this.unitName = unitName;
		}

		public static string Validate( byte value, LabeledParam rule) {

			if(rule.labels.Length < value) {
				return "The `" + rule.name + "` param has " + rule.labels.Length + " labels, but an invalid number (" + value + ") was chosen.";
			}

			return null;
		}
	}

	public class DictionaryParam {
		public string name;
		public Dictionary<byte, string> labels;
		public byte defValue;

		public DictionaryParam( string name, Dictionary<byte, string> labels, byte defValue ) {
			this.name = name;
			this.labels = labels;
			this.defValue = defValue;
		}

		public static string Validate( byte value, DictionaryParam rule) {

			if(!rule.labels.ContainsKey(value)) {
				return "The `" + rule.name + "` param does not contain the key \"" + value + "\" provided.";
			}

			return null;
		}
	}

	public class BoolParam {
		public string name;
		public bool defValue;

		public BoolParam( string name, bool defValue ) {
			this.name = name;
			this.defValue = defValue;
		}

		public static string Validate( bool value, BoolParam rule ) {
			return null; // If something other than a bool is ever sent, it would catch a more severe error anyway.
		}
	}
}
