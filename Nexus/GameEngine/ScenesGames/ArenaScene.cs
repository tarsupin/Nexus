using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class ArenaScene : LevelScene {

		public ArenaScene() : base() {}

		protected override void LoadMyPlayer() {

			// TODO HIGH PRIORITY: All characters need to be assigned to the level? Or are they somewhere else?
			// TODO HIGH PRIORITY: Assign All Characters according to the match rules:
			foreach(var character in this.rooms[0].objects[(byte)LoadOrder.Character]) {
				Systems.localServer.MyPlayer.AssignCharacter((Character)character.Value);
			}
		}

		protected override void RunSceneLoop() {

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				Player p = player.Value;

				// Check Player Survival
				if(p.character is Character == false || p.character.deathFrame > 0) {
					
					// TODO: Restart their character without rebuilding the room.
				}

				// p.input.UpdateKeyStates(Systems.timer.Frame);
				p.input.UpdateKeyStates(0); // TODO: Update LocalServer so frames are interpreted and assigned here.
			}
		}

		// Arena Levels don't get reset.
		protected override void RunRoomLoop() {
			foreach(RoomScene room in this.rooms) {
				if(room is RoomScene == false) { continue; }
				room.RunTick();
			}
		}
	}
}
