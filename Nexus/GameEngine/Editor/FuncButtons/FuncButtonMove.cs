using static Nexus.GameEngine.FuncTool;

namespace Nexus.GameEngine {

	public class FuncButtonMove : FuncButton {

		public FuncButtonMove() : base() {
			this.keyChar = "v";
			this.spriteName = "Icons/Move";
			this.title = "Move";
			this.description = "No behavior at this time.";
		}

		public override void ActivateFuncButton() {
			EditorTools.SetFuncTool(FuncTool.funcToolMap[(byte)FuncToolEnum.Move]);
			System.Console.WriteLine("Activated Function Button: Move");
		}
	}
}
