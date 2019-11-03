using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Chomper : TileGameObject {

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
				Systems.sounds.splat1.Play();

				// Otherwise, a sound to indicate failure.
				// Systems.sounds.blah.Play();
			}

			return true;
		}

		public override void Draw(byte subType, int posX, int posY) {

			if(subType == (byte) FacingSubType.FaceUp) {
				this.atlas.Draw("Chomper/Grass/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceDown) {
				this.atlas.DrawFaceDown("Chomper/Grass/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceLeft) {
				this.atlas.DrawFaceLeft("Chomper/Grass/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceRight) {
				this.atlas.DrawFaceRight("Chomper/Grass/Chomp1", posX, posY);
			}
		}
	}
}
