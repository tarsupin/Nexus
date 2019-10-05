using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlock : ClassGameObject {

		public string Texture;
		protected bool Toggled;		// Child class will use this to reference the global scene toggles.

		public ToggleBlock(LevelScene scene, ClassGameObjectId classId) : base(scene, classId, AtlasGroup.Tiles) {

		}

		public override void Draw(byte subType, ushort posX, ushort posY) {

			// If Global Toggle is ON for Blue/Red, draw accordingly:
			this.atlas.Draw((this.Toggled ? "ToggleOn" : "ToggleOff") + this.Texture, FVector.Create(posX, posY));
		}
	}
}
