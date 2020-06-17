using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PhaseMobility : PowerMobility {

		public PhaseMobility( Character character ) : base( character ) {
			this.IconTexture = "Power/Phase";
			this.subStr = "phase";
			this.SetActivationSettings(45, 1, 45);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			Character character = this.character;
			RoomScene room = character.room;
			TilemapLevel tilemap = room.tilemap;

			// The sound plays even if the phase fails so that the player can recognize it triggered.
			Systems.sounds.pop.Play();

			// Vertical Phasing
			if(character.input.isDown(IKey.Down) || character.input.isDown(IKey.Up)) {
				DirCardinal dir = character.input.isDown(IKey.Down) ? DirCardinal.Down : DirCardinal.Up;
				short toY;

				if(dir == DirCardinal.Down) {
					toY = (short)(character.GridY2 + 2);
					if(character.GridY2 >= (short)(tilemap.YCount - TilemapEnum.WorldGapDown - 2)) { return false; }
				} else {
					toY = (short)(character.GridY - 2);
					if(character.GridY <= (short)(TilemapEnum.WorldGapUp + 2)) { return false; }
				}

				// Phase to the given location if it's open:
				if(this.TestPhasingVertical(character, tilemap, toY, dir)) {
					character.physics.MoveToPosY(toY * (byte)TilemapEnum.TileHeight);
					return false;
				}

				// If the last location is blocked, try the next:
				toY = (short)(toY + (dir == DirCardinal.Down ? 1 : -1));

				if(this.TestPhasingVertical(character, tilemap, toY, dir)) {
					character.physics.MoveToPosY(toY * (byte)TilemapEnum.TileHeight);
					return false;
				}
			}

			// Horizontal Phasing
			else {
				DirCardinal dir = character.input.isDown(IKey.Left) ? DirCardinal.Left : DirCardinal.Right;
				short toX;

				if(dir == DirCardinal.Right) {
					toX = (short)(character.GridX2 + 2);
					if(character.GridX2 >= (short)(tilemap.XCount - TilemapEnum.WorldGapRight - 2)) { return false; }
				} else {
					toX = (short)(character.GridX - 2);
					if(character.GridX <= (short)(TilemapEnum.WorldGapLeft + 2)) { return false; }
				}

				// Phase to the given location if it's open:
				if(this.TestPhasingHorizontal(character, tilemap, toX, dir)) {
					character.physics.MoveToPosX(toX * (byte)TilemapEnum.TileWidth);
					return false;
				}

				// If the last location is blocked, try the next:
				toX = (short)(toX + (dir == DirCardinal.Right ? 1 : -1));

				if(this.TestPhasingHorizontal(character, tilemap, toX, dir)) {
					character.physics.MoveToPosX(toX * (byte)TilemapEnum.TileWidth);
					return false;
				}
			}

			return true;
		}

		// Returns TRUE if the character can phase to the vertical location.
		public bool TestPhasingVertical(Character character, TilemapLevel tilemap, short toY, DirCardinal dir) {

			bool blocking = CollideTile.IsBlockingSquare(tilemap, character.GridX, toY, dir);
			if(blocking) { return false; }

			if(character.GridX != character.GridX2) {
				blocking = CollideTile.IsBlockingSquare(tilemap, character.GridX2, toY, dir);
			}

			return !blocking;
		}

		// Returns TRUE if the character can phase to the horizontal location.
		public bool TestPhasingHorizontal(Character character, TilemapLevel tilemap, short toX, DirCardinal dir) {

			bool blocking = CollideTile.IsBlockingSquare(tilemap, toX, character.GridY, dir);
			if(blocking) { return false; }

			if(character.GridY != character.GridY2) {
				blocking = CollideTile.IsBlockingSquare(tilemap, toX, character.GridY2, dir);
			}

			return !blocking;
		}
	}
}
