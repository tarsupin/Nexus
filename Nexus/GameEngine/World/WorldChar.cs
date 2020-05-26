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
		private bool useRunSpeed = true;
		private bool faceRight = true;
		private Suit suit;
		private Head head;
		private Hat hat;

		// Position
		private int posX = 0;
		private int posY = 0;

		// Travel Position Tracking
		private bool tryAutoMove = false;
		private uint startTime = 0;
		private int duration = 0;
		private int startX = 0;
		private int startY = 0;
		private int endX = 0;
		private int endY = 0;

		public WorldChar(WorldScene scene, HeadSubType headSubType = HeadSubType.RyuHead) {
			this.SetDefaults();
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Objects];
			this.suit = SuitMap.BasicRedSuit;
			this.head = Head.GetHeadBySubType((byte) headSubType);
		}

		public bool IsAtNode { get { return this.startTime == 0; } }
		
		public void SetDefaults() {
			this.faceRight = true;

			// Determine Starting Position
			//let curNode = this.world.getNode(this.campaign.curNodeId);
			//if(!curNode) { curNode = this.world.getStartNode(); }
			//this.placeAtNode(curNode);
		}

		public void PlaceAtNode( NodeData node ) {
			//if(!node) { return; }
			//this.posX = node.x * (byte) WorldmapEnum.TileWidth - 4;
			//this.posY = node.y * (byte) WorldmapEnum.TileHeight - 20;
		}

		public void TravelPath( NodeData endNode ) {

			// Assign Travel Data
			this.startTime = Systems.timer.Frame;
			this.startX = this.posX;
			this.startY = this.posY;
			this.endX = endNode.gridX * (byte) WorldmapEnum.TileWidth - 4;
			this.endY = endNode.gridY * (byte) WorldmapEnum.TileHeight - 20;

			// Determine the standard walk duration.
			int dist = TrigCalc.GetDistance(this.startX, this.startY, this.endX, this.endY);
			this.duration = dist * 6;

			// Update Face Direction
			if(this.startX < this.endX) { this.faceRight = false; }
			else { this.faceRight = true; }

			// Update Speed (Walk or Run)
			this.useRunSpeed = (this.duration > 400);
		}

		public void RunTick() {

			// WorldChar travel updates don't run when stopped at a node.
			if(this.IsAtNode) {

				// If the WorldChar stops at a node, see if it continues automatically:
				if(this.tryAutoMove) {
					this.scene.TryTravel();
					this.tryAutoMove = false;
				}

				return;
			}

			// WorldChar is Moving
			// Identify Position based on Global Timing
			float weight = (float) (Systems.timer.Frame - this.startTime) / this.duration;

			if(weight >= 1) {
				weight = 1;
				this.startTime = 0;
				this.tryAutoMove = true;
			}

			// Set Position
			this.posX = (int) Math.Floor(Spectrum.GetValueFromPercent(weight, this.startX, this.endX));
			this.posY = (int) Math.Floor(Spectrum.GetValueFromPercent(weight, this.startY, this.endY));
		}

		public void Draw(int camX, int camY) {

			// TODO: DRAW WITH ANIMATIONS
			// TODO: SpriteName, , etc.
			// TODO: Atlas

			// Draw Character's Body
			this.atlas.Draw(this.SpriteName, this.posX - camX, this.posY - camY);

			// Draw Character's Head and Hat
			this.head.Draw(this.faceRight, posX, posY, camX, camY);
			if(this.hat is Hat) { this.hat.Draw(this.faceRight, posX, posY, camX, camY); }
		}
	}
}
