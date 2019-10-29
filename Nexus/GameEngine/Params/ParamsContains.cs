using Newtonsoft.Json.Linq;
using static Nexus.Objects.CollectableHat;
using static Nexus.Objects.CollectablePower;
using static Nexus.Objects.CollectableSuit;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	interface IParamsContains {
		string Object { get; set; }					// The "Object/Subtype" string that is contained.
	}

	public class IMechanicsContains {
		public byte ClassID { get; set; }			// The Class ID that identifies the object class.
		public byte SubType { get; set; }			// The SubType ID, relative to the object's class.
	}

	//public static class ParamsContainsRules {
	//}

	public static class ParamsContains {

		public static IMechanicsContains ConvertToMechanics(JObject paramsList) {
			IMechanicsContains mechanics = new IMechanicsContains();

			// Split the Object/Subtype string, so that we can determine the class type and subtype separately.
			string[] split = paramsList["tile"].ToString().Split('/');

			string objectName = split[0];
			string subTypeName = split[1];

			mechanics.SubType = 0;

			// Goodie
			if(objectName == "Goodie") {

				switch(subTypeName) {
					case "Apple": mechanics.SubType = (byte) GoodieSubType.Apple; break;
					case "Pear": mechanics.SubType = (byte) GoodieSubType.Pear; break;
					case "Heart": mechanics.SubType = (byte) GoodieSubType.Heart; break;
					case "Shield": mechanics.SubType = (byte) GoodieSubType.Shield; break;
					case "ShieldPlus": mechanics.SubType = (byte) GoodieSubType.ShieldPlus; break;

					case "Guard": mechanics.SubType = (byte) GoodieSubType.Guard; break;
					case "GuardPlus": mechanics.SubType = (byte) GoodieSubType.GuardPlus; break;

					case "Shiny": mechanics.SubType = (byte) GoodieSubType.Shiny; break;
					case "Stars": mechanics.SubType = (byte) GoodieSubType.Stars; break;
					case "GodMode": mechanics.SubType = (byte) GoodieSubType.GodMode; break;

					case "Plus5": mechanics.SubType = (byte) GoodieSubType.Plus5; break;
					case "Plus10": mechanics.SubType = (byte) GoodieSubType.Plus10; break;
					case "Plus20": mechanics.SubType = (byte) GoodieSubType.Plus20; break;
					case "Set5": mechanics.SubType = (byte) GoodieSubType.Set5; break;
					case "Set10": mechanics.SubType = (byte) GoodieSubType.Set10; break;
					case "Set20": mechanics.SubType = (byte) GoodieSubType.Set20; break;

					case "Disrupt": mechanics.SubType = (byte) GoodieSubType.Disrupt; break;
					case "Explosive": mechanics.SubType = (byte) GoodieSubType.Explosive; break;
					case "Key": mechanics.SubType = (byte) GoodieSubType.Key; break;
					case "Blood": mechanics.SubType = (byte) GoodieSubType.Blood; break;
				}
			}

			// TODO HIGH PRIORITY: Verify that params are being saved as "Hat/SubType" and not "CollectableHat/SubType"
			// Hats
			else if(objectName == "Hat") {

				switch(subTypeName) {
					case "RandomHat": mechanics.SubType = (byte) HatSubType.RandomHat; break;

					case "AngelHat": mechanics.SubType = (byte) HatSubType.AngelHat; break;
					case "BambooHat": mechanics.SubType = (byte) HatSubType.BambooHat; break;
					case "CowboyHat": mechanics.SubType = (byte) HatSubType.CowboyHat; break;
					case "FeatheredHat": mechanics.SubType = (byte) HatSubType.FeatheredHat; break;
					case "FedoraHat": mechanics.SubType = (byte) HatSubType.FedoraHat; break;
					case "HardHat": mechanics.SubType = (byte) HatSubType.HardHat; break;
					case "RangerHat": mechanics.SubType = (byte) HatSubType.RangerHat; break;
					case "SpikeyHat": mechanics.SubType = (byte) HatSubType.SpikeyHat; break;
					case "TopHat": mechanics.SubType = (byte) HatSubType.TopHat; break;
				}
			}

			// TODO HIGH PRIORITY: Verify that params are being saved as "Power/SubType" and not "CollectablePower/SubType"
			// Powers
			else if(objectName == "Power") {

				switch(subTypeName) {

					// Collectable Powers - Mobility
					case "RandBook": mechanics.SubType = (byte) PowerSubType.RandBook; break;
					case "SlowFall": mechanics.SubType = (byte) PowerSubType.SlowFall; break;
					case "Hover": mechanics.SubType = (byte) PowerSubType.Hover; break;
					case "Levitate": mechanics.SubType = (byte) PowerSubType.Levitate; break;
					case "Flight": mechanics.SubType = (byte) PowerSubType.Flight; break;
					case "Athlete": mechanics.SubType = (byte) PowerSubType.Athlete; break;
					case "Leap": mechanics.SubType = (byte) PowerSubType.Leap; break;
					case "Slam": mechanics.SubType = (byte) PowerSubType.Slam; break;
					case "Burst": mechanics.SubType = (byte) PowerSubType.Burst; break;
					case "Air": mechanics.SubType = (byte) PowerSubType.Air; break;
					case "Phase": mechanics.SubType = (byte) PowerSubType.Phase; break;
					case "Teleport": mechanics.SubType = (byte) PowerSubType.Teleport; break;

					// Collectable Powers - Weapon
					case "RandWeapon": mechanics.SubType = (byte) PowerSubType.RandWeapon; break;
					case "BoxingRed": mechanics.SubType = (byte) PowerSubType.BoxingRed; break;
					case "BoxingWhite": mechanics.SubType = (byte) PowerSubType.BoxingWhite; break;
					case "Dagger": mechanics.SubType = (byte) PowerSubType.Dagger; break;
					case "DaggerGreen": mechanics.SubType = (byte) PowerSubType.DaggerGreen; break;
					case "Spear": mechanics.SubType = (byte) PowerSubType.Spear; break;
					case "Sword": mechanics.SubType = (byte) PowerSubType.Sword; break;

					// Collectable Powers - Potion
					case "RandPot": mechanics.SubType = (byte) PowerSubType.RandPot; break;
					case "Electric": mechanics.SubType = (byte) PowerSubType.Electric; break;
					case "Fire": mechanics.SubType = (byte) PowerSubType.Fire; break;
					case "Frost": mechanics.SubType = (byte) PowerSubType.Frost; break;
					case "Rock": mechanics.SubType = (byte) PowerSubType.Rock; break;
					case "Water": mechanics.SubType = (byte) PowerSubType.Water; break;
					case "Slime": mechanics.SubType = (byte) PowerSubType.Slime; break;
					case "Ball": mechanics.SubType = (byte) PowerSubType.Ball; break;

					// Collectable Powers - Thrown
					case "RandThrown": mechanics.SubType = (byte) PowerSubType.RandThrown; break;
					case "Axe": mechanics.SubType = (byte) PowerSubType.Axe; break;
					case "Hammer": mechanics.SubType = (byte) PowerSubType.Hammer; break;
					case "Shuriken": mechanics.SubType = (byte) PowerSubType.Shuriken; break;

					// Power Collectable - Bolts
					case "RandBolt": mechanics.SubType = (byte) PowerSubType.RandBolt; break;
					case "Bolt": mechanics.SubType = (byte) PowerSubType.BoltBlue; break;
					case "BoltGold": mechanics.SubType = (byte) PowerSubType.BoltGold; break;
					case "BoltGreen": mechanics.SubType = (byte) PowerSubType.BoltGreen; break;
					case "BoltNecro": mechanics.SubType = (byte) PowerSubType.BoltNecro; break;
					case "Necro1": mechanics.SubType = (byte) PowerSubType.Necro1; break;
					case "Necro2": mechanics.SubType = (byte) PowerSubType.Necro2; break;

					// Collectable Powers - Stack
					case "Chakram": mechanics.SubType = (byte) PowerSubType.Chakram; break;
					case "ChakramPack": mechanics.SubType = (byte) PowerSubType.ChakramPack; break;
					case "Grenade": mechanics.SubType = (byte) PowerSubType.Grenade; break;
					case "GrenadePack": mechanics.SubType = (byte) PowerSubType.GrenadePack; break;
				}
			}

			// TODO HIGH PRIORITY: Verify that params are being saved as "Suit/SubType" and not "CollectableSuit/SubType"
			// Suits
			else if(objectName == "Suit") {

				switch(subTypeName) {
					case "RandomSuit": mechanics.SubType = (byte) SuitSubType.RandomSuit; break;
					case "RandomNinja": mechanics.SubType = (byte) SuitSubType.RandomNinja; break;
					case "RandomWizard": mechanics.SubType = (byte) SuitSubType.RandomWizard; break;

					case "BlackNinja": mechanics.SubType = (byte) SuitSubType.BlackNinja; break;
					case "BlueNinja": mechanics.SubType = (byte) SuitSubType.BlueNinja; break;
					case "GreenNinja": mechanics.SubType = (byte) SuitSubType.GreenNinja; break;
					case "RedNinja": mechanics.SubType = (byte) SuitSubType.RedNinja; break;
					case "WhiteNinja": mechanics.SubType = (byte) SuitSubType.WhiteNinja; break;

					case "BlueWizard": mechanics.SubType = (byte) SuitSubType.BlueWizard; break;
					case "GreenWizard": mechanics.SubType = (byte) SuitSubType.GreenWizard; break;
					case "RedWizard": mechanics.SubType = (byte) SuitSubType.RedWizard; break;
					case "WhiteWizard": mechanics.SubType = (byte) SuitSubType.WhiteWizard; break;
				}
			}

			//mechanics.DestRoom = paramsList["destRoom"] == null ? (byte)ParamsDoorRules.DestRoom.defValue : paramsList["destRoom"].Value<byte>();
			//mechanics.ExitType = paramsList["destType"] == null ? ParamsDoorRules.ExitType.defValue : paramsList["destType"].Value<byte>();

			return mechanics;
		}
	}
}
