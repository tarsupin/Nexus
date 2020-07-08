using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class StayFadeParticle : ParticleFree {

		public static ObjectPool<StayFadeParticle> stayPool = new ObjectPool<StayFadeParticle>(() => new StayFadeParticle());

		public static void SetCharFadeParticle(RoomScene room, Character character, int frameEnd, float alpha, float alphaDecay = -0.03f) {

			// Draw Suit
			StayFadeParticle particle = StayFadeParticle.stayPool.GetObject();
			particle.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
			//particle.spriteName = character.suit.texture + "Run" + (character.FaceRight ? "" : "Left");
			particle.spriteName = character.suit.texture + character.SpriteName;
			particle.pos = new Vector2(character.posX, character.posY);
			particle.alphaStart = alpha;
			particle.alphaEnd = alphaDecay;
			particle.frameEnd = frameEnd;
			room.particleHandler.AddParticle(particle);
			 
			// Draw Head
			StayFadeParticle headPart = StayFadeParticle.stayPool.GetObject();
			headPart.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
			headPart.spriteName = character.head.SpriteName + (character.FaceRight ? "Right" : "Left");
			headPart.pos = new Vector2(character.posX, character.posY);
			headPart.alphaStart = alpha;
			headPart.alphaEnd = alphaDecay;
			headPart.frameEnd = frameEnd;
			room.particleHandler.AddParticle(headPart);

			// Draw Hat
			if(character.hat is Hat) {
				StayFadeParticle hatPart = StayFadeParticle.stayPool.GetObject();
				hatPart.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
				hatPart.spriteName = character.hat.SpriteName + (character.FaceRight ? "" : "Left");
				hatPart.pos = new Vector2(character.posX, character.posY);
				hatPart.alphaStart = alpha;
				hatPart.alphaEnd = alphaDecay;
				hatPart.frameEnd = frameEnd;
				room.particleHandler.AddParticle(hatPart);
			}
		}

		public override void RunParticleTick() {
			if(this.HasExpired) { StayFadeParticle.stayPool.ReturnObject(this); }
			this.alphaStart += this.alphaEnd;
		}

		public override void Draw(int camX, int camY) {
			this.atlas.DrawWithColor(this.spriteName, (int)this.pos.X - camX, (int)this.pos.Y - camY, Color.White * this.alphaStart);
		}
	}
}
