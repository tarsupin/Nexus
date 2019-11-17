/*
 * SubTypes may overlap in many objects.
 */

namespace Nexus.Gameplay {

	public enum GroundSubTypes : byte {
		S = 0,
		FU = 1,
		FL = 2,
		FC = 3,
		FR = 4,
		FB = 5,
		H1 = 6,
		H2 = 7,
		H3 = 8,
		V1 = 9,
		V2 = 10,
		V3 = 11,
		FUL = 12,
		FUR = 13,
		FBL = 14,
		FBR = 15,
	}

	public enum PlatformSubTypes : byte {
		S,
		H1,
		H2,
		H3,
	}

	public enum FacingSubType : byte {
		FaceUp = 0,
		FaceLeft = 1,
		FaceRight = 2,
		FaceDown = 3,
	}
}
