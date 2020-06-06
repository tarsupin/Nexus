﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class TogglePlatDown : TileObject {

		public string Texture;
		protected bool toggleBR;    // TRUE if this tile toggles BR (blue-red), FALSE if toggles GY (green-yellow)
		protected bool isOn = false;

		public TogglePlatDown() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ToggleBlock];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			if(!ToggleBlock.TogCollides(room, this.toggleBR, this.isOn)) { return false; }

			// Actor must cross the UP threshold for this ledge; otherwise, it shouldn't compute any collision.
			if(!actor.physics.CrossedThresholdUp(gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight)) { return false; }

			bool collided = CollideTileFacing.RunImpact(actor, gridX, gridY, dir, DirCardinal.Down);

			// Additional Character Collisions (such as Wall Jumps)
			if(collided && actor is Character) {
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			bool toggled = ToggleBlock.Toggled(room, this.toggleBR);
			if(toggled == isOn) {
				this.atlas.DrawFaceDown((ToggleBlock.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			} else {
				this.atlas.DrawFaceDown((ToggleBlock.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX - 2, posY - 2);
			}
		}
	}
}