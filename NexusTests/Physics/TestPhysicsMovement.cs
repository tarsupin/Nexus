using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;
using Nexus;
using Nexus.Engine;
using Nexus.GameEngine;
using System;

namespace NexusTests {

	[TestClass]
	public class TestPhysicsMovement {
		public readonly GameClient game;

		public TestPhysicsMovement() {

			Action gameLoadInstructions = () => {
				SceneTransition.ToLevel("", "QCALQOD16");
				Systems.camera.CenterAtPosition(1200, 0);
			};
			
			this.game = new GameClient(gameLoadInstructions);
			this.game.RunOneFrame();
		}

		[TestMethod]
		public void AccelerateLeft() {
			LevelScene level = new LevelScene();
			var mapper = Systems.mapper;
			var a = 1;
		}
	}
}
