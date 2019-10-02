using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class LevelScene : Scene {

		public LevelScene( Systems systems ) : base( systems ) {

			// Generate Room 0
			systems.handler.level.generate.GenerateRoom(this, "0");
		}

		public void SpawnRoom() {

		}

		public override void Update() {

		}

		public override void Draw() {

			// Render Objects
			Atlas temp = this.systems.mapper.atlas[(byte)AtlasGroup.Blocks];
			temp.Draw("Grass/S", FVector.Create(100, 100));
			temp.Draw("Grass/H1", FVector.Create(160, 100));
		}
	}
}
