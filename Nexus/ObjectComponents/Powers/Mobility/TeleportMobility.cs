using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class TeleportMobility : PowerMobility {

		public TeleportMobility( Character character ) : base( character ) {
			this.IconTexture = "Power/Teleport";
			this.subStr = "teleport";
			this.SetActivationSettings(180, 1, 180);
		}

		public override bool Activate() {

			// This activation requires a PRESS, not just the HELD-DOWN that is typically required.
			if(!this.character.input.isPressed(IKey.BButton)) { return false; }

			// If the Power is Active, we need to trigger the teleportation.
			if(this.character.status.action is TeleportAction) {
				this.DoTeleport();
				return true;
			}

			// Make sure the power can be activated.
			if(!this.CanActivate()) { return false; }

			// Start the Slam Action
			ActionMap.Teleport.StartAction(this.character);

			Systems.sounds.click1.Play();

			return true;
		}

		private void DoTeleport() {
			CharacterStatus status = this.character.status;

			// End the Teleport Action (to prevent re-teleportation)
			this.character.status.action.EndAction(this.character);

			// Get X, Y Coordinates from Distance and Radian
			float trueRotation = Radians.Normalize(status.actionFloat1);
			int xCoord = (int)Radians.GetXFromRotation(trueRotation, status.actionNum1) + character.posX + character.bounds.MidX;
			int yCoord = (int)Radians.GetYFromRotation(trueRotation, status.actionNum1) + character.posY + character.bounds.MidY;

			// Make sure teleportation is within world bounds.
			if(yCoord < (byte)TilemapEnum.GapUpPixel || yCoord > this.character.room.Height + (byte)TilemapEnum.GapUpPixel || xCoord < (byte)TilemapEnum.GapLeftPixel || xCoord > this.character.room.Width + (byte)TilemapEnum.GapRightPixel) {
				Systems.sounds.disableCollectable.Play(0.5f, 0, 0);
				return;
			}

			TilemapLevel tilemap = character.room.tilemap;

			// Make sure teleportation is to a valid open area. Blocking tiles will prevent teleportation.
			int xMid = xCoord - this.character.bounds.MidX;
			int yMid = yCoord - this.character.bounds.MidY;

			bool upLeft = CollideTile.IsBlockingCoord(tilemap, xMid + character.bounds.Left, yMid + character.bounds.Top - 1, DirCardinal.None);
			bool upRight = CollideTile.IsBlockingCoord(tilemap, xMid + character.bounds.Right, yMid + character.bounds.Bottom - 1, DirCardinal.None);
			bool downLeft = CollideTile.IsBlockingCoord(tilemap, xMid + character.bounds.Left, yMid + character.bounds.Top - 1, DirCardinal.None);
			bool downRight = CollideTile.IsBlockingCoord(tilemap, xMid + character.bounds.Right, yMid + character.bounds.Bottom - 1, DirCardinal.None);

			// If all positions are blocked, prevent teleport.
			if(upLeft && upRight && downLeft && downRight) {
				Systems.sounds.disableCollectable.Play(0.5f, 0, 0);
				return;
			}

			// If some positions are blocked, adjust position:
			if(upLeft && downLeft) { xMid += 12; }
			if(upRight && downRight) { xMid -= 12; }
			if(upLeft && upRight) { yMid += 12; }
			if(downLeft && downRight) { yMid -= 12; }

			Systems.sounds.pop.Play();

			// Teleport Character
			this.character.physics.MoveToPos(xMid, yMid);
		}
	}
}
