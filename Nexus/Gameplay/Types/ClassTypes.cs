﻿
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
		Wall = 10,
		Log = 11,

		// Non-Solid, Toggled Blocks (15 - 18)
		ToggleBlockBlueOff = 15,
		ToggleBlockRedOff = 16,
		ToggleBlockGreenOff = 17,
		ToggleBlockYellowOff = 18,

		// Ledges (20 - 29)
		LedgeGrass = 20,
		LedgeGrassDecor = 21,
		PlatformFixed = 25,
		PlatformItem = 26,

		// Decor, Prompts (30 - 39)
		DecorVeg = 30,
		DecorDesert = 31,
		DecorCave = 32,
		DecorWater = 33,
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
		// ...
		Spike = 46,
		PuffBlock = 47,
		Conveyor = 48,

		// Solid, Toggled (50 - 55)
		// These can be TileGameObjects because Toggles are global properties, they are not saved by the individual object.
		ToggleBoxBR = 50,
		ToggleBoxGY = 51,
		ToggleBlockBlue = 52,
		ToggleBlockRed = 53,
		ToggleBlockGreen = 54,
		ToggleBlockYellow = 55,

		// Solid, Toggled Platforms (56 - 59)
		TogglePlatBlue = 56,
		TogglePlatRed = 57,
		TogglePlatGreen = 58,
		TogglePlatYellow = 59,

		// Generators (60 - 64)
		CannonHorizontal = 60,
		PlacerHorizontal = 61,
		CannonVertical = 62,
		PlacerVertical = 63,
		CannonDiagonal = 64,

		// Non-Solid, Toggled Platforms (65 - 69)
		TogglePlatBlueOff = 65,
		TogglePlatRedOff = 66,
		TogglePlatGreenOff = 67,
		TogglePlatYellowOff = 68,

		// Anything below this section has an ObjectID, possibly Update(), and Passive Collision.

		// Tile-Based Creatures (70 - 79)
		Plant = 70,
		ChomperGrass = 71,
		ChomperMetal = 72,
		ChomperFire = 73,

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

		// Tracks (5 - 9)
		Cluster = 5,
		Track = 6,

		// Land & Fixed Enemies (10 - 39)
		Moosh = 10,
		Shroom = 11,
		Bug = 12,
		Goo = 13,
		Liz = 14,
		Snek = 15,
		Wurm = 16,
		Octo = 17,
		Bones = 18,

		Turtle = 20,
		Snail = 21,
		Boom = 22,

		Poke = 25,
		Lich = 26,

		Chomper = 30,
		ChomperFire = 31,
		Plant = 32,

		// Flight Enemies (40 - 69)
		Ghost = 40,
		FlairElectric = 41,
		FlairFire = 42,
		FlairMagic = 43,

		ElementalAir = 45,
		ElementalEarth = 46,
		ElementalFire = 47,

		Buzz = 48,

		Saw = 60,
		Slammer = 61,
		ElementalEye = 62,
		WallBouncer = 63,

		Dire = 65,

		// Items, Fixed (70 - 79)
		SpringFixed = 70,
		ButtonFixed = 71,

		// Items, Mobile (80 - 99)
		Shell = 80,
		Boulder = 81,
		Bomb = 82,

		TNT = 85,

		SpringStandard = 90,
		ButtonStandard = 91,
		ButtonTimed = 92,

		MobileBlockBlue = 95,
		MobileBlockRed = 96,
		MobileBlockGreen = 97,
		MobileBlockYellow = 98,

		// Special Flags and Placements (100+)
		Character = 100,
	};

}
