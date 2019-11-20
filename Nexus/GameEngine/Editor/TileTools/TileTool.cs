using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorPlaceholder {
		public byte tileId;
		public byte subType;
	}

	public class TileTool {

		public List<EditorPlaceholder[]> placeholders;
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

		public TileTool() {
			this.placeholders = new List<EditorPlaceholder[]>();
		}
	}
}
