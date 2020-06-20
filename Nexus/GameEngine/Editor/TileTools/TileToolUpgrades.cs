using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.GameEngine {

	public class TileToolUpgrades : TileTool {

		public TileToolUpgrades() : base() {

			this.slotGroup = (byte)SlotGroup.Upgrades;

			// Suits (Ninja)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectableSuit,
					subType = (byte) SuitSubType.BlackNinja,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectableSuit,
					subType = (byte) SuitSubType.WhiteNinja,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectableSuit,
					subType = (byte) SuitSubType.GreenNinja,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectableSuit,
					subType = (byte) SuitSubType.BlueNinja,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectableSuit,
					subType = (byte) SuitSubType.RedNinja,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectableSuit,
					subType = (byte) SuitSubType.RandomNinja,
				},
			});

			// Suits (Wizard)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectableSuit,
					subType = (byte) SuitSubType.RedWizard,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableSuit,
					subType = (byte)SuitSubType.GreenWizard,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableSuit,
					subType = (byte)SuitSubType.BlueWizard,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableSuit,
					subType = (byte)SuitSubType.WhiteWizard,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableSuit,
					subType = (byte)SuitSubType.RandomWizard,
				},
			});

			// Hat
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectableHat,
					subType = (byte) HatSubType.AngelHat,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.BambooHat,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.CowboyHat,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.FeatheredHat,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.Fedora,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.HardHat,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.RangerHat,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.SpikeyHat,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.TopHat,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectableHat,
					subType = (byte)HatSubType.RandomPowerHat,
				},
			});

			// Power (Thrown)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Axe,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectablePower,
					subType = (byte)PowerSubType.Hammer,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectablePower,
					subType = (byte)PowerSubType.ShurikenGreen,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectablePower,
					subType = (byte)PowerSubType.ShurikenRed,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectablePower,
					subType = (byte)PowerSubType.ShurikenBlue,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectablePower,
					subType = (byte)PowerSubType.ShurikenYellow,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectablePower,
					subType = (byte)PowerSubType.RandomThrown
				},
			});

			// Power (Weapon)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Dagger,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.DaggerGreen,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Sword,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Spear,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.BoxingRed,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.BoxingWhite,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.RandomWeapon,
				},
			});
			
			// Power (Ball)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Fire,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Frost,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Water,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Electric,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Poison,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Rock,
				},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.CollectablePower,
				//	subType = (byte) PowerSubType.Necro1,
				//},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.CollectablePower,
				//	subType = (byte) PowerSubType.Necro2,
				//},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.RandomPotion,
				},
			});
			
			// Power (Bolt)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.BoltBlue,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.BoltGreen,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.BoltGold,
				},
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.CollectablePower,
				//	subType = (byte) PowerSubType.BoltNecro,
				//},
			});
			
			// Power (Packs)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Chakram,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.ChakramPack,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Grenade,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.GrenadePack,
				},
			});
			
			// Mobility Power
			this.placeholders.Add(new EditorPlaceholder[] {
				//new EditorPlaceholder() {
				//	tileId = (byte) TileEnum.CollectablePower,
				//	subType = (byte) PowerSubType.Athlete,
				//},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Leap,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Slam,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.SlowFall,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Hover,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Levitate,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Flight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Burst,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Air,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Phase,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Teleport,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.RandomPotion,
				},
			});

			// Special, Miscellaneous
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.BGDisable,
					subType = (byte) 0,
				},
			});
		}
	}
}
