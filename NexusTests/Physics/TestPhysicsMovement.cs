using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;
using System;
using System.Diagnostics;

namespace NexusTests {

	[TestClass]
	public class TestPhysicsMovement {

		public LevelScene levelScene;
		public RoomScene roomScene;
		public Character character;

		public TestPhysicsMovement() {
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

		[TestMethod]
		public void MoveToPosX() {
			this.character.physics.MoveToPosX(500);
			Debug.Assert(this.character.posX == 500, "MoveToPosX: Character should have moved to X position 500.");
		}

		[TestMethod]
		public void MoveToPosY() {
			this.character.physics.MoveToPosY(600);
			Debug.Assert(this.character.posY == 600, "MoveToPosY: Character should have moved to Y position 600.");
		}

		[TestMethod]
		// This test is just confirming that positioning with physPos is working correctly.
		// FInt and FVector were potentially messing up with positioning (due to how it used IntValue); but I created "RoundInt", and that should fix the issue.
		public void AccelerateFromGravity() {
			var phys = this.character.physics;

			// Setup
			phys.StopY(); // Reset the Character's Y-Velocity
			phys.MoveToPos(100, 100); // Reset the Character's Position
			phys.SetGravity(FInt.Create(0.55)); // Begin Acceleration Downward

			// Begin Falling
			phys.RunPhysicsTick();		this.VerifyVelocityY(0.55, 100.55);
			phys.RunPhysicsTick();		this.VerifyVelocityY(1.10, 101.65);
			phys.RunPhysicsTick();		this.VerifyVelocityY(1.65, 103.30);
			phys.RunPhysicsTick();		this.VerifyVelocityY(2.20, 105.50);
			phys.RunPhysicsTick();		this.VerifyVelocityY(2.75, 108.25);
			phys.RunPhysicsTick();		this.VerifyVelocityY(3.30, 111.55);
			phys.RunPhysicsTick();		this.VerifyVelocityY(3.85, 115.40);
			phys.RunPhysicsTick();		this.VerifyVelocityY(4.40, 119.80);
			phys.RunPhysicsTick();		this.VerifyVelocityY(4.95, 124.75);
		}
		
		[TestMethod]
		// This test confirms that left and right movement is equal, assuming velocity is equal.
		// This was originally a concern due to how FInt used "IntValue" - but we've since created "RoundInt" and begun using it.
		public void VelocityLeftAndRight() {
			var phys = this.character.physics;

			// Setup
			phys.StopX(); // Reset the Character's X-Velocity
			phys.MoveToPos(500, 100); // Reset the Character's Position
			phys.SetGravity(FInt.Create(0));

			// Move Right
			phys.velocity.X = FInt.Create(0.68);

			phys.RunPhysicsTick();		this.VerifyVelocityX(500.68);
			phys.RunPhysicsTick();		this.VerifyVelocityX(501.36);
			phys.RunPhysicsTick();		this.VerifyVelocityX(502.04);
			phys.RunPhysicsTick();		this.VerifyVelocityX(502.72);
			phys.RunPhysicsTick();		this.VerifyVelocityX(503.40);
			phys.RunPhysicsTick();		this.VerifyVelocityX(504.08);
			phys.RunPhysicsTick();		this.VerifyVelocityX(504.76);
			phys.RunPhysicsTick();		this.VerifyVelocityX(505.44);
			phys.RunPhysicsTick();		this.VerifyVelocityX(506.12);
			phys.RunPhysicsTick();		this.VerifyVelocityX(506.80);

			// Move Left
			phys.velocity.X = FInt.Create(-0.68);

			phys.RunPhysicsTick(); this.VerifyVelocityX(506.12);
			phys.RunPhysicsTick(); this.VerifyVelocityX(505.44);
			phys.RunPhysicsTick(); this.VerifyVelocityX(504.76);
			phys.RunPhysicsTick(); this.VerifyVelocityX(504.08);
			phys.RunPhysicsTick(); this.VerifyVelocityX(503.40);
			phys.RunPhysicsTick(); this.VerifyVelocityX(502.72);
			phys.RunPhysicsTick(); this.VerifyVelocityX(502.04);
			phys.RunPhysicsTick(); this.VerifyVelocityX(501.36);
			phys.RunPhysicsTick(); this.VerifyVelocityX(500.68);
			phys.RunPhysicsTick(); this.VerifyVelocityX(500.00);
			phys.RunPhysicsTick(); this.VerifyVelocityX(499.32);
		}

		private void VerifyVelocityX(double physPosXd) {
			int physPosX = (int) Math.Round(physPosXd);
			Debug.Assert(this.character.posX == physPosX, "Char.posX should be " + physPosX + " (" + physPosXd + "). Is actually: " + this.character.posX);
		}

		private void VerifyVelocityY(double velYd, double physPosYd) {
			int velY = (int) Math.Round(velYd);
			int physPosY = (int) Math.Round(physPosYd);
			Debug.Assert(this.character.physics.velocity.Y.RoundInt == velY, "Velocity-Y should be " + velY + " (" + velYd + "). Stored as: " + this.character.physics.velocity.Y.RoundInt);
			Debug.Assert(this.character.posY == physPosY, "Char.posY should be " + physPosY + " (" + physPosYd + "). Is actually: " + this.character.posY);
		}
	}
}
