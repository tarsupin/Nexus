using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class ElementalFire : Elemental {

		private const byte BaseAttackSpeed = 8;

		public enum ElementalFireSubType : byte {
			Left,
			Right,
		}

		public AttackSequence attack;
		public sbyte attSpeed = 0;

		public ElementalFire(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.ElementalFire].meta;
			this.attack = new AttackSequence(paramList);
			this.attSpeed = (sbyte)Math.Round((paramList.ContainsKey("speed") ? paramList["speed"] : 100) * 0.01 * BaseAttackSpeed);
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 4, -4, -12);
		}

		public override void RunTick() {
			base.RunTick();

			// Check if an attack needs to be made:
			if(this.attack.AttackThisFrame()) {
				ProjectileBall projectile = ProjectileBall.Create(room, (byte)ProjectileBallSubType.EnemyFire, FVector.Create(this.posX + this.bounds.MidX - 10, this.posY + this.bounds.MidY - 10), FVector.Create(this.attSpeed, 0));
			}
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte)ElementalFireSubType.Left) {
				this.SpriteName = "Elemental/Fire/Left";
				this.FaceRight = false;
				this.attSpeed = (sbyte)-this.attSpeed;
			} else {
				this.SpriteName = "Elemental/Fire/Right";
				this.FaceRight = true;
			}
		}
	}
}
