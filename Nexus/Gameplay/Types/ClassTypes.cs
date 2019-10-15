
namespace Nexus.Gameplay {

	// Tile Game Objects must have ONE state; it can have multiple properties, but all must be immutable.
	public enum TileGameObjectId {

		// Need "None" at position 0 to identify tile grid IDs that are set to invalid.
		None,

		// Ground, Immutable
		GroundGrass,
		GroundDirt,
		GroundMud,
		GroundStone,
		GroundSnow,
		GroundSlime,
		GroundCloud,

		Wall,
		Log,

		// Ledges
		LedgeGrass,
		LedgeGrassDecor,
		PlatformFixed,

		// Decor, Immutable
		DecorVeg,
		DecorDesert,
		DecorCave,
		DecorWater,
		DecorPet,
		DecorItems,

		PromptArrow,
		PromptIcon,

		// Solid, Toggled
		// These can be ClassGameObjects because Toggles are global properties, they are not saved by the individual object.
		ToggleBoxBR,
		ToggleBoxGY,
		ToggleBlockBlue,
		ToggleBlockRed,
		ToggleBlockGreen,
		ToggleBlockYellow,

		TogglePlatBlue,
		TogglePlatRed,
		TogglePlatGreen,
		TogglePlatYellow,

		// Fixed, Touch-Effect
		// These can be ClassGameObjects because they either exist or don't, which can be identfied by the Tile itself.
		Box,
		Lock,
		Spike,

		// These have multiple SubTypes, but are otherwise immutable in their behavior.
		PuffBlock,
		Conveyor,

		// Collectables
		CollectableCoin,
		CollectableGoodie,
		CollectableSuit,
		CollectableHat,
		CollectablePower,
	}

}
