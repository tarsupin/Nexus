﻿using Microsoft.Xna.Framework.Input;
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

		public static Dictionary<Keys, byte> funcToolKey = new Dictionary<Keys, byte>() {
			{ Keys.X, (byte) FuncToolEnum.Eraser },
			{ Keys.C, (byte) FuncToolEnum.Eyedrop },
			{ Keys.M, (byte) FuncToolEnum.Move },
			{ Keys.T, (byte) FuncToolEnum.Wand },
		};

		public FuncTool() {}
		public virtual void RunTick(EditorScene scene) {}
	}
}
