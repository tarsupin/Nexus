using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Engine {

	public enum PacketInstruction {
		IKeysPressed = 1,
		IKeysReleased = 2,
		IKeysBoth = 3,
	}

	class PacketFromClient {

		/*
		 * PacketIKeys:
		 *		- [0] is a flag for whether the IKeys provided are all PRESSED, RELEASED, or a combination of BOTH.
		 *		- [1?] applies if [0] flag indicates BOTH. [1] flag is added to describe how many PRESSED IKeys are sent.
		 *		- [x+] IKeys: The pressed IKeys and/or the released IKeys.
		 *				- If [1] was set, then [1] pressed IKeys are added first, followed by released IKeys.
		 */
		public static string PacketIKeys( PacketInstruction instruction, IKey[] pressedIKeys, byte pressedNum, IKey[] releasedIKeys, byte releasedNum ) {

			List<object> packet = new List<object>();
			packet.Add((byte)instruction);

			if(instruction == PacketInstruction.IKeysBoth) {
				packet.Add(pressedNum);
			}

			// Add Pressed IKeys to Packet
			if(instruction == PacketInstruction.IKeysBoth || instruction == PacketInstruction.IKeysPressed) {
				for( byte i = 0; i < pressedNum; i++ ) {
					packet.Add(pressedIKeys[i]);
				}
			}

			// Add Released IKeys to Packet
			if(instruction == PacketInstruction.IKeysBoth || instruction == PacketInstruction.IKeysReleased) {
				for(byte i = 0; i < releasedNum; i++) {
					packet.Add(releasedIKeys[i]);
				}
			}

			return DataConvert.ToJson(packet);
		}
	}
}
