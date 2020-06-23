using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Leaf : BlockTile {

		public string[] Texture;

		public enum LeafSubType : byte {
			Basic = 0,
			Reform = 1,

			// Invisible SubTypes at +10 the original value. When set, the Draw() method won't draw them; but this maintains their original nature.
			InvisibleBasic = 10,
			InvisibleReform = 11,

			// Untouchable SubTypes at +20 the original value. When set, the tile has no collision.
			UntouchableReform = 21,
		}

		private enum LeafTriggerEvent : byte {
			BreakApart = 0,
			Reform = 1,
		}

		private const byte LeafShakeDuration = 90;

		public Leaf() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Leaf;

			// Helper Texts
			this.titles = new string[2];
			this.titles[(byte)LeafSubType.Basic] = "Leaf Block";
			this.titles[(byte)LeafSubType.Reform] = "Reforming Leaf Block";

			this.descriptions = new string[2];
			this.descriptions[(byte)LeafSubType.Basic] = "Shatters a short duration after standing on it.";
			this.descriptions[(byte)LeafSubType.Reform] = "Shatters a short duration after standing on it, but reforms seconds later.";
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {

			if(actor is Projectile) {
				if(actor is GloveProjectile) {
					this.TriggerEvent(room, gridX, gridY, (byte)LeafTriggerEvent.BreakApart);
					return false;
				}
			}

			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			// If the SubType is over 20 (Untouchable), don't run any collisions. It's in an invisible and untouchable state.
			if(subType > 20) { return false; }

			// Slam-Down Action can break leaves without any shake delay.
			if(actor is Character && ((Character)actor).status.action is SlamAction) {
				this.TriggerEvent(room, gridX, gridY, (byte)LeafTriggerEvent.BreakApart);
				return true;
			}

			if(subType < 10) {

				// Destroy Leaf
				if(dir == DirCardinal.Up) {
					BlockTile.DamageAbove(room, gridX, gridY);
					this.TriggerEvent(room, gridX, gridY, (byte)LeafTriggerEvent.BreakApart);
				}

				// Begin Shaking. Add a Queue for 1 second that will break the leaf block.
				else if(dir == DirCardinal.Down) {

					// Perform Shake Delay normally.
					room.tilemap.SetTileSubType(gridX, gridY, (byte)(subType + 10));
					room.queueEvents.AddEvent(Systems.timer.Frame + Leaf.LeafShakeDuration, this.tileId, gridX, gridY, (byte)LeafTriggerEvent.BreakApart);

					// Create Visible Shaking Particle
					LeafShakeParticle.SetParticle(room, this.atlas, this.Texture[subType], new Vector2(gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight), Systems.timer.Frame + Leaf.LeafShakeDuration);
				}
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		// Trigger Event: BeginShake, Break Apart, Reform
		public override bool TriggerEvent(RoomScene room, short gridX, short gridY, short triggerType = 0, short val2 = 0) {

			// Break Apart Event
			if(triggerType == (byte) LeafTriggerEvent.BreakApart) {
				byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				// Display Leaf Breaking
				ExplodeEmitter.BoxExplosion(room, "Particles/Leaf", gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight);

				// Reforming Leafs will be reformed.
				if(subType == (byte)LeafSubType.InvisibleReform || subType == (byte)LeafSubType.Reform || subType == (byte)LeafSubType.UntouchableReform) {
					room.tilemap.SetTileSubType(gridX, gridY, (byte)LeafSubType.UntouchableReform);
					room.queueEvents.AddEvent(Systems.timer.Frame + 60, this.tileId, gridX, gridY, (byte)LeafTriggerEvent.Reform);
				}
				
				// Basic Leaf gets destroyed.
				else {
					room.tilemap.SetMainTile(gridX, gridY, 0, 0);
				}

				Systems.sounds.thudTap.Play();
			}

			// Reform Event
			else if(triggerType == (byte)LeafTriggerEvent.Reform) {
				room.tilemap.SetTileSubType(gridX, gridY, (byte)LeafSubType.Reform);
			}

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) LeafSubType.Basic] = "Leaf/Basic";
			this.Texture[(byte) LeafSubType.Reform] = "Leaf/Reform";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			
			// Don't Render any subtypes of 10+ (Invisible)
			if(subType >= 10) { return; }

			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
