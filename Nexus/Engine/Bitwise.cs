using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Engine {

	class Bitwise {

		// Position 0 is 1, Position 7 is 10000000
		public bool IsBitSet(byte b, byte pos) {
			return (b & (1 << pos)) != 0;
		}

		// Position 0 is ...1's place (furthest right)
		public bool IsBitSet(int b, byte pos) {
			return (b & (1 << pos)) != 0;
		}

	}
}
