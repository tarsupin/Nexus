using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class FuncTool {

		public static Dictionary<byte, FuncTool> funcToolMap = new Dictionary<byte, FuncTool>() {
			{ (byte) FuncToolEnum.Eraser, new FuncToolEraser() },
			{ (byte) FuncToolEnum.Eyedrop, new FuncToolEyedrop() },
			{ (byte) FuncToolEnum.Move, new FuncToolMove() },
			{ (byte) FuncToolEnum.Wand, new FuncToolWand() },
		};

		public FuncTool() {}
		public virtual void RunTick(EditorScene scene) {}
	}
}
