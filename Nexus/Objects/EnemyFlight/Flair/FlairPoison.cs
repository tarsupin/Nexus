using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum FlairPoisonSubType : byte { Normal };

	public class FlairPoison : Flair {

		private const byte BaseAttackSpeed = 3;
		public AttackSequence attack;
		public sbyte attSpeed = 0;
		public FInt gravity = FInt.Create(0);

		public FlairPoison(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.FlairPoison].meta;
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
				ProjectileEnemy projectile = ProjectileEnemy.Create(room, (byte)ProjectileEnemySubType.Poison, FVector.Create(this.posX + this.bounds.MidX - 10, this.posY + this.bounds.MidY + 10), FVector.Create(0, this.attSpeed));
				projectile.physics.SetGravity(this.gravity);
				Systems.sounds.flame.Play(0.6f, 0, 0);
			}
		}

		private void AssignSubType(byte subType) {
			this.animate = new Animate(this, "Flair/Poison/");
			this.animate.SetAnimation("Flair/Poison/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Flair/Poison/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}
	}
}
