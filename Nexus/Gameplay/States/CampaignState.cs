using Newtonsoft.Json;

namespace Nexus.Gameplay {

	public class CampaignJson {

		// Location Data
		public string worldId;			// World ID; e.g. "Scionax", "Astaria", etc.
		public byte zoneId;				// Zone ID (numeric).
		public ushort currentNodeId;	// The current Node # you're standing on.
		public ushort lastNodeId;		// The last Node # you moved from. Important for remembering paths.

		// Character Survival
		public byte lives;             // The number of lives you possess.
		public byte health;            // Health Wounds Available.
		public byte armor;             // Armorw Wounds Available.

		// Character Equipment
		public byte suit;              // Suit Equipped (e.g. SuitType.BlackNinja)
		public byte hat;               // Hat Equipped (e.g. HatType.TopHat)
		public byte powerAtt;          // Attack Power (e.g. AttackPowerType.Chakram)
		public byte powerMob;          // Mobility Power (e.g. MobilityPowerType.Levitate)
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
			this.SetNode();
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

		public void SetNode(ushort nodeId = 0, ushort lastNodeId = 0) {
			this.lastNodeId = lastNodeId == 0 ? this.currentNodeId : lastNodeId;
			this.currentNodeId = nodeId;
		}

		public void SetLives(byte lives = 30) {
			this.lives = lives;
		}

		public void SetWounds(byte health = 0, byte armor = 0) {
			this.health = health;
			this.armor = armor;
		}

		public void SetEquipment(byte suit = 0, byte hat = 0, byte powerAtt = 0, byte powerMob = 0) {
			this.suit = suit;
			this.hat = hat;
			this.powerAtt = powerAtt;
			this.powerMob = powerMob;
		}

		// TODO HIGH PRIORITY.
		// Return TRUE if the level has been completed in this campaign.
		public bool IsLevelWon( ushort nodeId ) {
			// TODO HIGH PRIORITY: isLevelWon() // see CampaignState.ts
			return false;
		}

		public void ProcessLevelCompletion() {
			// TODO HIGH PRIORITY: completedLevel() // see CampaignState.ts
		}


		public void SaveCampaign() {

			// Can only save a campaign if the world ID is assigned correctly.
			if(this.worldId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the world exists; not just that an ID is present.

			CampaignJson campaignJson = new CampaignJson {

				// Location Data
				worldId = this.worldId,
				zoneId = this.zoneId,
				currentNodeId = this.currentNodeId,
				lastNodeId = this.lastNodeId,

				// Character Survival
				lives = this.lives,
				health = this.health,
				armor = this.armor,

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
			this.SetNode(camp.currentNodeId);
			this.SetLives(camp.lives);
			this.SetWounds(camp.health, camp.armor);
			this.SetEquipment(camp.suit, camp.hat, camp.powerAtt, camp.powerMob);
		}
	}
}
