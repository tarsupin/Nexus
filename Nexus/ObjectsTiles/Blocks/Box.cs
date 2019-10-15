using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Box : BlockTile {

		public string[] Texture;

		public enum BoxSubType {
			Brown = 0,
			Gray = 1,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.Box)) {
				new Box(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.Box, subTypeId);
		}

		public Box(LevelScene scene) : base(scene, TileGameObjectId.Box) {
			this.CreateTextures();
		}

		// TODO HIGH PRIORITY: See DetectObject.DamageAbove() for process of damaging above the box when broken.
		// TODO HIGH PRIORITY: See DetectObject.DamageAbove() for process of damaging above the box when broken.
		// TODO HIGH PRIORITY: Also need a damaging effect (special collision), which will remove the tile.

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

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) BoxSubType.Brown] = "Box/Brown";
			this.Texture[(byte) BoxSubType.Gray] = "Box/Gray";
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
