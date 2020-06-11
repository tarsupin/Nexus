using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Chomper : TileObject {

		public string SpriteName;				// The default name for the Sprite.
		protected string KnockoutName;			// The particle texture string to use when it's knocked out
		protected DamageStrength DamageSurvive;	// The amount of damage something can take before dying.

		protected Chomper() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.EnemyFixed];
		}

		public override bool RunImpact(RoomScene room, GameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Chompers die when hit by projectiles of sufficient damage.
			if(actor is Projectile) {
				Projectile proj = (Projectile)actor;

				if(proj is ProjectileMagi) {
					if(proj.Damage < this.DamageSurvive) { return false; }
					if(!((ProjectileMagi) proj).CanDamage) { return false; }
				}

				// Verify the projectile does enough damage:
				if(proj.Damage >= this.DamageSurvive || this is Plant) {

					// Remove the Chomper and Display it's knockout effect.
					room.tilemap.ClearMainLayer(gridX, gridY);
					DeathEmitter.Knockout(room, this.KnockoutName, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
					Systems.sounds.splat1.Play();
				}

				// Have to destroy projectile afterward, since it can affect projectile strength if it has unique death properties.
				proj.Destroy(dir);

				return true;
			}
			
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			// Characters Receive Chomper Damage
			if(actor is Character) {
				Character character = (Character) actor;
				character.wounds.ReceiveWoundDamage(this is Plant ? DamageStrength.None : DamageStrength.Standard);
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
