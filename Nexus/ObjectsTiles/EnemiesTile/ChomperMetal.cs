using Nexus.Gameplay;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ChomperMetal : Chomper {

		public ChomperMetal() : base() {
			this.SpriteName = "Chomper/Metal/Chomp";
			this.KnockoutName = "Particles/Chomp/Metal";
			this.tileId = (byte)TileEnum.ChomperMetal;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			if(subType == (byte) FacingSubType.FaceUp) {
				this.atlas.Draw("Chomper/Metal/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceDown) {
				this.atlas.DrawFaceDown("Chomper/Metal/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceLeft) {
				this.atlas.DrawFaceLeft("Chomper/Metal/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceRight) {
				this.atlas.DrawFaceRight("Chomper/Metal/Chomp1", posX, posY);
			}
		}
	}
}
