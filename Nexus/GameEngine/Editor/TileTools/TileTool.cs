using Microsoft.Xna.Framework.Input;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorPlaceholder {
		public byte tileId;
		public byte subType;
	}

	public class TileTool {

		public List<EditorPlaceholder[]> placeholders;
		public string DefaultIcon;
		public int index = 0;
		public int subIndex = 0;

		public static Dictionary<byte, TileTool> tileToolMap = new Dictionary<byte, TileTool>() {
			{ (byte) SlotGroup.Blocks, new TileToolBlocks() },
			{ (byte) SlotGroup.Collectables, new TileToolCollectables() },
			{ (byte) SlotGroup.ColorToggles, new TileToolColorToggles() },
			{ (byte) SlotGroup.EnemyLand, new TileToolEnemyLand() },
			{ (byte) SlotGroup.Fixed, new TileToolFixed() },
			{ (byte) SlotGroup.EnemyFly, new TileToolEnemyFly() },
			{ (byte) SlotGroup.Items, new TileToolItems() },
			{ (byte) SlotGroup.Movers, new TileToolMovers() },
			{ (byte) SlotGroup.Interactives, new TileToolInteractives() },
			{ (byte) SlotGroup.Prompts, new TileToolPrompt() },
		};

		public static Dictionary<Keys, byte> tileToolKey = new Dictionary<Keys, byte>() {
			{ Keys.D1, (byte) SlotGroup.Blocks },
			{ Keys.D2, (byte) SlotGroup.Collectables },
			{ Keys.D3, (byte) SlotGroup.ColorToggles },
			{ Keys.D4, (byte) SlotGroup.EnemyLand },
			{ Keys.D5, (byte) SlotGroup.Fixed },
			{ Keys.D6, (byte) SlotGroup.EnemyFly },
			{ Keys.D7, (byte) SlotGroup.Items },
			{ Keys.D8, (byte) SlotGroup.Movers },
			{ Keys.D9, (byte) SlotGroup.Interactives },
			{ Keys.D0, (byte) SlotGroup.Prompts },
		};

		public TileTool() {
			this.placeholders = new List<EditorPlaceholder[]>();
		}
	}
}
