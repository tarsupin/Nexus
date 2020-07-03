using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class SlamMobility : PowerMobility {

		public SlamMobility( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Slam;
			this.IconTexture = "Power/Slam";
			this.subStr = "slam";
			this.SetActivationSettings(30, 1, 30);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// Make sure you're not on the ground when attempting to activate.
			if(this.character.physics.touch.toFloor) { return false; }

			// Start the Slam Action
			ActionMap.Slam.StartAction(this.character);

			// Display the "Air" particle event and play the "Air" sound.
			BurstEmitter.AirPuff(this.character.room, this.character.posX + this.character.bounds.MidX, this.character.posY + this.character.bounds.MidY, 0, 1, 18);
			this.character.room.PlaySound(Systems.sounds.air, 0.5f, this.character.posX + 16, this.character.posY + 16);

			return true;
		}
	}
}
