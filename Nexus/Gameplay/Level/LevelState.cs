
using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class FlagJson {
		public bool active;             // True if flag is marked as active.
		public byte roomId;             // # of the room (0 to 9)
		public ushort gridX;            // The GridX position of the flag.
		public ushort gridY;			// The GridY position of the flag.
	}

	public class LevelJson {

		// Level Data
		public string levelId;			// Level ID (e.g. "QCALQOD16")
		public byte roomId;             // Room ID (0 to 9)

		// Tracking
		public ushort coins;			// # of coins currently gathered.

		// Timer
		public int timeShift;           // The number of frames added or removed from the timer (for when timer collectables are acquired).

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
			this.FullLevelReset();
		}

		public void SetLevel(string levelId, byte roomId = 0) {
			this.levelId = levelId;
			this.SetRoom(roomId);
			this.FullLevelReset();
		}

		// Complete Level
		public void CompleteLevel() {
			this.FullLevelReset();
		}

		public void Die() {
			this.LevelReset();
		}

		// TODO HIGH PRIORITY: Time Reset (see LevelState.ts)
		public void TimerReset() {
			this.timer.ResetTimer();
			this.timeShift = 0;
		}

		// TODO HIGH PRIORITY: Frames Remaining (see LevelState.ts)
		// TODO HIGH PRIORITY: Time Remaining (track by 60 frames) (see LevelState.ts)

		// Performs a Full Level Reset (to the beginning)
		public void FullLevelReset() {
			this.SetRoom();
			this.ResetFlags();
			this.LevelReset();
		}

		public void SetRoom(byte roomId = 0) {
			this.roomId = 0;
		}

		// Resets Level to Last Flag
		public void LevelReset() {
			this.TimerReset();
			this.SetCoins();
			this.ResetFlags();
		}

		public void ResetFlags() {
			this.checkpoint.active = false;
			this.retryFlag.active = false;
		}

		public void SetCoins(ushort coins = 0) { this.coins = coins; }
		public void AddCoins(ushort coins = 0) { this.coins += coins; }

		// TODO HIGH PRIORITY: Set Checkpoint by "Flag" object
		public void SetCheckpoint() {
			//this.checkpoint.active = flag.id;
			//this.checkpoint.gridX = flag.gridX;
			//this.checkpoint.gridY = flag.gridY;
			this.checkpoint.roomId = this.roomId;

			// If there is a retry-flag active, must unset it:
			this.retryFlag.active = false;
		}

		// TODO HIGH PRIORITY: Set SetRetry by "Flag" object
		public void SetRetry() {
			//this.retryFlag.active = flag.id;
			//this.retryFlag.gridX = flag.gridX;
			//this.retryFlag.gridY = flag.gridY;
			this.retryFlag.roomId = this.roomId;
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
				this.FullLevelReset();
				return;
			}

			LevelJson level = JsonConvert.DeserializeObject<LevelJson>(json);

			this.SetLevel(level.levelId, level.roomId);
			this.SetCoins(level.coins);
			this.SetCheckpoint(); // set to level.checkpoint
			this.SetRetry(); // set to level.retryFlag
			this.timeShift = level.timeShift;
		}
	}
}
