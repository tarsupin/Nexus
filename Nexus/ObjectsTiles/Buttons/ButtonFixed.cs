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

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			// Get the Direction of an Inner Boundary
			DirCardinal newDir = TileSolidImpact.RunInnerImpact(actor, gridX * (byte)TilemapEnum.TileWidth, gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight + 32, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight);

			if(newDir == DirCardinal.None) { return false; }

			// Hit Button
			if(newDir == DirCardinal.Down) {
				if(actor is Character) {
					ActionMap.Jump.StartAction((Character)actor, 5, 0, 4, true);
				}

				else if(actor is EnemyLand || actor is Item) {
					actor.BounceUp(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, 5);
				}

				else { return false; }

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

					// Toggle the color-toggle that matches this tap type.
					room.colors.ToggleColor(this.toggleBR);
				}

				// Update Button Press for Timed Buttons
				else {

					// Timer Buttons only get toggled when they're up. If down, they stay down until finished.
					if(!this.isDown) {

						// If Button is Blue-Red Switch
						if(this.toggleBR) {
							room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedBRDown, 0);
						} else {
							room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedGYDown, 0);
						}

						// Toggle the color-toggle that matches this tap type.
						room.colors.ToggleColor(this.toggleBR, true);

						// Assign the 10-second return effect:
						room.queueEvents.AddEvent(Systems.timer.Frame + 600, this.tileId, (short) gridX, (short) gridY);
					}
					
					// Play a quiet 'click' noise if it doesn't get triggered.
					else {
						Systems.sounds.click3.Play(0.7f, 0, 0);
					}
				}
			}

			return true;
		}

		// Pop-Up the Timers that have expired their particular count.
		public override bool TriggerEvent(RoomScene room, short gridX, short gridY, short val1 = 0, short val2 = 0) {

			// If Button is Blue-Red Switch
			if(this.toggleBR) {
				room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedBRUp, 0);
			} else {
				room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedGYUp, 0);
			}

			// Toggle the color-toggle that matches this tap type.
			room.colors.ToggleColor(this.toggleBR);
			
			return true;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw( this.Texture + (ToggleBlock.Toggled(room, this.toggleBR) ? "" : "Off") + (this.isDown ? "Down" : ""), posX, posY);
		}
	}
}
