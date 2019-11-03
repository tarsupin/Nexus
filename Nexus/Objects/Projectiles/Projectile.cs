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

	public class Projectile : DynamicGameObject {

		// Projectile Traits
		public DamageStrength Damage { get; protected set; }
		public ProjectileCollisionType CollisionType { get; protected set; }
		public bool SafelyJumpOnTop { get; protected set; }

		// References
		public Power power;				// Reference to the power used for this projectile.

		// Projectile Status
		public uint Intangible;         // The frame # that intangibility ends. Makes it intangible to certain dynamic objects.

		// Rendering
		public float rotation;

		public Projectile(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.Projectile];
			this.physics = new Physics(this);
			this.physics.velocity = velocity;
			this.SafelyJumpOnTop = false;
			this.Damage = DamageStrength.Standard;
		}

		public void ResetProjectile(byte subType, FVector pos, FVector velocity) {
			this.subType = subType;
			this.posX = pos.X.IntValue;
			this.posY = pos.Y.IntValue;
			this.physics.velocity = velocity;
		}

		public void SetVelocity( FVector velocity ) {
			this.physics.velocity = velocity;
		}

		public virtual void Destroy( DirCardinal dir = DirCardinal.Center, GameObject obj = null ) {
			this.Disable();
		}

		// If set, this activates when the projectile bounces on the ground. Can set physics.velocity.Y here to a designated amount.
		public virtual void BounceOnGround() {}
	}
}
