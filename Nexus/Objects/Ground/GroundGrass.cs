using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundGrass : Ground {

		public static void ClassGenerate(LevelScene scene, byte subType) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered()) {
				new Ground(scene);
			}
		}

		public GroundGrass(LevelScene scene, byte subType) : base(scene, subType) {

		}

		public override void SetSubType( byte subType ) {
			this.Texture = "Grass/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}
	}
}
