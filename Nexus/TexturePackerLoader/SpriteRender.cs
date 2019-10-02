namespace TexturePackerLoader
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Nexus.Engine;

    public class SpriteRender
    {
        private const float ClockwiseNinetyDegreeRotation = (float)(Math.PI / 2.0f);

        private SpriteBatch spriteBatch;

        public SpriteRender (SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

		// <param name="position">This should be where you want the pivot point of the sprite image to be rendered.</param>
		[Obsolete]
		public void Draw(SpriteFrame sprite, FVector position, Color? color = null, float rotation = 0, float scale = 1, SpriteEffects spriteEffects = SpriteEffects.None)
        {
			FVector origin = sprite.Origin;
            if (sprite.IsRotated)
            {
                rotation -= ClockwiseNinetyDegreeRotation;
                switch (spriteEffects)
                {
                    case SpriteEffects.FlipHorizontally: spriteEffects = SpriteEffects.FlipVertically; break;
                    case SpriteEffects.FlipVertically: spriteEffects = SpriteEffects.FlipHorizontally; break;
                }
            }
            switch (spriteEffects)
            {
                case SpriteEffects.FlipHorizontally: origin.X = sprite.SourceRectangle.Width - origin.X; break;
                case SpriteEffects.FlipVertically: origin.Y = sprite.SourceRectangle.Height - origin.Y; break;
            }

            spriteBatch.Draw(
                texture: sprite.Texture,
                position: new Vector2(position.X.IntValue, position.Y.IntValue),
                sourceRectangle: sprite.SourceRectangle,
                color: color,
                rotation: rotation,
                origin: new Vector2(origin.X.IntValue, origin.Y.IntValue),
				scale: new Vector2(scale, scale),
                effects: spriteEffects,
                layerDepth: 0.0f
            );
        }
    }
}