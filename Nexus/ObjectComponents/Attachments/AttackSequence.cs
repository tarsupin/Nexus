using Nexus.Engine;
using System;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	// Requires Parameters for: Attack Frequency ("Cycle"), and the Timer Offset.
	public class AttackSequence {

		public short cycle { get; protected set; }
		public short offset { get; protected set; }
		public int nextAttackFrame = 0;

		public AttackSequence( Dictionary<string, short> paramList ) {

			// Default cycle of two seconds, and no timer offset.
			this.cycle = paramList != null && paramList.ContainsKey("cycle") ? paramList["cycle"] : (short) 120;
			this.offset = paramList != null && paramList.ContainsKey("offset") ? (byte)paramList["offset"] : (short) 0;

			// Determine First Attack Frame
			this.UpdateNextAttackFrame();
		}

		public void UpdateAttackFrequency(short cycle) { this.cycle = cycle; }
		public void UpdateTimerOffset(short offset) { this.offset = offset; }

		public void UpdateNextAttackFrame() {
			this.nextAttackFrame = (int)(Math.Ceiling((double)Systems.timer.Frame / this.cycle) * this.cycle) + this.offset;
		}

		public bool AttackThisFrame() {
			if(Systems.timer.Frame <= this.nextAttackFrame) { return false; }
			this.UpdateNextAttackFrame();
			return true;
		}
	}
}
