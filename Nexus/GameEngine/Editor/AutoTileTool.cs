using Nexus.Engine;
using Nexus.GameEngine;
using System;

namespace Nexus.Gameplay {

	public enum AutoGroup : byte {
		None,					// Has no auto-group, cannot be placed with the auto-tiler.
		Horizontal,				// Will use horizontals (S, H1, H2, H3)
		Vertical,				// Will use verticals (S, V1, V2, V3)
		Full,					// Will use ground sequence (horizontal, verticals, and full)
		Ledge,                  // Unique Ledge Behavior
		Static,                 // Will create the same block across the full selection.
		StaticCross,            // Will use either horizontal or vertical (whichever is dragged).
	}

	public class AutoTileTool {

		public int startFrame;			// The frame number when the auto-tile process begins. 0 if inactive.
		public byte tileId;				// The Tile ID being used in the auto-tile process.
		public byte subTypeId;          // The SubType index used on the tile.
		public LayerEnum layerEnum;		// The layer to place on. Can be BG or MAIN.
		public AutoGroup autoGroup;		// The auto-group that is being applied.

		public short xStart;			// X-Grid that the drag is starting at.
		public short yStart;			// Y-Grid that the drag is starting at.

		public AutoTileTool() {
			this.startFrame = 0;
		}

		public bool IsActive { get { return this.startFrame > 0 && this.tileId > 0; } }

		// Identify the Grid Width and Height of the selection.
		public short AutoWidth { get { return (short) (Cursor.TileGridX - this.xStart); } }
		public short AutoHeight { get { return (short) (Cursor.TileGridY - this.yStart); } }

		// Identify the Grid Positions of the selection's edges
		public short LeftTile { get { return Math.Min(Cursor.TileGridX, this.xStart); } }
		public short RightTile { get { return Math.Max(Cursor.TileGridX, this.xStart); } }
		public short TopTile { get { return Math.Min(Cursor.TileGridY, this.yStart); } }
		public short BottomTile { get { return Math.Max(Cursor.TileGridY, this.yStart); } }

		public void StartAutoTile(byte tileId, byte subTypeId, LayerEnum layerEnum, short gridX, short gridY) {
			this.autoGroup = AutoTileTool.IdentifyAutoGroup(tileId);
			
			if(this.autoGroup == AutoGroup.None) {
				this.ClearAutoTiles();
				return;
			}

			this.startFrame = Systems.timer.Frame;
			this.tileId = tileId;
			this.subTypeId = subTypeId;
			this.layerEnum = layerEnum;
			this.xStart = gridX;
			this.yStart = gridY;
		}

		public void ClearAutoTiles() {
			this.startFrame = 0;
			this.tileId = 0;
		}

		public void PlaceAutoTiles(EditorRoomScene scene) {

			// Skip this method if the auto-tile isn't in operation.
			if(!this.IsActive) { return; }

			short left = this.LeftTile;
			short right = this.RightTile;
			short top = this.TopTile;
			short bottom = this.BottomTile;

			if(this.autoGroup == AutoGroup.Full || this.autoGroup == AutoGroup.Static) {
				this.PlaceAutoTilesFull(scene, left, right, top, bottom);
			}

			else if(this.autoGroup == AutoGroup.Ledge) {
				this.PlaceAutoTilesLedge(scene, left, right, top, bottom);
			}

			else if(this.autoGroup == AutoGroup.Horizontal) {
				this.PlaceAutoTilesHorizontal(scene, left, right, this.yStart);
			}

			else if(this.autoGroup == AutoGroup.Vertical) {
				this.PlaceAutoTilesVertical(scene, top, bottom, this.xStart);
			}

			else if(this.autoGroup == AutoGroup.StaticCross) {

				// If Horizontal is greater than or equal to Vertical distance, draw horizontal:
				if(Math.Abs(right - left) > Math.Abs(bottom - top)) {
					this.PlaceAutoTilesHorizontal(scene, left, right, this.yStart);
				}

				// Otherwise, draw vertical:
				else {
					this.PlaceAutoTilesVertical(scene, top, bottom, this.xStart);
				}
			}

			this.ClearAutoTiles();
		}

		// Places a full X+Y grid.
		private void PlaceAutoTilesFull(EditorRoomScene scene, short left, short right, short top, short bottom) {
			for(short x = left; x <= right; x++) {
				for(short y = top; y <= bottom; y++) {
					byte subType = this.GetSubTypeAtPosition(x, y);
					scene.PlaceTile(scene.levelContent.data.rooms[scene.roomID].main, LayerEnum.main, x, y, this.tileId, subType, null);
				}
			}
		}
		
		// Special Ledge Placement (full axis movement). Top section will use Ledge, rest will use LedgeDecor.
		private void PlaceAutoTilesLedge(EditorRoomScene scene, short left, short right, short top, short bottom) {
			byte ledgeTool = this.tileId;
			byte decorTool = (byte)TileEnum.LedgeDecor;

			// Place Top of Ledge
			for(short x = left; x <= right; x++) {
				byte subType = this.GetSubTypeAtPosition(x, top);
				scene.PlaceTile(scene.levelContent.data.rooms[scene.roomID].main, LayerEnum.main, x, top, ledgeTool, subType, null);
			}

			// Place Rest of Ledge (Decor)
			top += 1;
			for(short x = left; x <= right; x++) {
				for(short y = top; y <= bottom; y++) {
					byte subType = this.GetSubTypeAtPosition(x, y);
					scene.PlaceTile(scene.levelContent.data.rooms[scene.roomID].bg, LayerEnum.bg, x, y, decorTool, subType, null);
				}
			}
		}

		// Restricts the dimensions of the AutoTile placement to the horizontal axis.
		private void PlaceAutoTilesHorizontal(EditorRoomScene scene, short left, short right, short yLevel) {
			for(short x = left; x <= right; x++) {
				byte subType = this.GetSubTypeAtPosition(x, yLevel);
				scene.PlaceTile(scene.levelContent.data.rooms[scene.roomID].main, LayerEnum.main, x, yLevel, this.tileId, subType, null);
			}
		}

		// Restricts the dimensions of the AutoTile placement to the vertical axis.
		private void PlaceAutoTilesVertical(EditorRoomScene scene, short top, short bottom, short xLevel) {
			for(short y = top; y <= bottom; y++) {
				byte subType = this.GetSubTypeAtPosition(xLevel, y);
				scene.PlaceTile(scene.levelContent.data.rooms[scene.roomID].main, LayerEnum.main, xLevel, y, this.tileId, subType, null);
			}
		}

		public void DrawAutoTiles() {
			
			// Skip this method if the auto-tile isn't in operation.
			if(this.startFrame == 0) { return; }

			short left = this.LeftTile;
			short right = this.RightTile;
			short top = this.TopTile;
			short bottom = this.BottomTile;

			if(this.autoGroup == AutoGroup.Full || this.autoGroup == AutoGroup.Static) {
				this.DrawAutoTilesFull(left, right, top, bottom);
				return;
			}

			if(this.autoGroup == AutoGroup.Ledge) {
				this.DrawAutoTilesLedge(left, right, top, bottom);
				return;
			}

			if(this.autoGroup == AutoGroup.Horizontal) {
				this.DrawAutoTilesHorizontal(left, right, this.yStart);
				return;
			}

			if(this.autoGroup == AutoGroup.Vertical) {
				this.DrawAutoTilesVertical(top, bottom, this.xStart);
				return;
			}

			if(this.autoGroup == AutoGroup.StaticCross) {

				// If Horizontal is greater than or equal to Vertical distance, draw horizontal:
				if(Math.Abs(right - left) > Math.Abs(bottom - top)) {
					this.DrawAutoTilesHorizontal(left, right, this.yStart);
					return;
				}
				
				// Otherwise, draw vertical:
				else {
					this.DrawAutoTilesVertical(top, bottom, this.xStart);
					return;
				}
			}
		}

		// Places a full X+Y grid.
		private void DrawAutoTilesFull(short left, short right, short top, short bottom) {
			for(short x = left; x <= right; x++) {
				for(short y = top; y<= bottom; y++) {

					// Get the SubType assigned for that auto-tile position, then draw it.
					byte subType = this.GetSubTypeAtPosition(x, y);
					TileObject tgo = Systems.mapper.TileDict[this.tileId];
					tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, y * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
				}
			}
		}

		// Special Ledge Draw. Top section will use Ledge, rest will use LedgeDecor.
		private void DrawAutoTilesLedge(short left, short right, short top, short bottom) {
			byte ledgeTool = this.tileId;
			byte decorTool = (byte) TileEnum.LedgeDecor;

			// Draw Top of Ledge
			for(short x = left; x <= right; x++) {
				byte subType = this.GetSubTypeAtPosition(x, top);
				TileObject tgo = Systems.mapper.TileDict[ledgeTool];
				tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, top * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
			}

			// Draw Rest of Ledge (Decor)
			top += 1;
			for(short x = left; x <= right; x++) {
				for(short y = top; y <= bottom; y++) {
					byte subType = this.GetSubTypeAtPosition(x, y);
					TileObject tgo = Systems.mapper.TileDict[decorTool];
					tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, y * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
				}
			}
		}

		// Restricts the dimensions of the AutoTile placement to the horizontal axis.
		private void DrawAutoTilesHorizontal(short left, short right, short yLevel) {
			for(short x = left; x <= right; x++) {
				byte subType = this.GetSubTypeAtPosition(x, yLevel);
				TileObject tgo = Systems.mapper.TileDict[this.tileId];
				tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, yLevel * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
			}
		}

		// Restricts the dimensions of the AutoTile placement to the vertical axis.
		private void DrawAutoTilesVertical(short top, short bottom, short xLevel) {
			for(short y = top; y <= bottom; y++) {
				byte subType = this.GetSubTypeAtPosition(xLevel, y);
				TileObject tgo = Systems.mapper.TileDict[this.tileId];
				tgo.Draw(null, subType, xLevel * (byte)TilemapEnum.TileWidth - Systems.camera.posX, y * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
			}
		}

		private byte GetSubTypeAtPosition( short gridX, short gridY ) {

			switch(this.autoGroup) {
				case AutoGroup.Full: return this.GetGroundSubType(gridX, gridY);
				case AutoGroup.Ledge: return this.GetGroundSubType(gridX, gridY);
				case AutoGroup.Horizontal: return this.GetHorizontalSubType(gridX, gridY);
			}

			// If the AutoGroup is Static, the subType doesn't change.
			return this.subTypeId;
		}

		private byte GetGroundSubType( short gridX, short gridY ) {

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
				if(gridY == this.TopTile) { return (byte) GroundSubTypes.FUR; }
				if(gridY == this.BottomTile) { return (byte) GroundSubTypes.FBR; }
				return (byte) GroundSubTypes.FR;
			}

			if(gridY == this.TopTile) { return (byte) GroundSubTypes.FU; }
			if(gridY == this.BottomTile) { return (byte) GroundSubTypes.FB; }
			return (byte) GroundSubTypes.FC;
		}

		private byte GetHorizontalSubType( short gridX, short gridY ) {
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
					return AutoGroup.Full;

				// Horizontal Types
				case (byte) TileEnum.Log:
				case (byte) TileEnum.SlabGray:
				case (byte) TileEnum.PlatformFixedUp:
				case (byte) TileEnum.PlatformItem:
					return AutoGroup.Horizontal;

				// Static Cross Types
				// Only draws the original tile, but allows one direction of movement.
				case (byte) TileEnum.CannonVertical:
				case (byte) TileEnum.CannonHorizontal:
				case (byte) TileEnum.ChomperFire:
				case (byte) TileEnum.ChomperGrass:
				case (byte) TileEnum.ChomperMetal:
				case (byte) TileEnum.PlatBlueUp:
				case (byte) TileEnum.PlatGreenUp:
				case (byte) TileEnum.PlatRedUp:
				case (byte) TileEnum.PlatYellowUp:
					return AutoGroup.StaticCross;

				// Ledges
				case (byte) TileEnum.LedgeGrass:
				case (byte) TileEnum.LedgeSnow:
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
					return AutoGroup.Static;
			}

			return AutoGroup.None;
		}
	}
}
