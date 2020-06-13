using Nexus.Gameplay;
using Nexus.GameEngine;
using System.Collections.Generic;
using System;
using Nexus.Engine;

namespace Nexus.Objects {

	public class ChomperFire : Chomper {

		public ChomperFire() : base() {
			this.hasSetup = true;
			this.SpriteName = "Chomper/Fire/Chomp";
			this.KnockoutName = "Particles/Chomp/Fire";
			this.DamageSurvive = DamageStrength.Standard;
			this.tileId = (byte)TileEnum.ChomperFire;
			this.title = "Fire Chomper";
			this.description = "Stationary enemy. Can shoot fireballs.";
			this.actParamSet = Params.ParamMap["FireSpit"];
		}

		public void SetupTile(RoomScene room, ushort gridX, ushort gridY) {

			// Identify the subtype and params for this tile.
			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			this.AddNextAttackCycle(room, paramList, gridX, gridY);
		}

		private void AddNextAttackCycle(RoomScene room, Dictionary<string, short> paramList, ushort gridX, ushort gridY) {

			// Determine How Frequently this effect runs:
			short cycle = paramList.ContainsKey("cycle") ? paramList["cycle"] : ParamsAttack.DefaultCycle;
			short offset = paramList.ContainsKey("offset") ? paramList["offset"] : (short) 0;

			// Identify the next frame to activate on:
			int nextFrame = ((int)Math.Floor((double)((Systems.timer.Frame) / cycle) + 1)) * cycle + offset;

			// Add the Recurring Event
			room.queueEvents.AddEvent((uint) nextFrame, this.tileId, (short)gridX, (short)gridY);
		}

		// Only return false if (and/or when) the event should no longer be looped in the QueueEvent class.
		public override bool TriggerEvent(RoomScene room, ushort gridX, ushort gridY, short val1 = 0, short val2 = 0) {

			byte[] tileData = room.tilemap.GetTileDataAtGrid(gridX, gridY);
			if(tileData == null || tileData[0] != tileId) { return false; }
			byte subType = tileData[1];

			// Identify params for this tile.
			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			// Loads the next sequence for this attack.
			this.AddNextAttackCycle(room, paramList, gridX, gridY);

			// Prepare Projectile Details
			byte count = paramList.ContainsKey("count") ? (byte)paramList["count"] : (byte) 1;
			float grav = (float)((paramList.ContainsKey("grav") ? paramList["grav"] : 100) * 0.01);

			short attackX = 0;
			short attackY = 0;

			// Determine Attack Direction
			float speedMult = (float)((paramList.ContainsKey("speed") ? paramList["speed"] : 100) * 0.01);

			if(subType == (byte)FacingSubType.FaceUp) { attackY = (short)((speedMult * -8) - 4); }
			else if(subType == (byte)FacingSubType.FaceDown) { attackY = (short)(speedMult * 6); }
			else if(subType == (byte)FacingSubType.FaceLeft) { attackX = (short)(speedMult * -8); }
			else if(subType == (byte)FacingSubType.FaceRight) { attackX = (short)(speedMult * 8); }

			// If there is only one fireball, run the fire attack as usual:
			if(count == 1) {
				this.FireAttack(room, gridX, gridY, attackX, attackY, grav);
			}

			// For two fireballs, consider the spread:
			else {
				short spread = (short)((paramList.ContainsKey("spread") ? paramList["spread"] : 100) * 0.01);

				// If we're facing left or right, the spread is based on vertical spread.
				if(subType == (byte)FacingSubType.FaceLeft || (subType == (byte)FacingSubType.FaceRight)) {
					this.FireAttack(room, gridX, gridY, attackX, (short)(attackY - spread), grav);
					this.FireAttack(room, gridX, gridY, attackX, (short)(attackY + spread), grav);
				}

				// Otherwise, the spread splits horizontally.
				else {
					this.FireAttack(room, gridX, gridY, (short)(attackX - spread), attackY, grav);
					this.FireAttack(room, gridX, gridY, (short)(attackX + spread), attackY, grav);
				}
			}

			Systems.sounds.flame.Play();

			return true;
		}

		public void FireAttack(RoomScene room, ushort gridX, ushort gridY, short attX, short attY, float gravity) {
			ProjectileBall projectile = ProjectileBall.Create(room, (byte)ProjectileBallSubType.EnemyFire, FVector.Create(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth - 10, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight - 10), FVector.Create(attX, attY));
			projectile.physics.SetGravity(FInt.Create(gravity * 0.35));
		}
	}
}
