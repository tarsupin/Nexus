using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public enum ProjectileCollisionType {
		IgnoreWalls = 1,			// Passes through walls harmlessly.
		DestroyOnCollide = 2,       // Projectile gets destroyed on collision.
		BounceOnFloor = 3,          // Bounces on the floor.
		BreakObjects = 4,           // Breaks objects (e.g. Boxing Glove).
		Special = 5,                // Special Collision type (Used for Earth, which runs Destroy())
	}

	public class Projectile : DynamicObject {

		// Projectile Traits
		public DamageStrength Damage { get; protected set; }
		public ProjectileCollisionType CollisionType { get; protected set; }
		public bool SafelyJumpOnTop { get; protected set; }
		public uint ByActorID { get; protected set; }

		// References
		protected Atlas atlas;
		public Power power;				// Reference to the power used for this projectile.

		// Projectile Status
		public uint Intangible;         // The frame # that intangibility ends. Makes it intangible to certain dynamic objects.

		// Essentials
		protected uint EndLife;
		public float spinRate;			// The rate of rotation, if applicable.
		public float rotation;

		public Projectile(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.Projectile];
			this.physics = new Physics(this);
			this.SafelyJumpOnTop = false;
			this.Damage = DamageStrength.Standard;
			this.ResetProjectile(subType, pos, velocity);
		}

		public override void RunTick() {

			// If the Projectile's life has expired.
			if(this.EndLife < Systems.timer.Frame) {
				this.ReturnToPool();
				return;
			}

			// Spin Rate
			if(this.spinRate != 0) { this.rotation += this.spinRate; }

			// Standard Physics
			this.physics.RunPhysicsTick();
		}

		public void SetActorID(DynamicObject actor) {
			this.ByActorID = actor.id;
		}

		public void SetEndLife( uint endFrame ) {
			this.EndLife = endFrame;
		}

		public void ResetProjectile(byte subType, FVector pos, FVector velocity) {
			this.subType = subType;
			this.spinRate = 0;
			this.physics.MoveToPos(pos.X.RoundInt, pos.Y.RoundInt);
			this.physics.velocity = velocity;
			this.ByActorID = 0;
			this.SetEndLife(Systems.timer.Frame + 600);
		}

		public void SetVelocity( FVector velocity ) {
			this.physics.velocity = velocity;
		}

		public virtual void Destroy( DirCardinal dir = DirCardinal.None, DynamicObject obj = null ) {
			this.ReturnToPool();
		}

		// TODO: MAKE THIS WORK CORRECTLY
		// Disables the instance of this object, returning it to a pool rather than destroying it altogether.
		public virtual void ReturnToPool() {
			this.room.RemoveFromScene(this);        // This is identical to Destroy(), but probably works since pool is connected elsewhere.
		}

		// If set, this activates when the projectile bounces on the ground. Can set physics.velocity.Y here to a designated amount.
		public virtual void BounceOnGround() {}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.DrawAdvanced(this.SpriteName, this.posX - camX, this.posY - camY, null, this.rotation);
		}
	}
}
