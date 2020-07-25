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

		public static bool Toggled(RoomScene room, bool toggleBR) {

			// Utility Bar (Editor) does not provide a room scene. Show default texture.
			if(room == null) { return true; }

			if(toggleBR) { return room.colors.toggleBR; }
			return room.colors.toggleGY;
		}

		public static bool TogCollides(RoomScene room, bool toggleBR, bool isOn) {
			if(ToggleBlock.Toggled(room, toggleBR)) { return isOn; }
			return !isOn;
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			if(this.isToggleBox || ToggleBlock.TogCollides(room, this.toggleBR, this.isOn)) {

				if(actor is Projectile) {
					if(actor is ShurikenProjectile && this.isToggleBox) {
						room.colors.ToggleColor(this.toggleBR);
					}
					return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
				}

				TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

				// If a ToggleBox was hit from below:
				if(this.isToggleBox && dir == DirCardinal.Up) {
					room.colors.ToggleColor(this.toggleBR);
				}

				// Additional Character Collisions (such as Wall Jumps)
				if(actor is Character) {
					TileCharBasicImpact.RunWallImpact((Character)actor, dir);
				}

				return true;
			}

			return false;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			// If Global Toggle is ON for Blue/Red, draw accordingly:
			this.atlas.Draw((ToggleBlock.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
		}
	}
}
