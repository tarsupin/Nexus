
using Nexus.GameEngine;
using System;
using System.Collections.Generic;

namespace Nexus.Engine {

	// These flags identify relative positions of the parallax scene; used for positioning.
	public enum ParallaxLoopFlag : byte {
		Top,
		Skyline,
		Horizon,
		Ground,
		Bottom,
	}

	// These parallax objects will repeatedly loop around the screen every time they are off-screened.
	public class ParallaxLoopers {
		public float x;
		public float y;
		public ushort width;
		public float parallaxDist;	// Parallax Z-Dist & Speed. (0 = ignores camera, 0.05 = far distance, 0.6 = close and fast, 1 = fast as camera)
		public float xVelocity;		// Natural Speed; how fast it moves regardless of camera movement.
		public string spriteName;	// Name of the sprite to draw.
	}

	public class ParallaxHandler {

		private readonly RoomScene room;
		private readonly Atlas atlas;
		public List<ParallaxLoopers> loopObjects;

		public ushort totalHeight;				// Total height of the parallax.
		public ushort skyLine;					// Reference point for Y-axis; where the high-area skyline starts (e.g. clouds).
		public ushort horizon;					// Reference point for Y-axis; where the horizon starts.
		public ushort groundLine;				// Reference point for Y-axis; where the ground "starts" for drawing purposes.

		public ParallaxHandler( RoomScene room, Atlas atlas, ushort groundLine, ushort horizon, ushort skyLine ) {
			this.room = room;
			this.atlas = atlas;
			this.groundLine = groundLine;
			this.horizon = horizon;
			this.skyLine = skyLine;
			this.loopObjects = new List<ParallaxLoopers>();
		}

		public void AddBackgroundLayer() {

		}

		public void AddLoopingObject( string spriteName, float parallaxDist, ParallaxLoopFlag lowFlag, ParallaxLoopFlag highFlag, float xVelocity, ushort width ) {

			// Randomize X and Y starting position if they're unassigned.
			float x = CalcRandom.IntBetween(-50, Systems.screen.windowWidth - 20);

			// Randomly determine Y-Position based on where the object should be layered:
			int high = 0;
			int low = Systems.screen.windowHeight;

			switch(lowFlag) {
				case ParallaxLoopFlag.Ground: low = this.groundLine; break;
				case ParallaxLoopFlag.Horizon: low = this.horizon; break;
				case ParallaxLoopFlag.Skyline: low = this.skyLine; break;
			}

			switch(highFlag) {
				case ParallaxLoopFlag.Ground: high = this.groundLine; break;
				case ParallaxLoopFlag.Horizon: high = this.horizon; break;
				case ParallaxLoopFlag.Skyline: high = this.skyLine; break;
			}

			float y = CalcRandom.IntBetween(high, low);

			this.AddLoopingObject(spriteName, parallaxDist, x, y, xVelocity, width);
		}

		public void AddLoopingObject( string spriteName, float parallaxDist, float x, float y, float xVelocity, ushort width ) {

			ParallaxLoopers item = new ParallaxLoopers() {
				x = x,
				y = y,
				width = width,
				parallaxDist = parallaxDist,
				xVelocity = xVelocity,
				spriteName = spriteName,
			};

			this.loopObjects.Add(item);
		}

		public void RunParallaxTick() {
			Camera camera = Systems.camera;
			ushort camWidth = camera.width;
			int camX = camera.posX;
			int camY = camera.posY;

			// Loop through all the parallax object and update accordingly:
			foreach(ParallaxLoopers loopObject in this.loopObjects) {

				// Update it's position by it's velocity.
				loopObject.x += loopObject.xVelocity;

				int screenX = (int) Math.Round(loopObject.x - camX * loopObject.parallaxDist);

				// If something goes off screen too far, reset its position:
				if(screenX + loopObject.width < -50) {
					loopObject.x += camWidth + loopObject.width + 50;
				} else if(screenX > camWidth + 50) {
					loopObject.x -= (camWidth + loopObject.width + 50);
				}
			}
		}

		public void Draw() {
			Camera camera = Systems.camera;
			int camX = camera.posX;
			int camY = camera.posY;
			ushort windowWidth = Systems.screen.windowWidth;

			// Draw all visible parallax objects:
			foreach( ParallaxLoopers loopObject in this.loopObjects ) {

				int screenX = (int) Math.Round(loopObject.x - camX * loopObject.parallaxDist);

				// Only render if the object is visible on the screen:
				if(screenX > 0 - loopObject.width && screenX < windowWidth) {
					this.atlas.Draw(loopObject.spriteName, screenX, (int) Math.Round(loopObject.y - camY));
				}
			}
		}
	}
}
