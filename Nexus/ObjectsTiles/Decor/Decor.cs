﻿using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Decor : TileGameObject {

		public string[] Texture;

		public Decor(RoomScene room, TileGameObjectId classId) : base(room, classId, AtlasGroup.Tiles) {
			this.collides = false; // Since 'collides' is false, it never runs RunCollision() in base class.
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
