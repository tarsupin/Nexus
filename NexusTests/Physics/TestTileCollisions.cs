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
	public class TestTileCollisions {

		public LevelScene levelScene;
		public RoomScene roomScene;
		public TilemapLevel tilemap;
		public Character character;

		public TestTileCollisions() {
			this.levelScene = (LevelScene)Systems.scene;
			this.roomScene = (RoomScene)this.levelScene.rooms[0];
			this.character = Systems.localServer.MyCharacter;
			this.tilemap = this.roomScene.tilemap;
		}

		[TestMethod]
		public void SolidTileCollision() {
			Character ch = this.character;
			Physics phys = ch.physics;

			// Create Tiles in diamond pattern.
			this.tilemap.SetMainTile(10, 8, (byte)TileEnum.Brick, 0);   // Position at 480, 384
			this.tilemap.SetMainTile(9, 9, (byte)TileEnum.Brick, 0);   // Position at 432, 432
			this.tilemap.SetMainTile(10, 10, (byte) TileEnum.Brick, 0);	// Position at 480, 480
			this.tilemap.SetMainTile(11, 9, (byte) TileEnum.Brick, 0);  // Position at 528, 432

			// --- Simultaneous Up AND Right Tile Collisions; not corner --- //
			phys.SetGravity(FInt.Create(0)); // End Gravity
			phys.MoveToPos(480 - ch.bounds.Right - 3, 480);
			phys.velocity = FVector.Create(6, -4);				// Character moving up-right.

			this.VerifyGridPos(ch, 9, 10, 9, 10);

			phys.RunPhysicsTick();
			ch.RunTick();

			CollideTile.RunTileCollision(ch);

			Debug.Assert(ch.posX + ch.bounds.Right == 480, "Character posX should be 480.");
			Debug.Assert(ch.posY + ch.bounds.Top == 480, "Character posY should be 480.");
			
			// --- Simultaneous Down AND Left Tile Collisions; not corner --- //
			phys.MoveToPos(528 - ch.bounds.Left + 2, 432 - ch.bounds.Bottom - 2);
			phys.velocity = FVector.Create(-5, 7);				// Character moving down-left.

			this.VerifyGridPos(ch, 11, 8, 11, 8);

			phys.RunPhysicsTick();
			ch.RunTick();

			CollideTile.RunTileCollision(ch);

			Debug.Assert(ch.posX + ch.bounds.Left == 528, "Character posX should be 528.");
			Debug.Assert(ch.posY + ch.bounds.Bottom == 432, "Character posY should be 432.");

		}

		[TestMethod]
		public void CrossThreshold() {
			Character ch = this.character;
			Physics phys = ch.physics;

			// Reset Character
			phys.MoveToPos(480 - ch.bounds.Left, 480 - ch.bounds.Top);
			phys.velocity = FVector.Create(-5, -5);

			phys.RunPhysicsTick();

			Debug.Assert(phys.CrossedThresholdUpDist(480) == -5);
			Debug.Assert(phys.CrossedThresholdLeftDist(480) == -5);

			// The character's posY passes up through 475 - 480.
			Debug.Assert(phys.CrossedThresholdUp(485) == false);
			Debug.Assert(phys.CrossedThresholdUp(481) == false);
			Debug.Assert(phys.CrossedThresholdUp(480) == true);
			Debug.Assert(phys.CrossedThresholdUp(479) == true);
			Debug.Assert(phys.CrossedThresholdUp(476) == true);
			Debug.Assert(phys.CrossedThresholdUp(475) == true);
			Debug.Assert(phys.CrossedThresholdUp(470) == false);

			// The "Down" threshold should not be triggered.
			Debug.Assert(phys.CrossedThresholdDown(485) == false);
			Debug.Assert(phys.CrossedThresholdDown(481) == false);
			Debug.Assert(phys.CrossedThresholdDown(480) == false);
			Debug.Assert(phys.CrossedThresholdDown(476) == false);
			Debug.Assert(phys.CrossedThresholdDown(475) == false);

			// The character's posX passes left through 475 - 480.
			Debug.Assert(phys.CrossedThresholdLeft(481) == false);
			Debug.Assert(phys.CrossedThresholdLeft(480) == true);
			Debug.Assert(phys.CrossedThresholdLeft(475) == true);
			Debug.Assert(phys.CrossedThresholdLeft(470) == false);

			// The "Right" threshold should not be triggered.
			Debug.Assert(phys.CrossedThresholdRight(485) == false);
			Debug.Assert(phys.CrossedThresholdRight(481) == false);
			Debug.Assert(phys.CrossedThresholdRight(480) == false);
			Debug.Assert(phys.CrossedThresholdRight(475) == false);

			// Reset Character
			phys.MoveToPos(100 - ch.bounds.Right, 100 - ch.bounds.Bottom);
			phys.velocity = FVector.Create(5, 5);

			phys.RunPhysicsTick();

			Debug.Assert(phys.CrossedThresholdDownDist(100) == 5);
			Debug.Assert(phys.CrossedThresholdRightDist(100) == 5);

			// The character's posY passes down through 100 - 105.
			Debug.Assert(phys.CrossedThresholdDown(100) == true);
			Debug.Assert(phys.CrossedThresholdDown(102) == true);
			Debug.Assert(phys.CrossedThresholdDown(105) == true);
			Debug.Assert(phys.CrossedThresholdDown(99) == false);
			Debug.Assert(phys.CrossedThresholdDown(106) == false);

			// The "Up" threshold should not be triggered.
			Debug.Assert(phys.CrossedThresholdUp(100) == false);
			Debug.Assert(phys.CrossedThresholdUp(102) == false);
			Debug.Assert(phys.CrossedThresholdUp(105) == false);
			Debug.Assert(phys.CrossedThresholdUp(99) == false);
			Debug.Assert(phys.CrossedThresholdUp(106) == false);
			
			// The character's posY passes right through 100 - 105.
			Debug.Assert(phys.CrossedThresholdRight(100) == true);
			Debug.Assert(phys.CrossedThresholdRight(102) == true);
			Debug.Assert(phys.CrossedThresholdRight(105) == true);
			Debug.Assert(phys.CrossedThresholdRight(99) == false);
			Debug.Assert(phys.CrossedThresholdRight(106) == false);

			// The "Left" threshold should not be triggered.
			Debug.Assert(phys.CrossedThresholdLeft(100) == false);
			Debug.Assert(phys.CrossedThresholdLeft(102) == false);
			Debug.Assert(phys.CrossedThresholdLeft(105) == false);
			Debug.Assert(phys.CrossedThresholdLeft(99) == false);
			Debug.Assert(phys.CrossedThresholdLeft(106) == false);

		}

		private void VerifyGridPos(DynamicObject obj, ushort gridX, ushort gridY, ushort gridX2, ushort gridY2) {
			Debug.Assert(gridX == obj.GridX, "Object's GridX should be " + gridX + ". Is actually " + obj.GridX);
			Debug.Assert(gridY == obj.GridY, "Object's GridY should be " + gridY + ". Is actually " + obj.GridY);
			Debug.Assert(gridX2 == obj.GridX2, "Object's GridX2 should be " + gridX2 + ". Is actually " + obj.GridX2);
			Debug.Assert(gridY2 == obj.GridY2, "Object's GridY2 should be " + gridY2 + ". Is actually " + obj.GridY2);
		}
	}
}
