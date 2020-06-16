using Nexus.Gameplay;
using static Nexus.Objects.Bomb;
using static Nexus.Objects.Boulder;
using static Nexus.Objects.CannonDiag;
using static Nexus.Objects.CannonHor;
using static Nexus.Objects.CannonVert;
using static Nexus.Objects.OrbItem;
using static Nexus.Objects.Placer;
using static Nexus.Objects.Shell;
using static Nexus.Objects.SportBall;
using static Nexus.Objects.SpringFixed;
using static Nexus.Objects.SpringSide;
using static Nexus.Objects.SpringHeld;
using static Nexus.Objects.TNT;

namespace Nexus.GameEngine {

	public class TileToolGadgets : TileTool {

		public TileToolGadgets() : base() {

			this.slotGroup = (byte)SlotGroup.Gadgets;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonHorizontal,
					subType = (byte) CannonHorSubType.Left,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonHorizontal,
					subType = (byte) CannonHorSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonVertical,
					subType = (byte) CannonVertSubType.Up,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonVertical,
					subType = (byte) CannonVertSubType.Down,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonDiagonal,
					subType = (byte) CannonDiagSubType.DownLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonDiagonal,
					subType = (byte) CannonDiagSubType.DownRight,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonDiagonal,
					subType = (byte) CannonDiagSubType.UpLeft,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CannonDiagonal,
					subType = (byte) CannonDiagSubType.UpRight,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Placer,
					subType = (byte) PlacerSubType.Up,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Placer,
					subType = (byte) PlacerSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Placer,
					subType = (byte) PlacerSubType.Down,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Placer,
					subType = (byte) PlacerSubType.Left,
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
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonFixedBRUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonFixedGYUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonTimedBRUp,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.ButtonTimedGYUp,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.SpringHeld,
					subType = (byte) SpringHeldSubType.Norm,
					layerEnum = LayerEnum.obj,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.SpringFixed,
					subType = (byte) SpringFixedSubType.Up,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.SpringSide,
					subType = (byte) SpringSideSubType.Right,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.SpringFixed,
					subType = (byte) SpringFixedSubType.Rev,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.SpringSide,
					subType = (byte) SpringSideSubType.Left,
				},
			});

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
