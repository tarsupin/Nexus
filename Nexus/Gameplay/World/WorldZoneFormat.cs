using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexus.Gameplay {

	public class WorldZoneFormat {

		// byte[] { Base Terrain, Top Terrain, Category, Layer, Object, Node ID }
		//		Base Terrain: the very lowest layer. A "b1" to "b#" variation of the base tile.
		//		Top Terrain: the layer above the base, when applicable, such as a path (e7, s3, etc). Not used in conjunction with Category.
		//		Category: a folder to access. Used in conjunction with Base Layer.
		//			- Example: a Base of "Grass" and a Category of "Mountain" would use Grass/Mountain/{layer}
		//		Layer: used to indicate the last part of the image name, such as "e3", "b1", etc. Applies to Top if Top is used; otherwise, with Base.
		//			- Example: a Base of "Grass" and a layer of "b3" would use Grass/b3.
		//			- Example: a Top of "Grass" and a layer of "e6" would use Grass/e6 for the Top layer, and still draw the Base below it.

		[JsonProperty("tiles")]
		public byte[][][] tiles { get; set; }

		//[JsonProperty("nodes")]
		//public WorldNodeFormat[][] nodes { get; set; }
	}
}
