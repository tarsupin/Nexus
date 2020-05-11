
using static Nexus.GameEngine.FuncTool;

namespace Nexus.GameEngine {

	public class FuncButtonWand : FuncButton {

		public FuncButtonWand() : base() {
			this.keyChar = "e";
			this.spriteName = "Icons/Wand";
			this.title = "Wand";
			this.description = "An advanced tool that allows you to modify properties on game objects.";
		}

		public override void ActivateFuncButton() {
			EditorTools.SetFuncTool(FuncTool.funcToolMap[(byte)FuncToolEnum.Wand]);
			System.Console.WriteLine("Activated Function Button: Wand");
		}
	}
}
