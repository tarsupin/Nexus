using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class CheckFlag : TileObject {

		protected string Texture;

		public CheckFlag() : base() {
			this.setupRules = SetupRules.SetupTile;
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Flag];
		}

		public virtual void SetupTile(RoomScene room, short gridX, short gridY) {

			// Add a Room Exit, which tracks where all the "destinations" are for Character transitions between rooms.
			room.roomExits.AddExit(this.tileId, 0, gridX, gridY);
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {
			
			// Characters interact with CheckFlag:
			if(actor is Character) {
				if(actor.posX + actor.bounds.Left > gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth) { return false; }
				this.TouchFlag(room, (Character)actor, gridX, gridY);
			}

			return false;
		}

		protected virtual void TouchFlag( RoomScene room, Character character, short gridX, short gridY ) {}

		protected void ReceiveFlagUpgrades(RoomScene room, Character character, short gridX, short gridY) {
			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			// Suit
			if(paramList.ContainsKey("suit") && paramList["suit"] > 0) {
				Suit.AssignToCharacter(character, ParamTrack.AssignSuitIDs[(byte) paramList["suit"]], true);
			}
			
			// Hat
			if(paramList.ContainsKey("hat") && paramList["hat"] > 0) {
				Hat.AssignToCharacter(character, ParamTrack.AssignHatIDs[(byte) paramList["hat"]], true);
			}

			// Mobility Power
			if(paramList.ContainsKey("mob") && paramList["mob"] > 0) {
				if(paramList["mob"] == 1) { Power.RemoveMobilityPower(character); }
				Power.AssignPower(character, ParamTrack.AssignMobilityIDs[(byte) paramList["mob"]]);
			}

			// Attack Power
			byte attType = paramList.ContainsKey("att") ? (byte) paramList["att"] : (byte) 0;

			// No Attack Power
			if(attType == 1) {
				Power.RemoveAttackPower(character);
			}

			// Weapon
			if(attType == 2) {
				byte power = paramList.ContainsKey("weapon") ? (byte)paramList["weapon"] : (byte)0;
				Power.AssignPower(character, ParamTrack.AssignWeaponIDs[power]);
			}

			// Spells
			if(attType == 3) {
				byte power = paramList.ContainsKey("spell") ? (byte)paramList["spell"] : (byte)0;
				Power.AssignPower(character, ParamTrack.AssignSpellsIDs[power]);
			}

			// Thrown
			if(attType == 4) {
				byte power = paramList.ContainsKey("thrown") ? (byte)paramList["thrown"] : (byte)0;
				Power.AssignPower(character, ParamTrack.AssignThrownIDs[power]);
			}

			// Bolts
			if(attType == 5) {
				byte power = paramList.ContainsKey("bolt") ? (byte)paramList["bolt"] : (byte)0;
				Power.AssignPower(character, ParamTrack.AssignBoltsIDs[power]);
			}

			character.stats.ResetCharacterStats();
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
