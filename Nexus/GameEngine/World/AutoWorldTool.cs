using Nexus.Engine;
using Nexus.GameEngine;
using System;

namespace Nexus.Gameplay {

	public enum WorldAutoGroup : byte {
		None,					// Has no auto-group, cannot be placed with the auto-tiler.
		Horizontal,				// Will use horizontals (S, H1, H2, H3)
		Vertical,				// Will use verticals (S, V1, V2, V3)
		Full,					// Will use ground sequence (horizontal, verticals, and full)
		Ledge,                  // Unique Ledge Behavior
		Static,                 // Will create the same block across the full selection.
		StaticCross,            // Will use either horizontal or vertical (whichever is dragged).
	}

	public class AutoWorldTool {

		public uint startFrame;			// The frame number when the auto-tile process begins. 0 if inactive.
		public byte tBase;				// The Tile ID being used in the auto-tile process.
		public byte tCat;				// The SubType index used on the tile.
		public WorldAutoGroup WorldAutoGroup;		// The auto-group that is being applied.

		public ushort xStart;			// X-Grid that the drag is starting at.
		public ushort yStart;			// Y-Grid that the drag is starting at.

		public AutoWorldTool() {
			this.startFrame = 0;
		}

		public bool IsActive { get { return this.startFrame > 0 && this.tBase > 0; } }

		// Identify the Grid Width and Height of the selection.
		public ushort AutoWidth { get { return (ushort) (Cursor.TileGridX - this.xStart); } }
		public ushort AutoHeight { get { return (ushort) (Cursor.TileGridY - this.yStart); } }

		// Identify the Grid Positions of the selection's edges
		public ushort LeftTile { get { return Math.Min(Cursor.TileGridX, this.xStart); } }
		public ushort RightTile { get { return Math.Max(Cursor.TileGridX, this.xStart); } }
		public ushort TopTile { get { return Math.Min(Cursor.TileGridY, this.yStart); } }
		public ushort BottomTile { get { return Math.Max(Cursor.TileGridY, this.yStart); } }

		public void StartAutoTile(byte tBase, byte tCat, ushort gridX, ushort gridY) {
			this.WorldAutoGroup = AutoWorldTool.IdentifyWorldAutoGroup(tBase);
			
			if(this.WorldAutoGroup == WorldAutoGroup.None) {
				this.ClearAutoTiles();
				return;
			}

			this.startFrame = Systems.timer.Frame;
			this.tBase = tBase;
			this.tCat = tCat;
			this.xStart = gridX;
			this.yStart = gridY;
		}

		public void ClearAutoTiles() {
			this.startFrame = 0;
			this.tBase = 0;
		}

		public void PlaceAutoTiles(WEScene scene) {

			// Skip this method if the auto-tile isn't in operation.
			if(!this.IsActive) { return; }

			ushort left = this.LeftTile;
			ushort right = this.RightTile;
			ushort top = this.TopTile;
			ushort bottom = this.BottomTile;

			if(this.WorldAutoGroup == WorldAutoGroup.Full || this.WorldAutoGroup == WorldAutoGroup.Static) {
				this.PlaceAutoTilesFull(scene, left, right, top, bottom);
			}

			else if(this.WorldAutoGroup == WorldAutoGroup.Ledge) {
				this.PlaceAutoTilesLedge(scene, left, right, top, bottom);
			}

			else if(this.WorldAutoGroup == WorldAutoGroup.Horizontal) {
				this.PlaceAutoTilesHorizontal(scene, left, right, this.yStart);
			}

			else if(this.WorldAutoGroup == WorldAutoGroup.Vertical) {
				this.PlaceAutoTilesVertical(scene, top, bottom, this.xStart);
			}

			else if(this.WorldAutoGroup == WorldAutoGroup.StaticCross) {

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
		private void PlaceAutoTilesFull(WEScene scene, ushort left, ushort right, ushort top, ushort bottom) {
			for(ushort x = left; x <= right; x++) {
				for(ushort y = top; y <= bottom; y++) {
					byte subType = this.GetSubTypeAtPosition(x, y);
					//scene.PlaceTile(scene.worldContent.data.zones[scene.campaign.zoneId].main, LayerEnum.main, x, y, this.tBase, subType, null);
				}
			}
		}
		
		// Special Ledge Placement (full axis movement). Top section will use Ledge, rest will use LedgeDecor.
		private void PlaceAutoTilesLedge(WEScene scene, ushort left, ushort right, ushort top, ushort bottom) {
			byte ledgeTool = this.tBase;
			byte decorTool = (byte)TileEnum.LedgeDecor;

			// Place Top of Ledge
			for(ushort x = left; x <= right; x++) {
				byte subType = this.GetSubTypeAtPosition(x, top);
				//scene.PlaceTile(scene.worldContent.data.zones[scene.campaign.zoneId].main, LayerEnum.main, x, top, ledgeTool, subType, null);
			}

			// Place Rest of Ledge (Decor)
			top += 1;
			for(ushort x = left; x <= right; x++) {
				for(ushort y = top; y <= bottom; y++) {
					byte subType = this.GetSubTypeAtPosition(x, y);
					//scene.PlaceTile(scene.worldContent.data.zones[scene.campaign.zoneId].bg, LayerEnum.bg, x, y, decorTool, subType, null);
				}
			}
		}

		// Restricts the dimensions of the AutoTile placement to the horizontal axis.
		private void PlaceAutoTilesHorizontal(WEScene scene, ushort left, ushort right, ushort yLevel) {
			for(ushort x = left; x <= right; x++) {
				byte subType = this.GetSubTypeAtPosition(x, yLevel);
				//scene.PlaceTile(scene.worldContent.data.zones[scene.campaign.zoneId].main, LayerEnum.main, x, yLevel, this.tBase, subType, null);
			}
		}

		// Restricts the dimensions of the AutoTile placement to the vertical axis.
		private void PlaceAutoTilesVertical(WEScene scene, ushort top, ushort bottom, ushort xLevel) {
			for(ushort y = top; y <= bottom; y++) {
				byte subType = this.GetSubTypeAtPosition(xLevel, y);
				//scene.PlaceTile(scene.worldContent.data.zones[scene.campaign.zoneId].main, LayerEnum.main, xLevel, y, this.tBase, subType, null);
			}
		}

		public void DrawAutoTiles() {
			
			// Skip this method if the auto-tile isn't in operation.
			if(this.startFrame == 0) { return; }

			ushort left = this.LeftTile;
			ushort right = this.RightTile;
			ushort top = this.TopTile;
			ushort bottom = this.BottomTile;

			if(this.WorldAutoGroup == WorldAutoGroup.Full || this.WorldAutoGroup == WorldAutoGroup.Static) {
				this.DrawAutoTilesFull(left, right, top, bottom);
				return;
			}

			if(this.WorldAutoGroup == WorldAutoGroup.Ledge) {
				this.DrawAutoTilesLedge(left, right, top, bottom);
				return;
			}

			if(this.WorldAutoGroup == WorldAutoGroup.Horizontal) {
				this.DrawAutoTilesHorizontal(left, right, this.yStart);
				return;
			}

			if(this.WorldAutoGroup == WorldAutoGroup.Vertical) {
				this.DrawAutoTilesVertical(top, bottom, this.xStart);
				return;
			}

			if(this.WorldAutoGroup == WorldAutoGroup.StaticCross) {

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
		private void DrawAutoTilesFull(ushort left, ushort right, ushort top, ushort bottom) {
			for(ushort x = left; x <= right; x++) {
				for(ushort y = top; y<= bottom; y++) {

					// Get the SubType assigned for that auto-tile position, then draw it.
					byte subType = this.GetSubTypeAtPosition(x, y);
					TileObject tgo = Systems.mapper.TileDict[this.tBase];
					tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, y * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
				}
			}
		}

		// Special Ledge Draw. Top section will use Ledge, rest will use LedgeDecor.
		private void DrawAutoTilesLedge(ushort left, ushort right, ushort top, ushort bottom) {
			byte ledgeTool = this.tBase;
			byte decorTool = (byte) TileEnum.LedgeDecor;

			// Draw Top of Ledge
			for(ushort x = left; x <= right; x++) {
				byte subType = this.GetSubTypeAtPosition(x, top);
				TileObject tgo = Systems.mapper.TileDict[ledgeTool];
				tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, top * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
			}

			// Draw Rest of Ledge (Decor)
			top += 1;
			for(ushort x = left; x <= right; x++) {
				for(ushort y = top; y <= bottom; y++) {
					byte subType = this.GetSubTypeAtPosition(x, y);
					TileObject tgo = Systems.mapper.TileDict[decorTool];
					tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, y * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
				}
			}
		}

		// Restricts the dimensions of the AutoTile placement to the horizontal axis.
		private void DrawAutoTilesHorizontal(ushort left, ushort right, ushort yLevel) {
			for(ushort x = left; x <= right; x++) {
				byte subType = this.GetSubTypeAtPosition(x, yLevel);
				TileObject tgo = Systems.mapper.TileDict[this.tBase];
				tgo.Draw(null, subType, x * (byte)TilemapEnum.TileWidth - Systems.camera.posX, yLevel * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
			}
		}

		// Restricts the dimensions of the AutoTile placement to the vertical axis.
		private void DrawAutoTilesVertical(ushort top, ushort bottom, ushort xLevel) {
			for(ushort y = top; y <= bottom; y++) {
				byte subType = this.GetSubTypeAtPosition(xLevel, y);
				TileObject tgo = Systems.mapper.TileDict[this.tBase];
				tgo.Draw(null, subType, xLevel * (byte)TilemapEnum.TileWidth - Systems.camera.posX, y * (byte)TilemapEnum.TileHeight - Systems.camera.posY);
			}
		}

		private byte GetSubTypeAtPosition( ushort gridX, ushort gridY ) {

			switch(this.WorldAutoGroup) {
				case WorldAutoGroup.Full: return this.GetGroundSubType(gridX, gridY);
				case WorldAutoGroup.Ledge: return this.GetGroundSubType(gridX, gridY);
				case WorldAutoGroup.Horizontal: return this.GetHorizontalSubType(gridX, gridY);
			}

			// If the WorldAutoGroup is Static, the subType doesn't change.
			return this.tCat;
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
				if(gridY == this.TopTile) { return (byte) GroundSubTypes.FUR; }
				if(gridY == this.BottomTile) { return (byte) GroundSubTypes.FBR; }
				return (byte) GroundSubTypes.FR;
			}

			if(gridY == this.TopTile) { return (byte) GroundSubTypes.FU; }
			if(gridY == this.BottomTile) { return (byte) GroundSubTypes.FB; }
			return (byte) GroundSubTypes.FC;
		}

		private byte GetHorizontalSubType( ushort gridX, ushort gridY ) {
			if(this.LeftTile == this.RightTile) { return (byte) HorizontalSubTypes.S; }

			if(gridX == this.LeftTile) { return (byte) HorizontalSubTypes.H1; }
			if(gridX == this.RightTile) { return (byte) HorizontalSubTypes.H3; }
			return (byte) HorizontalSubTypes.H2;
		}

		private static WorldAutoGroup IdentifyWorldAutoGroup(byte tBase) {
			
			switch(tBase) {

				// Ground Types
				case (byte) TileEnum.GroundGrass:
				case (byte) TileEnum.GroundMud:
				case (byte) TileEnum.GroundSlime:
				case (byte) TileEnum.GroundSnow:
				case (byte) TileEnum.GroundStone:
					return WorldAutoGroup.Full;

				// Horizontal Types
				case (byte) TileEnum.Log:
				case (byte) TileEnum.SlabGray:
				case (byte) TileEnum.PlatformFixed:
				case (byte) TileEnum.PlatformItem:
					return WorldAutoGroup.Horizontal;

				// Static Cross Types
				// Only draws the original tile, but allows one direction of movement.
				case (byte) TileEnum.CannonVertical:
				case (byte) TileEnum.CannonHorizontal:
				case (byte) TileEnum.ChomperFire:
				case (byte) TileEnum.ChomperGrass:
				case (byte) TileEnum.ChomperMetal:
				case (byte) TileEnum.TogglePlatBlue:
				case (byte) TileEnum.TogglePlatGreen:
				case (byte) TileEnum.TogglePlatRed:
				case (byte) TileEnum.TogglePlatYellow:
					return WorldAutoGroup.StaticCross;

				// Ledges
				case (byte) TileEnum.LedgeGrass:
				case (byte) TileEnum.LedgeSnow:
					return WorldAutoGroup.Ledge;

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
					return WorldAutoGroup.Static;
			}

			return WorldAutoGroup.None;
		}
	}
}
