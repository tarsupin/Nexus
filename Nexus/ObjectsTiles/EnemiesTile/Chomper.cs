using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Chomper : TileGameObject {

		protected string SpriteName;	// The base name for the texture.
		protected string KnockoutName;	// The particle texture string to use when it's knocked out.

		protected Chomper(RoomScene room, TileGameObjectId classId) : base(room, classId, AtlasGroup.Tiles) {
			this.collides = true;
		}

		// TODO HIGH PRIORITY: Chomper Impacts (projectiles, character, etc.)
		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
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
				this.Destroy(gridX, gridY);
				DeathEmitter.Knockout(this.room, this.KnockoutName, gridX * (byte) TilemapEnum.TileWidth, gridY * (byte) TilemapEnum.TileHeight);
				Systems.sounds.splat1.Play();

				// Otherwise, a sound to indicate failure.
				// Systems.sounds.blah.Play();
			}

			return true;
		}
	}
}
