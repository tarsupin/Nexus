﻿using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power activates Hovering.
	public class HoverMobility : PowerMobility {

		public HoverMobility( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Hover;
			this.IconTexture = "Power/Hover";
			this.subStr = "hover";
			this.SetActivationSettings(180, 1, 180);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// Start the Hover Action
			ActionMap.Hover.StartAction(this.character, false);
			this.character.room.PlaySound(Systems.sounds.wooshDeep, 1f, this.character.posX + 16, this.character.posY + 16);
			return true;
		}
	}
}
