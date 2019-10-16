
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class ParticlePathGenerator {

		public ParticlePathGenerator() {

		}

		// In Degrees: Angle of Starting Degree, Distance from Start, Rotation Lerp, Speed that Spiral Grows
		public static FVector SpiralStep( FInt startingDegree, FInt startDist, FInt rotationLerp, FInt growLerp, int step = 0 ) {

			FInt curDegree = Degrees.Normalize(startingDegree + (rotationLerp * step));
			FInt curDist = startDist + (growLerp * step);

			// Get Coordinates
			FInt x = Degrees.GetXFromRotation(curDegree, curDist);
			FInt y = Degrees.GetYFromRotation(curDegree, curDist);

			return FVector.Create(x.IntValue, y.IntValue);
		}
	}
}
