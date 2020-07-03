using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;

namespace Nexus.Objects {

	public class PuffBlockOmni : BlockTile {

		public string Texture;

		public PuffBlockOmni() : base() {
			this.tileId = (byte) TileEnum.PuffBlockOmni;
			this.Texture = "Puff/Omni";
			this.title = "Omni-Puff Block";
			this.description = "Touching it enhances the character's momentum.";
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			// Only run this test for Characters.
			if(!(actor is Character)) { return false; }

			Character character = (Character) actor;

			if(character.status.action is FastMoveAction) { return false; }

			var phys = character.physics;

			int velX = phys.velocity.X.RoundInt;
			int velY = phys.velocity.Y.RoundInt;
			
			// Default Speed Increase, if applicable:
			if(velX > 1) { phys.velocity.X += FInt.Create(1); }
			else if(velX < -1) { phys.velocity.X += FInt.Create(-1); }
			
			if(velY > 1) { phys.velocity.Y += FInt.Create(1); }
			else if(velY < -1) { phys.velocity.Y += FInt.Create(-1); }

			// Increase Character's Momentum
			if(Math.Abs(velX) > 8) {
				if(Math.Abs(velX) < 14) { phys.velocity.X *= FInt.Create(1.3); }
			}
			else { phys.velocity.X *= FInt.Create(1.6); }
			
			if(velY < 0) {
				if(velY < -8) {
					if(velY > -16) { phys.velocity.Y *= FInt.Create(1.8); }
				} else {
					phys.velocity.Y *= FInt.Create(2.2);
				}
			} else if(velY > 0) {
				if(velY < 8) {
					phys.velocity.Y *= FInt.Create(1.3);
				}
			}

			ActionMap.FastMove.StartAction(character, 10);

			room.PlaySound(Systems.sounds.air, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);

			return true;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
