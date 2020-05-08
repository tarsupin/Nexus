using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class ToggleBlock : TileGameObject {

		public string Texture;
		protected bool toggleBR;    // TRUE if this tile toggles BR (blue-red), FALSE if toggles GY (green-yellow)
		protected bool on;          // TRUE if this tile is an ON tile, false if it's an OFF tile.

		public ToggleBlock() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ToggleBlock];
		}

		public bool Toggled(RoomScene room, bool toggleBR) {

			// Utility Bar (Editor) does not provide a room scene. Show default texture.
			if(room == null) { return this.on; }

			if(toggleBR) { return room.flags.toggleBR ? this.on : !this.on; }
			return room.flags.toggleGY ? this.on : !this.on;
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(this.Toggled(room, this.toggleBR)) {
				TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

				if(actor is Character) {

					// Standard Character Tile Collisions
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
