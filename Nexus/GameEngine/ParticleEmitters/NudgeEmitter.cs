using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class NudgeEmitter {

		public static EmitterSimple BoxNudge( RoomScene room, string spriteName, int posX, int posY, byte duration = 8 ) {
			uint frame = Systems.timer.Frame;

			EmitterSimple emitter = EmitterSimple.NewEmitter(room, Systems.mapper.atlas[(byte)AtlasGroup.Tiles], spriteName, new Vector2(posX, posY), new Vector2(0, 0), 0.5f, frame + duration, frame + duration, 1, 1);

			emitter.AddParticle(new Vector2(posX, posY), new Vector2(0, -3f), 0, 0);

			return emitter;
		}
	}
}
