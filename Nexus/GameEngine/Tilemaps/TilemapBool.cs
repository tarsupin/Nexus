
namespace Nexus.GameEngine {

	interface TilemapBool {

		byte[] GetTileDataAtGrid(ushort gridX, ushort gridY);

		byte GetMainSubType(ushort gridX, ushort gridY);
		byte GetBGSubType(ushort gridX, ushort gridY);
		byte GetFGSubType(ushort gridX, ushort gridY);

		void SetTile(ushort gridY, ushort gridX, byte id = 0, byte subType = 0, byte bgId = 0, byte bgSubType = 0, byte fgId = 0, byte fgSubType = 0);
		void SetTileSubType(ushort gridY, ushort gridX, byte subType = 0);

		void RemoveTile(ushort gridX, ushort gridY);

		void ClearBGLayer(ushort gridX, ushort gridY);
		void ClearMainLayer(ushort gridX, ushort gridY);
		void ClearFGLayer(ushort gridX, ushort gridY);

		// TODO: Uncomment once we've updated to C# 8.0
		//static byte GridX(int posX);
		//static byte GridY(int posY);
	}
}
