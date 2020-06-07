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
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)CannonHorSubType.Left] = "Cannon/Left";
			this.Texture[(byte)CannonHorSubType.Right] = "Cannon/Right";
		}
	}
}
