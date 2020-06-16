using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class ButtonHeld : Item {

		protected bool toggleBR = false;    // TRUE if this tile toggles BR (blue-red), FALSE if toggles GY (green-yellow)
		protected bool isDown = false;      // TRUE if this the DOWN version.

		public ButtonHeld(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.ButtonHeld].meta;
			this.ThrowStrength = 14;

			// Grip Points (When Held)
			this.gripLeft = -35;
			this.gripRight = 25;
			this.gripLift = -8;

			// Physics
			this.physics.SetGravity(FInt.Create(0.5));

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(12, 4, -4, 0);

			// Assign ToggleBR - designates if the button is toggled with Blue-Red or Green-Yellow
			this.toggleBR = subType == (byte)ButtonSubTypes.BR ? true : false;
			this.isDown = false;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) ButtonSubTypes.BR) { this.SpriteName = "Button/BR"; }
			else if(subType == (byte) ButtonSubTypes.GY) { this.SpriteName = "Button/GY"; }
		}

		public override void ActivateItem(Character character) {

			// Press Button Down
			this.isDown = !this.isDown;

			// Toggle the color-toggle that matches this tap type.
			this.room.colors.ToggleColor(this.toggleBR);
		}

		public override bool CollideObjUp(GameObject obj) {

			if(obj is Character) {
				ActionMap.Jump.StartAction((Character)obj, 5, 0, 4, true);
			}
			
			else if(obj is EnemyLand || obj is Item) {
				obj.BounceUp(this.posX + this.bounds.MidX, 9, 1, 2);
				obj.physics.SetExtraMovement(0, -2);
				obj.physics.RunPhysicsTick();
			}
			
			else { return false; }

			this.ActivateItem(null);

			return base.CollideObjUp(obj);
		}

		public override void Draw(int camX, int camY) {
			this.Meta.Atlas.Draw(this.SpriteName + ((this.toggleBR ? this.room.colors.toggleBR : this.room.colors.toggleGY) ? "" : "Off") + (this.isDown ? "Down" : ""), this.posX - camX, this.posY - camY);
		}
	}
}
