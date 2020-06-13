using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TrackDot : TileObject {

		public string[] Texture;
		protected TrackDotSubType subType;

		public enum TrackDotSubType : byte {
			Blue = 0,
			Green = 1,
			Red = 2,
		}

		// TODO: TrackDots must be assigned track data in the track system.

		public TrackDot() : base() {
			this.collides = false;
			this.CreateTextures();
			this.Meta = Systems.mapper.MetaList[MetaGroup.Interactives];
			this.tileId = (byte)TileEnum.TrackDot;
			this.title = "Track Dot";
			this.description = "A designation that tracked items can move between.";
			this.moveParamSet =  Params.ParamMap["TrackDot"];
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
