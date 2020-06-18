using Nexus.GameEngine;
using Nexus.Gameplay;
using System;

namespace Nexus.Engine {

	public class Camera {

		// Systems
		private Scene scene;

		// Camera Traits
		public int posX;
		public int posY;
		public short width;				// Width of the Camera (in pixels).
		public short height;				// Height of the Camera (in pixels).
		private int midX;					// X offset to center the Camera.
		private int midY;                   // Y offset to center the Camera.
		private int rightX;					// X position on the right of the camera.
		private int bottomY;				// Y position at the bottom of the camera.
		private BoundsCamera bounds;		// Limits of how far within the scene the camera can travel.
		private float speedMult;			// Camera follows or moves at this speed.
		private byte controlSpeed;			// Speed (Pixel Movement) when using Manual Control (Directions)

		// Shake Effect, measured by Frames
		private int shakeStart;
		private int shakeEnd;
		private byte shakeStrength;

		public Camera( Scene scene ) {
			this.UpdateScene(scene);
			this.posX = 0;
			this.posY = 0;
		}

		public short GridX { get { return (short)Math.Floor((double)this.posX / (short)TilemapEnum.TileWidth); } }
		public short GridY { get { return (short)Math.Floor((double)this.posY / (short)TilemapEnum.TileHeight); } }
		
		public short MiniX { get { return (short)Math.Floor((double)this.posX / (short)WorldmapEnum.TileWidth); } }
		public short MiniY { get { return (short)Math.Floor((double)this.posY / (short)WorldmapEnum.TileHeight); } }

		public void UpdateScene( Scene scene, int top = 0, int left = 0, int right = 0, int bottom = 0) {
			this.scene = scene;
			this.speedMult = 0.08f; // 0.08f;
			this.controlSpeed = 8;
			this.SetSize(1440, 900);        // TODO HIGH PRIORITY: Change camera size to window size, and update accordingly when resizing.
			this.bounds = new BoundsCamera();
			this.BindToScene(top, left, right, bottom);
			this.shakeEnd = 0;
		}

		public void SetSize( short width, short height ) {
			this.midX = (int) Math.Floor((double)(width * 0.5));
			this.midY = (int) Math.Floor((double)(height * 0.5));
			this.width = width;
			this.height = height;
		}

		public void SetInputMoveSpeed( byte controlSpeed ) {
			this.controlSpeed = controlSpeed;
		}

		// Camera Bounds
		public void BindToScene( int top = 0, int left = 0, int right = 0, int bottom = 0 ) {
			this.bounds.Top = top;
			this.bounds.Left = left;
			this.bounds.Right = right != 0 ? right : this.scene.Width - Systems.screen.windowWidth;
			this.bounds.Bottom = bottom != 0 ? bottom : this.scene.Height - Math.Min(this.scene.Height, Systems.screen.windowHeight);
		}

		public void StayBoundedAuto( short extraWidth = 0, short extraHeight = 0 ) {
			this.posX = Math.Min(this.bounds.Right + extraWidth, Math.Max(this.bounds.Left, this.posX));
			this.posY = Math.Min(this.bounds.Bottom + extraHeight, Math.Max(this.bounds.Top, this.posY));
			this.rightX = this.posX + this.width;
			this.bottomY = this.posY + this.height;
		}

		public void StayBounded(int left = 0, int right = 0, int top = 0, int bottom = 0) {
			if(this.posX < left) { this.posX = left; }
			else if(this.posX + this.width > right) { this.posX = right - this.width; }
			if(this.posY < top) {
				this.posY = top;
			}
			else if(this.posY + this.height > bottom) {
				this.posY = bottom - this.height;
			}
			this.rightX = this.posX + this.width;
			this.bottomY = this.posY + this.height;
		}

		// Camera Movement
		public void CenterAtPosition( int posX, int posY ) {
			this.posX = posX - this.midX;
			this.posY = posY - this.midY;
		}

		public void CutToPosition( int posX, int posY ) {
			
			// If the Camera is at (0, 0), it may be possible to attempt a smooth follow here.
			// We use this so that when the level initially starts, it tries to follow Character.
			if(this.posX == 0 && this.posY == 0) {

				// Calculate the distance between the two positions.
				int dist = TrigCalc.GetDistance( this.posX, this.posY, posX, posY );

				// If the positions are less than 1800 pixels apart, do a follow movement rather than jump.
				if(dist < 1800) {
					this.Follow(posX, posY);
				}
			}

			this.CenterAtPosition( posX, posY );
		}

		// Move the Camera, such that it will slowly follow to a given spot.
		public void Follow( int posX, int posY, int leewayX = 0, int leewayY = 0 ) {

			int dist;
			int centerX = posX - this.midX;
			int centerY = posY - this.midY;

			// Follow X-Axis
			if(this.posX < centerX - leewayX) {
				dist = (centerX - leewayX) - this.posX;
				this.posX += (int) (dist * this.speedMult);
			} else if(this.posX > centerX + leewayX) {
				dist = this.posX - (centerX + leewayX);
				this.posX -= (int) (dist * this.speedMult);
			}

			// Follow Y-Axis
			if(this.posY < centerY - leewayY) {
				dist = (centerY - leewayY) - this.posY;
				this.posY += (int) (dist * this.speedMult);
			} else if(this.posY > centerY + leewayY) {
				dist = this.posY - (centerY + leewayY);
				this.posY -= (int) (dist * this.speedMult);
			}
		}

		// Camera Input Control
		public void MoveWithInput( PlayerInput input, byte moveMult = 1 ) {

			if(input.isDown(IKey.Left)) {
				this.posX -= this.controlSpeed * moveMult;
			} else if(input.isDown(IKey.Right)) {
				this.posX += this.controlSpeed * moveMult;
			}

			if(input.isDown(IKey.Up)) {
				this.posY -= this.controlSpeed * moveMult;
			} else if(input.isDown(IKey.Down)) {
				this.posY += this.controlSpeed * moveMult;
			}
		}

		// Camera Detection
		public bool ActorIsOnCamera( GameObject actor ) {
			return actor.posX < this.rightX && actor.posY < this.bottomY && actor.posX + actor.bounds.Right > this.posX && actor.posY + actor.bounds.Bottom > this.posY;
		}

		public int ActorXDistanceFromCamera( GameObject actor ) {
			if(actor.posX > this.rightX) { return actor.posX - this.rightX; }
			if(actor.posX + actor.bounds.Right < this.posX) { return actor.posX + actor.bounds.Right - this.posX; }
			return 0;
		}

		// Camera Shake
		public void BeginCameraShake( byte framesDuration, byte strength ) {
			this.shakeStart = Systems.timer.Frame;
			this.shakeEnd = this.shakeStart + framesDuration;
			this.shakeStrength = strength;
		}

		public bool IsShaking() {
			return this.shakeEnd > Systems.timer.Frame;
		}

		public int GetCameraShakeOffsetX() {
			return (int) Interpolation.EaseBothDir(-this.shakeStrength * 2, this.shakeStrength * 2, (float)(Systems.timer.frame16Modulus) / (float)16);
		}

		public int GetCameraShakeOffsetY() {
			return (int) Interpolation.EaseBothDir(-this.shakeStrength, this.shakeStrength, (float)(Systems.timer.Frame - this.shakeStart) / (float)(this.shakeEnd - this.shakeStart));
		}
	}
}
