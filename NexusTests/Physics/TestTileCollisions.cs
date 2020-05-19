using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;
using System;
using System.Diagnostics;

namespace NexusTests {

	[TestClass]
	public class TestTileCollisions {

		public LevelScene levelScene;
		public RoomScene roomScene;
		public Character character;

		public TestTileCollisions() {
			this.levelScene = (LevelScene)Systems.scene;
			this.roomScene = (RoomScene)this.levelScene.rooms[0];
			this.character = Systems.localServer.MyCharacter;
		}

		[TestMethod]
		public void MoveToPos() {
			this.character.physics.MoveToPos(100, 250);
			Debug.Assert(this.character.posX == 100, "MoveToPos: Character should have moved to X position 100.");
			Debug.Assert(this.character.posY == 250, "MoveToPos: Character should have moved to Y position 250.");
		}
	}
}
