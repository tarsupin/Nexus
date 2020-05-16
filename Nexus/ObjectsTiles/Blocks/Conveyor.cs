using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Conveyor : BlockTile {

		public string[] Texture;

		// TODO: Add Conveyor movement.

		public enum ConveyorSubType : byte {
			Left = 0,
			Right = 1,
			SlowLeft = 2,
			SlowRight = 3,
		}

		public Conveyor() : base() {
			this.CreateTextures();
			this.tileId = (byte) TileEnum.Conveyor;
			this.title = "Conveyor";
			this.description = "Moves creatures and items that are on it.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			//if(actor is Character) {
			//	Character character = (Character) actor;

			// // Get the SubType
			// byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			//}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void CreateTextures() {
			this.Texture = new string[4];
			this.Texture[(byte)ConveyorSubType.Left] = "Conveyor/Left";
			this.Texture[(byte)ConveyorSubType.Right] = "Conveyor/Right";
			this.Texture[(byte)ConveyorSubType.SlowLeft] = "Conveyor/SlowLeft";
			this.Texture[(byte)ConveyorSubType.SlowRight] = "Conveyor/SlowRight";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			// Drawing For Editor
			if(room == null) {
				this.atlas.Draw(this.Texture[subType], posX, posY);
			}

			// Standard Draw (Animated Conveyor)
			// 'track1', 'track2', 'track3' - processed by Global Animation
			//if(subType == (byte) ConveyorSubType.Left) {
				
			//} else if(subType == (byte) ConveyorSubType.Right) {
				
			//} else if(subType == (byte) ConveyorSubType.SlowLeft) {
				
			//} else if(subType == (byte) ConveyorSubType.SlowRight) {
				
			//}
		}
	}
}
