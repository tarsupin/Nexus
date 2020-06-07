using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;

namespace Nexus.Objects {

	public class SpringSide : SpringTile {

		public enum SpringSideSubType : byte {
			Left = 0,
			Right = 1,
		}

		public SpringSide() : base() {
			this.title = "Side Spring";
			this.description = "A spring that things bounce off of.";
			this.CreateTextures();
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			if(!base.RunImpact(room, actor, gridX, gridY, dir)) { return false; }

			// Get the SubType
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			if(subType == (byte)SpringSideSubType.Left && dir == DirCardinal.Right) {
				this.BounceSide(actor, gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, 20);
				Systems.sounds.spring.Play(0.4f, 0, 0);
			} else if(subType == (byte)SpringSideSubType.Right && dir == DirCardinal.Left) {
				this.BounceSide(actor, gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, -20);
				Systems.sounds.spring.Play(0.4f, 0, 0);
			}

			return true;
		}

		public void BounceSide(DynamicObject actor, int midY, sbyte strengthMod = 4, byte maxY = 2, sbyte relativeMult = 5) {

			actor.physics.velocity.X = FInt.Create(-strengthMod);

			if(maxY > 0) {
				short yDiff = CollideRect.GetRelativeX(actor, midY);
				FInt yAdjust = FInt.Create(Math.Min(maxY, Math.Abs(yDiff / relativeMult)));

				if(yDiff > 0) {
					actor.physics.velocity.Y += yAdjust;
				} else {
					actor.physics.velocity.Y -= yAdjust;
				}
			}
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)SpringSideSubType.Left] = "Spring/Left";
			this.Texture[(byte)SpringSideSubType.Right] = "Spring/Right";
		}
	}
}
