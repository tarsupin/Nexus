using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CannonHor : Cannon {

		protected CannonHorSubType subType;

		public enum CannonHorSubType : byte {
			Left = 0,
			Right = 1,
		}

		public CannonHor() : base() {
			this.Texture = "Cannon/Horizontal";
			this.tileId = (byte)TileEnum.CannonHorizontal;
		}

		public override void ActivateCannon(RoomScene room, byte subType, ushort gridX, ushort gridY) {

			// Left
			if(subType == (byte) CannonHorSubType.Left) {
				// projectile = Projectile.fire( this.scene, ProjectileBullet, "Bullet", this.pos.x - 30, this.pos.y + 0, -this.beats.speed, 0 );
			}
			
			// Right
			else {
				// projectile = Projectile.fire( this.scene, ProjectileBullet, "Bullet", this.pos.x + 30, this.pos.y + 0, this.beats.speed, 0 );
			}

			Systems.sounds.cannonFire.Play();
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
				if(subType == (byte) CannonHorSubType.Left) {
				this.atlas.Draw(this.Texture, posX, posY);
			} else {
				this.atlas.DrawFaceDown(this.Texture, posX, posY);
			}
		}
	}
}
