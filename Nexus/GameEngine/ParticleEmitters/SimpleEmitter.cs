using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class SimpleEmitter {

		public static EmitterSimple GravityParticle( RoomScene room, string spriteName, int posX, int posY, byte duration = 8, float ySpeed = -3f, float gravity = 0.5f ) {
			uint frame = Systems.timer.Frame;

			EmitterSimple emitter = EmitterSimple.NewEmitter(room, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], spriteName, new Vector2(posX, posY), new Vector2(0, 0), gravity, frame + duration, frame + duration, 1, 1);

			emitter.AddParticle(new Vector2(posX, posY), new Vector2(0, ySpeed), 0, 0);

			return emitter;
		}

		public static EmitterSimple DirectionParticle( RoomScene room, string spriteName, int posX, int posY, byte duration = 8, float xSpeed = 0f, float ySpeed = 0f ) {
			uint frame = Systems.timer.Frame;

			EmitterSimple emitter = EmitterSimple.NewEmitter(room, Systems.mapper.atlas[(byte)AtlasGroup.Objects], spriteName, new Vector2(posX, posY), new Vector2(0, 0), 0, frame + duration, frame + duration, 1, 1);

			emitter.AddParticle(new Vector2(posX, posY), new Vector2(xSpeed, ySpeed), 0, 0);

			return emitter;
		}
	}
}
