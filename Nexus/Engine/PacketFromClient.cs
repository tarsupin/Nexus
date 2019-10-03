using System.Collections.Generic;

namespace Nexus.Engine {

	public enum ClientPacketIns : byte {
		IKeysPressed = 1,
		IKeysReleased = 2,
		IKeysBoth = 3,
	}

	public class PacketFromClient {

		/*
		 * PacketIKeys:
		 *		- [0] is a flag for whether the IKeys provided are all PRESSED, RELEASED, or a combination of BOTH.
		 *		- [1?] applies if [0] flag indicates BOTH. [1] flag is added to describe how many PRESSED IKeys are sent.
		 *		- [x+] IKeys: The pressed IKeys and/or the released IKeys.
		 *				- If [1] was set, then [1] pressed IKeys are added first, followed by released IKeys.
		 */
		public static string PacketIKeys( ClientPacketIns instruction, IKey[] pressedIKeys, byte pressedNum, IKey[] releasedIKeys, byte releasedNum ) {

			List<object> packet = new List<object>();
			packet.Add((byte)instruction);

			if(instruction == ClientPacketIns.IKeysBoth) {
				packet.Add(pressedNum);
			}

			// Add Pressed IKeys to Packet
			if(instruction == ClientPacketIns.IKeysBoth || instruction == ClientPacketIns.IKeysPressed) {
				for( byte i = 0; i < pressedNum; i++ ) {
					packet.Add(pressedIKeys[i]);
				}
			}

			// Add Released IKeys to Packet
			if(instruction == ClientPacketIns.IKeysBoth || instruction == ClientPacketIns.IKeysReleased) {
				for(byte i = 0; i < releasedNum; i++) {
					packet.Add(releasedIKeys[i]);
				}
			}

			return DataConvert.ToJson(packet);
		}
	}
}
