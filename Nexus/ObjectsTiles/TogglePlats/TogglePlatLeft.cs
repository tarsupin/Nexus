using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class TogglePlatLeft : TileObject {

		public string Texture;
		protected bool toggleBR;    // TRUE if this tile toggles BR (blue-red), FALSE if toggles GY (green-yellow)
		protected bool isOn = false;

		public TogglePlatLeft() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ToggleBlock];
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {
			if(!ToggleBlock.TogCollides(room, this.toggleBR, this.isOn)) { return false; }

			// Actor must cross the RIGHT threshold for this ledge; otherwise, it shouldn't compute any collision.
			if(!actor.physics.CrossedThresholdRight(gridX * (byte)TilemapEnum.TileWidth)) { return false; }

			if(actor is Projectile) {
				if(!CollideTileFacing.RunImpactTest(dir, DirCardinal.Left)) { return false; }
				return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
			}

			bool collided = CollideTileFacing.RunImpact(actor, gridX, gridY, dir, DirCardinal.Left);

			// Additional Character Collisions (such as Wall Jumps)
			if(collided && actor is Character) {
				TileCharBasicImpact.RunWallImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			bool toggled = ToggleBlock.Toggled(room, this.toggleBR);
			if(toggled == isOn) {
				this.atlas.DrawFaceLeft((ToggleBlock.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX, posY);
			} else {
				this.atlas.DrawFaceLeft((ToggleBlock.Toggled(room, this.toggleBR) ? "ToggleOn" : "ToggleOff") + this.Texture, posX + 2, posY);
			}
		}
	}
}
