using Nexus.Engine;
using Nexus.GameEngine;
using System;

namespace Nexus.Gameplay {

	public enum AutoGroup : byte {
		None,					// Has no auto-group, cannot be placed with the auto-tiler.
		Horizontal,				// Will use horizontals (S, H1, H2, H3)
		Vertical,				// Will use verticals (S, V1, V2, V3)
		Ground,					// Will use ground sequence (horizontal, verticals, and full)
		Ledge,                  // Unique Ledge Behavior
		Static,                 // Will create the same block across the full selection.
		StaticCross,            // Will use either horizontal or vertical (whichever is dragged).
	}

	public class AutoTileTool {

		public uint startFrame;			// The frame number when the auto-tile process begins. 0 if inactive.
		public byte tileId;				// The Tile ID being used in the auto-tile process.
		public byte subTypeId;			// The SubType index used on the tile.
		public AutoGroup autoGroup;		// The auto-group that is being applied.

		public ushort xStart;			// X-Grid that the drag is starting at.
		public ushort yStart;			// Y-Grid that the drag is starting at.

		public AutoTileTool() {
			this.startFrame = 0;
		}

		public bool IsActive { get { return this.startFrame > 0 && this.tileId > 0; } }

		// Identify the Grid Width and Height of the selection.
		public ushort AutoWidth { get { return (ushort) (Cursor.MouseGridX - this.xStart); } }
		public ushort AutoHeight { get { return (ushort) (Cursor.MouseGridY - this.yStart); } }

		// Identify the Grid Positions of the selection's edges
		public ushort LeftTile { get { return Math.Min(Cursor.MouseGridX, this.xStart); } }
		public ushort RightTile { get { return Math.Max(Cursor.MouseGridX, this.xStart); } }
		public ushort TopTile { get { return Math.Min(Cursor.MouseGridY, this.yStart); } }
		public ushort BottomTile { get { return Math.Max(Cursor.MouseGridY, this.yStart); } }

		public void StartAutoTile(byte tileId, byte subTypeId, ushort gridX, ushort gridY) {
			this.startFrame = Systems.timer.Frame;
			this.tileId = tileId;
			this.subTypeId = subTypeId;
			this.autoGroup = AutoTileTool.IdentifyAutoGroup(tileId);
			this.xStart = gridX;
			this.yStart = gridY;
		}

		public void ClearAutoTiles() {
			this.startFrame = 0;
			this.tileId = 0;
		}

		public void PlaceAutoTiles(EditorRoomScene scene) {

			ushort left = this.LeftTile;
			ushort right = this.RightTile;
			ushort top = this.TopTile;
			ushort bottom = this.BottomTile;
			
			// Loop through all grid squres in Auto-Tile:
			for(ushort x = left; x <= right; x++) {
				for(ushort y = top; y<= bottom; y++) {

					// Get the SubType assigned for that auto-tile position, then place it into the Editor Room.
					byte subType = this.GetSubTypeAtPosition(x, y);
					scene.PlaceTile(scene.levelContent.data.rooms[scene.roomID].main, x, y, this.tileId, subType, null);
				}
			}

			this.ClearAutoTiles();
		}

		public void DrawAutoTiles() {
			
			// Skip this method if the auto-tile isn't in operation.
			if(this.startFrame == 0) { return; }

			ushort left = this.LeftTile;
			ushort right = this.RightTile;
			ushort top = this.TopTile;
			ushort bottom = this.BottomTile;
			
			// Loop through all grid squres in Auto-Tile:
			for(ushort x = left; x <= right; x++) {
				for(ushort y = top; y<= bottom; y++) {

					// Get the SubType assigned for that auto-tile position, then draw it.
					byte subType = this.GetSubTypeAtPosition(x, y);
					TileGameObject tgo = Systems.mapper.TileDict[this.tileId];
					tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, y * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
				}
			}
		}

		private byte GetSubTypeAtPosition( ushort gridX, ushort gridY ) {

			switch(this.autoGroup) {
				case AutoGroup.Ground: return this.GetGroundSubType(gridX, gridY);
				case AutoGroup.Horizontal: return this.GetHorizontalSubType(gridX, gridY);
			}

			// If the AutoGroup is Static, the subType doesn't change.
			return this.subTypeId;
		}

		private byte GetGroundSubType( ushort gridX, ushort gridY ) {

			// Strictly Vertical
			if(this.LeftTile == this.RightTile) {
				if(this.TopTile == this.BottomTile) { return (byte) GroundSubTypes.S; }
				if(gridY == this.TopTile) { return (byte) GroundSubTypes.V1; }
				if(gridY == this.BottomTile) { return (byte) GroundSubTypes.V3; }
				return (byte) GroundSubTypes.V2;
			}

			// Strictly Horizontal
			if(this.TopTile == this.BottomTile) {
				if(gridX == this.LeftTile) { return (byte) GroundSubTypes.H1; }
				if(gridX == this.RightTile) { return (byte) GroundSubTypes.H3; }
				return (byte) GroundSubTypes.H2;
			}

			// Full Spread
			if(gridX == this.LeftTile) {
				if(gridY == this.TopTile) { return (byte) GroundSubTypes.FUL; }
				if(gridY == this.BottomTile) { return (byte) GroundSubTypes.FBL; }
				return (byte) GroundSubTypes.FL;
			}

			if(gridX == this.RightTile) {
				if(gridY == this.TopTile) { return (byte)GroundSubTypes.FUR; }
				if(gridY == this.BottomTile) { return (byte)GroundSubTypes.FBR; }
				return (byte)GroundSubTypes.FR;
			}

			if(gridY == this.TopTile) { return (byte)GroundSubTypes.FU; }
			if(gridY == this.BottomTile) { return (byte)GroundSubTypes.FB; }
			return (byte) GroundSubTypes.FC;
		}

		private byte GetHorizontalSubType( ushort gridX, ushort gridY ) {
			if(this.LeftTile == this.RightTile) { return (byte) HorizontalSubTypes.S; }

			if(gridX == this.LeftTile) { return (byte) HorizontalSubTypes.H1; }
			if(gridX == this.RightTile) { return (byte) HorizontalSubTypes.H3; }
			return (byte) HorizontalSubTypes.H2;
		}

		private static AutoGroup IdentifyAutoGroup(byte tileId) {
			
			switch(tileId) {

				// Ground Types
				case (byte) TileEnum.GroundGrass:
				case (byte) TileEnum.GroundMud:
				case (byte) TileEnum.GroundSlime:
				case (byte) TileEnum.GroundSnow:
				case (byte) TileEnum.GroundStone:
					return AutoGroup.Ground;

				// Horizontal Types
				case (byte) TileEnum.Log:
				case (byte) TileEnum.Wall:
				case (byte) TileEnum.PlatformFixed:
				case (byte) TileEnum.PlatformItem:
				case (byte) TileEnum.TogglePlatBlue:
				case (byte) TileEnum.TogglePlatGreen:
				case (byte) TileEnum.TogglePlatRed:
				case (byte) TileEnum.TogglePlatYellow:
					return AutoGroup.Horizontal;

				// Vertical Types
					//return AutoGroup.Vertical;

				// Static Cross Types
				// Only draws the original tile, but allows one direction of movement.
				case (byte) TileEnum.CannonVertical:
				case (byte) TileEnum.CannonHorizontal:
				case (byte) TileEnum.ChomperFire:
				case (byte) TileEnum.ChomperGrass:
				case (byte) TileEnum.ChomperMetal:
					return AutoGroup.StaticCross;

				// Ledges
				case (byte) TileEnum.LedgeGrass:
					return AutoGroup.Ledge;

				// Static
				case (byte) TileEnum.BGDisable:
				case (byte) TileEnum.BGTap:
				case (byte) TileEnum.Box:
				case (byte) TileEnum.Brick:
				case (byte) TileEnum.Coins:
				case (byte) TileEnum.GroundCloud:
				case (byte) TileEnum.Leaf:
				case (byte) TileEnum.Lock:
				case (byte) TileEnum.Plant:
				case (byte) TileEnum.Spike:
				case (byte) TileEnum.ToggleBlockBlue:
				case (byte) TileEnum.ToggleBlockGreen:
				case (byte) TileEnum.ToggleBlockRed:
				case (byte) TileEnum.ToggleBlockYellow:
					return AutoGroup.Static;
			}

			return AutoGroup.None;
		}
	}
}
