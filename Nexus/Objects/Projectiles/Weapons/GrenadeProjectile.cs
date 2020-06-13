using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class GrenadeProjectile : Projectile {

		private uint endFrame;          // The frame that a movement style ends on.

		public GrenadeProjectile() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) {
			this.Damage = DamageStrength.None;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			this.physics.SetGravity(FInt.Create(0.4));
			this.SetSpriteName("Projectiles/Grenade");
		}

		public static GrenadeProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			GrenadeProjectile projectile = ProjectilePool.GrenadeProjectile.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.SetState((byte)CommonState.Move);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.spinRate = projectile.physics.velocity.X > 0 ? 0.07f : -0.07f;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawRotation(this.SpriteName, this.posX + 16 - camX, this.posY + 16 - camY, this.rotation, new Vector2(16, 16));
		}

		public override void Destroy(DirCardinal dir = DirCardinal.None, GameObject obj = null) {
			if(this.State == (byte)CommonState.Death) { return; }

			this.SetState((byte)CommonState.Death);
			this.physics.SetGravity(FInt.Create(0.7));
			this.endFrame = Systems.timer.Frame + 12;

			Physics physics = this.physics;

			if(dir == DirCardinal.Right || dir == DirCardinal.Left) {
				physics.velocity.Y = physics.velocity.Y < 0 ? physics.velocity.Y * FInt.Create(0.25) : FInt.Create(-3);
				physics.velocity.X = dir == DirCardinal.Right ? FInt.Create(-1) : FInt.Create(1);
			} else if(dir == DirCardinal.Down || dir == DirCardinal.Up) {
				physics.velocity.X = physics.velocity.X * FInt.Create(0.25);
				physics.velocity.Y = dir == DirCardinal.Down ? FInt.Create(-4) : FInt.Create(1);
			} else {
				physics.velocity.X = FInt.Create(0);
				physics.velocity.Y = FInt.Create(-3);
			}

			Systems.sounds.shellThud.Play(0.4f, 0, 0);
		}

		public override void RunTick() {

			this.rotation += this.spinRate;

			// Standard Motion for Shuriken
			if(this.State == (byte)CommonState.MotionStart) {
				if(this.endFrame == Systems.timer.Frame) {
					this.SetState((byte)CommonState.Motion);
					this.physics.SetGravity(FInt.Create(0.4));
				}
			}

			// Death Motion for Shuriken
			else if(this.State == (byte)CommonState.Death) {
				this.rotation += this.spinRate > 0 ? -0.08f : 0.08f;

				// Explosion
				if(this.endFrame == Systems.timer.Frame) {

					TilemapLevel tilemap = this.room.tilemap;
					int midX = this.posX + 16;
					int midY = this.posY + 16;
					int gridX = (int) Math.Floor((double)(midX / (byte)TilemapEnum.TileWidth));
					int gridY = (int) Math.Floor((double)(midY / (byte)TilemapEnum.TileHeight));

					// Destroy Enemies Within Area of Effect
					List<GameObject> enemiesFound = CollideRect.FindAllObjectsTouchingArea(room.objects[(byte)LoadOrder.Enemy], (uint)(midX - 144), (uint)(midY - 96), 288, 192);

					foreach(GameObject enemy in enemiesFound) {
						((Enemy)enemy).Die(DeathResult.Knockout);
					}

					// Destroy Certain Tiles Within Area of Effect
					ushort startX = (ushort) Math.Max(0, gridX - 3);
					ushort startY = (ushort) Math.Max(0, gridY - 2);
					ushort endX = (ushort) Math.Min(tilemap.XCount, gridX + 3);
					ushort endY = (ushort) Math.Min(tilemap.YCount, gridY + 2);

					// Locate Tiles: Chompers, Boxes, Bricks, Leafs, etc.
					var tilesFound = tilemap.GetTilesByMainIDsWithinArea(new byte[7] { (byte)TileEnum.ChomperFire, (byte)TileEnum.ChomperGrass, (byte)TileEnum.ChomperMetal, (byte)TileEnum.Plant, (byte)TileEnum.Box, (byte)TileEnum.Brick, (byte)TileEnum.Leaf }, startX, startY, endX, endY);

					var TileDict = Systems.mapper.TileDict;

					foreach(var tileInfo in tilesFound) {
						TileObject tile = TileDict[tileInfo.tileId];

						if(tile is Chomper) { ((Chomper)tile).DestroyChomper(this.room, tileInfo.gridX, tileInfo.gridY); }
						else if(tile is Brick) { ((Brick)tile).DestroyBrick(this.room, tileInfo.gridX, tileInfo.gridY); }
						else if(tile is Box) { ((Box)tile).DestroyBox(this.room, tileInfo.gridX, tileInfo.gridY); }
						else if(tile is Leaf) { ((Leaf)tile).TriggerEvent(this.room, tileInfo.gridX, tileInfo.gridY, 0); }
					}

					// Display Particle Effect
					ExplodeEmitter.BoxExplosion(this.room, "Particles/GrenadeFrag", midX, midY, 20, 15);
					ExplodeEmitter.BoxExplosion(this.room, "Particles/GrenadeFrag", midX, midY, 70, 50);

					Systems.sounds.explosion.Play(0.4f, 0, 0);

					// Camera Shake
					Systems.camera.BeginCameraShake(35, 6);

					this.ReturnToPool();
				}
			}

			// Standard Physics
			this.physics.RunPhysicsTick();
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.GrenadeProjectile.ReturnObject(this);
		}
	}
}
