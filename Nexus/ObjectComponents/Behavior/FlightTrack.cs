using Nexus.Engine;
using Nexus.GameEngine;
using System;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightTrack : FlightBehavior {

		public Physics physics;     // Reference to the actor's physics.

		// Last Track Location
		public Track lastTrack;
		public Track nextTrack;

		public int lastArrivalFrame;		// The frame that this object arrived at the last track.

		public FlightTrack(GameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.physics = actor.physics;

			this.nextTrack = null;
			this.lastTrack = null;
			this.lastArrivalFrame = 0;

			// Get Last Track Position
			byte toTrackId = paramList.ContainsKey("toTrack") ? (byte)paramList["toTrack"] : (byte)1;

			if(toTrackId > 0) {
				actor.room.trackSys.AssignToTrackId(toTrackId, actor);
			}
		}

		public override void RunTick() {

			// If the actor doesn't have a track destination set, don't make any movements.
			if(this.nextTrack == null) { return; }

			// Get Last Track Position (if applicable)
			int posX, posY, duration;

			if(this.lastTrack is Track) {
				posX = this.lastTrack.posX;
				posY = this.lastTrack.posY;
				duration = this.lastTrack.duration;
			} else {
				posX = this.startX;
				posY = this.startY;
				duration = this.duration;
			}

			// Determine Lerp Position
			float weight = Math.Max((float) 0, (float)(Systems.timer.Frame - this.lastArrivalFrame) / (float)duration);
			int newX, newY;

			// If it has arrived at the next track position:
			if(weight >= 1) {

				newX = (int) Math.Round(Interpolation.Number(posX, this.nextTrack.posX, 1));
				newY = (int) Math.Round(Interpolation.Number(posY, this.nextTrack.posY, 1));

				// If the track indicates the the object should begin to fall:
				if(this.nextTrack.beginsFall) {
					this.RunFallTick();
					return;
				}

				this.physics.velocity.X = FInt.Create(newX - this.actor.posX);
				this.physics.velocity.Y = FInt.Create(newY - this.actor.posY);

				// Assign the next track destination:
				this.lastTrack = this.nextTrack;
				this.lastArrivalFrame = Systems.timer.Frame + this.nextTrack.delay;
				this.nextTrack = this.nextTrack.NextTrack;

				// If that was the last track, lose all motion and freeze in position.
				if(this.nextTrack == null) {
					this.physics.RunPhysicsTick();
					this.physics.velocity.X = FInt.Create(0);
					this.physics.velocity.Y = FInt.Create(0);
				}
				
				return;
			}
			
			// Run Track Motion
			newX = (int)Math.Round(Interpolation.Number(posX, this.nextTrack.posX, weight));
			newY = (int)Math.Round(Interpolation.Number(posY, this.nextTrack.posY, weight));

			this.physics.velocity.X = FInt.Create(newX - this.actor.posX);
			this.physics.velocity.Y = FInt.Create(newY - this.actor.posY);
		}

		private void RunFallTick() {
			this.physics.velocity.Y += FInt.Create(0.25); // Every frame, keep falling a bit more.
			if(this.physics.velocity.Y > 16) { this.physics.velocity.Y = FInt.Create(16); }
		}
	}
}
