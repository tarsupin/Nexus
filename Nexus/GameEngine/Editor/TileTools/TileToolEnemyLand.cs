
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class TileToolEnemyLand : TileTool {

		public TileToolEnemyLand() : base() {

			this.slotGroup = (byte)SlotGroup.EnemiesLand;

			// TODO LOW PRIORITY: Add this whole section

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperGrass,
			//		subType = (byte) Something.Standard,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperGrass,
			//		subType = (byte) Something.Standard,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperGrass,
			//		subType = (byte) Something.Standard,
			//		face: DirRotate.FlipVert,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperGrass,
			//		subType = (byte) Something.Standard,
			//		face: DirRotate.Left,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperMetal,
			//		subType = (byte) Something.Metal,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperMetal,
			//		subType = (byte) Something.Metal,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperMetal,
			//		subType = (byte) Something.Metal,
			//		face: DirRotate.FlipVert,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperMetal,
			//		subType = (byte) Something.Metal,
			//		face: DirRotate.Left,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperFire,
			//		subType = (byte) Something.Fire,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperFire,
			//		subType = (byte) Something.Fire,
			//		face: DirRotate.Right,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperFire,
			//		subType = (byte) Something.Fire,
			//		face: DirRotate.FlipVert,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ChomperFire,
			//		subType = (byte) Something.Fire,
			//		face: DirRotate.Left,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Plant,
			//		subType = (byte) Something.Fixed,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.Plant,
			//		subType = (byte) Something.FixedMetal,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) TileEnum.ElementalEye,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Vin,
			//		subType = (byte) Something.Vin,
			//	},
			//});


			// TODO LOW PRIORITY: Add this whole section
			//this.placeholdersAdd(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Goo,
			//		subType = (byte) Something.Green,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Goo,
			//		subType = (byte) Something.Blue,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Goo,
			//		subType = (byte) Something.Orange,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Snail,
			//		subType = (byte) Something.Standard,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Bug,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Moosh,
			//		subType = (byte) Something.Brown,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Moosh,
			//		subType = (byte) Something.Purple,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Moosh,
			//		subType = (byte) Something.White,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Shroom,
			//		subType = (byte) Something.Red,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Shroom,
			//		subType = (byte) Something.Purple,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Shroom,
			//		subType = (byte) Something.Black,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Snek,
			//		subType = (byte) Something.Snek,
			//	},
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Wurm,
			//		subType = (byte) Something.Wurm,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Octo,
			//		subType = (byte) Something.Octo,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Boom,
			//		subType = (byte) Something.Boom,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Turtle,
			//		subType = (byte) Something.Standard,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Bones,
			//		subType = (byte) Something.Bones,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Liz,
			//		subType = (byte) Something.Liz,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Poke,
			//		subType = (byte) Something.Poke,
			//	},
			//});

			//this.placeholders.Add(new EditorPlaceholder[] {
			//	new EditorPlaceholder() {
			//		tileId = (byte) ObjectEnum.Lich,
			//		subType = (byte) Something.Lich,
			//	},
			//});
		}
	}
}
