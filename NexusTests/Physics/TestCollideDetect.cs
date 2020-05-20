using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System;
using System.Diagnostics;

namespace NexusTests {

	[TestClass]
	public class TestCollideDetect {

		public LevelScene levelScene;
		public RoomScene roomScene;
		public Character character;

		public TestCollideDetect() {
			this.levelScene = (LevelScene)Systems.scene;
			this.roomScene = (RoomScene)this.levelScene.rooms[0];
			this.character = Systems.localServer.MyCharacter;
		}

		[TestMethod]
		public void CollisionDetection() {
			Character ch = this.character;
			Physics phys = ch.physics;

			// Setup Character
			phys.StopX(); phys.StopY(); // Reset the Character's Velocity
			phys.MoveToPos(600, 300); // Reset the Character's Position
			phys.SetGravity(FInt.Create(0));
			phys.intend = FVector.Create(3, 3);

			// Setup Shroom
			Shroom shroom = new Shroom(this.roomScene, (byte) ShroomSubType.Black, FVector.Create(700, 300), null);
			shroom.physics.intend = FVector.Create(-1, -1);

			// Confirm that Physics Alignments, IsOverlapping, GetOverlapX, GetOverlapY are working correctly:
			shroom.physics.AlignRight(ch);
			Debug.Assert(CollideDetect.IsOverlapping(ch, shroom) == false);

			shroom.physics.MoveToPosX(shroom.posX - 1);
			Debug.Assert(CollideDetect.IsOverlapping(ch, shroom) == true);
			Debug.Assert(CollideDetect.GetOverlapX(ch, shroom, true) == 1);

			var asdfdf = CollideDetect.GetMaxOverlapX(phys, shroom.physics);
			var fasdfb = CollideDetect.GetMaxOverlapY(phys, shroom.physics);

			Debug.Assert(CollideDetect.GetMaxOverlapX(phys, shroom.physics) == 1);
			Debug.Assert(CollideDetect.GetMaxOverlapY(phys, shroom.physics) == 1);

			shroom.physics.AlignLeft(ch);
			Debug.Assert(CollideDetect.IsOverlapping(ch, shroom) == false);

			shroom.physics.MoveToPosX(shroom.posX + 1);
			Debug.Assert(CollideDetect.IsOverlapping(ch, shroom) == true);
			Debug.Assert(CollideDetect.GetOverlapX(shroom, ch, true) == 1);

			shroom.physics.AlignUp(ch);
			Debug.Assert(CollideDetect.IsOverlapping(ch, shroom) == false);

			shroom.physics.MoveToPosY(shroom.posY + 1);
			Debug.Assert(CollideDetect.IsOverlapping(ch, shroom) == true);
			Debug.Assert(CollideDetect.GetOverlapY(shroom, ch, true) == 1);

			shroom.physics.AlignDown(ch);
			Debug.Assert(CollideDetect.IsOverlapping(ch, shroom) == false);

			shroom.physics.MoveToPosY(shroom.posY - 1);
			Debug.Assert(CollideDetect.IsOverlapping(ch, shroom) == true);
			Debug.Assert(CollideDetect.GetOverlapY(ch, shroom, true) == 1);

			// Move Away (no longer overlapping, off by 2)
			shroom.physics.MoveToPosY(shroom.posY + 3);
			Debug.Assert(CollideDetect.GetOverlapY(ch, shroom, true) == -2);


		}

		private void VerifyVelocityX(double physPosXd) {
			int physPosX = (int)Math.Round(physPosXd);
			Debug.Assert(this.character.posX == physPosX, "Char.posX should be " + physPosX + " (" + physPosXd + "). Is actually: " + this.character.posX);
		}
	}
}
