using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ChomperMetal : Chomper {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte)TileEnum.ChomperMetal)) {
				new ChomperMetal(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte)TileEnum.ChomperMetal, subTypeId);
		}

		private ChomperMetal(RoomScene room) : base(room, TileEnum.ChomperMetal) {
			this.SpriteName = "Chomper/Metal/Chomp";
			this.KnockoutName = "Particles/Chomp/Metal";
		}

		public override void Draw(byte subType, int posX, int posY) {

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
