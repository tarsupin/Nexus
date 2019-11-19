using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public class TileToolCollectables : TileTool {

		public TileToolCollectables(EditorScene scene) : base(scene) {

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
					subType = (byte)HatSubType.FedoraHat,
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
					subType = (byte)HatSubType.RandomHat,
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
					subType = (byte)PowerSubType.Shuriken,
				},
				new EditorPlaceholder() {
					tileId = (byte)TileEnum.CollectablePower,
					subType = (byte)PowerSubType.RandThrown
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
					subType = (byte) PowerSubType.RandWeapon,
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
					subType = (byte) PowerSubType.Slime,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Rock,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Necro1,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.Necro2,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.RandBook,
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
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CollectablePower,
					subType = (byte) PowerSubType.BoltNecro,
				},
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
					subType = (byte) PowerSubType.Athlete,
				},
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
					subType = (byte) PowerSubType.RandPot,
				},
			});
			
			// Coins
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Coins,
					subType = (byte) CoinsSubType.Coin,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Coins,
					subType = (byte) CoinsSubType.Gem,
				},
			});
			
			// Goodies (Health)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Apple,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Pear,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Heart,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Shield,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.ShieldPlus,
				},
			});
			
			// Goodies (Protection)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Guard,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.GuardPlus,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Explosive,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Disrupt,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Shiny,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Stars,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.GodMode,
				},
			});

			// Goodies (Time)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Set5,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Set10,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Set20,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Plus5,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Plus10,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Plus20,
				},
			});

			// Goodies (Key)
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Key,
				},
			});

			// BGDisable
			// TODO LOW PRIORITY: Need correct options. Not just subType = 0;
			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Goodie,
					subType = (byte) GoodieSubType.Key,
				},
			});
		}
	}
}
