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
		public ushort width;				// Width of the Camera (in pixels).
		public ushort height;				// Height of the Camera (in pixels).
		private int midX;					// X offset to center the Camera.
		private int midY;					// Y offset to center the Camera.
		private BoundsCamera bounds;		// Limits of how far within the scene the camera can travel.
		private float speedMult;			// Camera follows or moves at this speed.
		private byte controlSpeed;			// Speed (Pixel Movement) when using Manual Control (Directions)

		// Shake Effect, measured by Frames
		private uint shakeStart;
		private uint shakeEnd;
		private byte shakeStrength;

		public Camera( Scene scene ) {
			this.UpdateScene(scene);
		}

		public ushort GridX { get { return (ushort)Math.Floor((double)this.posX / (ushort)TilemapEnum.TileWidth); } }
		public ushort GridY { get { return (ushort)Math.Floor((double)this.posY / (ushort)TilemapEnum.TileHeight); } }

		public void UpdateScene( Scene scene ) {
			this.scene = scene;
			this.posX = 0;
			this.posY = 0;
			this.speedMult = 0.08f; // 0.08f;
			this.controlSpeed = 8;
			this.SetSize(1440, 816);        // TODO HIGH PRIORITY: Change camera size to window size, and update accordingly when resizing.
			this.bounds = new BoundsCamera();
			this.BindToScene();
		}

		public void SetSize( ushort width, ushort height ) {
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
			this.bounds.Top = top + ((byte)TilemapEnum.WorldGapUp * (byte)TilemapEnum.TileHeight);
			this.bounds.Left = left + ((byte) TilemapEnum.WorldGapLeft * (byte)TilemapEnum.TileWidth);
			this.bounds.Right = right != 0 ? right : this.scene.Width - Systems.screen.windowWidth + (byte) TilemapEnum.TileWidth; ;
			this.bounds.Bottom = bottom != 0 ? bottom : this.scene.Height - Math.Min(this.scene.Height, Systems.screen.windowHeight) + (byte) TilemapEnum.TileHeight;
		}

		private void StayBounded( short extraWidth = 0, short extraHeight = 0 ) {
			this.posX = Math.Min(this.bounds.Right + extraWidth, Math.Max(this.bounds.Left, this.posX));
			this.posY = Math.Min(this.bounds.Bottom + extraHeight, Math.Max(this.bounds.Top, this.posY));
		}

		// Camera Movement
		public void CenterAtPosition( int posX, int posY ) {
			this.posX = posX - this.midX;
			this.posY = posY - this.midY;
			this.StayBounded();
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

			// Force the Camera to remain within bounds.
			this.StayBounded();
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

			this.StayBounded();
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
			return (int) Interpolation.EaseBothDir(-this.shakeStrength * 2, this.shakeStrength * 2, (Systems.timer.Frame % 15) / 15);
		}

		public int GetCameraShakeOffsetY() {
			return (int) Interpolation.EaseBothDir(-this.shakeStrength, this.shakeStrength, (Systems.timer.Frame - this.shakeStart) / (this.shakeEnd - this.shakeStart));
		}
	}
}
