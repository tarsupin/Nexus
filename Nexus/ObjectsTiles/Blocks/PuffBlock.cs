using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PuffBlock : BlockTile {

		public string Texture;

		public enum PuffBlockSubType : byte {
			Up = 0,
			Right = 1,
			Down = 2,
			Left = 3,
		}

		// TODO: PuffBlock needs a particle to appear that makes it turn "on" after use.

		public PuffBlock() : base() {
			this.tileId = (byte) TileEnum.PuffBlock;
			this.Texture = "Puff/Off";
			this.title = "Puff Block";
			this.description = "Touching it causes the character to burst quickly in the designated direction.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				Character character = (Character) actor;

				// Make sure the character is fully within the chest tile.
				if(!CollideTile.IsWithinPaddedTile(character, gridX, gridY, 6, 6, 6, 6)) { return false; }

				// Get the SubType
				byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				// Determine Movement Pattern
				sbyte hor = 0;
				sbyte vert = 0;

				if(subType == (byte)PuffBlockSubType.Up) {
					vert = -1;
				} else if(subType == (byte)PuffBlockSubType.Right) {
					hor = 1;
				} else if(subType == (byte)PuffBlockSubType.Down) {
					hor = -1;
				} else if(subType == (byte)PuffBlockSubType.Left) {
					vert = 1;
				}

				// Character is sent into an "Air Burst" action.
				ActionMap.AirBurst.StartAction(character, hor, vert);

				Systems.sounds.air.Play();
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
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
