/*
 * SubTypes may overlap in many objects.
 */

namespace Nexus.Gameplay {

	public enum GroundSubTypes : byte {
		S,
		FU,
		FL,
		FC,
		FR,
		FB,
		H1,
		H2,
		H3,
		V1,
		V2,
		V3,
		FUL,
		FUR,
		FBL,
		FBR,
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
