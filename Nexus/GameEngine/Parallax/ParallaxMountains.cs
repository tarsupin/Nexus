
namespace Nexus.Engine {

	public static class ParallaxMountains {

		public static void GenerateMountainLayout( ParallaxHandler pxHandler ) {
			ParallaxMountains.AddMountain(pxHandler, "BG/mountain1", 0.03f, 128);	// Furthest Mountain, Slowest
			ParallaxMountains.AddMountain(pxHandler, "BG/mountain2", 0.07f, 224);
			ParallaxMountains.AddMountain(pxHandler, "BG/mountain3", 0.11f, 282);	// Closest Mountain, Fastest
		}

		public static void AddMountain( ParallaxHandler pxHandler, string spriteName, float parallaxDist, short width ) {
			pxHandler.AddLoopingObject(spriteName, parallaxDist, ParallaxLoopFlag.Horizon, ParallaxLoopFlag.Horizon, 0, width);
		}
	}
}
