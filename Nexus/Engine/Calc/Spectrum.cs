
namespace Nexus.Engine {

	public static class Spectrum {
		
		public static int Wrap( int value, int min, int max ) {
			var range = max - min;
			return ((value - range) % range) + range;
		}

		public static FInt Wrap( FInt value, FInt min, FInt max ) {
			var range = max - min;
			return ((value - range) % range) + range;
		}

		public static FInt GetPercentFromValue( FInt value, FInt min, FInt max ) {
			return (value - min) / (max - min);
		}

		public static FInt GetValueFromPercent( FInt percent, FInt min, FInt max ) {
			return (max - min) * percent;
		}
	}
}
