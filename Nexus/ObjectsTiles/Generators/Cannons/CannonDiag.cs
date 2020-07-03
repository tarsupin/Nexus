using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CannonDiag : Cannon {

		public string[] Texture;

		public enum CannonDiagSubType : byte {
			UpRight = 0,
			DownRight = 1,
			DownLeft = 2,
			UpLeft = 3,
		}

		public CannonDiag() : base() {
			this.tileId = (byte)TileEnum.CannonDiagonal;
			this.CreateTextures();
		}

		public override void ActivateCannon(RoomScene room, byte subType, short gridX, short gridY, byte cannonSpeed) {

			FVector pos = FVector.Create(gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
			FInt angleSpeed = FInt.Create(cannonSpeed * 0.707);

			// Up Right
			if(subType == (byte) CannonDiagSubType.UpRight) {
				pos.X += 18;
				pos.Y -= 18;
				ProjectileBullet.Create(room, (byte)0, pos, FVector.Create(angleSpeed, 0-angleSpeed));
			}

			// Down Right
			else if(subType == (byte) CannonDiagSubType.DownRight) {
				pos.X += 16;
				pos.Y += 19;
				ProjectileBullet.Create(room, (byte)0, pos, FVector.Create(angleSpeed, angleSpeed));
			}

			// Down Left
			else if(subType == (byte) CannonDiagSubType.DownLeft) {
				pos.X -= 17;
				pos.Y += 19;
				ProjectileBullet.Create(room, (byte)0, pos, FVector.Create(0-angleSpeed, angleSpeed));
			}

			// Up Left
			else {
				pos.X -= 17;
				pos.Y -= 20;
				ProjectileBullet.Create(room, (byte)0, pos, FVector.Create(0 - angleSpeed, 0 - angleSpeed));
			}

			room.PlaySound(Systems.sounds.cannonFire, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		private void CreateTextures() {
			this.Texture = new string[4];
			this.Texture[(byte)CannonDiagSubType.UpRight] = "Cannon/UpRight";
			this.Texture[(byte)CannonDiagSubType.DownRight] = "Cannon/DownRight";
			this.Texture[(byte)CannonDiagSubType.DownLeft] = "Cannon/DownLeft";
			this.Texture[(byte)CannonDiagSubType.UpLeft] = "Cannon/UpLeft";
		}
	}
}
