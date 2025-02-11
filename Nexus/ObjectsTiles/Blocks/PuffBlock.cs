﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PuffBlock : BlockTile {

		public string Texture;
		public sbyte puffDuration;

		public enum PuffBlockSubType : byte {
			Up = 0,
			Right = 1,
			Down = 2,
			Left = 3,
		}

		public PuffBlock() : base() {
			this.tileId = (byte) TileEnum.PuffBlock;
			this.Texture = "Puff/Puff";
			this.title = "Puff Block";
			this.description = "Touching it causes the character to burst powerfully in the designated direction.";
			this.puffDuration = 4;
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			// Only run this test for Characters.
			if(!(actor is Character)) { return false; }

			Character character = (Character) actor;

			// Don't run this test if the character is already in an air-burst action.
			if(character.status.action is AirBurst) { return false; }

			// Test against an Inner Boundary so that it doesn't touch so easily.
			DirCardinal newDir = TileSolidImpact.RunOverlapTest(actor, gridX * (byte)TilemapEnum.TileWidth + 6, gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth - 6, gridY * (byte)TilemapEnum.TileHeight + 6, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight - 6);

			if(newDir == DirCardinal.None) { return false; }

			// If the character isn't centered with the puff block, move it closer to center.
			//sbyte diffX = (sbyte)(character.posX + character.bounds.MidX - (gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth));
			//sbyte diffY = (sbyte)(character.posY + character.bounds.MidY - (gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight));

			// Get the SubType
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			// Determine Movement Pattern
			sbyte hor = 0;
			sbyte vert = 0;

			if(subType == (byte)PuffBlockSubType.Up) {
				vert = -1;
				character.physics.MoveToPosX(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth - character.bounds.MidX);
			} else if(subType == (byte)PuffBlockSubType.Right) {
				hor = 1;
				character.physics.MoveToPosY(gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight - character.bounds.MidY);
			} else if(subType == (byte)PuffBlockSubType.Down) {
				vert = 1;
				character.physics.MoveToPosX(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth - character.bounds.MidX);
			} else if(subType == (byte)PuffBlockSubType.Left) {
				hor = -1;
				character.physics.MoveToPosY(gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight - character.bounds.MidY);
			}

			// Character is sent into an "Air Burst" action.
			ActionMap.AirBurst.StartAction(character, hor, vert, true, this.puffDuration);

			// Draw Overlap Particle
			BurstEmitter.AirPuff(room, character.posX + character.bounds.MidX, character.posY + character.bounds.MidY, hor, vert, 18);

			room.PlaySound(Systems.sounds.air, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);

			return true;
		}
		
		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			if(subType == (byte)PuffBlockSubType.Up) {
				this.atlas.Draw(this.Texture, posX, posY);
			} else if(subType == (byte)PuffBlockSubType.Right) {
				this.atlas.DrawFaceRight(this.Texture, posX, posY);
			} else if(subType == (byte)PuffBlockSubType.Down) {
				this.atlas.DrawFaceDown(this.Texture, posX, posY);
			} else if(subType == (byte)PuffBlockSubType.Left) {
				this.atlas.DrawFaceLeft(this.Texture, posX, posY);
			}
		}
	}
}
