using Nexus.Gameplay;
using static Nexus.Objects.Bomb;
using static Nexus.Objects.Boulder;
using static Nexus.Objects.OrbItem;
using static Nexus.Objects.Shell;
using static Nexus.Objects.SportBall;
using static Nexus.Objects.SpringHeld;
using static Nexus.Objects.TNT;

namespace Nexus.GameEngine {

	public class TileToolItems : TileTool {

		public TileToolItems() : base() {

			this.slotGroup = (byte)SlotGroup.Items;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shell,
					subType = (byte) ShellSubType.Red,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shell,
					subType = (byte) ShellSubType.Green,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shell,
					subType = (byte) ShellSubType.GreenWing,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Shell,
					subType = (byte) ShellSubType.Heavy,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ButtonHeld,
					subType = (byte) ButtonSubTypes.BR,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.ButtonHeld,
					subType = (byte) ButtonSubTypes.GY,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.SpringHeld,
					subType = (byte) SpringHeldSubType.Norm,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Bomb,
					subType = (byte) BombSubType.Bomb,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.TNT,
					subType = (byte) TNTSubType.TNT,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Boulder,
					subType = (byte) BoulderSubType.Boulder,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.OrbItem,
					subType = (byte) OrbSubType.Magic,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.SportBall,
					subType = (byte) SportBallSubType.Forest,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.SportBall,
					subType = (byte) SportBallSubType.Earth,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.SportBall,
					subType = (byte) SportBallSubType.Fire,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.SportBall,
					subType = (byte) SportBallSubType.Water,
					layerEnum = LayerEnum.obj,
				},
			});
		}
	}
}
