using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum CollectableRule : byte {
		OneUseOnly = 0,					// The consumable can only be collected once, by one player.
		OnePerPlayer = 1,				// The consumable can be collected once by every player.
		Permanent = 2,					// The consumable is permanent; doesn't get consumed on use.
		RegeneratesAfterUse = 3,		// The consumable disappears, but regenerates after a designed number of frames.
	}

	public class Collectable : TileGameObject {

		protected string[] Texture;

		public Collectable() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Collectable];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Characters receive Collectable:
			if(actor is Character) {
				this.Collect( room, (Character) actor, gridX, gridY );
			}

			return false;
		}

		public virtual void Collect( RoomScene room, Character character, ushort gridX, ushort gridY ) {
			room.tilemap.RemoveTile(gridX, gridY);
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
