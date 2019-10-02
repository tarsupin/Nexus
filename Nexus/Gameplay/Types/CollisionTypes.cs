
using Microsoft.Xna.Framework;
using Nexus.GameEngine;

namespace Nexus.Gameplay {

	public class Bounds {
		public byte Left { get; set; }
		public byte Right { get; set; }
		public byte Top { get; set; }
		public byte Bottom { get; set; }
	}

	public class BoundsCamera {
		public int Left { get; set; }
		public int Right { get; set; }
		public int Top { get; set; }
		public int Bottom { get; set; }
	}

	public enum DirCardinal : byte {
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

	public interface ITouching {
		bool Any { get; set; }
		GameObject Platform { get; set; }
		bool Floor { get; set; }
		bool Left { get; set; }
		bool Right { get; set; }
		bool Top { get; set; }
		bool Down { get; set; }
	}

	public interface IItemGripCardinal {
		Vector2 Left { get; set; }
		Vector2 Right { get; set; }
	}
}
