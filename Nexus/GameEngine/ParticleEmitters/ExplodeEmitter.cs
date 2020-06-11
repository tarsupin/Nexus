using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public static class ExplodeEmitter {

		public static EmitterSimple BoxExplosion( RoomScene room, string spriteName, int posX, int posY, sbyte xSpread = 3, sbyte ySpread = 3, byte duration = 55 ) {
			uint frame = Systems.timer.Frame;

			EmitterSimple emitter = EmitterSimple.NewEmitter(room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], spriteName, new Vector2(posX, posY), new Vector2(0, 0), 0.5f, frame + duration, frame + duration - 25);

			float radianUpLeft = -1.82f; // -5, -20
			float radianUpRight = -1.32f; // 5, -20

			// Launch Up-Left Particle
			emitter.AddParticle(
				new Vector2(
					posX - (int) Math.Floor(xSpread * CalcRandom.FloatBetween(0.5f, 2f)),
					posY - (int) Math.Floor(ySpread * CalcRandom.FloatBetween(0.5f, 2f))
				),
				new Vector2(
					CalcRandom.FloatBetween(-0.25f, -1.5f),
					CalcRandom.FloatBetween(-9f, -13f)
				),
				radianUpLeft + CalcRandom.FloatBetween(-0.6f, 0.6f),
				-0.08f + CalcRandom.FloatBetween(-0.12f, 0.12f)
			);

			// Launch Up-Right Particle
			emitter.AddParticle(
				new Vector2(
					posX + (int)Math.Floor(xSpread * CalcRandom.FloatBetween(0.5f, 2f)),
					posY - (int)Math.Floor(ySpread * CalcRandom.FloatBetween(0.5f, 2f))
				),
				new Vector2(
					CalcRandom.FloatBetween(0.25f, 1.5f),
					CalcRandom.FloatBetween(-9f, -13f)
				),
				radianUpRight + CalcRandom.FloatBetween(-0.6f, 0.6f),
				0.08f + CalcRandom.FloatBetween(-0.12f, 0.12f)
			);

			// Launch Down-Left Particle
			emitter.AddParticle(
				new Vector2(
					posX - (int)Math.Floor(xSpread * CalcRandom.FloatBetween(0.5f, 2f)),
					posY + (int)Math.Floor(ySpread * CalcRandom.FloatBetween(0.5f, 2f))
				),
				new Vector2(
					CalcRandom.FloatBetween(-0.25f, -1.5f),
					CalcRandom.FloatBetween(-4f, -7f)
				),
				radianUpLeft + CalcRandom.FloatBetween(-0.6f, 0.6f),
				-0.08f + CalcRandom.FloatBetween(-0.12f, 0.12f)
			);

			// Launch Down-Right Particle
			emitter.AddParticle(
				new Vector2(
					posX + (int)Math.Floor(xSpread * CalcRandom.FloatBetween(0.5f, 2f)),
					posY + (int)Math.Floor(ySpread * CalcRandom.FloatBetween(0.5f, 2f))
				),
				new Vector2(
					CalcRandom.FloatBetween(0.25f, 1.5f),
					CalcRandom.FloatBetween(-4f, -7f)
				),
				radianUpRight + CalcRandom.FloatBetween(-0.6f, 0.6f),
				0.08f + CalcRandom.FloatBetween(-0.12f, 0.12f)
			);

			return emitter;
		}
	}
}
