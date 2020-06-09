using Nexus.Engine;

namespace Nexus.GameEngine {

	public static class ShakeParticle {

		public static ParticleTrack LeafShake( RoomScene room, string spriteName, int posX, int posY, byte duration = 8, float ySpeed = -3f, float gravity = 0.5f ) {
			uint frame = Systems.timer.Frame;

			//ParticleTrack particle = ParticleTrack.SetParticle(room, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], spriteName, new Vector2(posX, posY), new Vector2(0, 0), gravity, frame + duration, frame + duration, 1, 1);

			//return particle;
			return null;
		}
	}
}
