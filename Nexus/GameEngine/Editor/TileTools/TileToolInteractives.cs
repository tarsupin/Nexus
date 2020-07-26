using Nexus.Gameplay;
using static Nexus.Objects.Chest;
using static Nexus.Objects.Door;
using static Nexus.Objects.NPC;

namespace Nexus.GameEngine {

	public class TileToolInteractives : TileTool {

		// WARNING: DO NOT CHANGE THIS FILE WITHOUT THE FOLLOWING:
		// WARNING: DO NOT CHANGE THIS FILE WITHOUT THE FOLLOWING:
		// WARNING: DO NOT CHANGE THIS FILE WITHOUT THE FOLLOWING:
		// The "TutorialEditor.cs" file relies on the index of this.placeholders being in specific positions for Characters.
		//		e.g. EditorTools.tileTool.index == 0
		// So either add a const value that can track this information, or keep them consistent.

		public TileToolInteractives() : base() {

			this.slotGroup = (byte)SlotGroup.Interactives;

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					objectId = (byte) ObjectEnum.Character,
					subType = (byte) 0,
					layerEnum = LayerEnum.obj,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CheckFlagFinish,
					subType = (byte) 0,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CheckFlagPass,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CheckFlagCheckpoint,
					subType = (byte) 0,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.CheckFlagRetry,
					subType = (byte) 0,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Door,
					subType = (byte) DoorSubType.Blue,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Door,
					subType = (byte) DoorSubType.Green,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Door,
					subType = (byte) DoorSubType.Red,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Door,
					subType = (byte) DoorSubType.Yellow,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Door,
					subType = (byte) DoorSubType.Open,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DoorLock,
					subType = (byte) DoorSubType.Blue,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DoorLock,
					subType = (byte) DoorSubType.Green,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DoorLock,
					subType = (byte) DoorSubType.Red,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.DoorLock,
					subType = (byte) DoorSubType.Yellow,
				},
			});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalWarp,
			//		subType = (byte) Something.Blue,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalWarp,
			//		subType = (byte) Something.Blue,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalWarp,
			//		subType = (byte) Something.Blue,
			//		face: DirRotate.Half,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalWarp,
			//		subType = (byte) Something.Blue,
			//		face: DirRotate.Left,
			//	},
			//});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Chest,
					subType = (byte) ChestSubType.Closed,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.Chest,
					subType = (byte) ChestSubType.Locked,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.Girl,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.Guy,
				},
			});

			this.placeholders.Add(new EditorPlaceholder[] {
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.NinjaBlack,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.NinjaBlue,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.NinjaGreen,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.NinjaRed,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.NinjaWhite,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.NinjaMaster,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.WizardBlue,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.WizardGreen,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.WizardRed,
				},
				new EditorPlaceholder() {
					tileId = (byte) TileEnum.NPC,
					subType = (byte) NPCSubType.WizardWhite,
				},
			});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PeekMap,
			//		subType = (byte) Something.Standard,
			//	},
			//});
		}
	}
}
