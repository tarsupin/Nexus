
namespace Nexus.Gameplay {

	// List of Tile Types
	// This includes any tile that isn't "foreground", including full-space tiles.
	// Also includes fixed objects that can accept tile behavior for collision detection.
	// Tile Game Objects must have ONE state; it can have multiple properties, but all must be immutable.
	// Foreground Tiles include decorations, prompts, or tiles that appear in the front.
	public enum TileEnum : byte {

		// Need "None" at position 0 to identify tile grid IDs that are set to invalid.
		None = 0,

		// Ground, Immutable (0 - 9)
		GroundGrass = 1,
		GroundDirt = 2,
		GroundMud = 3,
		GroundStone = 4,
		GroundSnow = 5,
		GroundSlime = 6,
		GroundCloud = 7,

		// Ground-Esque, Immutable (10 - 19)
		SlabGray = 10,
		Log = 11,
		SlabBrown = 12,

		BarrierWall = 19,

		// Ledges (20 - 29)
		LedgeGrass = 20,
		LedgeDecor = 21,
		LedgeSnow = 22,

		// Fixed Platforms
		PlatformFixedUp = 25,
		PlatformFixedDown = 26,
		PlatformFixedLeft = 27,
		PlatformFixedRight = 28,

		// Decor, Prompts (30 - 39)
		DecorVeg = 30,
		DecorDesert = 31,
		DecorCave = 32,
		DecorSnow = 33,
		DecorPet = 34,
		DecorItems = 35,
		// ...
		PromptArrow = 38,
		PromptIcon = 39,

		// Fixed, Touch-Effect (40 - 49)
		Brick = 40,
		Box = 41,
		Lock = 42,
		Leaf = 43,
		ExclaimBlock = 44,
		PuffBlockMini = 45,
		Spike = 46,
		PuffBlock = 47,
		Conveyor = 48,
		PuffBlockOmni = 49,

		// Solid, Toggled (50 - 55)
		// These can be TileGameObjects because Toggles are global properties, they are not saved by the individual object.
		ToggleBoxBR = 50,
		ToggleBoxGY = 51,
		ToggleBlockBlue = 52,
		ToggleBlockRed = 53,
		ToggleBlockGreen = 54,
		ToggleBlockYellow = 55,

		// Up-Facing Toggled Platforms (56 - 59)
		PlatBlueUp = 56,
		PlatRedUp = 57,
		PlatGreenUp = 58,
		PlatYellowUp = 59,

		// Generators (60 - 64)
		CannonHorizontal = 60,
		CannonVertical = 61,
		CannonDiagonal = 62,
		Placer = 63,

		// Hidden Special Objects (65 - 69)
		TrackDot = 65,

		// Anything below this section has an ObjectID, possibly Update(), and Passive Collision.

		// Tile-Based Creatures (70 - 79)
		Plant = 70,
		ChomperGrass = 71,
		ChomperMetal = 72,
		ChomperFire = 73,

		// Fixed Buttons (80 - 89)
		ButtonFixedBRUp = 80,
		ButtonFixedBRDown = 81,
		ButtonFixedGYUp = 82,
		ButtonFixedGYDown = 83,
		ButtonTimedBRUp = 84,
		ButtonTimedBRDown = 85,
		ButtonTimedGYUp = 86,
		ButtonTimedGYDown = 87,

		// Fixed Springs (90 - 99)
		SpringFixed = 90,
		SpringSide = 91,

		// Character Flags
		BasicCharacter = 100,

		// Rock Platforms (110 - 114)
		PlatformRockUp = 110,
		PlatformRockDown = 111,
		PlatformRockLeft = 112,
		PlatformRockRight = 113,

		// Side-Facing Toggled Platforms (120 - 134)
		PlatBlueDown = 120,
		PlatRedDown = 121,
		PlatGreenDown = 122,
		PlatYellowDown = 123,
		PlatBlueLeft = 124,
		PlatRedLeft = 125,
		PlatGreenLeft = 126,
		PlatYellowLeft = 127,
		PlatBlueRight = 128,
		PlatRedRight = 129,
		PlatGreenRight = 130,
		PlatYellowRight = 131,

		// Misc Platforms (135 - 139)
		PlatformItem = 135,

		// Anything below can only be interacted with by a character:
		// These will have Passive Collision by Character Only.

		// Background Interactives (140 - 149)
		BGDisable = 140,
		BGTap = 141,
		BGWind = 142,

		// Character Interactives (150 - 159)
		Flag = 150,		// TODO: Remove "Flag" from this list; it's been moved to the Flags category (170-174)
		Chest = 151,
		NPC = 152,
		PeekMap = 153,

		Door = 155,
		DoorLock = 156,

		// Collectables (160 - 169)
		Coins = 160,
		Goodie = 161,
		CollectableSuit = 162,
		CollectableHat = 163,
		CollectablePower = 164,

		// Flags (170 - 174)
		CheckFlagFinish = 170,
		CheckFlagCheckpoint = 171,
		CheckFlagPass = 172,
		CheckFlagRetry = 173,
	};

	// List of Game Object Types
	public enum ObjectEnum : byte {

		// Platforms (1 - 4)
		PlatformDip = 1,
		PlatformDelay = 2,
		PlatformFall = 3,
		PlatformMove = 4,

		// Land & Fixed Enemies (10 - 39)
		Moosh = 10,
		Shroom = 11,
		Bug = 12,
		Goo = 13,
		Liz = 14,
		Snek = 15,
		Octo = 17,
		Bones = 18,

		Turtle = 20,
		Snail = 21,
		Boom = 22,

		Poke = 25,
		Lich = 26,
		
		// Flight Enemies (40 - 69)
		Ghost = 40,
		FlairElectric = 41,
		FlairFire = 42,
		FlairPoison = 43,
		FlairNormal = 44,

		ElementalAir = 45,
		ElementalEarth = 46,
		ElementalFire = 47,

		Buzz = 48,

		Saw = 60,
		Slammer = 61,
		HoveringEye = 62,
		Bouncer = 63,

		Dire = 65,

		// Items, Mobile (80 - 99)
		Shell = 80,
		Boulder = 81,
		Bomb = 82,
		OrbItem = 83,
		SportBall = 84,
		TNT = 85,

		SpringHeld = 90,
		ButtonHeld = 91,

		// Special Flags and Placements (100+)
		Character = 100,

		ClusterDot = 150,
	};

}
