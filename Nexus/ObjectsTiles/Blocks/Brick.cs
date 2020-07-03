using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Brick : BlockTile {

		public string[] Texture;

		public enum BrickSubType : byte {
			Brown = 0,
			Gray = 1,

			// Invisible SubTypes at +10 the original value. When set, the Draw() method won't draw them; but this maintains their original nature.
			InvisibleBrown = 10,
			InvisibleGray = 11,
		}

		public Brick() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Brick;
			this.title = "Brick";
			this.description = "Nudged from underneath. Can be destroyed with Spikey Hat.";
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			if(actor is Projectile) {
				if(actor is GloveProjectile) {
					this.DestroyBrick(room, gridX, gridY);
					return false;
				}
				return base.RunImpact(room, actor, gridX, gridY, dir);
			}

			// Nudge Brick Upward
			if(dir == DirCardinal.Up) {

				if(actor is Character) {
					Character character = (Character) actor;
					
					// Only Spikey Hats destroy Bricks
					if(character.hat is SpikeyHat) {
						BlockTile.DamageAbove(room, gridX, gridY);
						this.DestroyBrick(room, gridX, gridY);
						return base.RunImpact(room, actor, gridX, gridY, dir);
					}
				}

				// Nudge. Damages enemies above.
				byte sub = room.tilemap.GetMainSubType(gridX, gridY);

				// Make the brick disappear (turn invisible) and replace it with a nudge particle. Then return the brick to visible after completion.
				if(sub < 10) {
					SimpleEmitter.GravityParticle(room, this.Texture[sub], gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
					room.tilemap.SetTileSubType(gridX, gridY, (byte)(sub + 10));
					room.queueEvents.AddEvent(Systems.timer.Frame + 9, this.tileId, (short)gridX, (short)gridY);
				}

				BlockTile.DamageAbove(room, gridX, gridY);
				room.PlaySound(Systems.sounds.thudHit, 0.7f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
			}

			// Slam-Down Action can break bricks.
			if(actor is Character && ((Character)actor).status.action is SlamAction) {

				// Bricks will cause some hindrance, slowing down vertical velocity.
				actor.physics.velocity.Y *= FInt.Create(0.6);
				this.DestroyBrick(room, gridX, gridY);

				return true;
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		public void DestroyBrick(RoomScene room, short gridX, short gridY) {

			// Display Particle Effect
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			ExplodeEmitter.BoxExplosion(room, "Particles/Brick" + (subType == (byte)BrickSubType.Gray ? "Gray" : ""), gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight);
			room.PlaySound(Systems.sounds.brickBreak, 1f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);

			// Destroy Brick Tile
			room.tilemap.SetMainTile(gridX, gridY, 0, 0);
		}

		// Trigger Event: Swap between invisible and visible.
		public override bool TriggerEvent(RoomScene room, short gridX, short gridY, short val1 = 0, short val2 = 0) {
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			if(subType >= 10) {
				room.tilemap.SetTileSubType(gridX, gridY, (byte)(subType - 10));
			}

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) BrickSubType.Brown] = "Brick/Brown";
			this.Texture[(byte) BrickSubType.Gray] = "Brick/Gray";
		}
		
		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			// Don't Render any subtypes of 10+ (Invisible)
			if(subType >= 10) { return; }

			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
