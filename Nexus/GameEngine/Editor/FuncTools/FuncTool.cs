
namespace Nexus.GameEngine {

	public class FuncTool {

		protected readonly EditorScene scene;

		public FuncTool( EditorScene scene ) {
			this.scene = scene;
		}

		public virtual void RunTick() {}
	}
}
