using Nexus.Config;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonFixed : ButtonTile {

		public ButtonFixed() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ButtonFixed];
			this.title = "Fixed Button";
			this.description = "Toggles colors.";
			this.CreateTextures();
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			int x1 = gridX * (byte)TilemapEnum.TileWidth;
			int x2 = gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth;
			int y1 = gridY * (byte)TilemapEnum.TileHeight + 32;
			int y2 = gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight;
			
			// Check Overlap with Altered Border
			if(!CollideRect.IsOverlappingStrict(actor, x1, x2, y1, y2)) { return false; }
			
			DirCardinal newDir = CollideRect.GetDirectionOfCollision(actor, x1, x2, y1, y2);

			if(newDir == DirCardinal.Down) {
				actor.CollidePosDown(y1 - actor.bounds.Bottom);
			} else if(newDir == DirCardinal.Right) {
				actor.CollidePosRight(x1 - actor.bounds.Right);
			} else if(newDir == DirCardinal.Left) {
				actor.CollidePosLeft(x2 - actor.bounds.Left);
			} else if(newDir == DirCardinal.Up) {
				actor.CollidePosUp(y2 - actor.bounds.Top);
			}

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[8];
			this.Texture[(byte)ButtonSubTypes.BR] = "Button/Fixed/BR";
			this.Texture[(byte)ButtonSubTypes.BRDown] = "Button/Fixed/BRDown";
			this.Texture[(byte)ButtonSubTypes.BROff] = "Button/Fixed/BROff";
			this.Texture[(byte)ButtonSubTypes.BROffDown] = "Button/Fixed/BROffDown";
			this.Texture[(byte)ButtonSubTypes.GY] = "Button/Fixed/GY";
			this.Texture[(byte)ButtonSubTypes.GYDown] = "Button/Fixed/GYDown";
			this.Texture[(byte)ButtonSubTypes.GYOff] = "Button/Fixed/GYOff";
			this.Texture[(byte)ButtonSubTypes.GYOffDown] = "Button/Fixed/GYOffDown";
		}
	}
}
