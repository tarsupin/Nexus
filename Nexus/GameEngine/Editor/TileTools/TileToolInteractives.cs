﻿using Nexus.Gameplay;
using Nexus.Objects;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public class TileToolInteractives : TileTool {

		public TileToolInteractives() : base() {

			// TODO LOW PRIORITY: Add this whole section

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Character,
			//		subType = (byte) Something.Ryu,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.Girl,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.Guy,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.BlackNinja,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.BlueNinja,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.GreenNinja,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.RedNinja,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.WhiteNinja,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.MasterNinja,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.BlueWizard,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.GreenWizard,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.RedWizard,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.NPC,
			//		subType = (byte) Something.WhiteWizard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PeekMap,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Flag,
			//		subType = (byte) Something.Pass,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Flag,
			//		subType = (byte) Something.Checkpoint,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Flag,
			//		subType = (byte) Something.Retry,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Flag,
			//		subType = (byte) Something.Finish,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoor,
			//		subType = (byte) Something.Blue,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoor,
			//		subType = (byte) Something.Green,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoor,
			//		subType = (byte) Something.Red,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoor,
			//		subType = (byte) Something.Yellow,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoor,
			//		subType = (byte) Something.Open,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoorLock,
			//		subType = (byte) Something.Blue,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoorLock,
			//		subType = (byte) Something.Green,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoorLock,
			//		subType = (byte) Something.Red,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.PortalDoorLock,
			//		subType = (byte) Something.Yellow,
			//	},
			//});

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
