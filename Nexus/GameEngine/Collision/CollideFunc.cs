using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class CollideFunc {

		public static sbyte GetHorizontalSign( DirCardinal dir ) {
			return (sbyte) (dir == DirCardinal.Left ? -1 : (dir == DirCardinal.Right ? 1 : 0));
		}

		public static sbyte GetVerticalSign( DirCardinal dir ) {
			return (sbyte) (dir == DirCardinal.Up ? -1 : (dir == DirCardinal.Down ? 1 : 0));
		}

		// Get the reverse direction of something:
		public static DirCardinal ReverseDirection( DirCardinal dir ) {
			if(dir == DirCardinal.Down) { return DirCardinal.Up; }
			if(dir == DirCardinal.Up) { return DirCardinal.Down; }
			if(dir == DirCardinal.Left) { return DirCardinal.Right; }
			if(dir == DirCardinal.Right) { return DirCardinal.Left; }
			return dir;
		}
	}
}
