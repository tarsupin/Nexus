using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class CheckFlag : TileObject {

		protected string Texture;
		protected FlagSubType subType;
		protected bool activated;			// TRUE if the flag has been activated (touched by character) already.
		// TODO: Set up the param mechanics and draw the NPC for CheckFlags.

		public enum FlagSubType : byte {
			FinishFlag = 0,
			Checkpoint = 1,
			PassFlag = 2,
			RetryFlag = 3,
		}

		public CheckFlag(FlagSubType subType) : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Flag];
			this.subType = subType;

			// TODO: Must add Flag parameters (HEAD, SUIT, HAT, POWER granted)

			// TODO:
			// Special Pass-Flag Behavior (White Flag)
			// Needs to have a RunTick() function that detects when you're vertically crossing it.
		}

		public override bool RunImpact(RoomScene room, GameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Characters interact with CheckFlag:
			if(actor is Character) {
				//byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				// TODO: Make a flag touch behavior
				//this.TouchFlag( room, (Character) actor, gridId );
			}

			return false;
		}

		protected void TouchFlag( RoomScene room, Character character, uint gridId ) {

			// Don't activate the flag if it's already been activated.
			if(this.activated) { return; }

			// Finish Flag
			if(this.subType == FlagSubType.FinishFlag) {
				Systems.sounds.woohoo.Play();
				// TODO: Complete the level. Return scene.levelComplete()
				return;
			}

			// Play Flag Touch Sound
			Systems.sounds.flag.Play();

			// TODO: if(this.subType == "Retry") { this.scene.game.level.state.setRetry(this); }
			// TODO: else { this.scene.game.level.state.setCheckpoint(this); }

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
