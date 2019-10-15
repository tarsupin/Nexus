using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Collectable : TileGameObject {

		protected string[] Texture;

		public Collectable(LevelScene scene, TileGameObjectId classId) : base(scene, classId, AtlasGroup.Tiles) {
			this.collides = true;
		}

		public override bool RunCollision(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			
			// Characters receive Collectable:
			if(actor is Character) {
				uint gridId = this.scene.tilemap.GetGridID(gridX, gridY);
				this.Collect( gridId );
			}

			return false;
		}

		public virtual void Collect( uint gridId ) {
			this.scene.tilemap.RemoveTile(gridId);
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
