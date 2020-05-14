﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class BGDisable : BGTile {

		public string Texture;

		public BGDisable() : base() {
			this.tileId = (byte) TileEnum.BGDisable;
			this.Texture = "BGTile/Disable";
			this.title = "Disable Zone";
			this.description = "Removes all equipment and powers.";
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				Character character = (Character)actor;

				if(character.DisableAbilities()) {
					Systems.sounds.disableCollectable.Play();
				}
			}

			return false;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
