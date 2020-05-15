using Newtonsoft.Json.Linq;
using Nexus.Engine;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ParamGroup {

		public string name;			// Human title of the parameter, e.g. "Number of Bolts"
		public string key;			// Computer key for parameter, e.g. "boltnum"
		public string unitName;     // The name of the unit type, e.g. " frames" or " ms" (usually preceeded by a space)
		public short defValue;		// The default value as a number.
		public string defStr;		// The default value as a string.

		public ParamGroup( string key, string name ) {
			this.name = name;
			this.key = key;
		}

		public string Cycle(string curVal, bool up = true) {
			return "";
		}
	}

	public class IntParam : ParamGroup {
		public short min;
		public short max;
		public short increment;

		public IntParam( string key, string name, short min, short max, short increment, short defValue, string unitName = "" ) : base(key, name) {
			this.min = min;
			this.max = max;
			this.increment = increment;
			this.defValue = defValue;
			this.unitName = unitName;
			this.defStr = defValue.ToString() + this.unitName;
		}

		public string Validate(short value) {
			if(value < this.min) { return this.name + " must be between " + this.min.ToString() + " and " + this.max.ToString() + this.unitName + ". " + value + " is too low."; }
			if(value > this.max) { return this.name + " must be between " + this.min.ToString() + " and " + this.max.ToString() + this.unitName + ". " + value + " is too high."; }
			return null;
		}
	}

	public class PercentParam : ParamGroup {
		public short min;
		public short max;
		public short increment;
		public FInt baseValue;			// This is the value that the percent is based on. So 100% would equal exactly this value.

		public PercentParam( string key, string name, short min, short max, short increment, short defValue, FInt baseValue ) : base(key, name) {
			this.min = min;
			this.max = max;
			this.increment = increment;
			this.defValue = defValue;
			this.baseValue = baseValue;
			this.unitName = "%";
			this.defStr = defValue.ToString() + this.unitName;
		}

		public FInt GetTrueValue(short value) {
			return FInt.Create(value / 100) * this.baseValue;
		}

		public string Validate(short value) {
			if(value < this.min) { return this.name + " must be between " + this.min.ToString() + "% and " + this.max.ToString() + "%. " + value + "% is too low."; }
			if(value > this.max) { return this.name + " must be between " + this.min.ToString() + "% and " + this.max.ToString() + "%. " + value + "% is too high."; }
			return null;
		}
	}

	public class LabeledParam : ParamGroup {
		public string[] labels;

		public LabeledParam( string key, string name, string[] labels, short defValue) : base(key, name) {
			this.labels = labels;
			this.defValue = defValue;
			this.defStr = labels == null ? "" : labels[defValue];
			this.unitName = "";
		}

		public string Validate(byte value) {
			if(this.labels.Length < value) { return this.name + " has " + this.labels.Length + " labels, but an invalid number (" + value.ToString() + ") was chosen."; }
			return null;
		}
	}

	public class DictParam : ParamGroup {
		public Dictionary<byte, string> dict;

		public DictParam( string key, string name, Dictionary<byte, string> dict, short defValue) : base(key, name) {
			this.dict = dict;
			this.defValue = defValue;
			this.defStr = dict[(byte) defValue];
			this.unitName = "";
		}
	}
}
