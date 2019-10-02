using System;

/*
 * Binary Literals:
 *		0b00000001		// 1			1				0b_0000_0001		// Optional Underscores are allowed
 *		0b00000010		// 2			1 << 1			0b_0000_0010
 *		0b00000100		// 4			1 << 2
 *		0b00001000		// 8			1 << 3
 *		0b00010000		// 16			1 << 4
 *		0b00100000		// 32			1 << 5
 *		0b01000000		// 64			1 << 6
 *		0b10000000		// 128			1 << 7
 */

namespace Nexus.Engine {

	[Flags]
	public enum BitFlags : byte {
		Zero = 0,
		One = 0b00000001,
		Two = 0b00000010,
		Three = 0b00000100,
		Four = 0b00001000,
		Five = 0b00010000,
		Six = 0b00100000,
		Seven = 0b01000000,
		Eight = 0b10000000,
	}

	public class Bitwise {

		// Position 0 is 1, Position 7 is 10000000
		public bool IsBitSet(byte b, byte pos) {
			return (b & (1 << pos)) != 0;
		}

		// Position 0 is ...1's place (furthest right)
		public bool IsBitSet(int b, byte pos) {
			return (b & (1 << pos)) != 0;
		}
		
		public byte Set4Bits( BitFlags a, BitFlags b, BitFlags c, BitFlags d ) {
			return (byte) (a | b | c | d);
		}
	}
}
