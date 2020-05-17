
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class TileToolEnemyFly : TileTool {

		public TileToolEnemyFly() : base() {

			this.slotGroup = (byte)SlotGroup.EnemiesFly;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Buzz,
					subType = (byte) BuzzSubType.Buzz,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalAir,
					subType = (byte) ElementalAirSubType.Normal,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalFire,
					subType = (byte) ElementalFireSubType.Normal,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalEarth,
					subType = (byte) ElementalEarthSubType.Normal,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.FlairElectric,
					subType = (byte) FlairElectricSubType.Normal,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.FlairFire,
					subType = (byte) FlairFireSubType.Normal,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.FlairMagic,
					subType = (byte) FlairMagicSubType.Normal,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Ghost,
					subType = (byte) GhostSubType.Norm,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Ghost,
					subType = (byte) GhostSubType.Hide,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Ghost,
					subType = (byte) GhostSubType.Hat,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Saw,
					subType = (byte) SawSubType.Small,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Saw,
					subType = (byte) SawSubType.LethalSmall,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Saw,
					subType = (byte) SawSubType.Large,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Saw,
					subType = (byte) SawSubType.LethalLarge,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.HoveringEye,
					subType = (byte) HoveringEyeSubType.Eye,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Slammer,
					subType = (byte) SlammerSubType.Slammer,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Dire,
					subType = (byte) DireSubType.Dire,
				},
			});
		}
	}
}
