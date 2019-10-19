
namespace Nexus.Engine {

	public static class ParallaxClouds {

		public static void GenerateCloudLayout( ParallaxHandler pxHandler ) {

			// Distant Clouds
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue1", 0.05f, 144);
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue1", 0.07f, 144);
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue1", 0.08f, 144);
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue1", 0.1f, 144);

			// Semi-Distant Clouds
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue2", 0.28f, 154);
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue2", 0.30f, 154);
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue2", 0.33f, 154);
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue2", 0.36f, 154);

			// Nearby Clouds
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue3", 0.5f, 216);
			ParallaxClouds.AddCloud(pxHandler, "BG/CloudBlue3", 0.6f, 216);
		}

		public static void AddCloud( ParallaxHandler pxHandler, string spriteName, float parallaxDist, ushort width ) {
			float xVel = CalcRandom.FloatBetween(-0.5f, 0.5f) * parallaxDist; // Randomize X-Velocity
			pxHandler.AddLoopingObject(spriteName, parallaxDist, ParallaxLoopFlag.Skyline, ParallaxLoopFlag.Top, xVel, width);
		}
	}
}
