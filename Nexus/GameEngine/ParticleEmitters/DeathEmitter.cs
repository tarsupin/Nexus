using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

// DeathEmitter.Knockout(this.room, "Moosh/White/Left2", posX, posY);

namespace Nexus.GameEngine {

	public static class DeathEmitter {

		public static EmitterSimple Knockout(RoomScene room, string spriteName, int posX, int posY, byte duration = 60) {
			uint frame = Systems.timer.Frame;

			// Prepare Emnitter
			EmitterSimple emitter = EmitterSimple.NewEmitter(room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], spriteName, new Vector2(posX, posY), new Vector2(0, 0), 0.5f, frame + duration, frame + duration - 25);

			// Randomize Knockout Direction and Rotation
			Vector2 randVelocity = new Vector2(
				CalcRandom.FloatBetween(-4.5f, 4.5f),
				CalcRandom.FloatBetween(-5f, -11f)
			);

			float rotSpeed = CalcRandom.FloatBetween(0.07f, 0.15f) * (CalcRandom.IntBetween(1, 100) >= 51 ? 1 : -1);

			// Add Knockout Particle
			emitter.AddParticle(
				new Vector2(posX, posY),
				randVelocity,
				0,
				rotSpeed
			);

			return emitter;
		}
	}
}
