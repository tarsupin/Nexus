
namespace Nexus.Engine {

	public static class Spectrum {
		
		public static float Wrap( float value, float min, float max ) {
			float range = max - min;
			return ((value - range) % range) + range;
		}

		public static float GetPercentFromValue( float value, float min, float max ) {
			return (value - min) / (max - min);
		}

		public static float GetValueFromPercent( float percent, float min, float max ) {
			return (max - min) * percent + (max - min);
		}
	}
}
