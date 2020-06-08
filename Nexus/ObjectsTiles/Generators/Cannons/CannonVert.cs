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

			// Up
			if(subType == (byte)CannonVertSubType.Up) {
				// projectile = Projectile.fire( this.scene, ProjectileBullet, "Bullet", this.pos.x + 2, this.pos.y - 30, 0, -this.beats.speed );
			}

			// Down
			else {
				// projectile = Projectile.fire(this.scene, ProjectileBullet, "Bullet", this.pos.x + 2, this.pos.y + 30, 0, this.beats.speed);
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
