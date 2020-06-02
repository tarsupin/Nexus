using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;

namespace Nexus.GameEngine {

	public class WorldChar {

		private WorldScene scene;
		
		// Appearance
		private readonly Atlas atlas;
		private string SpriteName;
		private bool useRunSpeed = false;
		private bool faceRight = true;
		private Suit suit;
		private Head head;
		private Hat hat;

		// Position
		private int posX = 0;
		private int posY = 0;

		// Travel Position Tracking
		private uint startTime = 0;
		private int duration = 0;
		private int startX = 0;
		private int startY = 0;
		private int endX = 0;
		private int endY = 0;

		public byte curX = 0;
		public byte curY = 0;
		private byte willArriveX = 0;
		private byte willArriveY = 0;
		public DirCardinal lastDir = DirCardinal.Center;

		public WorldChar(WorldScene scene) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];

			// Assign Character Details based on Campaign State
			this.faceRight = true;
			this.head = Head.GetHeadBySubType((byte) HeadSubType.RyuHead);
			this.hat = Hat.GetHatBySubType(0);
			this.suit = Suit.GetSuitBySubType((byte) SuitSubType.RedBasic);

			this.SpriteName = "Stand";
		}

		public bool IsAtNode { get { return this.startTime == 0; } }

		public void SetCharacter( CampaignState campaign ) {

			// Assign Character Details based on Campaign State
			this.faceRight = true;
			this.head = Head.GetHeadBySubType(campaign.head);
			this.hat = Hat.GetHatBySubType(campaign.hat);
			this.suit = Suit.GetSuitBySubType(campaign.suit);

			// Position Character
			this.PlaceAtPosition(campaign.curX, campaign.curY);
		}

		private void PlaceAtPosition( byte gridX, byte gridY ) {

			// Reset Character Arrival Stats
			this.willArriveX = 0;
			this.willArriveY = 0;

			// Assign Position
			this.curX = gridX;
			this.curY = gridY;
			this.posX = this.curX * (byte)WorldmapEnum.TileWidth - 4;
			this.posY = this.curY * (byte)WorldmapEnum.TileHeight - 20;
		}

		public void TravelPath( byte toGridX, byte toGridY, DirCardinal dirMoved ) {

			// Assign Travel Data
			this.willArriveX = toGridX;
			this.willArriveY = toGridY;
			this.lastDir = dirMoved;
			this.startTime = Systems.timer.Frame;
			this.startX = this.posX;
			this.startY = this.posY;
			this.endX = toGridX * (byte) WorldmapEnum.TileWidth - 4;
			this.endY = toGridY * (byte) WorldmapEnum.TileHeight - 20;

			// Determine the standard walk duration.
			int dist = TrigCalc.GetDistance(this.startX, this.startY, this.endX, this.endY);
			this.duration = dist;

			// Update Face Direction
			if(this.endX > this.startX) { this.faceRight = true; }
			else { this.faceRight = false; }

			// Update Speed (Walk or Run)
			if(this.duration > 90) {
				this.duration = (int)(this.duration * 0.4);
				this.useRunSpeed = true;
			} else {
				this.useRunSpeed = false;
			}
		}

		public void RunTick() {

			// WorldChar travel updates don't run when stopped at a node.
			if(this.IsAtNode) {

				// If Arrival Was Designated
				if(this.willArriveX != 0 || this.willArriveY != 0) {
					this.PlaceAtPosition(this.willArriveX, this.willArriveY);
					this.scene.ArriveAtLocation(this.curX, this.curY);
				}

				return;
			}

			// WorldChar is Moving
			// Identify Position based on Global Timing
			float weight = (float) (Systems.timer.Frame - this.startTime) / this.duration;

			if(weight >= 1) {
				weight = 1;
				this.startTime = 0;
			}

			// Set Position
			this.posX = (int) Math.Floor(Spectrum.GetValueFromPercent(weight, this.startX, this.endX));
			this.posY = (int) Math.Floor(Spectrum.GetValueFromPercent(weight, this.startY, this.endY));

			// Update Sprite
			this.SpriteTick();
		}

		// Update Animations and Sprite Changes for Characters.
		private void SpriteTick() {

			// If Not Traveling
			if(this.IsAtNode) {
				this.SpriteName = "Stand" + (this.faceRight ? "" : "Left");
				return;
			}

			// Run Cycle
			if(this.useRunSpeed) {
				var walkCycle = (Systems.timer.Frame / 7) % 2;
				this.SpriteName = this.faceRight ? AnimCycleMap.CharacterRunRight[walkCycle] : AnimCycleMap.CharacterRunLeft[walkCycle];
			}

			// Walk Cycle
			else {
				var walkCycle = (Systems.timer.Frame / 11) % 2;
				this.SpriteName = this.faceRight ? AnimCycleMap.CharacterWalkRight[walkCycle] : AnimCycleMap.CharacterWalkLeft[walkCycle];
			}
		}

		public void Draw(int camX, int camY) {

			// TODO: DRAW WITH ANIMATIONS
			// TODO: Atlas

			// Draw Character's Body
			this.suit.Draw(this.SpriteName, posX, posY, camX, camY);

			// Draw Character's Head and Hat
			this.head.Draw(this.faceRight, this.posX, this.posY, camX, camY);
			if(this.hat is Hat) { this.hat.Draw(this.faceRight, this.posX, this.posY, camX, camY); }
		}
	}
}
