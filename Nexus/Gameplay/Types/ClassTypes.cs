
namespace Nexus.Gameplay {

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
		GroundWall = 20,
		GroundLog = 21,

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
		FixedBox = 100,
		FixedBrick = 101,
		// ...
		FixedLeaf = 105,
		// ...
		FixedLock = 110,
		// ...
		PlatformDip = 120,
		PlatformDelay = 121,
		PlatformFall = 122,
		PlatformMove = 123,
		// ...
		ToggleBlockOn = 130,
		ToggleBlockOff = 131,

		// Land & Fixed Enemies (150 - 199)
		FixedChomper = 150,
		FixedChomperFire = 151,
		FixedPlant = 152,
		// ...
		LandMoosh = 160,
		LandShroom = 161,
		LandBug = 162,
		LandGoo = 163,
		LandLiz = 164,
		LandSnek = 167,
		LandWurm = 168,
		// ...
		LandTurtle = 170,
		LandSnail = 171,
		// ...
		LandBoom = 175,
		// ...
		LandOcto = 180,
		LandBones = 181,
		LandPoke = 182,
		// ...
		LandLich = 185,

		// Flight Enemies (200 - 229)
		FlightBuzz = 200,
		Saw = 201,
		// ...
		FlightDire = 205,
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
