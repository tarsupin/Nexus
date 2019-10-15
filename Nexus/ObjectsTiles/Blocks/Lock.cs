using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Lock : BlockTile {

		public string Texture;

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.Lock)) {
				new Lock(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.Lock, subTypeId);
		}

		public Lock(LevelScene scene) : base(scene, TileGameObjectId.Lock) {
			this.Texture = "Lock/Lock";
		}

		public override bool RunCollision(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			bool collided = TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			// Characters Can Wall Jump
			if(actor is Character) {
				if(dir == DirCardinal.Left || dir == DirCardinal.Right) {
					TileCharWallImpact.RunImpact((Character)actor, dir == DirCardinal.Right);
				}
			}

			return collided;
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
