using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using static Nexus.Engine.UIHandler;

namespace Nexus.GameEngine {

	public class LevelScene : Scene {

		// References
		public readonly LevelUI levelUI;
		public RoomScene[] rooms;

		// Trackers
		public int levelResetFrame = 0;
		public bool isSinglePlayer;

		public LevelScene(bool grantCampaignEquipment) : base() {

			// Defaults
			this.isSinglePlayer = true;

			// UI State
			UIHandler.SetUIOptions(false, false);
			UIHandler.SetMenu(null, false);

			// Create UI
			this.levelUI = new LevelUI();

			// Generate Each Room Class
			this.rooms = new RoomScene[8];

			for(byte roomID = 0; roomID < Systems.handler.levelContent.data.rooms.Count; roomID++) {
				this.rooms[roomID] = new RoomScene(this, roomID);
			}

			// If we're on single player, 
			Systems.localServer.ResetPlayers();

			// Restart the level, generate all rooms.
			this.RestartLevel(true);

			// Update Character with World Map abilities (if applicable)
			if(grantCampaignEquipment) {
				Character character = Systems.localServer.MyCharacter;
				CampaignState campaign = Systems.handler.campaignState;

				// Update Character Equipment
				if(character.suit is Suit || !character.suit.IsPowerSuit) { Suit.AssignToCharacter(character, campaign.suit, true); }
				if(character.hat is Hat == false || !character.hat.IsPowerHat) { Hat.AssignToCharacter(character, campaign.hat, true); }
				if(character.shoes is Shoes == false) { Shoes.AssignShoe(character, campaign.shoes); }
				if(character.attackPower is PowerAttack == false) { PowerAttack.AssignPower(character, campaign.powerAtt); }
				if(character.mobilityPower is PowerMobility == false) { PowerMobility.AssignPower(character, campaign.powerMob); }
				if(character.wounds.Health < campaign.health) { character.wounds.SetHealth(campaign.health); }
				if(character.wounds.Armor < campaign.armor) { character.wounds.SetArmor(campaign.armor); }
			}

			// Play or Stop Music
			Systems.music.Play((byte) Systems.handler.levelContent.data.music);
		}

		protected virtual void LoadMyPlayer() {

			// If we're on single player, 
			Systems.localServer.ResetPlayersSoft();

			// Assign All Characters according to the match rules:
			foreach(var character in this.rooms[0].objects[(byte)LoadOrder.Character]) {
				Character nChar = (Character)character.Value;

				// Each character can only be assigned to one player:
				if(nChar.player is Player) { continue; }

				// TODO: Determine which player(s) meet the parameters for this character:
				// TODO

				// If My Character has not been assigned, assign it now:
				if(Systems.localServer.MyCharacter is Character == false) {
					Systems.localServer.MyPlayer.AssignCharacter(nChar, true);
					continue;
				}

				// If the Character has no players to assign to it:
				nChar.AssignPlayer(Placeholders.Player);
			}
		}

		protected virtual void RunSceneLoop() {
			Character character = Systems.localServer.MyCharacter;

			// Run Transport Action, if applicable
			if(character is Character) {
				if(character.status.action is TransportAction) {
					((TransportAction)character.status.action).TriggerTransport(character);
				}
			}

			// Check Player Survival
			if(character is Character == false || character.deathFrame > 0) {

				// Prepare the whole level to be rebuilt.
				this.levelResetFrame = Systems.timer.Frame + 2;
			}

			Systems.localServer.MyPlayer.input.UpdateKeyStates(0);
		}

		public override void RunTick() {

			// Scene Loop will perform scene-specific critical checks, such as identifying all players' input.
			// Single Player will only retrieve one player, while MP will review all players connected.
			this.RunSceneLoop();

			InputClient input = Systems.input;
			PlayerInput playerInput = Systems.localServer.MyPlayer.input;

			// Update UI
			UIComponent.ComponentWithFocus = null;
			Cursor.UpdateMouseState();
			UIHandler.cornerMenu.RunTick();

			// Menu State
			if(UIHandler.uiState == UIState.Menu) {
				UIHandler.menu.RunTick();
				return;
			}

			// Playing State
			else {

				// Open Menu
				if(input.LocalKeyPressed(Keys.Tab) || input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
					UIHandler.SetMenu(UIHandler.levelMenu, true);
				}

				// Open Console (Tilde)
				else if(Systems.input.LocalKeyPressed(Keys.OemTilde)) {
					UIHandler.levelConsole.Open();
				}
			}

			// Some Scenes will disable this, or limit behavior (such as for multiplayer).
			if(this.RunLocalDebugFeatures()) { return; }

			// Update Timer
			Systems.timer.RunTick();

			// Run Each Room in Level
			this.RunRoomLoop();

			// If the time runs out:
			int framesRemain = Systems.handler.levelState.FramesRemaining;
			if(framesRemain <= 600) {
				Character mychar = Systems.localServer.MyCharacter;

				// Play Tick Sounds to Alert Player
				if(framesRemain <= 300) {
					int m8 = Systems.timer.frame60Modulus % 15;

					if(m8 == 0) {
						mychar.room.PlaySound(Systems.sounds.timer1, 1f, Systems.camera.posX + Systems.camera.halfWidth, Systems.camera.posY + Systems.camera.halfHeight);
					} else if(m8 == 8) {
						mychar.room.PlaySound(Systems.sounds.timer2, 1f, Systems.camera.posX + Systems.camera.halfWidth, Systems.camera.posY + Systems.camera.halfHeight);
					}
				}

				else if(Systems.timer.IsBeatFrame) {

					if(Systems.timer.beat4Modulus % 2 == 0) {
						mychar.room.PlaySound(Systems.sounds.timer1, 1f, Systems.camera.posX + Systems.camera.halfWidth, Systems.camera.posY + Systems.camera.halfHeight);
					} else {
						mychar.room.PlaySound(Systems.sounds.timer2, 1f, Systems.camera.posX + Systems.camera.halfWidth, Systems.camera.posY + Systems.camera.halfHeight);
					}

				}

				if(framesRemain <= 0) {
					mychar.wounds.ReceiveWoundDamage(DamageStrength.InstantKill, true);
				}
			}
		}

		protected virtual void RunRoomLoop() {
			
			// If this level is in the process of being reset, we cannot allow rooms to continue their activity.
			if(this.levelResetFrame > 0) {

				// Rebuild the Level
				if(this.levelResetFrame > Systems.timer.Frame) {
					this.RestartLevel();
				}

				return;
			}

			// Run the room the character is currently in.
			this.rooms[Systems.localServer.MyCharacter.room.roomID].RunTick();
		}

		protected virtual bool RunLocalDebugFeatures() {

			// If we're in debug mode:
			if(DebugConfig.Debug) {
				this.DebugToggles();

				// Tick Speed
				if(DebugConfig.TickSpeed != (byte)DebugTickSpeed.StandardSpeed) {

					switch(DebugConfig.TickSpeed) {

						case DebugTickSpeed.HalfSpeed:
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 2 != 0) { return true; }
							break;

						case DebugTickSpeed.QuarterSpeed:
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 4 != 0) { return true; }
							break;

						case DebugTickSpeed.EighthSpeed:
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 8 != 0) { return true; }
							break;

						case DebugTickSpeed.WhenYPressed:
							if(!Systems.localServer.MyPlayer.input.isPressed(IKey.YButton)) { return true; }
							break;


						case DebugTickSpeed.WhileYHeld:
							if(!Systems.localServer.MyPlayer.input.isDown(IKey.YButton)) { return true; }
							break;

						case DebugTickSpeed.WhileYHeldSlow:
							if(!Systems.localServer.MyPlayer.input.isDown(IKey.YButton)) { return true; }
							DebugConfig.trackTicks++;
							if(DebugConfig.trackTicks % 4 != 0) { return true; }
							break;
					}
				}
			}

			return false;
		}

		protected void DebugToggles() {

			// Change Active Debug Mode (press F8)
			InputClient input = Systems.input;

			if(input.LocalKeyPressed(Keys.F1)) { UIHandler.levelConsole.SendCommand(Systems.settings.input.macroF1); }
			else if(input.LocalKeyPressed(Keys.F2)) { UIHandler.levelConsole.SendCommand(Systems.settings.input.macroF2); }
			else if(input.LocalKeyPressed(Keys.F3)) { UIHandler.levelConsole.SendCommand(Systems.settings.input.macroF3); }
			else if(input.LocalKeyPressed(Keys.F4)) { UIHandler.levelConsole.SendCommand(Systems.settings.input.macroF4); }
			else if(input.LocalKeyPressed(Keys.F5)) { UIHandler.levelConsole.SendCommand(Systems.settings.input.macroF5); }
			else if(input.LocalKeyPressed(Keys.F6)) { UIHandler.levelConsole.SendCommand(Systems.settings.input.macroF6); }
			else if(input.LocalKeyPressed(Keys.F7)) { UIHandler.levelConsole.SendCommand(Systems.settings.input.macroF7); }
			else if(input.LocalKeyPressed(Keys.F8)) { UIHandler.levelConsole.SendCommand(Systems.settings.input.macroF8); }

			//else if(input.LocalKeyPressed(Keys.F5)) { DebugConfig.ResetDebugValues(); }
			//else if(input.LocalKeyPressed(Keys.F6)) { DebugConfig.ToggleDebugFrames(); }
			//else if(input.LocalKeyPressed(Keys.F7)) { DebugConfig.ToggleTickSpeed(true); }
			//else if(input.LocalKeyPressed(Keys.F8)) { DebugConfig.ToggleTickSpeed(false); }
		}

		public override void Draw() {

			// My Character
			Character MyCharacter = Systems.localServer.MyCharacter;

			// Draw the Room that the local character is in:
			if(MyCharacter is Character) {
				this.rooms[MyCharacter.room.roomID].Draw();
			}

			// Draw UI
			if(UIHandler.uiState == UIState.Playing) { this.levelUI.Draw(); }
			else {
				UIHandler.cornerMenu.Draw();
			}
			UIHandler.menu.Draw();
		}

		public virtual void RestartLevel(bool fullReset = false) {
			this.levelResetFrame = 0;

			// Reset the level to it's full restarted position.
			if(fullReset) { Systems.handler.levelState.FullReset(); }

			// Timer Reset
			Systems.timer.Unpause();
			Systems.timer.ResetTimer();
			
			// Build Each Room
			foreach(RoomScene room in this.rooms) {
				if(room is RoomScene == false) { continue; }
				room.BuildRoom();
			}

			// Retrieves the local player (as opposed to other players connected online, who may also be linked up).
			this.LoadMyPlayer();

			Character character = Systems.localServer.MyCharacter;

			// Update Camera Limitations
			Systems.camera.UpdateScene(this.rooms[character.room.roomID], (byte)TilemapEnum.GapUp * (byte)TilemapEnum.TileHeight, (byte)TilemapEnum.GapLeft * (byte)TilemapEnum.TileWidth);

			Systems.camera.CutToPosition(character.posX, character.posY);

			// Reset Level State, Maintain Checkpoints.
			LevelState levelState = Systems.handler.levelState;
			levelState.SoftReset();

			// Reset Character's Position To Appropriate Checkpoint (if applicable)
			FlagJson checkpoint = levelState.checkpoint;

			if(checkpoint.active) {
				levelState.checkpoint.active = false;
				ActionMap.Transport.StartAction(character, checkpoint.roomId, levelState.checkpoint.gridX * (byte)TilemapEnum.TileWidth, levelState.checkpoint.gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight);
			}

			// Freeze Character for brief moment:
			character.frozenFrame = Systems.timer.Frame + 25;
		}

		public virtual void EndLevel() {

			// If there is an active world, return to the world stage.
			if(Systems.handler.campaignState.worldId.Length > 0) {
				SceneTransition.ToWorld(Systems.handler.campaignState.worldId);
				return;
			}

			// Otherwise, go to the planet selection scene:
			SceneTransition.ToPlanetSelection();
		}

		// NOTE: You probably want to call this from TransportAction - it will handle your room transitions correctly.
		public void MoveCharacterToNewRoom(Character character, byte roomID) {

			// Make sure the character isn't already in this room:
			if(character.room.roomID == roomID) { return; }

			// Remove Character from Scene's Objects
			character.room.RemoveFromScene(character, true);

			// Add Character to New Scene's Objects
			this.rooms[roomID].AddToScene(character, true);

			// Update Character's New Room
			character.MoveToNewRoom(roomID);
		}
	}
}
