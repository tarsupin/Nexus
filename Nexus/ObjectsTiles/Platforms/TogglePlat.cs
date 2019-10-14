using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	// TogglePlat class has special collisions, since it can cause side-collisions or underneath-collisions.

	public class TogglePlat : TileGameObject {

		public string Texture;
		protected bool Toggled;		// Child class will use this to reference the global scene toggles.

		public TogglePlat(LevelScene scene, byte subTypeId, TileGameObjectId classId) : base(scene, classId, AtlasGroup.Tiles) {

			// Platform Faces Left
			if(subTypeId == (byte) FixedPlatSubType.FaceLeft) {
				// TODO: Platform Facing
			// Platform Faces Right
			} else if(subTypeId == (byte) FixedPlatSubType.FaceRight) {
				// TODO: Platform Facing
			// Platform Faces Down
			} else if(subTypeId == (byte) FixedPlatSubType.UpsideDown) {
				// TODO: Platform Facing
			}
		}

		public override void Draw(byte subType, int posX, int posY) {

			// If Global Toggle is ON for Blue/Red, draw accordingly:
			this.atlas.Draw((this.Toggled ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
		}
	}
}
