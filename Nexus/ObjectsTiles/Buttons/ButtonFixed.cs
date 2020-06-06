using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class ButtonFixed : TileObject {

		public string Texture;
		protected bool toggleBR = false;	// TRUE if this tile toggles BR (blue-red), FALSE if toggles GY (green-yellow)
		protected bool isDown = false;		// TRUE if this the DOWN version.
		protected bool isTimer = false;		// TRUE if the button is a timer.

		public ButtonFixed() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.ButtonFixed];
			this.title = "Fixed Button";
			this.description = "Toggles colors.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Get the Direction of an Inner Boundary
			DirCardinal newDir = TileSolidImpact.RunInnerImpact(actor, gridX * (byte)TilemapEnum.TileWidth, gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight + 32, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight);

			if(newDir == DirCardinal.None) { return false; }

			// Hit Button
			if(newDir == DirCardinal.Down) {
				if(actor is Character) {
					((Character)actor).BounceUp(0, 5);
				}

				else if(actor is EnemyLand || actor is Item) {
					actor.BounceUp(gridX * (byte)TilemapEnum.TileWidth + 24, 5);
				}

				else { return false; }

				// Toggle the color-toggle that matches this tap type.
				room.ToggleColor(this.toggleBR);

				// Update Button Press for Fixed Buttons
				if(!this.isTimer) {

					// If Button is Blue-Red Switch
					if(this.toggleBR) {
						if(!this.isDown) { room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonFixedBRDown, 0); }
						else { room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonFixedBRUp, 0); }
					} else {
						if(!this.isDown) { room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonFixedGYDown, 0); }
						else { room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonFixedGYUp, 0); }
					}
				}
				
				// Update Button Press for Timed Buttons
				else {

					// If Button is Blue-Red Switch
					if(this.toggleBR) {
						if(!this.isDown) { room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedBRDown, 0); }
						else { room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedBRUp, 0); }
					} else {
						if(!this.isDown) { room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedGYDown, 0); }
						else { room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedGYUp, 0); }
					}
				}
			}

			return true;
		}

		//public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
		//	if(actor is Projectile) {
		//		return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
		//	}
		//	return TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
		//}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw( this.Texture + (ToggleBlock.Toggled(room, this.toggleBR) ? "" : "Off") + (this.isDown ? "Down" : ""), posX, posY);
		}
	}
}
