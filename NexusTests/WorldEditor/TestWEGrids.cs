using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System;
using System.Diagnostics;

namespace NexusTests {

	[TestClass]
	public class TestWEGrids {

		public TestWEGrids() {}

		[TestMethod]
		public void RelativeDirectionOfTiles() {
			Debug.Assert(NodeData.RelativeDirectionOfTiles(-2, -2) == DirCardinal.Up);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(-2, -3) == DirCardinal.Up);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(-2, 3) == DirCardinal.Down);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(2, -3) == DirCardinal.Up);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(4, -3) == DirCardinal.Right);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(4, 4) == DirCardinal.Down);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(-4, 4) == DirCardinal.Down);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(-4, 3) == DirCardinal.Left);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(-4, 5) == DirCardinal.Down);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(4, 0) == DirCardinal.Right);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(0, 4) == DirCardinal.Down);
			Debug.Assert(NodeData.RelativeDirectionOfTiles(0, -4) == DirCardinal.Up);
		}
	}
}
