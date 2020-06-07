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

		public override void ActivateCannon(RoomScene room, byte subType, ushort gridX, ushort gridY) {

			// Up Right
			if(subType == (byte) CannonDiagSubType.UpRight) {
				// projectile = Projectile.fire( this.scene, ProjectileBullet, "Bullet", this.pos.x + 18, this.pos.y - 18, this.angleSpeed, -this.angleSpeed );
			}

			// Down Right
			else if(subType == (byte) CannonDiagSubType.DownRight) {
				// projectile = Projectile.fire( this.scene, ProjectileBullet, "Bullet", this.pos.x + 16, this.pos.y + 19, this.angleSpeed, this.angleSpeed );
			}

			// Down Left
			else if(subType == (byte) CannonDiagSubType.DownLeft) {
				// projectile = Projectile.fire( this.scene, ProjectileBullet, "Bullet", this.pos.x - 17, this.pos.y + 19, -this.angleSpeed, this.angleSpeed );
			}

			// Up Left
			else {
				// projectile = Projectile.fire( this.scene, ProjectileBullet, "Bullet", this.pos.x - 17, this.pos.y - 20, -this.angleSpeed, -this.angleSpeed );
			}

			Systems.sounds.cannonFire.Play();
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
