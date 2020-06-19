using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class TrackDot : TileObject {

		public string[] Texture;
		protected TrackDotSubType subType;

		public enum TrackDotSubType : byte {
			Blue = 0,
			Green = 1,
			Red = 2,
		}

		// TrackDots must be assigned to the track system.
		public TrackDot() : base() {
			this.setupRules = SetupRules.PreSetupOnly;
			this.collides = false;
			this.CreateTextures();
			this.Meta = Systems.mapper.MetaList[MetaGroup.Track];
			this.tileId = (byte)TileEnum.TrackDot;
			this.title = "Track Dot";
			this.description = "A designation that tracked items can move between.";
			this.moveParamSet =  Params.ParamMap["TrackDot"];
		}

		public void PreSetup(RoomScene room, short gridX, short gridY, byte tileId, byte subType, Dictionary<string, short> paramList = null) {
			if(paramList == null) { paramList = new Dictionary<string, short>(); }

			room.trackSys.AddTrack(
				paramList.ContainsKey("trackNum") ? (byte) paramList["trackNum"] : (byte) 0,
				gridX,
				gridY,
				paramList.ContainsKey("to") ? (byte) paramList["to"] : (byte) 0,
				paramList.ContainsKey("duration") ? (short) paramList["duration"] : (short) 0,
				paramList.ContainsKey("delay") ? (short) paramList["delay"] : (short) 0,
				paramList.ContainsKey("beginFall") ? (paramList["beginFall"] == 1 ? true : false) : false
			);
		}

		private void CreateTextures() {
			this.Texture = new string[3];
			this.Texture[(byte)TrackDotSubType.Blue] = "HiddenObject/TrackBlue";
			this.Texture[(byte)TrackDotSubType.Green] = "HiddenObject/TrackGreen";
			this.Texture[(byte)TrackDotSubType.Red] = "HiddenObject/TrackRed";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			// Only draw in the editor (when room is null):
			if(room == null) {
				this.atlas.Draw(this.Texture[subType], posX, posY);
			}
		}
	}
}
