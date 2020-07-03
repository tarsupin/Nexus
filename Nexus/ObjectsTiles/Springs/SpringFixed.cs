using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class SpringFixed : SpringTile {

		public enum SpringFixedSubType : byte {
			Up = 0,
			Rev = 1,
		}

		public SpringFixed() : base() {
			this.title = "Fixed Spring";
			this.description = "A spring to bounce things on.";
			this.CreateTextures();
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {
			if(!base.RunImpact(room, actor, gridX, gridY, dir)) { return false; }

			// Get the SubType
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			
			if(subType == (byte)SpringFixedSubType.Up && dir == DirCardinal.Down) {
				if(actor is Character) {
					ActionMap.Jump.StartAction((Character)actor, 10, 0, 6, true, false);
					room.PlaySound(Systems.sounds.spring, 0.4f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				} else {
					actor.BounceUp(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, 8, 0, 5);
					room.PlaySound(Systems.sounds.spring, 0.4f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
				}
			}
			
			else if (subType == (byte)SpringFixedSubType.Rev && dir == DirCardinal.Up) {
				actor.BounceUp(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, -9, 0, 5);
				room.PlaySound(Systems.sounds.spring, 0.4f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
			}

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)SpringFixedSubType.Up] = "Spring/Up";
			this.Texture[(byte)SpringFixedSubType.Rev] = "Spring/Rev";
		}
	}
}
