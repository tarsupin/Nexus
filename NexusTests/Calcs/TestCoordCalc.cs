using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexus.Engine;
using System.Diagnostics;

namespace NexusTests {

	[TestClass]
	public class TestCoordCalc {

		public TestCoordCalc() {}

		[TestMethod]
		public void TestMappingCoordsToInts() {

			Debug.Assert(Coords.MapToInt(4, 4) == 25);
			Debug.Assert(Coords.MapToInt(3, 4) == 24);
			Debug.Assert(Coords.MapToInt(2, 4) == 23);
			Debug.Assert(Coords.MapToInt(1, 4) == 22);
			Debug.Assert(Coords.MapToInt(0, 4) == 21);
			Debug.Assert(Coords.MapToInt(4, 3) == 20);
			Debug.Assert(Coords.MapToInt(4, 2) == 19);
			Debug.Assert(Coords.MapToInt(4, 1) == 18);
			Debug.Assert(Coords.MapToInt(4, 0) == 17);

			Debug.Assert(Coords.MapToInt(6, 0) == 37);
			Debug.Assert(Coords.MapToInt(0, 6) == 43);
			Debug.Assert(Coords.MapToInt(3, 6) == 46);
			Debug.Assert(Coords.MapToInt(6, 5) == 42);
			Debug.Assert(Coords.MapToInt(6, 6) == 49);

			var val = Coords.GetFromInt(47);
			Debug.Assert(val.x == 4 && val.y == 6);

			val = Coords.GetFromInt(49);
			Debug.Assert(val.x == 6 && val.y == 6);

			val = Coords.GetFromInt(41);
			Debug.Assert(val.x == 6 && val.y == 4);

			val = Coords.GetFromInt(37);
			Debug.Assert(val.x == 6 && val.y == 0);

			val = Coords.GetFromInt(43);
			Debug.Assert(val.x == 0 && val.y == 6);

			val = Coords.GetFromInt(44);
			Debug.Assert(val.x == 1 && val.y == 6);

			val = Coords.GetFromInt(21);
			Debug.Assert(val.x == 0 && val.y == 4);

			val = Coords.GetFromInt(24);
			Debug.Assert(val.x == 3 && val.y == 4);

			val = Coords.GetFromInt(18);
			Debug.Assert(val.x == 4 && val.y == 1);

			val = Coords.GetFromInt(4);
			Debug.Assert(val.x == 1 && val.y == 1);

			val = Coords.GetFromInt(1);
			Debug.Assert(val.x == 0 && val.y == 0);
		}
	}
}
