using Nexus.Gameplay;
using Nexus.Objects;
using static Nexus.Objects.ElementalAir;
using static Nexus.Objects.ElementalEarth;
using static Nexus.Objects.ElementalFire;

namespace Nexus.GameEngine {

	public class TileToolEnemyFly : TileTool {

		public TileToolEnemyFly() : base() {

			this.slotGroup = (byte)SlotGroup.EnemiesFly;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Buzz,
					subType = (byte) BuzzSubType.Buzz,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalAir,
					subType = (byte) ElementalAirSubType.Left,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalAir,
					subType = (byte) ElementalAirSubType.Right,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalFire,
					subType = (byte) ElementalFireSubType.Left,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalFire,
					subType = (byte) ElementalFireSubType.Right,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalEarth,
					subType = (byte) ElementalEarthSubType.Left,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ElementalEarth,
					subType = (byte) ElementalEarthSubType.Right,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.FlairElectric,
					subType = (byte) FlairElectricSubType.Normal,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.FlairFire,
					subType = (byte) FlairFireSubType.Normal,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.FlairMagic,
					subType = (byte) FlairMagicSubType.Normal,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Ghost,
					subType = (byte) GhostSubType.Norm,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Ghost,
					subType = (byte) GhostSubType.Hide,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Ghost,
					subType = (byte) GhostSubType.Hat,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Saw,
					subType = (byte) SawSubType.Small,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Saw,
					subType = (byte) SawSubType.LethalSmall,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Saw,
					subType = (byte) SawSubType.Large,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Saw,
					subType = (byte) SawSubType.LethalLarge,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.HoveringEye,
					subType = (byte) HoveringEyeSubType.Eye,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Slammer,
					subType = (byte) SlammerSubType.Slammer,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Dire,
					subType = (byte) DireSubType.Dire,
					layerEnum = LayerEnum.obj,
				},
			});
		}
	}
}
