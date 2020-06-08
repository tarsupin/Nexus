using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Chomper : TileObject {

		public string SpriteName;       // The default name for the Sprite.
		protected string KnockoutName;	// The particle texture string to use when it's knocked out.

		protected Chomper() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyFixed];
		}

		// TODO HIGH PRIORITY: Chomper Impacts (projectiles, character, etc.)
		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			// Characters Receive Chomper Damage
			if(actor is Character) {
				Character character = (Character) actor;
				character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}
			
			// Chompers die when hit by projectiles of sufficient damage.
			else if(actor is Projectile) {
				(actor as Projectile).Destroy(dir);

				// TODO: Check that projectile deals enough damage.
				room.tilemap.RemoveTile(gridX, gridY);
				DeathEmitter.Knockout(room, this.KnockoutName, gridX * (byte) TilemapEnum.TileWidth, gridY * (byte) TilemapEnum.TileHeight);
				Systems.sounds.splat1.Play();

				// Otherwise, a sound to indicate failure.
				// Systems.sounds.blah.Play();
			}

			return true;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			// Get the Chomping Texture with the Global Animation AnimId
			string tex = this.SpriteName + (AnimGlobal.Get4PSAnimId(Systems.timer) % 2 + 1).ToString();

			if(subType == (byte) FacingSubType.FaceUp) {
				this.atlas.Draw(tex, posX, posY);
			} else if(subType == (byte) FacingSubType.FaceDown) {
				this.atlas.DrawFaceDown(tex, posX, posY);
			} else if(subType == (byte) FacingSubType.FaceLeft) {
				this.atlas.DrawFaceLeft(tex, posX, posY);
			} else if(subType == (byte) FacingSubType.FaceRight) {
				this.atlas.DrawFaceRight(tex, posX, posY);
			}
		}
	}
}
