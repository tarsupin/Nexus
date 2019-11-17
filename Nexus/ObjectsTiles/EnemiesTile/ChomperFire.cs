using Newtonsoft.Json.Linq;
using Nexus.Gameplay;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ChomperFire : Chomper {

		public ChomperFire() : base() {
			this.SpriteName = "Chomper/Fire/Chomp";
			this.KnockoutName = "Particles/Chomp/Fire";
			this.tileId = (byte)TileEnum.ChomperFire;
		}

		public override void UpdateParams(JObject paramList) {
			// JToken token = paramList["attGrav"];
			// if(token != null) {
			//	int a = (int) paramList.GetValue("attGrav");
			//	System.Console.WriteLine("GRAV: " + a.ToString());
			// }
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			if(subType == (byte) FacingSubType.FaceUp) {
				this.atlas.Draw("Chomper/Fire/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceDown) {
				this.atlas.DrawFaceDown("Chomper/Fire/Chomp1", posX, posY);
			}

			else if(subType == (byte) FacingSubType.FaceLeft) {
				this.atlas.DrawFaceLeft("Chomper/Fire/Chomp1", posX, posY);
			}
			
			else if(subType == (byte) FacingSubType.FaceRight) {
				this.atlas.DrawFaceRight("Chomper/Fire/Chomp1", posX, posY);
			}
		}
	}
}
