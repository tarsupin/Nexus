using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	// TogglePlat class has special collisions, since it can cause side-collisions or underneath-collisions.

	public class TogglePlat : TileGameObject {

		public string Texture;
		protected DirCardinal facing;
		protected bool Toggled;		// Child class will use this to reference the global scene toggles.

		public TogglePlat(LevelScene scene, byte subTypeId, TileGameObjectId classId) : base(scene, classId, AtlasGroup.Tiles) {
			this.collides = true;

			// Platform Faces Left
			if(subTypeId == (byte) FixedPlatSubType.FaceLeft) {
				this.facing = DirCardinal.Left;
			}

			// Platform Faces Right
			else if(subTypeId == (byte) FixedPlatSubType.FaceRight) {
				this.facing = DirCardinal.Right;
			}

			// Platform Faces Down
			else if(subTypeId == (byte) FixedPlatSubType.UpsideDown) {
				this.facing = DirCardinal.Down;
			}

			// Platform Faces Up
			else {
				this.facing = DirCardinal.Up;
			}
		}

		public override bool RunCollision(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(this.Toggled) {
				bool collided = TileFacingImpact.RunImpact(actor, gridX, gridY, dir, this.facing);

				if(collided && actor is Character) {

					// Standard Character Tile Collisions
					TileCharBasicImpact.RunImpact((Character)actor, dir);
				}

				return collided;
			}

			return false;
		}

		public override void Draw(byte subType, int posX, int posY) {

			// If Global Toggle is ON for Blue/Red, draw accordingly:
			this.atlas.Draw((this.Toggled ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
		}
	}
}
