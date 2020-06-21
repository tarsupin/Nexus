
using Newtonsoft.Json;
using Nexus.Engine;
using Nexus.Objects;
using System;

namespace Nexus.Gameplay {

	public class FlagJson {
		public bool active;			// True if flag is marked as active.
		public byte roomId;			// # of the room (0 to 9)
		public short gridX;			// The GridX position of the flag.
		public short gridY;			// The GridY position of the flag.
	}

	public class LevelJson {

		// Level Data
		public string levelId;			// Level ID (e.g. "QCALQOD16")
		public byte roomId;				// Room ID (0 to 9)

		// Tracking
		public short coins;				// # of coins currently gathered.

		// Timer
		public int frameStarted;		// The frame when the timer started.
		public int timeShift;			// The number of frames added or removed from the timer (for when timer collectables are acquired).

		// Locations
		public FlagJson checkpoint;
		public FlagJson retryFlag;
	}

	public class LevelState : LevelJson {

		// References
		private readonly GameHandler handler;
		private readonly TimerGlobal timer;

		public LevelState(GameHandler handler) {
			this.handler = handler;
			this.timer = Systems.timer;
			
			// Build Flags
			this.checkpoint = new FlagJson {
				active = false,
				roomId = 0,
				gridX = 0,
				gridY = 0,
			};

			this.retryFlag = new FlagJson {
				active = false,
				roomId = 0,
				gridX = 0,
				gridY = 0,
			};

			// Reset Level
			this.FullReset();
		}

		public void SetLevel(string levelId, byte roomId = 0) {
			this.levelId = levelId;
			this.SetRoom(roomId);
			this.FullReset();
		}

		// Complete Level
		public void CompleteLevel() {
			this.FullReset();
		}

		// Time Reset
		public void TimerReset() {
			this.timer.ResetTimer();
			this.frameStarted = this.timer.Frame;
			this.timeShift = 0;
		}

		// Time Elapsed and Remaining
		public int FramesElapsed { get { return this.timer.Frame - this.frameStarted; } }
		public short TimeElapsed { get { return (short) Math.Ceiling((double) this.FramesElapsed / 60); } }
		public int FramesRemaining { get { return (300 * 60) + this.frameStarted + this.timeShift - this.timer.Frame; } }
		public short TimeRemaining { get { return (short) Math.Ceiling((double) this.FramesRemaining / 60); } }

		// Performs a Full Level Reset (back to beginning, lose all checkpoints)
		public void FullReset() {
			this.SetRoom();
			this.ResetFlags();
			this.SoftReset();
		}

		public void SetRoom(byte roomId = 0) {
			this.roomId = 0;
		}

		// Resets Level to Last Flag
		public void SoftReset() {
			this.TimerReset();
			this.SetCoins(null);
		}

		public void ResetFlags() {
			this.checkpoint.active = false;
			this.retryFlag.active = false;
		}

		// Coins must identify the Character, since some levels will distribute coins directly to characters.
		public virtual void SetCoins( Character character, short coins = 0) { this.coins = coins; }
		public virtual void AddCoins( Character character, short coins = 0) { this.coins += coins; }

		// Set Checkpoint by "Flag" object
		public bool SetCheckpoint(byte roomId, short gridX, short gridY) {

			// Return FALSE if this checkpoint is already active.
			if(this.checkpoint.gridX == gridX && this.checkpoint.gridY == gridY && this.checkpoint.active == true) { return false; }

			this.checkpoint.active = true;
			this.checkpoint.gridX = gridX;
			this.checkpoint.gridY = gridY;
			this.checkpoint.roomId = roomId;

			// If there is a retry-flag active, must unset it:
			this.retryFlag.active = false;

			return true;
		}

		// Set SetRetry by "Flag" object
		public bool SetRetry(byte roomId, short gridX, short gridY) {

			// Return FALSE if this retry flag is already active.
			if(this.retryFlag.gridX == gridX && this.retryFlag.gridY == gridY && this.retryFlag.active == true) { return false; }

			this.retryFlag.active = true;
			this.retryFlag.gridX = gridX;
			this.retryFlag.gridY = gridY;
			this.retryFlag.roomId = roomId;

			return true;
		}

		public void SaveLevelState() {

			// Can only save a level state if the level ID is assigned correctly.
			if(this.levelId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the levelId exists; not just that an ID is present.

			LevelJson levelJson = new LevelJson {
				levelId = this.levelId,
				roomId = this.roomId,
				coins = this.coins,
				timeShift = this.timeShift,
				checkpoint = this.checkpoint,
				retryFlag = this.retryFlag,
			};

			// Save State
			string json = JsonConvert.SerializeObject(levelJson);
			this.handler.GameStateWrite("Level", json);
		}

		public void LoadLevelState() {
			string json = this.handler.GameStateRead("Level");

			// If there is no JSON content, load an empty state:
			if(json == "") {
				this.FullReset();
				return;
			}

			LevelJson level = JsonConvert.DeserializeObject<LevelJson>(json);

			this.SetLevel(level.levelId, level.roomId);
			this.SetCoins(null, level.coins);
			this.SetCheckpoint(level.checkpoint.roomId, level.checkpoint.gridX, level.checkpoint.gridY);
			this.SetRetry(level.checkpoint.roomId, level.checkpoint.gridX, level.checkpoint.gridY);
			this.timeShift = level.timeShift;
		}
	}
}
