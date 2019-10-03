using System.Collections.Generic;

/*
 * Packets (From Server) have the following conventions:
 *	
 * packet
 *  .instruction	(PacketInstruction enum)
 *	.frame?			(uint) Identifies the frame that the instruction applies to.
 *	.player?		(uint) Identifies the player ID responsible for the packet.
 *	.data			(object[]) The information contained in the packet.
 * 
 * 
 * Input Packet:
 *	
 *	packet
 *		.instruction = PacketInstruction.IKeys
 *		.frame = frame
 *		.data[playerId]
 *			[0] iKeysPressed
 *			[1] iKeysReleased
 * 
 */

namespace Nexus.Engine {

	public enum ServerPacketIns : byte {
		IKeys = 1,
	}

	public class ServerPacket {
		public ServerPacketIns instruction { get; set; }
		public uint frame { get; set; }
		public uint player { get; set; }
		public object data { get; set; }
	}
	
	public class IKeyPacket : ServerPacket {
		public new Dictionary<byte, Dictionary<byte, IKey[]>> data { get; set; }
	}

	public class PacketFromServer {

	}
}
