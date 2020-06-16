using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;

namespace Nexus.Engine {

	public static class TextHelper {

		public static string[] WrapTextSplit(SpriteFont spriteFont, string text, float maxLineWidth) {

			List<string> contents = new List<string>();
			string[] words = text.Split(' ');
			StringBuilder sb = new StringBuilder();
			float lineWidth = 0f;
			float spaceWidth = spriteFont.MeasureString(" ").X;

			foreach(string word in words) {
				Vector2 size = spriteFont.MeasureString(word);

				if(lineWidth + size.X < maxLineWidth) {
					lineWidth += size.X + spaceWidth;
				} else {
					contents.Add(sb.ToString());
					sb.Clear();
					lineWidth = size.X + spaceWidth;
				}

				sb.Append(word + " ");
			}

			// If there is any remaining part of the message, it must be added to the contents.
			if(sb.Length > 0) {
				contents.Add(sb.ToString());
			}

			return contents.ToArray();
		}
	}
}
