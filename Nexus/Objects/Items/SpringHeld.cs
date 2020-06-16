using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class SpringHeld : Item {

		public enum  SpringHeldSubType : byte {
			Norm,
		}

		public SpringHeld(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.SpringHeld].meta;
			this.ThrowStrength = 14;

			// Grip Points (When Held)
			this.gripLeft = -35;
			this.gripRight = 25;
			this.gripLift = -8;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 2, -2, 0);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) SpringHeldSubType.Norm) {
				this.SpriteName = "Spring/Up";
			}
		}

		// Something collided with the top of the Spring.
		public override bool CollideObjUp(GameObject obj) {
			
			if(obj is Character) {
				ActionMap.Jump.StartAction((Character)obj, 10, 0, 6, true, false);
			}
			
			else {
				obj.BounceUp(this.posX + this.bounds.MidX, 8, 0, 5);
			}

			Systems.sounds.spring.Play(0.4f, 0, 0);

			return base.CollideObjUp(obj);
		}

		public override bool CollideObjDown(GameObject obj) {

			if(obj is Character) {
				Character character = (Character)obj;

				// End any action that ends upward:
				Action action = character.status.action;

				if(action is JumpAction || action is WallJumpAction) {
					character.status.action.EndAction(character);
				}
			}

			obj.BounceUp(this.posX + this.bounds.MidX, -9, 0, 5);
			Systems.sounds.spring.Play(0.4f, 0, 0);

			this.physics.touch.TouchDown();
			this.physics.AlignUp(obj);
			this.physics.StopY();
			this.physics.velocity.Y -= 5;

			return true;
		}
	}
}
