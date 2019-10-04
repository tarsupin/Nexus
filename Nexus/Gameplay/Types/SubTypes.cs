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

	public enum FixedPlatSubType {
		Standard,
		FaceLeft,
		FaceRight,
		UpsideDown,
	}
}
