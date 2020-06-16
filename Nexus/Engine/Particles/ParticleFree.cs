using Microsoft.Xna.Framework;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Engine {

	// Free Particles are particle effects that don't need an emitter because there's only meant to be one of them.
	// Examples include Brick Nudges, Leaf Shaking, etc.
	public class ParticleFree {

		public static ObjectPool<ParticleFree> pool = new ObjectPool<ParticleFree>(() => new ParticleFree());

		public Atlas atlas;         // Reference to the atlas used (for texturing particles).
		public string spriteName;   // Name of the sprite to draw all particles with.
		public float gravity;       // Gravity to apply to particles each tick.

		public Vector2 pos;
		public Vector2 vel;

		public float rotation;
		public float rotationSpeed;

		// Lifespan & Fading
		public int frameEnd;       // The frame # that indicates the end of the particle's life.
		public int fadeStart;      // The frame # that indicates the particle should begin to fade.
		public float alphaStart;    // The amount of alpha to apply at max visibility (0 to 1). Typically 1.
		public float alphaEnd;      // The amount of alpha to apply at min visibility (0 to 1). Typically 0.

		public bool HasExpired { get { return this.frameEnd < Systems.timer.Frame; } }

		public static ParticleFree SetParticle( RoomScene room, Atlas atlas, string spriteName, Vector2 pos, Vector2 vel, int frameEnd, int fadeStart = 0, float alphaStart = 1, float alphaEnd = 0, float rotation = 0, float rotationSpeed = 0, float gravity = 0 ) {

			// Retrieve an available particle from the pool.
			ParticleFree particle = ParticleFree.pool.GetObject();

			particle.atlas = atlas;
			particle.spriteName = spriteName;
			particle.gravity = gravity;
			particle.pos = pos;
			particle.vel = vel;
			particle.frameEnd = frameEnd;
			particle.fadeStart = fadeStart == 0 ? frameEnd + 1 : fadeStart;
			particle.alphaStart = alphaStart;
			particle.alphaEnd = alphaEnd;
			particle.rotation = rotation;
			particle.rotationSpeed = rotationSpeed;

			// Add the Particle to the Particle Handler
			room.particleHandler.AddParticle(particle);

			return particle;
		}

		public virtual void RunParticleTick() {
			this.pos += this.vel;
			this.rotation += this.rotationSpeed;

			// Expires
			if(this.HasExpired) { ParticleFree.pool.ReturnObject(this); }
		}

		public bool IsOnScreen(Camera camera) {

			// Only draw the particle if it's on the camera.
			if(this.pos.X < camera.posX - (byte)TilemapEnum.TileWidth || this.pos.X > camera.posX + camera.width + (byte)TilemapEnum.TileWidth || this.pos.Y < camera.posY - (byte)TilemapEnum.TileHeight || this.pos.Y > camera.posY + camera.height + (byte)TilemapEnum.TileHeight) {
				return false;
			}

			return true;
		}

		public virtual void Draw(int camX, int camY) {

			// Determine Alpha of Particle (can be affected by fading)
			int frame = Systems.timer.Frame;
			float alpha = this.fadeStart < frame ? ParticleHandler.AlphaByFadeTime(frame, this.fadeStart, this.frameEnd, this.alphaStart, this.alphaEnd) : 1;
			
			this.atlas.DrawAdvanced(this.spriteName, (int)this.pos.X - camX, (int)this.pos.Y - camY, Color.White * alpha, this.rotation);
		}
	}
}
