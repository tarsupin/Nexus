using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	// TogglePlat class has special collisions, since it can cause side-collisions or underneath-collisions.

	public class TogglePlat : TileGameObject {

		public string Texture;
		protected bool toggleBR;    // TRUE if this tile toggles BR (blue-red), FALSE if toggles GY (green-yellow)

		public TogglePlat() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ToggleBlock];
		}

		public bool Toggled(RoomScene room, bool toggleBR) {
			if(toggleBR) { return room.flags.toggleBR; }
			return room.flags.toggleGY;
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(this.Toggled(room, this.toggleBR)) {

				// TODO HIGH PRIORITY: Need to send subtype to RunImpact();
				// TODO HIGH PRIORITY: Change DirCardinal.Up to the direction this tile is facing (based on the subtype)
				bool collided = TileFacingImpact.RunImpact(actor, gridX, gridY, dir, DirCardinal.Up);

				if(collided && actor is Character) {

					// Standard Character Tile Collisions
					TileCharBasicImpact.RunImpact((Character)actor, dir);
				}

				return collided;
			}

			return false;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			if(subType == (byte) FacingSubType.FaceUp) {
				this.atlas.Draw((this.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceDown) {
				this.atlas.DrawFaceDown((this.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceLeft) {
				this.atlas.DrawFaceLeft((this.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceRight) {
				this.atlas.DrawFaceRight((this.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			}
		}
	}
}
