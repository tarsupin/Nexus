namespace TexturePackerLoader
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Nexus.Engine;

    public class SpriteFrame
    {
        public SpriteFrame(Texture2D texture, Rectangle sourceRect, FVector size, FVector pivotPoint, bool isRotated)
        {
            this.Texture = texture;
            this.SourceRectangle = sourceRect;
            this.Size = size;
            this.Origin = isRotated ? FVector.Create(sourceRect.Width * (1 - pivotPoint.Y.IntValue), sourceRect.Height * pivotPoint.X.IntValue)
                                    : FVector.Create(sourceRect.Width * pivotPoint.X.IntValue, sourceRect.Height * pivotPoint.Y.IntValue);
            this.IsRotated = isRotated;
        }

        public Texture2D Texture { get; private set; }

        public Rectangle SourceRectangle { get; private set; }

        public FVector Size { get; private set; }

        public bool IsRotated { get; private set; }

        public FVector Origin { get; private set; }
    }
}
