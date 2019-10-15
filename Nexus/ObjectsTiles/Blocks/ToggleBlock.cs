﻿using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class ToggleBlock : TileGameObject {

		public string Texture;
		protected bool Toggled;		// Child class will use this to reference the global scene toggles.

		public ToggleBlock(LevelScene scene, TileGameObjectId classId) : base(scene, classId, AtlasGroup.Tiles) {
			this.collides = true;
		}

		public override bool RunCollision(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(this.Toggled) {
				bool collided = TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
				
				// Characters Can Wall Jump
				if(actor is Character) {
					if(dir == DirCardinal.Left || dir == DirCardinal.Right) {
						TileCharWallImpact.RunImpact((Character)actor, dir == DirCardinal.Right);
					}
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
