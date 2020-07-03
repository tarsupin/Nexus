using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum FlairElectricSubType : byte { Normal };

	public class FlairElectric : Flair {

		private const byte BaseAttackSpeed = 6;
		public AttackSequence attack;
		public sbyte attSpeed = 0;
		public FInt gravity = FInt.Create(0);

		public FlairElectric(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.FlairElectric].meta;
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
				ProjectileEnemy projectile = ProjectileEnemy.Create(room, (byte)ProjectileEnemySubType.Electric, FVector.Create(this.posX + this.bounds.MidX - 10, this.posY + this.bounds.MidY + 4), FVector.Create(this.FaceRight ? this.attSpeed : -this.attSpeed, -5));
				projectile.physics.SetGravity(this.gravity);
				this.room.PlaySound(Systems.sounds.flame, 0.6f, this.posX + 16, this.posY + 16);
			}
		}

		private void AssignSubType( byte subType ) {
			this.animate = new Animate(this, "Flair/Electric/");
			this.animate.SetAnimation("Flair/Electric/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}

		public override void OnDirectionChange() {
			this.animate.SetAnimation("Flair/Electric/" + (this.FaceRight ? "Right" : "Left"), AnimCycleMap.Cycle3BothWays, 12);
		}
	}
}
