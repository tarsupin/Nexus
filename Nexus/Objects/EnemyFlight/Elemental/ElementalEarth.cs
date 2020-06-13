using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class ElementalEarth : Elemental {

		private const byte BaseAttackSpeed = 4;

		public enum ElementalEarthSubType : byte {
			Left,
			Right,
		}

		public AttackSequence attack;
		public sbyte attSpeed = 0;

		public ElementalEarth(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.ElementalEarth].meta;
			this.attack = new AttackSequence(paramList);
			this.attSpeed = (sbyte)Math.Round((paramList != null && paramList.ContainsKey("speed") ? paramList["speed"] : 100) * 0.01 * BaseAttackSpeed);
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 4, -4, -12);
		}

		public override void RunTick() {
			base.RunTick();

			// Check if an attack needs to be made:
			if(this.attack.AttackThisFrame()) {
				ProjectileEarth projectile = ProjectileEarth.Create(room, (byte)ProjectileEarthSubType.Earth, FVector.Create(this.posX + this.bounds.MidX - 10, this.posY + this.bounds.Bottom - 10), FVector.Create(0, this.attSpeed));
				Systems.sounds.rock.Play(0.4f, 0, 0);
			}
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte)ElementalEarthSubType.Left) {
				this.SpriteName = "Elemental/Earth/Left";
				this.FaceRight = false;
			} else {
				this.SpriteName = "Elemental/Earth/Right";
				this.FaceRight = true;
			}
		}
	}
}
