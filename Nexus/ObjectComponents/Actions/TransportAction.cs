using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// status.actionNum1 (roomID)	:: the ID of the room to transport to
	// status.actionNum2 (posX)		:: the X coordinate to transport to
	// status.actionNum3 (posY)		:: the Y coordinate to transport to

	// The Transport Action is activated when moving between rooms or positions, such as for doors and checkpoints.
	public class TransportAction : Action {

		public TransportAction() : base() {
			this.endsOnLanding = false;
		}

		public void StartAction( Character character, byte roomId, int posX, int posY ) {
			this.EndLastActionIfActive(character);

			CharacterStatus status = character.status;

			status.action = ActionMap.Transport;
			status.actionNum1 = roomId;
			status.actionNum2 = posX;
			status.actionNum3 = posY;
		}

		public void TriggerTransport( Character character ) {
			CharacterStatus status = character.status;
			character.room.scene.MoveCharacterToNewRoom(character, (byte) status.actionNum1);
			character.physics.MoveToPos(status.actionNum2, status.actionNum3);
			this.EndAction(character);
		}
	}
}
