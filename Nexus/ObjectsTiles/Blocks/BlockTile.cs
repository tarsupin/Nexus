using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class BlockTile : TileObject {

		public BlockTile() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Block];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
				return TileCharBasicImpact.RunImpact((Character)actor, dir);
			}
			
			if(actor is Projectile) {
				return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
			}

			return TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
		}

		public static void BreakApart(RoomScene room, ushort gridX, ushort gridY) {

			// Damage Creatures Above (if applicable)
			uint enemyFoundId = CollideDetect.FindObjectsTouchingArea(room.objects[(byte)LoadOrder.Enemy], (uint)gridX * (byte)TilemapEnum.TileWidth + 16, (uint)gridY * (byte)TilemapEnum.TileHeight - 4, 16, 4);

			if(enemyFoundId > 0) {
				Enemy enemy = (Enemy)room.objects[(byte)LoadOrder.Enemy][enemyFoundId];
				enemy.Die(DeathResult.Knockout);
			}

			// Destroy Brick Tile
			room.tilemap.RemoveTile(gridX, gridY);

			// Brick Breaking Sound
			Systems.sounds.brickBreak.Play();
		}
	}
}
