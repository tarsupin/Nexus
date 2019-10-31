using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	// TogglePlat class has special collisions, since it can cause side-collisions or underneath-collisions.

	public class TogglePlat : TileGameObject {

		public string Texture;
		protected bool Toggled;		// Child class will use this to reference the global scene toggles.

		public TogglePlat(RoomScene room, TileGameObjectId classId) : base(room, classId, AtlasGroup.Tiles) {
			this.collides = true;
		}

		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(this.Toggled) {
				
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

		public override void Draw(byte subType, int posX, int posY) {

			if(subType == (byte) FacingSubType.FaceUp) {
				this.atlas.Draw((this.Toggled ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceDown) {
				this.atlas.DrawFaceDown((this.Toggled ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceLeft) {
				this.atlas.DrawFaceLeft((this.Toggled ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceRight) {
				this.atlas.DrawFaceRight((this.Toggled ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			}
		}
	}
}
