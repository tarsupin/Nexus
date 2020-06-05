using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class ToggleBlock : TileObject {

		public string Texture;
		protected bool toggleBR;    // TRUE if this tile toggles BR (blue-red), FALSE if toggles GY (green-yellow)
		protected bool isOn = false;
		protected bool isToggleBox = false;

		public ToggleBlock() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ToggleBlock];
		}

		public bool Toggled(RoomScene room, bool toggleBR) {

			// Utility Bar (Editor) does not provide a room scene. Show default texture.
			if(room == null) { return true; }

			if(toggleBR) { return room.flags.toggleBR; }
			return room.flags.toggleGY;
		}

		public bool TogCollides(RoomScene room, bool toggleBR) {
			if(this.isToggleBox) { return true; }
			if(this.Toggled(room, toggleBR)) { return this.isOn; }
			return !this.isOn;
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(this.TogCollides(room, this.toggleBR)) {
				TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

				// If a ToggleBox was hit from below:
				if(this.isToggleBox && dir == DirCardinal.Up) {
					room.ToggleColor(this.toggleBR);
				}

				// Additional Character Collisions (such as Wall Jumps)
				if(actor is Character) {
					TileCharBasicImpact.RunImpact((Character)actor, dir);
				}

				return true;
			}

			return false;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			// If Global Toggle is ON for Blue/Red, draw accordingly:
			this.atlas.Draw((this.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
		}
	}
}
