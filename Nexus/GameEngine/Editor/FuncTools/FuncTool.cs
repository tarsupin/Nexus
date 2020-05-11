using Microsoft.Xna.Framework.Input;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class FuncTool {

		public enum FuncToolEnum : byte {
			None,
			Eraser,
			Eyedrop,
			Move,
			Wand
		}

		public static Dictionary<byte, FuncTool> funcToolMap = new Dictionary<byte, FuncTool>() {
			{ (byte) FuncToolEnum.Eraser, new FuncToolEraser() },
			{ (byte) FuncToolEnum.Eyedrop, new FuncToolEyedrop() },
			{ (byte) FuncToolEnum.Move, new FuncToolMove() },
			{ (byte) FuncToolEnum.Wand, new FuncToolWand() },
		};

		public static Dictionary<Keys, byte> funcToolKey = new Dictionary<Keys, byte>() {
			{ Keys.X, (byte) FuncToolEnum.Eraser },
			{ Keys.C, (byte) FuncToolEnum.Eyedrop },
			{ Keys.V, (byte) FuncToolEnum.Move },
			{ Keys.E, (byte) FuncToolEnum.Wand },
		};

		public string spriteName;

		public FuncTool() {

		}

		public virtual void RunTick(EditorRoomScene scene) {}
	}
}
