using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum FlairFireSubType : byte { Normal };

	public class FlairFire : Flair {

		private const byte BaseAttackSpeed = 4;
		public AttackSequence attack;
		public sbyte attSpeed = 0;
		public FInt gravity = FInt.Create(0);

		public FlairFire(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.FlairFire].meta;
			this.attack = new AttackSequence(paramList);
			this.attSpeed = (sbyte)Math.Round(paramList == null || !paramList.ContainsKey("speed") ? BaseAttackSpeed : paramList["speed"] * 0.01 * BaseAttackSpeed);
			this.gravity = FInt.Create(paramList == null || !paramList.ContainsKey("grav") ? 0 : paramList["grav"] * 0.01f * 0.5f);
			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(2, 4, -4, -10);
		}

		public override void RunTick() {
			base.RunTick();

			// Check if an attack needs to be made:
			if(this.attack.AttackThisFrame()) {
				ProjectileEnemy projectile = ProjectileEnemy.Create(room, (byte)ProjectileEnemySubType.Fire, FVector.Create(this.posX + this.bounds.MidX - 10, this.posY + this.bounds.MidY + 4), FVector.Create(this.FaceRight ? this.attSpeed : -this.attSpeed, 0));
				projectile.physics.SetGravity(this.gravity);
				this.room.PlaySound(Systems.sounds.flame, 0.6f, this.posX + 16, this.posY + 16);
			}
		}

		private void AssignSubType(byte subType) {
			this.animate = new Animate(this, "Flair/Fire/");
			this.animate.SetAnimation("Flair/Fire/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Flair/Fire/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}
	}
}
