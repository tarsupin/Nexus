
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class WCReset {

		public static void ResetOptions() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.PrepareTabLookup(resetCodes, currentIns, "Reset options for this world.");

			if(resetCodes.ContainsKey(currentIns)) {
				resetCodes[currentIns].Invoke();
				return;
			}
		}

		public static readonly Dictionary<string, System.Action> resetCodes = new Dictionary<string, System.Action>() {
			{ "position", WCReset.ResetPosition },
			{ "world", WCReset.ResetWorld },
		};

		public static void ResetWorld() {
			ConsoleTrack.possibleTabs = "Example: `reset world`";
			ConsoleTrack.helpText = "Reset the entire world, losing any progress made.";

			if(ConsoleTrack.activate) {
				WorldScene scene = (WorldScene)Systems.scene;
				CampaignState campaign = scene.campaign;
				StartNodeFormat start = scene.worldData.start;

				// Adjust Campaign Level Status
				campaign.levelStatus = new Dictionary<byte, Dictionary<string, CampaignLevelStatus>>();

				// Reload Campaign State
				campaign.SetWorld(campaign.worldId);
				campaign.SetZone(campaign.zoneId);
				campaign.SetPosition(campaign.curX, campaign.curY, campaign.lastDir);
				campaign.SetLives(campaign.lives);
				campaign.SetWounds(campaign.health, campaign.armor);
				campaign.SetHead(campaign.head);
				campaign.SetUpgrades(campaign.suit, campaign.hat, campaign.powerAtt, campaign.powerMob, campaign.health, campaign.armor);
				campaign.SetLevelStatus(campaign.levelStatus);

				// Update Campaign Positions
				campaign.lastDir = (byte) DirCardinal.None;
				campaign.curX = start.x;
				campaign.curY = start.y;
				campaign.zoneId = start.zoneId;

				// Update Character
				scene.character.SetCharacter(campaign);
			}
		}

		public static void ResetPosition() {
			ConsoleTrack.possibleTabs = "Example: `reset position`";
			ConsoleTrack.helpText = "Reset your position to the original start point.";

			if(ConsoleTrack.activate) {
				WorldScene scene = (WorldScene)Systems.scene;
				StartNodeFormat start = scene.worldData.start;
				CampaignState campaign = scene.campaign;

				// Update Campaign Positions
				campaign.lastDir = (byte)DirCardinal.None;
				campaign.curX = start.x;
				campaign.curY = start.y;
				campaign.zoneId = start.zoneId;

				// Update Character
				scene.character.SetCharacter(campaign);
			}
		}
	}
}
