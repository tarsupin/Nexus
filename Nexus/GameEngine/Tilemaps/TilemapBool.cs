
namespace Nexus.GameEngine {

	interface TilemapBool {

		byte[] GetTileDataAtGridID(uint gridId);
		byte[] GetTileDataAtGrid(ushort gridX, ushort gridY);

		void AddTileAtGrid(ushort gridX, ushort gridY, byte id = 0, byte subType = 0, byte fgId = 0, byte fgSubType = 0);

		void SetTile(uint gridId, byte id = 0, byte subType = 0, byte fgId = 0, byte fgSubType = 0);
		void SetTileSubType(uint gridId, byte subType = 0);

		void RemoveTileByGrid(ushort gridX, ushort gridY);
		void RemoveTile(uint gridId);

		void ClearMainLayer(uint gridId);
		uint GetGridID(ushort gridX, ushort gridY);

		// TODO: Uncomment once we've updated to C# 8.0
		//static ushort GridX(int posX);
		//static ushort GridY(int posY);
	}
}
