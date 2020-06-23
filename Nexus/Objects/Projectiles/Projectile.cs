using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum ProjectileCollisionType {
		IgnoreWalls = 1,			// Passes through walls harmlessly.
		DestroyOnCollide = 2,       // Projectile gets destroyed on collision.
		BounceOnFloor = 3,          // Bounces on the floor.
	}

	public class Projectile : GameObject {

		// Projectile Traits
		public DamageStrength Damage { get; protected set; }
		public ProjectileCollisionType CollisionType { get; protected set; }
		public bool SafelyJumpOnTop { get; protected set; }
		public int ByCharacterId { get; protected set; }

		// References
		protected Atlas atlas;
		//public Power power;			// Reference to the power used for this projectile.

		// Projectile Status
		public int Intangible;			// The frame # that intangibility ends. Makes it intangible to certain dynamic objects.

		// Essentials
		protected int EndLife;
		public float spinRate;			// The rate of rotation, if applicable.
		public float rotation;

		public Projectile(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.Projectile];
			this.physics = new Physics(this);
			this.ResetProjectile(room, subType, pos, velocity);
		}

		public void SetCollisionType(ProjectileCollisionType type) { this.CollisionType = type; }
		public void SetSafelyJumpOnTop(bool safe = true) { this.SafelyJumpOnTop = safe; }
		public void SetActorID(GameObject actor) { this.ByCharacterId = actor.id; }
		public void SetDamage(DamageStrength damage) { this.Damage = damage; }
		public void SetEndLife(int endFrame) { this.EndLife = endFrame; }
		public void SetVelocity(FVector velocity) { this.physics.velocity = velocity; }

		public override void RunTick() {

			// If the Projectile's life has expired.
			if(this.EndLife < Systems.timer.Frame) {
				this.ReturnToPool();
				return;
			}

			// Spin Rate
			if(this.spinRate != 0) { this.rotation += this.spinRate; }

			// Run Physics
			if(this.physics.velocity.Y > 20) { this.physics.velocity.Y = FInt.Create(20); }
			this.physics.RunPhysicsTick();
		}

		public void ResetProjectile(RoomScene room, byte subType, FVector pos, FVector velocity) {
			this.room = room;
			if(this.id == 0) { this.id = this.room == null ? 0 : this.room.nextId; }
			this.subType = subType;
			this.spinRate = 0;
			this.physics.MoveToPos(pos.X.RoundInt, pos.Y.RoundInt);
			this.physics.velocity = velocity;
			this.ByCharacterId = 0;
			this.SafelyJumpOnTop = false;
			this.Damage = DamageStrength.Standard;
			this.EndLife = Systems.timer.Frame + 300;
		}

		public virtual void Destroy( DirCardinal dir = DirCardinal.None, GameObject obj = null ) {
			this.ReturnToPool();
		}

		// Disables the instance of this object, returning it to a pool rather than destroying it altogether.
		public virtual void ReturnToPool() {
			//this.room.RemoveFromScene(this);        // This is identical to Destroy(), but probably works since pool is connected elsewhere.
			throw new System.Exception("Must overwrite this method");
		}

		// If set, this activates when the projectile bounces on the ground. Can set physics.velocity.Y here to a designated amount.
		public virtual void BounceOnGround() {}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawAdvanced(this.SpriteName, this.posX - camX, this.posY - camY, null, this.rotation);
		}
	}
}
