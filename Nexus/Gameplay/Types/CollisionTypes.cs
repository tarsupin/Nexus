using System;
using Microsoft.Xna.Framework;
using Nexus.GameEngine;

namespace Nexus.Gameplay {

	public class Bounds {
		public byte Left { get; set; }
		public byte Right { get; set; }     // Right == Left + Width
		public byte Top { get; set; }
		public byte Bottom { get; set; }    // Bottom == Top + Height

		public byte MidX => (byte)Math.Floor((double)(this.Right - this.Left) / 2);
		public byte MidY => (byte)Math.Floor((double)(this.Bottom - this.Top) / 2);

		public Bounds( byte Top, byte Left, byte Right, byte Bottom ) {
			this.Top = Top;
			this.Left = Left;
			this.Right = Right;
			this.Bottom = Bottom;
		}
	}

	public class BoundsCamera {
		public int Left { get; set; }
		public int Right { get; set; }
		public int Top { get; set; }
		public int Bottom { get; set; }
	}

	public enum DirCardinal : byte {
		Center = 5,
		Up = 8,
		Down = 2,
		Left = 4,
		Right = 6,
	}

	public enum DirRotate : byte {
		UpRight = 9,
		Up = 8,
		UpLeft = 7,
		Right = 6,
		Left = 4,
		DownRight = 3,
		Down = 2,
		Half = 2,
		DownLeft = 1,
		FlipVert = 10,
		FlipHor = 15,
	}

	public enum Solidity : byte {
		Up = 8,
		Down = 2,
		Left = 4,
		Right = 6,
		All = 5,
		None = 0,
	}

	public interface IItemGripCardinal {
		Vector2 Left { get; set; }
		Vector2 Right { get; set; }
	}
}
