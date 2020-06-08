using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CannonVert : Cannon {

		public string[] Texture;

		public enum CannonVertSubType : byte {
			Up = 0,
			Down = 1,
		}

		public CannonVert() : base() {
			this.tileId = (byte)TileEnum.CannonVertical;
			this.CreateTextures();
		}

		public override void ActivateCannon(RoomScene room, byte subType, ushort gridX, ushort gridY, byte cannonSpeed) {

			FVector pos = FVector.Create(gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
			FInt angleSpeed = FInt.Create(cannonSpeed * 0.707);

			// Up
			if(subType == (byte)CannonVertSubType.Up) {
				pos.Y -= 30;
				ProjectileBullet.Create(room, (byte)0, pos, FVector.Create(FInt.Create(0), 0 - angleSpeed));
			}

			// Down
			else {
				pos.Y += 30;
				ProjectileBullet.Create(room, (byte)0, pos, FVector.Create(FInt.Create(0), angleSpeed));
			}

			Systems.sounds.cannonFire.Play();
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)CannonVertSubType.Up] = "Cannon/Up";
			this.Texture[(byte)CannonVertSubType.Down] = "Cannon/Down";
		}
	}
}
