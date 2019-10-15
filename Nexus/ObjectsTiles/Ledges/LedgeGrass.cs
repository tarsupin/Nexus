using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class LedgeGrass : Ledge {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Add the Top Layers of Grass Ledges as "LedgeGrass" tiles.
			// Only some SubTypes apply.
			switch(subTypeId) {
				case (byte) GroundSubTypes.S:
				case (byte) GroundSubTypes.FUL:
				case (byte) GroundSubTypes.FU:
				case (byte) GroundSubTypes.FUR:
				case (byte) GroundSubTypes.H1:
				case (byte) GroundSubTypes.H2:
				case (byte) GroundSubTypes.H3:
				case (byte) GroundSubTypes.V1:

					// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
					if(!scene.IsClassGameObjectRegistered((byte)TileGameObjectId.LedgeGrass)) {
						new LedgeGrass(scene);
					}

					scene.tilemap.AddTile(gridX, gridY, (byte)TileGameObjectId.LedgeGrass, subTypeId);
					return;
			}

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte)TileGameObjectId.LedgeGrassDecor)) {
				new LedgeGrassDecor(scene);
			}

			// Add to Ledge Grass Decor (no collisions, no facing).
			scene.tilemap.AddTile(gridX, gridY, (byte)TileGameObjectId.LedgeGrassDecor, subTypeId);
		}

		public LedgeGrass(LevelScene scene) : base(scene, TileGameObjectId.LedgeGrass) {
			this.BuildTextures("GrassLedge/");
		}
	}
}
