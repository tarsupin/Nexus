using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldScene : Scene {

		// TODO: The IDs here match OTerrain exactly, so this whole thing seems like it's useless.
		// TODO: Remove this when we can.
		public static Dictionary<byte, object> OverTerrain = new Dictionary<byte, object> {
			{ 1, new RandTypeCur(OTerrain.Grass, "g") },
			{ 2, new RandTypeCur(OTerrain.Desert, "d") },
			{ 3, new RandTypeCur(OTerrain.Snow, "s") },
			{ 4, new RandTypeCur(OTerrain.Water, "w") },
			{ 5, new RandTypeCur(OTerrain.Water, "w") },
			{ 6, new RandTypeCur(OTerrain.Mud, "u") },
			{ 7, new RandTypeCur(OTerrain.Dirt, "d") },
			{ 8, new RandTypeCur(OTerrain.Cobble, "c") },
			{ 9, new RandTypeCur(OTerrain.Road, "r") },
			{ 10, new RandTypeCur(OTerrain.Ice, "i") },
			{ 11, new RandTypeCur(OTerrain.GrassDeep, "x") },
		};

		// References
		public readonly WorldUI worldUI;
		public readonly PlayerInput playerInput;
		public WorldChar character;
		public CampaignState campaign;

		// Zones
		public Dictionary<ushort, WorldZone> zones;

		// World Metadata
		public string id = "";
		public string name = "";
		public string description = "";
		public string author = "";
		public ushort version = 0;
		public byte score = 0;

		public WorldScene() : base() {

			// Prepare Components
			this.worldUI = new WorldUI(this);
			this.character = new WorldChar(this);
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.campaign = Systems.handler.campaignState;

			// Prepare Zones
			this.zones = new Dictionary<ushort, WorldZone>();

			// Camera Update
			Systems.camera.UpdateScene(this);
		}

		public WorldZone currentZone { get { return this.zones[this.campaign.zoneId]; } }

		public override void StartScene() {

			// TODO: Make sure that world data is available.
			//if(this.data) {
			//	throw new Exception("Unable to load world. No world data available.");
			//}

			// Assign Default Zone
			//if(Systems.campaign.zone != null) {
			//	Systems.campaign.zone = 0;
			//}

			// Generate Character
			this.character = new WorldChar(this, HeadSubType.RyuHead);

			// Reset Timer
			Systems.timer.ResetTimer();

			// Begin New Music Track
			// TODO: MUSIC HERE
			//Systems.music.SomeTrack.Play();
		}

		public override void EndScene() {
			//if(Systems.music.whatever) { Systems.music.SomeTrack.Stop(); }
		}

		public override void RunTick() {

			// Update Timer
			Systems.timer.RunTick();

			// Run World UI Updates
			this.worldUI.RunTick();

			// Debug Console (only runs if visible)
			Systems.worldConsole.RunTick();

			// Check Input Updates
			this.RunInputCheck();
		}

		public void RunInputCheck() {
			InputClient input = Systems.input;

			// Get the Local Keys Held Down
			//Keys[] localKeys = input.GetAllLocalKeysDown();
			//if(localKeys.Length == 0) { return; }

			// Menu-Specific Key Presses
			// if(this.game.menu.isOpen) { return this.game.menu.onKeyDown( key, iKeyPressed ); }

			// Open Menu
			if(input.LocalKeyPressed(Keys.Tab) || input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
				// TODO: Open a context menu here.
			}

			// Movement
			else if(playerInput.isPressed(IKey.Up)) { this.TryTravel(DirCardinal.Up); }
			else if(playerInput.isPressed(IKey.Down)) { this.TryTravel(DirCardinal.Down); }
			else if(playerInput.isPressed(IKey.Left)) { this.TryTravel(DirCardinal.Left); }
			else if(playerInput.isPressed(IKey.Right)) { this.TryTravel(DirCardinal.Right); }

			// Activate Node
			else if(playerInput.isPressed(IKey.AButton) == true) {
				// TODO: ACTIVATE NODE HERE
			}
		}

		public void TryTravel( DirCardinal dir = DirCardinal.Center, bool leaveSpot = false ) {

			// Can only move if the character is at a Node.
			if(this.character.IsAtNode) { return; }

			// Get Current Node
			NodeData curNode = this.currentZone.GetNode(this.campaign.currentNodeId);

			// AUTO-TRAVEL : Attempt to automatically determine a direction when one is not provided.
			if(dir == DirCardinal.Center) {

				// Check for Auto-Warps (to new World Zones)
				if(curNode.type >= NodeType.Warp && curNode.warp > 0) {

					// If we're supposed to leave the warp (after an arrival)
					if(!leaveSpot) {
						dir = this.GetAutoDir(curNode, 0);
					} else {
						this.ActivateWarp(curNode.zone, curNode.warp);
						return;
					}
				}

				// Check for auto-move travel nodes:
				else {
					if(curNode.type != NodeType.TravelMove && curNode.type != NodeType.TravelPoint) { return; }

					// Identify the Remaining Travel Direction(s):
					dir = this.GetAutoDir(curNode, this.campaign.lastNodeId);
				}
			}

			// Make sure the direction indicated is a valid option:
			ushort nextId = curNode.GetIdOfNodeDir(dir);
			if(nextId == 0) { return; }

			// Get Last Node
			NodeData nextNode = this.currentZone.GetNode(nextId);
			if(nextNode == null) { return; }

			// If the current level / node hasn't been completed, prevent movement if an open path hasn't been declared.
			// However, you can always move to the last node ID you came from.
			if(curNode.type < NodeType.TravelPoint && !this.campaign.IsLevelWon(this.campaign.currentNodeId)) {
				if(nextId != campaign.lastNodeId) { return; }
			}

			// Have Character Travel Path
			this.campaign.SetNode(nextId, this.campaign.currentNodeId);
			this.character.TravelPath(nextNode);
		}

		public DirCardinal GetAutoDir( NodeData node, ushort lastNodeId = 0, byte countReq = 1 ) {
			byte count = 0;

			// Identify the remaining travel direction(s):
			DirCardinal lastDir = lastNodeId > 0 ? this.currentZone.FindDirToNode(node, lastNodeId) : DirCardinal.None;

			DirCardinal dir = DirCardinal.None;
			if(node.Down > 0 && lastDir != DirCardinal.Down) { dir = DirCardinal.Down; count++; }
			if(node.Left > 0 && lastDir != DirCardinal.Left) { dir = DirCardinal.Left; count++; }
			if(node.Right > 0 && lastDir != DirCardinal.Right) { dir = DirCardinal.Right; count++; }
			if(node.Up > 0 && lastDir != DirCardinal.Up) { dir = DirCardinal.Up; count++; }

			return count != countReq ? DirCardinal.None : dir;
		}

		public void ActivateWarp( byte zoneId, ushort warpId ) {

			// Ignore the warp if it goes to its own zone.
			if(zoneId == this.campaign.zoneId) { return; }

			// TODO: FINISH
			// Find the warp in the designated zone:
			//ushort nodeId = this.zones.();
		}

		public override void Draw() {

			// Draw UI
			this.worldUI.Draw();
			Systems.worldConsole.Draw();
		}
	}
}
