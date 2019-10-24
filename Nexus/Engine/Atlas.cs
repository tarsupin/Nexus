using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Nexus.Engine {

	public class AtlasBounds {
		public byte Left { get; set; }
		public byte Right { get; set; }     // Right == Left + Width
		public byte Top { get; set; }
		public byte Bottom { get; set; }    // Bottom == Top + Height
	}

	public class Atlas {

		private const float Rot90Deg = 1.5707963267949f;
		private const float Rot180Deg = 3.14159265358979f;
		private const float Rot270Deg = 4.71238898038469f;

		// References
		private readonly SpriteBatch spriteBatch;

		// Atlas Contents
		private Texture2D Texture;
		private readonly IDictionary<string, SpriteFrame> spriteList;

		// Setup Atlas for Texture Packager
		public Atlas(GameClient game, SpriteBatch spriteBatch, string filePath) {
			this.spriteBatch = spriteBatch;
			this.spriteList = new Dictionary<string, SpriteFrame>();
			this.LoadAtlasData(game, filePath);
		}

		public AtlasBounds GetBounds( string spriteName ) {
			SpriteFrame spriteFrame = this.spriteList[spriteName];

			return new AtlasBounds {
				Left = spriteFrame.XOffset,
				Right = (byte) (spriteFrame.XOffset + spriteFrame.Width),
				Top = spriteFrame.YOffset,
				Bottom = (byte) (spriteFrame.YOffset + spriteFrame.Height),
			};
		}

		// <param name="position">This should be where you want the pivot point of the sprite image to be rendered.</param>
		public void Draw(string spriteName, int posX, int posY) {
			SpriteFrame sprite = this.spriteList[spriteName];

			spriteBatch.Draw(
				texture: this.Texture,
				position: new Vector2(posX + sprite.XOffset, posY + sprite.YOffset),
				sourceRectangle: sprite.TextureRect,
				color: null
			);
		}

		// <param name="position">This should be where you want the pivot point of the sprite image to be rendered.</param>
		public void DrawAdvanced(string spriteName, int posX, int posY, Color? color = null, float rotation = 0, float scale = 1, SpriteEffects spriteEffects = SpriteEffects.None) {
			SpriteFrame sprite = this.spriteList[spriteName];
			
			//if(sprite.IsRotated) {
			//	rotation -= ClockwiseNinetyDegreeRotation;
			//	switch(spriteEffects) {
			//		case SpriteEffects.FlipHorizontally: spriteEffects = SpriteEffects.FlipVertically; break;
			//		case SpriteEffects.FlipVertically: spriteEffects = SpriteEffects.FlipHorizontally; break;
			//	}
			//}

			//switch(spriteEffects) {
			//	case SpriteEffects.FlipHorizontally: origin.X = sprite.TextureRect.Width - origin.X; break;
			//	case SpriteEffects.FlipVertically: origin.Y = sprite.TextureRect.Height - origin.Y; break;
			//}

			spriteBatch.Draw(
				texture: this.Texture,
				position: new Vector2(posX, posY),
				sourceRectangle: sprite.TextureRect,
				color: color,
				rotation: rotation,
				origin: sprite.Origin,
				scale: new Vector2(scale, scale),
				effects: spriteEffects
				//layerDepth: 0.0f            // 0.0f is bottom layer, 1.0f is top layer
			);
		}

		public void DrawRotation(string spriteName, int posX, int posY, float rotation, Vector2 origin ) {
			SpriteFrame sprite = this.spriteList[spriteName];

			spriteBatch.Draw(
				texture: this.Texture,
				position: new Vector2(posX, posY),
				sourceRectangle: sprite.TextureRect,
				rotation: rotation,
				origin: origin
			);
		}

		public void DrawFaceLeft(string spriteName, int posX, int posY) {
			this.DrawRotation(spriteName, posX, posY, Rot270Deg, new Vector2(48, 0));
		}

		public void DrawFaceRight(string spriteName, int posX, int posY) {
			this.DrawRotation(spriteName, posX, posY, Rot90Deg, new Vector2(0, 48));
		}
		
		public void DrawFaceDown(string spriteName, int posX, int posY) {
			this.DrawRotation(spriteName, posX, posY, Rot180Deg, new Vector2(48, 48));
		}

		private void LoadAtlasData(GameClient game, string imageResource) {
			string imageFile = Path.Combine(game.Content.RootDirectory, imageResource);
			string jsonFile = Path.ChangeExtension(imageFile, "json");

			FileStream fileStream = new FileStream(imageFile, FileMode.Open);

			// Assign Texture to Atlas
			this.Texture = Texture2D.FromStream(game.GraphicsDevice, fileStream);

			fileStream.Dispose();

			string jsonText = File.ReadAllText(jsonFile);

			var jsonData = JObject.Parse(jsonText);

			// Loop through each JSON element and update the sprite accordingly:
			foreach( dynamic spriteObj in jsonData["frames"] ) {

				var textureFrame = spriteObj.Value["frame"];
				var sourceInfo = spriteObj.Value["spriteSourceSize"];
				var size = spriteObj.Value["sourceSize"];

				Rectangle textureRect = new Rectangle(
					(int) textureFrame["x"],
					(int) textureFrame["y"],
					(int) textureFrame["w"],
					(int) textureFrame["h"]
				);

				//SpriteFrame sprite = new SpriteFrame(textureRect, (byte) sourceInfo["x"], (byte) sourceInfo["y"], (short)size["w"], (short)size["h"]);
				SpriteFrame sprite = new SpriteFrame(textureRect, (byte) sourceInfo["x"], (byte) sourceInfo["y"], (short)(sourceInfo["w"]), (short)(sourceInfo["h"]));
				spriteList.Add(spriteObj.Name, sprite);
			}
		}
	}

	public class SpriteFrame {
		public Rectangle TextureRect { get; private set; }
		public byte XOffset { get; private set; }
		public byte YOffset { get; private set; }
		public short Width { get; private set; }
		public short Height { get; private set; }
		public Vector2 Origin { get; private set; }     // Don't have any origins set yet.

		public SpriteFrame( Rectangle textureRect, byte xOffset, byte yOffset, short width, short height ) {
			this.TextureRect = textureRect;
			this.XOffset = xOffset;
			this.YOffset = yOffset;
			this.Width = width;
			this.Height = height;
			this.Origin = new Vector2(this.Width / 2, this.Height / 2);
		}
	}

	//public class SpriteFrame {

	//	public Rectangle SourceRectangle { get; private set; }
	//	public Vector2 Size { get; private set; }
	//	public Vector2 Origin { get; private set; }

	//	public SpriteFrame(Rectangle sourceRect, Vector2 size, Vector2 pivotPoint) {
	//		this.SourceRectangle = sourceRect;
	//		this.Size = size;
	//		this.Origin = new Vector2(sourceRect.Width * pivotPoint.X, sourceRect.Height * pivotPoint.Y);
	//	}
	//}

}
