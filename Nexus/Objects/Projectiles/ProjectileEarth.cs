using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum ProjectileEarthSubType : byte {
		Earth
	}

	public class ProjectileEarth : Projectile {

		public ProjectileEarth() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) {
			this.SetCollisionType(ProjectileCollisionType.DestroyOnCollide);
			this.SetSpriteName("Projectiles/Earth1");
			this.SetDamage(DamageStrength.Lethal);
			this.SetSafelyJumpOnTop(true);
			this.physics.SetGravity(FInt.Create(0.1));
		}

		public static ProjectileEarth Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ProjectileEarth projectile = ProjectilePool.ProjectileEarth.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.spinRate = CalcRandom.FloatBetween(-0.07f, 0.07f);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		public override void Destroy( DirCardinal dir = DirCardinal.None, GameObject obj = null ) {
			EndBounceParticle.SetParticle(this.room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "Projectiles/Earth1", new Vector2(this.posX, this.posY), Systems.timer.Frame + 10);
			Systems.sounds.shellThud.Play();
			base.Destroy();
		}

		// Return Projectile to the Pool
		public override void ReturnToPool() {
			this.room.RemoveFromScene(this);
			ProjectilePool.ProjectileEarth.ReturnObject(this);
		}
	}
}
