using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CannonHor : Cannon {

		public string[] Texture;

		public enum CannonHorSubType : byte {
			Left = 0,
			Right = 1,
		}

		public CannonHor() : base() {
			this.tileId = (byte)TileEnum.CannonHorizontal;
			this.CreateTextures();
		}

		public override void ActivateCannon(RoomScene room, byte subType, short gridX, short gridY, byte cannonSpeed) {

			FVector pos = FVector.Create(gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
			FInt angleSpeed = FInt.Create(cannonSpeed * 0.707);

			// Left
			if(subType == (byte) CannonHorSubType.Left) {
				pos.X -= 30;
				ProjectileBullet.Create(room, (byte)0, pos, FVector.Create(0-angleSpeed, FInt.Create(0)));
			}
			
			// Right
			else {
				pos.X += 30;
				ProjectileBullet.Create(room, (byte)0, pos, FVector.Create(angleSpeed, FInt.Create(0)));
			}

			room.PlaySound(Systems.sounds.cannonFire, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)CannonHorSubType.Left] = "Cannon/Left";
			this.Texture[(byte)CannonHorSubType.Right] = "Cannon/Right";
		}
	}
}
