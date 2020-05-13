using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ParamGroup {
		public string name;

		public ParamGroup( string name ) {
			this.name = name;
		}
	}

	public class IntParam : ParamGroup {
		public short min;
		public short max;
		public short increment;
		public short defValue;
		public string unitName;

		public IntParam( string name, short min, short max, short increment, short defValue, string unitName = "" ) : base(name) {
			this.min = min;
			this.max = max;
			this.increment = increment;
			this.defValue = defValue;
			this.unitName = unitName;
		}

		public string Validate(short value) {

			if(value < this.min) {
				return "The minimum for `" + this.name + "` is " + this.min.ToString() + this.unitName + ". " + value + this.unitName + " is too low.";
			}

			if(value > this.max) {
				return "The maximum for `" + this.name + "` is " + this.max.ToString() + this.unitName + ". " + value + this.unitName + " is too high.";
			}

			return null;
		}
	}

	public class LabeledParam : ParamGroup {
		public string[] labels;
		public short defValue;
		public string unitName;

		public LabeledParam( string name, string[] labels, short defValue, string unitName = "") : base(name) {
			this.name = name;
			this.labels = labels;
			this.defValue = defValue;
			this.unitName = unitName;
		}

		public string Validate(byte value) {

			if(this.labels.Length < value) {
				return "The `" + this.name + "` param has " + this.labels.Length + " labels, but an invalid number (" + value.ToString() + ") was chosen.";
			}

			return null;
		}
	}

	public class DictionaryParam : ParamGroup {
		public Dictionary<byte, string> labels;
		public byte defValue;

		public DictionaryParam( string name, Dictionary<byte, string> labels, byte defValue) : base(name) {
			this.labels = labels;
			this.defValue = defValue;
		}

		public string Validate(byte value) {

			if(!this.labels.ContainsKey(value)) {
				return "The `" + this.name + "` param does not contain the key \"" + value.ToString() + "\" provided.";
			}

			return null;
		}
	}

	public class BoolParam : ParamGroup {
		public bool defValue;

		public BoolParam( string name, bool defValue) : base(name) {
			this.defValue = defValue;
		}

		public string Validate(bool value) {
			return null; // If something other than a bool is ever sent, it would catch a more severe error anyway.
		}
	}
}
