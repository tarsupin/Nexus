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

		public Collectable(LevelScene scene, TileGameObjectId classId) : base(scene, classId, AtlasGroup.Tiles) {
			this.collides = true;
		}

		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Characters receive Collectable:
			if(actor is Character) {
				uint gridId = this.scene.tilemap.GetGridID(gridX, gridY);
				this.Collect( (Character) actor, gridId );
			}

			return false;
		}

		public virtual void Collect( Character character, uint gridId ) {
			this.scene.tilemap.RemoveTile(gridId);
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
