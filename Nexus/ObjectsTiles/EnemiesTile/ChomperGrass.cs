using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ChomperGrass : Chomper {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte)TileGameObjectId.ChomperGrass)) {
				new ChomperGrass(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte)TileGameObjectId.ChomperGrass, subTypeId);
		}

		private ChomperGrass(RoomScene room) : base(room, TileGameObjectId.ChomperGrass) {
			this.SpriteName = "Chomper/Grass/Chomp";
			this.KnockoutName = "Particles/Chomp/Grass";
		}

		public override void Draw(byte subType, int posX, int posY) {

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
