using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Detector : TileObject {

		protected string Texture;

		public Detector() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Detect];
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {
			
			// Characters interact with Detector:
			if(actor is Character) {
				return this.RunSpecialDetection(room, (Character) actor, gridX, gridY, dir);
			}

			return false;
		}

		public virtual bool RunSpecialDetection(RoomScene room, Character actor, short gridX, short gridY, DirCardinal dir) { return false; }

		// Detectors don't get rendered.
		public override void Draw(RoomScene room, byte subType, int posX, int posY) {}
	}
}
