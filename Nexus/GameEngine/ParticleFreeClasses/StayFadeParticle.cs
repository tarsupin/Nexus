using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class StayFadeParticle : ParticleFree {

		public static ObjectPool<StayFadeParticle> stayPool = new ObjectPool<StayFadeParticle>(() => new StayFadeParticle());

		public static void SetCharFadeParticle(RoomScene room, Character character, int frameEnd) {

			// Draw Suit
			StayFadeParticle particle = StayFadeParticle.stayPool.GetObject();
			particle.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
			particle.spriteName = character.suit.texture + "Run" + (character.FaceRight ? "" : "Left");
			particle.pos = new Vector2(character.posX, character.posY);
			particle.frameEnd = frameEnd;
			room.particleHandler.AddParticle(particle);

			// Draw Head
			StayFadeParticle headPart = StayFadeParticle.stayPool.GetObject();
			headPart.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
			headPart.spriteName = character.head.SpriteName + (character.FaceRight ? "Right" : "Left");
			headPart.pos = new Vector2(character.posX, character.posY);
			headPart.frameEnd = frameEnd;
			room.particleHandler.AddParticle(headPart);

			// Draw Hat
			if(character.hat is Hat) {
				StayFadeParticle hatPart = StayFadeParticle.stayPool.GetObject();
				hatPart.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
				hatPart.spriteName = character.hat.SpriteName + (character.FaceRight ? "" : "Left");
				hatPart.pos = new Vector2(character.posX, character.posY);
				hatPart.frameEnd = frameEnd;
				room.particleHandler.AddParticle(hatPart);
			}
		}

		public override void RunParticleTick() {
			if(this.HasExpired) { StayFadeParticle.stayPool.ReturnObject(this); }
		}

		public override void Draw(int camX, int camY) {
			this.atlas.DrawWithColor(this.spriteName, (int)this.pos.X - camX, (int)this.pos.Y - camY, Color.White * 0.5f);
		}
	}
}
