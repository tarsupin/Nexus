using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class Params {
		public Dictionary<string, ParamGroup> rules = new Dictionary<string, ParamGroup>();
		public Params() {}
	}

	// Params TODO: 
	//		- ParamsCluster
	//		- ParamsClusterChar
	//		- ParamsClusterScreen
	//		- ParamsContains (combine dictionary with system)
	//		- ParamsMoveFlight (advanced mechanics)
	//		- ParamsPlacer (requires dictionary, like Contains)

	public class ParamsAttackBolt : Params {

		public ParamsAttackBolt() {
			this.rules.Add("count", new IntParam("attCount", "Number of Bolts", 1, 3, 1, 1));                               // Number of bolts that gets shot simultaneously (1 to 3)
			this.rules.Add("speed", new PercentParam("attSpeed", "Bolt Speed", 20, 200, 10, 100, FInt.Create(4)));          // Velocity of the bolts (Y-axis).
			this.rules.Add("spread", new PercentParam("attSpread", "Bolt Spread", 50, 200, 10, 100, FInt.Create(0.3)));     // The % spread between each bolt.
			this.rules.Add("cycle", new IntParam("attCycle", "Attack Frequency", 60, 300, 15, 120, " frames"));             // Frequency of the attack (in frames)
			this.rules.Add("offset", new IntParam("attOffset", "Timer Offset", 0, 300, 15, 0, " frames"));                  // The offset of the frequency on the global timer.
		}
	}

	public class ParamsAttackEarth : Params {

		public ParamsAttackEarth() {
			this.rules.Add("speed", new PercentParam("attSpeed", "Stone Speed", 20, 200, 10, 100, FInt.Create(4)));         // Velocity of the stones (Y-axis).
			this.rules.Add("cycle", new IntParam("attCycle", "Attack Frequency", 60, 300, 15, 120, " frames"));             // Frequency of the attack (in frames)
			this.rules.Add("offset", new IntParam("attOffset", "Timer Offset", 0, 300, 15, 0, " frames"));                  // The offset of the frequency on the global timer.
		}
	}

	public class ParamsBeats : Params {

		public ParamsBeats() {

			// Every second has four beats. This indicates which it should trigger at, for two seconds worth of beats.
			this.rules.Add("beat1", new IntParam("beat1", "Beat #1", 0, 7, 1, 0));
			this.rules.Add("beat2", new IntParam("beat2", "Beat #2", 0, 7, 1, 0));
			this.rules.Add("beat3", new IntParam("beat3", "Beat #3", 0, 7, 1, 0));
			this.rules.Add("beat4", new IntParam("beat4", "Beat #4", 0, 7, 1, 0));
		}
	}

	public class ParamsCollectable : Params {

		public ParamsCollectable() {

			// The Collectable Rule governs how the collectable behaves after collection; this can affect multiplayer, reuse, etc.
			this.rules.Add("rule", new LabeledParam("beat1", "Collectable Rule", new string[4] { "One Use Only", "One Per Player", "Always Available", "Regenerates After Use" }, (byte)CollectableRule.OneUseOnly));

			// Regen only applies if the rule is set to "Regenerates After Use"
			this.rules.Add("regen", new IntParam("regen", "Regeneration Time", 0, 60, 1, 0));
		}
	}

	public class ParamsDoor : Params {

		public ParamsDoor() {

			// The room # that the door will take you to.
			this.rules.Add("room", new IntParam("room", "Room Destination", 0, 9, 1, 0, ""));

			// Determines what type of door you'll enter to (or to a checkpoint)
			this.rules.Add("exit", new DictionaryParam("exit", "Exit Type", new Dictionary<byte, string>() {
				{ (byte) DoorExitType.ToSameColor, "To Same Door Color" },
				{ (byte) DoorExitType.ToOpenDoor, "To Open Doorway" },
				{ (byte) DoorExitType.ToCheckpoint, "To Checkpoint" },
			}, (byte)DoorExitType.ToSameColor));
		}
	}

	public class ParamsEmblem : Params {

		public ParamsEmblem() {
			this.rules.Add("color", new LabeledParam("color", "Emblem Color", new string[5] { "None", "Blue", "Red", "Green", "Yellow" }, (byte) 0));
			this.rules.Add("on", new BoolParam("on", "Active at Start", false));
		}
	}

	public class ParamsFireBurst : Params {

		// TODO: Need to account for X and Y directions of Fire Burst Params
		// if(dirFacing == DirCardinal.Left) { xSpeed = -8 * speedMult; }
		// else if(dirFacing == DirCardinal.Right) { xSpeed = 8 * speedMult; }
		// else if(dirFacing == DirCardinal.Down) { ySpeed = 4 * speedMult; }
		// else { ySpeed = (-8 * speedMult) - 4; }

		public ParamsFireBurst() {
			this.rules.Add("count", new IntParam("attCount", "Gravity Influence", 1, 3, 1, 1));                                 // The percent that gravity influences the fire.
			this.rules.Add("count", new IntParam("attCount", "Number of Fireballs", 1, 3, 1, 1));                               // Number of bolts that gets shot simultaneously (1 to 3)
			this.rules.Add("speed", new PercentParam("attSpeed", "Fireball Speed", 20, 200, 10, 100, FInt.Create(4)));          // Velocity of the bolts (Y-axis).
			this.rules.Add("spread", new PercentParam("attSpread", "Fireball Spread", 50, 250, 10, 100, FInt.Create(0.3)));     // The % spread between each bolt.
			this.rules.Add("cycle", new IntParam("attCycle", "Attack Frequency", 60, 300, 15, 120, " frames"));                 // Frequency of the attack (in frames)
			this.rules.Add("offset", new IntParam("attOffset", "Timer Offset", 0, 300, 15, 0, " frames"));                      // The offset of the frequency on the global timer.
		}
	}

	public class ParamsMoveBounce : Params {

		// This applies to the bouncing movement creature.
		public ParamsMoveBounce() {
			this.rules.Add("x", new IntParam("x", "X Movement", -6, 6, 1, 2, " tiles(s)"));
			this.rules.Add("y", new IntParam("y", "Y Movement", -6, 6, 1, 2, " tiles(s)"));
		}
	}

	public class ParamsMoveChase : Params {

		// This applies to chasing creatures.
		public ParamsMoveChase() {
			this.rules.Add("axis", new LabeledParam("axis", "Movement Axis", new string[3] { "Both", "Vertical", "Horizontal" }, (byte) FlightChaseAxis.Both));
			this.rules.Add("speed", new PercentParam("speed", "Movement Speed", 10, 200, 10, 10, FInt.Create(2)));
			this.rules.Add("chase", new IntParam("chase", "Chase Range", 0, 40, 1, 10, " tile(s)"));
			this.rules.Add("flee", new IntParam("flee", "Flee Range", 0, 40, 1, 0, " tile(s)"));
			this.rules.Add("stall", new IntParam("stall", "Stall Range", 0, 40, 1, 8, " tile(s)"));
			this.rules.Add("returns", new BoolParam("returns", "Returns to Start", true));
			this.rules.Add("retDelay", new IntParam("retDelay", "Delay for Returning", 0, 300, 15, 120, " frames"));
		}
	}

	public class ParamsShell : Params {

		// This applies to the bouncing movement creature.
		public ParamsShell() {
			this.rules.Add("x", new IntParam("x", "X Movement", -7, 7, 1, 0, " tiles(s)"));
			this.rules.Add("y", new IntParam("y", "Y Movement", -12, 7, 1, 0, " tiles(s)"));
		}
	}

	public class ParamsMoveTrack : Params {

		public ParamsMoveTrack() {
			this.rules.Add("trackNum", new IntParam("trackNum", "Track ID", 0, 99, 1, 0, ""));
			this.rules.Add("to", new IntParam("to", "Goes to Track ID", 0, 99, 1, 0, ""));
			this.rules.Add("duration", new IntParam("duration", "Travel Duration", 60, 3600, 15, 180, " frames"));
			this.rules.Add("delay", new IntParam("delay", "Departure Delay", 0, 3600, 15, 0, " frames"));
			this.rules.Add("beginFall", new BoolParam("beginFall", "Falls On Arrival", false));
		}
	}


}
