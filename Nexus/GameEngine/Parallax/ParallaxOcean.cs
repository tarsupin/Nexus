using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Engine {

	public static class ParallaxOcean {

		public static ParallaxHandler CreateOceanParallax( LevelScene scene ) {

			Atlas atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];

			// Create Parallax Handler
			ParallaxHandler pxHandler = new ParallaxHandler(scene, atlas, 800, 578, 458);

			// Build Ocean Background
			// TODO HIGH PRIORITY: Also apply Y-axis movement.

			// Generate Standard Cloud Layout
			ParallaxClouds.GenerateCloudLayout(pxHandler);

			return pxHandler;
		}
	}
}
