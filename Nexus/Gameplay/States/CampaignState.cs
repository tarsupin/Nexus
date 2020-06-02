using Newtonsoft.Json;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class CampaignLevelStatus {
		public bool won = false;	// TRUE if the level has been completed.
		// public bool secret;		// May eventually allow secret exits.
	}

	public class CampaignJson {

		// Location Data
		public string worldId;			// World ID; e.g. "Scionax", "Astaria", etc.
		public byte zoneId;				// Zone ID (numeric).
		public byte curX;               // The current gridX position.
		public byte curY;				// The current gridY position.
		public byte lastDir;			// The last direction you moved from. Important for remembering paths.

		// Character Nature
		public byte head;               // Head Equipped (e.g. HeadSubType.Ryu)

		// Character Survival
		public byte lives;				// The number of lives you possess.
		public byte health;				// Health Wounds Available.
		public byte armor;				// Armorw Wounds Available.
		
		// Character Equipment
		public byte suit;				// Suit Equipped (e.g. SuitType.BlackNinja)
		public byte hat;				// Hat Equipped (e.g. HatType.TopHat)
		public byte powerAtt;			// Attack Power (e.g. AttackPowerType.Chakram)
		public byte powerMob;           // Mobility Power (e.g. MobilityPowerType.Levitate)

		// Nodes Completed / Status
		protected Dictionary<byte, Dictionary<string, CampaignLevelStatus>> levelStatus = new Dictionary<byte, Dictionary<string, CampaignLevelStatus>>();
	}

	public class CampaignState : CampaignJson {

		// References
		private readonly GameHandler handler;

		public CampaignState(GameHandler handler) {
			this.handler = handler;
			this.Reset();
		}

		public void Reset() {
			this.SetWorld();
			this.SetZone();
			this.SetPosition(0, 0, (byte) DirCardinal.None);
			this.SetLives();
			this.SetWounds();
			this.SetEquipment();
		}

		public void SetWorld(string worldId = "") {
			this.worldId = worldId;
		}

		public void SetZone(byte zone = 0) {
			this.zoneId = zone;
		}

		public void SetPosition(byte gridX, byte gridY, byte lastDir) {
			this.curX = gridX;
			this.curY = gridY;
			this.lastDir = lastDir;
		}

		public void SetLives(byte lives = 30) {
			this.lives = lives;
		}

		public void SetWounds(byte health = 0, byte armor = 0) {
			this.health = health;
			this.armor = armor;
		}

		public void SetHead(byte head = (byte) HeadSubType.RyuHead) {
			this.head = head;
		}

		public void SetEquipment(byte suit = 0, byte hat = 0, byte powerAtt = 0, byte powerMob = 0) {
			this.suit = suit;
			this.hat = hat;
			this.powerAtt = powerAtt;
			this.powerMob = powerMob;
		}

		// Return TRUE if the level has been completed in this campaign.
		public bool IsLevelWon( byte zoneId, string levelId ) {
			if(!this.levelStatus.ContainsKey(zoneId)) { return false; }
			if(this.levelStatus[zoneId].ContainsKey(levelId)) {
				return this.levelStatus[zoneId][levelId].won;
			}
			return false;
		}

		public void ProcessLevelCompletion( byte zoneId, string levelId ) {

			// Make sure the zone exists in this dictionary:
			if(!this.levelStatus.ContainsKey(zoneId)) {
				this.levelStatus.Add(zoneId, new Dictionary<string, CampaignLevelStatus>());
			}

			var levels = this.levelStatus[zoneId];

			if(!levels.ContainsKey(levelId)) {
				levels.Add(levelId, new CampaignLevelStatus());
			}

			levels[levelId].won = true;

			// Save Campaign (to Local Storage)
			this.SaveCampaign();
		}

		public void SaveCampaign() {

			// Can only save a campaign if the world ID is assigned correctly.
			if(this.worldId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the world exists; not just that an ID is present.

			CampaignJson campaignJson = new CampaignJson {

				// Location Data
				worldId = this.worldId,
				zoneId = this.zoneId,
				curX = this.curX,
				curY = this.curY,
				lastDir = this.lastDir,

				// Character Survival
				lives = this.lives,
				health = this.health,
				armor = this.armor,
				
				// Character Nature
				head = this.head,

				// Character Equipment
				suit = this.suit,
				hat = this.hat,
				powerAtt = this.powerAtt,
				powerMob = this.powerMob,
			};

			// Save State
			string json = JsonConvert.SerializeObject(campaignJson);
			this.handler.GameStateWrite("Campaign", json);
		}

		public void LoadCampaign() {
			string json = this.handler.GameStateRead("Campaign");

			// If there is no JSON content, load an empty state:
			if(json == "") {
				this.Reset();
				return;
			}

			CampaignJson camp = JsonConvert.DeserializeObject<CampaignJson>(json);

			this.SetWorld(camp.worldId);
			this.SetZone(camp.zoneId);
			this.SetPosition(camp.curX, camp.curY, camp.lastDir);
			this.SetLives(camp.lives);
			this.SetWounds(camp.health, camp.armor);
			this.SetHead(camp.head);
			this.SetEquipment(camp.suit, camp.hat, camp.powerAtt, camp.powerMob);
		}
	}
}
