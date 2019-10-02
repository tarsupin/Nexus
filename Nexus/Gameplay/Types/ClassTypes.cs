
namespace Nexus.Gameplay {

	// Class Game Objects must have ONE state; it can have multiple properties, but all must be immutable.
	public enum ClassGameObjectType {
		
		// Ground, Immutable
		GroundGrass,
		GroundDirt,
		GroundMud,
		GroundStone,
		GroundSlime,
		GroundCloud,

		Wall,
		Log,

		// Ledges
		LedgeGrass,
		PlatformFixed,

		// Decor, Immutable
		DecorVeg,
		DecorDesert,
		DecorCave,
		DecorWater,
		DecorPet,
		DecorItems,

		PromptArrow,
		PromptSign,
		
		// Solid, Toggled
		// These can be ClassGameObjects because Toggles are global properties, they are not saved by the individual object.
		BoxToggle,
		ToggleBlockOn,
		ToggleBlockOff,
		PlatformToggleOn,
		PlatformToggleOff,
		
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

	// TODO CLEANUP: Am I even using this class? Can probably remove it.
	// TODO CLEANUP: Right now, this doesn't interact with GameMaps.ObjectMap.
	// TODO CLEANUP: If I don't update this by 10/5/2019 remove it.

	//
	// NOTE: If this section is updated, you must ALSO update GameMaps.ObjectMap
	// NOTE: If this section is updated, you must ALSO update GameMaps.ObjectMap
	//
	public enum ClassEnum: short {

		// Ground, Immutable (0 - 29)
		GroundGrass = 1,
		GroundDirt = 2,
		GroundMud = 3,
		GroundStone = 4,
		GroundSnow = 5,
		GroundSlime = 6,
		GroundCloud = 7,
		// ...
		LedgeGrass = 10,
		// ...
		Wall = 20,
		Log = 21,

		// Decor, Immutable (30 - 49)
		DecorVeg = 30,
		DecorDesert = 31,
		DecorCave = 32,
		DecorWater = 33,
		// ...
		DecorPet = 40,
		DecorItems = 41,
		// ...
		PromptArrow = 45,
		PromptSign = 46,

		// Fixed, Unmoveable (50 - 79)
		Spike = 50,
		// ...
		ExclaimBlock = 55,
		BoxToggle = 56,
		// ...
		PuffBlock = 60,
		Conveyor = 61,
		// ...
		PlatformFixed = 70,
		PlatformToggleOn = 75,
		PlatformToggleOff = 76,

		// Unused Section (80 - 99)

		// Solid, but Moveable (100 - 149)
		Box = 100,
		Brick = 101,
		// ...
		Leaf = 105,
		// ...
		Lock = 110,
		// ...
		PlatformDip = 120,
		PlatformDelay = 121,
		PlatformFall = 122,
		PlatformMove = 123,
		// ...
		ToggleBlockOn = 130,
		ToggleBlockOff = 131,

		// Land & Fixed Enemies (150 - 199)
		Chomper = 150,
		ChomperFire = 151,
		Plant = 152,
		// ...
		Moosh = 160,
		Shroom = 161,
		Bug = 162,
		Goo = 163,
		Liz = 164,
		Snek = 167,
		Wurm = 168,
		// ...
		Turtle = 170,
		Snail = 171,
		// ...
		Boom = 175,
		// ...
		Octo = 180,
		Bones = 181,
		Poke = 182,
		// ...
		Lich = 185,

		// Flight Enemies (200 - 229)
		Buzz = 200,
		Saw = 201,
		// ...
		Dire = 205,
		// ..
		ElementalAir = 210,
		ElementalEarth = 211,
		ElementalFire = 212,
		// ...
		Ghost = 215,
		FlairElectric = 216,
		FlairFire = 217,
		FlairMagic = 218,
		// ..
		ElementalEye = 220,
		ElementalMini = 221,
		// ..
		Slammer = 225,

		// Items, Handheld (230 - 269)
		Shell = 230,
		Boulder = 231,
		// ..
		TNT = 235,
		Bomb = 236,
		// ..
		ButtonStandard = 240,
		ButtonFixed = 241,
		ButtonTimed = 242,
		// ...
		SpringFixed = 245,
		SpringStandard = 246,
		// ..
		Handheld = 250,

		// Generators (270 - 279)
		Cannon = 270,
		Placer = 271,

		// Interactives (280 - 289)
		Flag = 280,
		Chest = 281,
		NPC = 282,
		PeekMap = 283,
		// ...
		Door = 285,
		DoorLock = 286,

		// Special (290 - 299)
		Cluster = 290,
		Track = 291,
		// ...
		GrowObj = 292,

		// Collectables (300 - 309)
		CollectableCoin = 300,
		CollectableGoodie = 301,
		CollectableSuit = 302,
		CollectableHat = 303,
		CollectablePower = 304,
	}
}
