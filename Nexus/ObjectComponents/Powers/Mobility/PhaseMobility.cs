﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PhaseMobility : PowerMobility {

		private enum PhaseSuccess : byte {
			NoAction,
			Fail,
			Success,
		}

		public PhaseMobility( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Phase;
			this.IconTexture = "Power/Phase";
			this.subStr = "phase";
			this.SetActivationSettings(45, 1, 45);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			PhaseSuccess ps = this.PerformBlink();

			// The sound plays even if the phase fails so that the player can recognize it triggered.
			if(ps != PhaseSuccess.NoAction) {
				this.character.room.PlaySound(Systems.sounds.pop, 1f, this.character.posX + 16, this.character.posY + 16);
				return true;
			}

			return false;
		}

		private PhaseSuccess PerformBlink() {

			Character character = this.character;
			RoomScene room = character.room;
			TilemapLevel tilemap = room.tilemap;

			// Vertical Phasing
			if(character.input.isDown(IKey.Down) || character.input.isDown(IKey.Up)) {
				DirCardinal dir = character.input.isDown(IKey.Down) ? DirCardinal.Down : DirCardinal.Up;
				short toY;

				if(dir == DirCardinal.Down) {
					toY = (short)(character.GridY2 + 2);
					if(toY >= (short)(tilemap.YCount + (byte) TilemapEnum.GapUp - 1)) { return PhaseSuccess.Success; }
				} else {
					toY = (short)(character.GridY - 2);
					if(toY <= (short)TilemapEnum.GapUp + 1) { return PhaseSuccess.Success; }
				}

				// Phase to the given location if it's open:
				if(this.TestPhasingVertical(character, tilemap, toY, dir)) {
					character.physics.MoveToPosY(toY * (byte)TilemapEnum.TileHeight);
					return PhaseSuccess.Success;
				}

				// If the last location is blocked, try the next:
				toY = (short)(toY + (dir == DirCardinal.Down ? 1 : -1));

				if(this.TestPhasingVertical(character, tilemap, toY, dir)) {
					character.physics.MoveToPosY(toY * (byte)TilemapEnum.TileHeight);
					return PhaseSuccess.Success;
				}
			}

			// Horizontal Phasing
			else if(character.input.isDown(IKey.Left) || character.input.isDown(IKey.Right)) {
				DirCardinal dir = character.input.isDown(IKey.Left) ? DirCardinal.Left : DirCardinal.Right;
				short toX;

				if(dir == DirCardinal.Right) {
					toX = (short)(character.GridX2 + 2);
					if(toX >= (short)(tilemap.XCount + (byte)TilemapEnum.GapLeft - 1)) { return PhaseSuccess.Success; }
				} else {
					toX = (short)(character.GridX - 2);
					if(toX <= (short)TilemapEnum.GapLeft + 1) { return PhaseSuccess.Success; }
				}

				// Phase to the given location if it's open:
				if(this.TestPhasingHorizontal(character, tilemap, toX, dir)) {
					character.physics.MoveToPosX(toX * (byte)TilemapEnum.TileWidth);
					return PhaseSuccess.Success;
				}

				// If the last location is blocked, try the next:
				toX = (short)(toX + (dir == DirCardinal.Right ? 1 : -1));

				if(this.TestPhasingHorizontal(character, tilemap, toX, dir)) {
					character.physics.MoveToPosX(toX * (byte)TilemapEnum.TileWidth);
					return PhaseSuccess.Success;
				}
			}

			// No inputs were provided.
			else {
				return PhaseSuccess.NoAction;
			}

			return PhaseSuccess.Fail;
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
