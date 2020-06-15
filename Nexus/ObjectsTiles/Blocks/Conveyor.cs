using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Conveyor : BlockTile {

		public string[] Texture;
		private string TrackStr = "Conveyor/Track";

		public enum ConveyorSubType : byte {
			SlowLeft = 0,
			Left = 1,
			SlowRight = 2,
			Right = 3,
		}

		public Conveyor() : base() {
			this.CreateTextures();
			this.tileId = (byte) TileEnum.Conveyor;
			this.title = "Conveyor";
			this.description = "Moves creatures and items that are on it.";
		}

		public override bool RunImpact(RoomScene room, GameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(dir == DirCardinal.Down) {

				// Get the SubType
				byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				if(subType <= (byte)ConveyorSubType.Left) {
					actor.physics.SetExtraMovement(subType == (byte)ConveyorSubType.SlowLeft ? -2 : -4, 0);
				} else {
					actor.physics.SetExtraMovement(subType == (byte)ConveyorSubType.SlowRight ? 2 : 4, 0);
				}
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[4];
			this.Texture[(byte)ConveyorSubType.SlowLeft] = "Conveyor/SlowLeft";
			this.Texture[(byte)ConveyorSubType.Left] = "Conveyor/Left";
			this.Texture[(byte)ConveyorSubType.SlowRight] = "Conveyor/SlowRight";
			this.Texture[(byte)ConveyorSubType.Right] = "Conveyor/Right";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			// Drawing For Editor
			if(room == null) {
				this.atlas.Draw(this.Texture[subType], posX, posY);
				return;
			}

			// Draw the Track using the Global Animation AnimId
			//string tex = this.TrackStr + AnimGlobal.Get3PSAnimId(Systems.timer).ToString();
			//this.atlas.Draw(tex, posX, posY);

			// Draw the Track using the Global Animation AnimId
			string tex = this.TrackStr + AnimGlobal.Get3PHSAnimId(Systems.timer).ToString();
			this.atlas.Draw(tex, posX, posY);
		}
	}
}
