﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Nexus.Engine {

	public class Atlas {

		// private const float Clockwise90DegRot = (float)(Math.PI / 2.0f);

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

		// <param name="position">This should be where you want the pivot point of the sprite image to be rendered.</param>
		public void Draw(string spriteName, FVector position) {
			SpriteFrame sprite = this.spriteList[spriteName];
			FVector origin = sprite.Origin;

			spriteBatch.Draw(
				texture: this.Texture,
				position: new Vector2(position.X.IntValue, position.Y.IntValue),
				sourceRectangle: sprite.SourceRectangle,
				color: null,
				rotation: 0,
				origin: new Vector2(origin.X.IntValue, origin.Y.IntValue),
				scale: new Vector2(1, 1),
				effects: SpriteEffects.None,
				layerDepth: 0.0f            // 0.0f is bottom layer, 1.0f is top layer
			);
		}

		// <param name="position">This should be where you want the pivot point of the sprite image to be rendered.</param>
		public void DrawAdvanced(string spriteName, FVector position, Color? color = null, float rotation = 0, float scale = 1, SpriteEffects spriteEffects = SpriteEffects.None) {
			SpriteFrame sprite = this.spriteList[spriteName];
			FVector origin = sprite.Origin;

			//if(sprite.IsRotated) {
			//	rotation -= ClockwiseNinetyDegreeRotation;
			//	switch(spriteEffects) {
			//		case SpriteEffects.FlipHorizontally: spriteEffects = SpriteEffects.FlipVertically; break;
			//		case SpriteEffects.FlipVertically: spriteEffects = SpriteEffects.FlipHorizontally; break;
			//	}
			//}

			switch(spriteEffects) {
				case SpriteEffects.FlipHorizontally: origin.X = sprite.SourceRectangle.Width - origin.X; break;
				case SpriteEffects.FlipVertically: origin.Y = sprite.SourceRectangle.Height - origin.Y; break;
			}

			spriteBatch.Draw(
				texture: this.Texture,
				position: new Vector2(position.X.IntValue, position.Y.IntValue),
				sourceRectangle: sprite.SourceRectangle,
				color: color,
				rotation: rotation,
				origin: new Vector2(origin.X.IntValue, origin.Y.IntValue),
				scale: new Vector2(scale, scale),
				effects: spriteEffects,
				layerDepth: 0.0f            // 0.0f is bottom layer, 1.0f is top layer
			);
		}

		private void LoadAtlasData(GameClient game, string imageResource) {
			string imageFile = Path.Combine(game.Content.RootDirectory, imageResource);
			string dataFile = Path.ChangeExtension(imageFile, "txt");

			FileStream fileStream = new FileStream(imageFile, FileMode.Open);

			// Assign Texture to Atlas
			this.Texture = Texture2D.FromStream(game.GraphicsDevice, fileStream);

			fileStream.Dispose();

			string[] dataFileLines = File.ReadAllLines(dataFile);

			// Load All Sprites into Atlas
			foreach(
				
				// Search Query for Atlas Sheet Data
				string[] cols in
					from row in dataFileLines
					where !string.IsNullOrEmpty(row) && !row.StartsWith("#")
					select row.Split(';')) {

				if(cols.Length != 10) {
					throw new InvalidDataException("Incorrect format data in spritesheet data file");
				}

				string name = cols[0];

				Rectangle sourceRectangle = new Rectangle(
					int.Parse(cols[2]),
					int.Parse(cols[3]),
					int.Parse(cols[4]),
					int.Parse(cols[5]));

				FVector size = FVector.Create(
					int.Parse(cols[6]),
					int.Parse(cols[7]));

				FVector pivotPoint = FVector.Create(
					(int)float.Parse(cols[8], CultureInfo.InvariantCulture),
					(int)float.Parse(cols[9], CultureInfo.InvariantCulture));

				// bool isRotated = int.Parse(cols[1]) == 1;		// I remove rotations in the Texture Packer.

				SpriteFrame sprite = new SpriteFrame(sourceRectangle, size, pivotPoint);

				spriteList.Add(name, sprite);
			}
		}
	}

	public class SpriteFrame {

		public Rectangle SourceRectangle { get; private set; }
		public FVector Size { get; private set; }
		public FVector Origin { get; private set; }

		public SpriteFrame(Rectangle sourceRect, FVector size, FVector pivotPoint) {
			this.SourceRectangle = sourceRect;
			this.Size = size;
			this.Origin = FVector.Create(sourceRect.Width * pivotPoint.X.IntValue, sourceRect.Height * pivotPoint.Y.IntValue);
		}
	}

}
