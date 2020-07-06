using Newtonsoft.Json;
using Nexus.ObjectComponents;
using Nexus.Objects;
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
		public byte armor;				// Armor Wounds Available.
		
		// Character Equipment
		public byte suit;				// Suit Equipped (e.g. SuitSubType.BlackNinja)
		public byte hat;				// Hat Equipped (e.g. HatSubType.TopHat)
		public byte shoes;				// Shoes Equipped (e.g. ShoeSubType.Spike)
		public byte powerAtt;			// Attack Power (e.g. AttackPowerSubType.Chakram)
		public byte powerMob;           // Mobility Power (e.g. MobilityPowerSubType.Levitate)

		// Nodes Completed / Status
		public Dictionary<byte, Dictionary<string, CampaignLevelStatus>> levelStatus = new Dictionary<byte, Dictionary<string, CampaignLevelStatus>>();
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
			this.SetUpgrades(0, 0, 0, 0, 0, 0, 0);
			this.SetLevelStatus(new Dictionary<byte, Dictionary<string, CampaignLevelStatus>>());
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

		public void SetUpgrades(byte suit, byte hat, byte shoes, byte att, byte mob, byte health, byte armor) {
			this.suit = suit;
			this.hat = hat;
			this.shoes = shoes;
			this.powerAtt = att;
			this.powerMob = mob;
			this.health = health;
			this.armor = armor;
		}

		public void SetUpgradesByCharacter(Character character) {
			this.SetUpgrades(
				character.suit == null ? (byte) 0 : character.suit.subType,
				character.hat == null ? (byte) 0 : character.hat.subType,
				character.shoes == null ? (byte) 0 : character.shoes.subType,
				character.attackPower == null ? (byte) 0 : character.attackPower.subType,
				character.mobilityPower == null ? (byte) 0 : character.mobilityPower.subType,
				character.wounds.Health,
				character.wounds.Armor
			);
		}

		public void SetLevelStatus(Dictionary<byte, Dictionary<string, CampaignLevelStatus>> levelStatus) {
			this.levelStatus = levelStatus;
		}

		// Return TRUE if the level has been completed in this campaign.
		public bool IsLevelWon( byte zoneId, string levelId ) {
			if(!this.levelStatus.ContainsKey(zoneId)) { return false; }
			if(this.levelStatus[zoneId].ContainsKey(levelId)) {
				return this.levelStatus[zoneId][levelId].won;
			}
			return false;
		}

		public void ProcessLevelCompletion( string levelId ) {

			// Make sure the zone exists in this dictionary:
			if(!this.levelStatus.ContainsKey(this.zoneId)) {
				this.levelStatus.Add(this.zoneId, new Dictionary<string, CampaignLevelStatus>());
			}

			// Mark the level as completed:
			if(!this.levelStatus[this.zoneId].ContainsKey(levelId)) {
				this.levelStatus[this.zoneId].Add(levelId, new CampaignLevelStatus());
			}

			this.levelStatus[this.zoneId][levelId].won = true;

			// Reposition Character on World


			// Save Campaign (to Local Storage)
			this.SaveCampaign();
		}

		public void SaveCampaign() {

			// Verify that the world exists. Otherwise, cannot save the campaign.
			if(!WorldContent.WorldExists(this.worldId)) { return; }

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
				shoes = this.shoes,
				powerAtt = this.powerAtt,
				powerMob = this.powerMob,
				
				// Nodes Completed / Status
				levelStatus = this.levelStatus,
			};

			// Save State
			string json = JsonConvert.SerializeObject(campaignJson);
			this.handler.GameStateWrite("Campaign/" + this.worldId, json);
		}

		public void LoadCampaign( string worldId, StartNodeFormat start ) {
			string json = this.handler.GameStateRead("Campaign/" + worldId);

			// If there is no JSON content, load an empty state:
			if(json == "") {

				this.Reset();
				this.worldId = worldId;

				// Starting Details
				if(start != null) {
					this.head = start.character;
					this.curX = start.x;
					this.curY = start.y;
					this.zoneId = start.zoneId;
				} else {
					this.head = 1;
					this.curX = 3;
					this.curY = 3;
					this.zoneId = 0;
				}

				return;
			}

			CampaignJson campaign = JsonConvert.DeserializeObject<CampaignJson>(json);

			this.SetWorld(campaign.worldId);
			this.SetZone(campaign.zoneId);
			this.SetPosition(campaign.curX, campaign.curY, campaign.lastDir);
			this.SetLives(campaign.lives);
			this.SetWounds(campaign.health, campaign.armor);
			this.SetHead(campaign.head);
			this.SetUpgrades(campaign.suit, campaign.hat, campaign.shoes, campaign.powerAtt, campaign.powerMob, campaign.health, campaign.armor);
			this.SetLevelStatus(campaign.levelStatus);
		}
	}
}
