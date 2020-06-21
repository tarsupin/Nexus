using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlag : TileObject {

		protected string Texture;

		public CheckFlag() : base() {
			this.setupRules = SetupRules.SetupTile;
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Flag];
		}

		public void SetupTile(RoomScene room, short gridX, short gridY) {

			// Add a Room Exit, which tracks where all the "destinations" are for Character transitions between rooms.
			room.roomExits.AddExit(this.tileId, 0, gridX, gridY);
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {
			
			// Characters interact with CheckFlag:
			if(actor is Character) {
				this.TouchFlag(room, (Character)actor, gridX, gridY);
			}

			return false;
		}

		protected virtual void TouchFlag( RoomScene room, Character character, short gridX, short gridY ) {

			// Play Flag Touch Sound
			Systems.sounds.flag.Play();

			this.ReceiveFlagUpgrades();
		}

		protected void ReceiveFlagUpgrades() {
			// TODO: Assign Suit
				// if this.mechanics.suit { character.suit = .... }
			// TODO: Assign Hat
			// TODO: Assign Mobility Power
			// TODO: Assign Attack Power
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
