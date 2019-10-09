
namespace Nexus.Gameplay {

	// Character Action Classes; maps with ID
	public enum CharacterActionId {

		None,

		// Standard Movement
		Jump,
		Slide,

		// Wall Movement
		GrabWall,
		WallJump,

		// Special Movement (Powers, etc)
		AirBurst,
		Flight,
		Hover,

		// Items
		DropItem,
		KickItem,
		ThrowItem,

		// Special
		Death,
		Stall,
	}

	// Enemy Action Classes; maps with ID
	public enum EnemyActionId {
		Charge,
		HopUp,
	}
}
