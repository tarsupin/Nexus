using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class BurstEmitter {

		public static EmitterSimple AirPuff( RoomScene room, int posX, int posY, sbyte dirHor, sbyte dirVert, byte duration = 8 ) {
			int frame = Systems.timer.Frame;

			// Readjust positions to adjust for midX and midY of AirPuff image.
			posX -= 20;
			posY -= 20;

			float radian = Radians.GetRadiansByCardinalDir((sbyte)-dirHor, (sbyte)-dirVert);

			// Get the Particle Directions based on Cardinal Steps from the original.
			float rad1 = Radians.GetRadiansByCardinalDir((sbyte)-dirHor, (sbyte)-dirVert, -3);
			float rad2 = Radians.GetRadiansByCardinalDir((sbyte)-dirHor, (sbyte)-dirVert, 3);

			float xOffset = Radians.GetXOffsetFromRotation(radian, 15, -15);
			float xOffset2 = Radians.GetXOffsetFromRotation(radian, 15, 15);
			float yOffset = Radians.GetYOffsetFromRotation(radian, 15, -15);
			float yOffset2 = Radians.GetYOffsetFromRotation(radian, 15, 15);

			EmitterSimple emitter = EmitterSimple.NewEmitter(room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], "Particles/AirPuff", new Vector2(posX, posY), new Vector2(0, 0), 0f, frame + duration, frame + duration - 25);

			// Launch First Particle
			emitter.AddParticle(
				new Vector2(
					posX + xOffset,
					posY + yOffset
				),
				new Vector2(
					dirHor * -1.5f + (xOffset / 16),
					dirVert * -1.5f + (yOffset / 16)
				),
				rad1 - 0.5f,
				0f
			);

			// Launch Second Particle
			emitter.AddParticle(
				new Vector2(
					posX + xOffset2,
					posY + yOffset2
				),
				new Vector2(
					dirHor * -1.5f + (xOffset2 / 16),
					dirVert * -1.5f + (yOffset2 / 16)
				),
				rad2 + 0.5f,
				0f
			);

			return emitter;
		}
	}
}
