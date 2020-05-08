using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CannonVert : Cannon {

		protected CannonVertSubType subType;

		public enum CannonVertSubType : byte {
			Up = 0,
			Down = 1,
		}

		public CannonVert() : base() {
			this.Texture = "Cannon/Vertical";
			this.tileId = (byte)TileEnum.CannonVertical;
		}

		public override void ActivateCannon(RoomScene room, byte subType, ushort gridX, ushort gridY) {

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
			if(subType == (byte) CannonVertSubType.Up) {
				this.atlas.Draw(this.Texture, posX, posY);
			} else {
				this.atlas.DrawFaceDown(this.Texture, posX, posY);
			}
		}
	}
}
