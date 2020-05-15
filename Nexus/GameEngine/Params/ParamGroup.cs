using Newtonsoft.Json.Linq;
using Nexus.Engine;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.GameEngine {

	public class ParamGroup {

		public string name;			// Human title of the parameter, e.g. "Number of Bolts"
		public string key;			// Computer key for parameter, e.g. "boltnum"
		public string unitName;     // The name of the unit type, e.g. " frames" or " ms" (usually preceeded by a space)
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
		public short defValue;

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
		public short defValue;
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

		public FInt GetFIntFromJObject(JObject value) {
			return (value == null ? FInt.Create(this.defValue) : FInt.Create(value.Value<byte>()));
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
		public short defValue;

		public LabeledParam( string key, string name, string[] labels, short defValue) : base(key, name) {
			this.name = name;
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

	public class SpecialLabelParam : LabeledParam {
		public string parentParamKey;
		public Dictionary<byte, string>[] parentDict;

		public SpecialLabelParam( string key, string name, string parentParamKey, Dictionary<byte, string>[] parentDict ) : base(key, name, null, 0) {
			this.parentParamKey = parentParamKey;
			this.parentDict = parentDict;
		}

		public string GetLabelText(byte parentDictId, byte contentId) {
			Dictionary<byte, string> dict = this.parentDict[parentDictId];
			byte[] contentKeys = dict.Keys.ToArray<byte>();
			return this.parentDict[parentDictId][contentKeys[contentId]];
		}
	}
}
