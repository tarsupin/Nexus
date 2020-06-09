using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexus.Engine;
using System.Diagnostics;

namespace NexusTests {

	[TestClass]
	public class TestCoordCalc {

		public TestCoordCalc() {}

		[TestMethod]
		public void TestMappingCoordsDiagToInts() {

			Debug.Assert(CoordsDiag.MapToInt(4, 4) == 25);
			Debug.Assert(CoordsDiag.MapToInt(3, 4) == 24);
			Debug.Assert(CoordsDiag.MapToInt(2, 4) == 23);
			Debug.Assert(CoordsDiag.MapToInt(1, 4) == 22);
			Debug.Assert(CoordsDiag.MapToInt(0, 4) == 21);
			Debug.Assert(CoordsDiag.MapToInt(4, 3) == 20);
			Debug.Assert(CoordsDiag.MapToInt(4, 2) == 19);
			Debug.Assert(CoordsDiag.MapToInt(4, 1) == 18);
			Debug.Assert(CoordsDiag.MapToInt(4, 0) == 17);

			Debug.Assert(CoordsDiag.MapToInt(6, 0) == 37);
			Debug.Assert(CoordsDiag.MapToInt(0, 6) == 43);
			Debug.Assert(CoordsDiag.MapToInt(3, 6) == 46);
			Debug.Assert(CoordsDiag.MapToInt(6, 5) == 42);
			Debug.Assert(CoordsDiag.MapToInt(6, 6) == 49);

			// Testing Typescript's Results
			Debug.Assert(CoordsDiag.MapToInt(53, 21) == 2831);
			Debug.Assert(CoordsDiag.MapToInt(18, 44) == 1999);
			Debug.Assert(CoordsDiag.MapToInt(27, 10) == 740);

			var val = CoordsDiag.GetFromInt(47);
			Debug.Assert(val.x == 4 && val.y == 6);

			val = CoordsDiag.GetFromInt(49);
			Debug.Assert(val.x == 6 && val.y == 6);

			val = CoordsDiag.GetFromInt(41);
			Debug.Assert(val.x == 6 && val.y == 4);

			val = CoordsDiag.GetFromInt(37);
			Debug.Assert(val.x == 6 && val.y == 0);

			val = CoordsDiag.GetFromInt(43);
			Debug.Assert(val.x == 0 && val.y == 6);

			val = CoordsDiag.GetFromInt(44);
			Debug.Assert(val.x == 1 && val.y == 6);

			val = CoordsDiag.GetFromInt(21);
			Debug.Assert(val.x == 0 && val.y == 4);

			val = CoordsDiag.GetFromInt(24);
			Debug.Assert(val.x == 3 && val.y == 4);

			val = CoordsDiag.GetFromInt(18);
			Debug.Assert(val.x == 4 && val.y == 1);

			val = CoordsDiag.GetFromInt(4);
			Debug.Assert(val.x == 1 && val.y == 1);

			val = CoordsDiag.GetFromInt(1);
			Debug.Assert(val.x == 0 && val.y == 0);
			
			// Testing Typescript's Results
			val = CoordsDiag.GetFromInt(1037);
			Debug.Assert(val.x == 32 && val.y == 12);
			
			val = CoordsDiag.GetFromInt(247);
			Debug.Assert(val.x == 6 && val.y == 15);
		}
	}
}
