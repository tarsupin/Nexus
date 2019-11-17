using Nexus.Gameplay;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ChomperGrass : Chomper {

		public ChomperGrass() : base() {
			this.SpriteName = "Chomper/Grass/Chomp";
			this.KnockoutName = "Particles/Chomp/Grass";
			this.tileId = (byte)TileEnum.ChomperGrass;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			if(subType == (byte) FacingSubType.FaceUp) {
				this.atlas.Draw("Chomper/Grass/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceDown) {
				this.atlas.DrawFaceDown("Chomper/Grass/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceLeft) {
				this.atlas.DrawFaceLeft("Chomper/Grass/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceRight) {
				this.atlas.DrawFaceRight("Chomper/Grass/Chomp1", posX, posY);
			}
		}
	}
}
