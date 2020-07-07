using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum SlammerSubType : byte {
		Slammer
	}

	public class Slammer : Enemy {

		public Slammer(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Slammer].meta;
			this.ProjectileResist = DamageStrength.InstantKill;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(0, 0, -0, -0);

			// Assign Slammer Behavior
			this.behavior = new SlammerBehavior(this, paramList);
		}

		public override void CollidePosDown(int posY) {
			base.CollidePosUp(posY);
			((SlammerBehavior)this.behavior).EndSlam(this);
		}
		
		public override bool CollideObjUp(GameObject obj) {
			this.physics.touch.TouchUp();
			obj.physics.touch.TouchDown();
			obj.physics.AlignUp(this);
			return true;
		}

		public override bool RunCharacterImpact(Character character) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, this);

			if(dir == DirCardinal.Left) {
				TileCharBasicImpact.RunWallImpact(character, dir, DirCardinal.None);
			} else if(dir == DirCardinal.Right) {
				TileCharBasicImpact.RunWallImpact(character, dir, DirCardinal.None);
			}
			
			// Character is Beneath
			else if(dir == DirCardinal.Up) {
				if(this.physics.velocity.Y > 0) {
					character.wounds.ReceiveWoundDamage(DamageStrength.Standard);

					// Will kill if character is on ground.
					if(character.physics.touch.toBottom) {
						character.wounds.ReceiveWoundDamage(DamageStrength.InstantKill);
						return true;
					}
				}

				TileCharBasicImpact.RunWallImpact(character, dir, DirCardinal.None);
			}

			return Impact.StandardImpact(character, this, dir);
		}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Slammer/Standard";
		}
	}
}
