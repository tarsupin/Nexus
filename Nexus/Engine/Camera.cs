using Nexus.GameEngine;
using Nexus.Gameplay;
using System;

namespace Nexus.Engine {

	public class Camera {

		// Systems
		private readonly Scene scene;
		private readonly TimerGlobal time;

		// Camera Traits
		public FVector pos;
		public ushort width;				// Width of the Camera (in pixels).
		public ushort height;				// Height of the Camera (in pixels).
		private FVector centerOffset;		// X, Y offset to center the Camera.
		private BoundsCamera bounds;		// Limits of how far within the scene the camera can travel.
		private FInt speedMult;             // Camera follows or moves at this speed.
		private byte controlSpeed;			// Speed (Pixel Movement) when using Manual Control (Directions)

		// Shake Effect, measured by Frames
		private uint shakeStart;
		private uint shakeEnd;
		private byte shakeStrength;

		public Camera( Scene scene ) {
			this.scene = scene;
			this.time = scene.time;
			this.pos = FVector.Create(0, 0);
			this.speedMult = FInt.FromParts(0, 80); // 0.08f;
			this.controlSpeed = 10;
			this.SetSize(FVector.Create(1440, 816));		// TODO HIGH PRIORITY: Change camera size to window size, and update accordingly when resizing.
			this.bounds = new BoundsCamera();
			this.BindToScene();
		}

		public ushort GridX { get { return (ushort)Math.Floor((double)this.pos.X.IntValue / (byte)TilemapEnum.TileWidth); } }
		public ushort GridY { get { return (ushort)Math.Floor((double)this.pos.Y.IntValue / (byte)TilemapEnum.TileHeight); } }

		public void SetSize( FVector size ) {
			this.centerOffset = FVector.Create((int) Math.Floor((double) (size.X.IntValue / 2)), (int) Math.Floor((double) (size.Y.IntValue / 2)));
			this.width = (ushort) size.X.IntValue;
			this.height = (ushort) size.Y.IntValue;
		}

		// Camera Bounds
		public void BindToScene( int top = 0, int left = 0, int right = 0, int bottom = 0 ) {
			this.bounds.Top = top;
			this.bounds.Left = left;
			this.bounds.Right = right != 0 ? right : this.scene.screen.width - this.scene.screen.windowWidth;
			this.bounds.Bottom = bottom != 0 ? bottom : this.scene.screen.height - this.scene.screen.windowHeight;
		}

		public void StayBounded( short extraWidth = 0, short extraHeight = 0 ) {
			this.pos.X = (FInt) Math.Min(this.bounds.Right + extraWidth, Math.Max(this.bounds.Left, this.pos.X.IntValue));
			this.pos.Y = (FInt) Math.Min(this.bounds.Bottom + extraHeight, Math.Max(this.bounds.Top, this.pos.Y.IntValue));
		}

		// Camera Movement
		public void CenterAtPosition(FVector pos2) {
			this.pos = FVector.VectorSubtract(pos2, this.centerOffset);
			this.StayBounded();
		}

		public void CutToPosition(FVector pos2) {
			
			// If the Camera is at (0, 0), it may be possible to attempt a smooth follow here.
			if(this.pos.X.IntValue == 0 && this.pos.Y.IntValue == 0) {

				// Calculate the distance between the two positions.
				FInt dist = Calc.GetDistance(pos, pos2);

				// If the positions are less than 1800 pixels apart, do a follow movement rather than jump.
				if(dist < 1800) {
					this.Follow(pos2);
				}
			}

			this.CenterAtPosition(pos2);
		}

		// Move the Camera, such that it will slowly follow to a given spot.
		public void Follow( FVector pos2, int leewayX = 0, int leewayY = 0 ) {

			FInt dist;
			FInt centerX = pos2.X.IntValue - this.centerOffset.X;
			FInt centerY = pos2.Y.IntValue - this.centerOffset.Y;

			// Follow X-Axis
			if(this.pos.X.IntValue < centerX - leewayX) {
				dist = (centerX - leewayX) - this.pos.X.IntValue;
				this.pos.X += dist * this.speedMult;
			} else if(this.pos.X.IntValue > centerX + leewayX) {
				dist = this.pos.X - (centerX - leewayX);
				this.pos.X -= dist * this.speedMult;
			}

			// Follow Y-Axis
			if(this.pos.Y.IntValue < centerY - leewayY) {
				dist = (centerY - leewayY) - this.pos.Y.IntValue;
				this.pos.Y += dist * this.speedMult;
			} else if(this.pos.Y.IntValue > centerY + leewayY) {
				dist = this.pos.Y - (centerY - leewayY);
				this.pos.Y -= dist * this.speedMult;
			}

			// Force the Camera to remain within bounds.
			this.StayBounded();
		}

		// Camera Input Control
		public void MoveWithInput( PlayerInput input ) {

			if(input.isDown(IKey.Left)) {
				this.pos.X -= this.controlSpeed;
			} else if(input.isDown(IKey.Right)) {
				this.pos.X += this.controlSpeed;
			}

			if(input.isDown(IKey.Up)) {
				this.pos.Y -= this.controlSpeed;
			} else if(input.isDown(IKey.Down)) {
				this.pos.Y += this.controlSpeed;
			}

			this.StayBounded();
		}

		// Camera Shake
		public void BeginCameraShake( byte framesDuration, byte strength ) {
			this.shakeStart = this.time.frame;
			this.shakeEnd = this.shakeStart + framesDuration;
			this.shakeStrength = strength;
		}

		public bool IsShaking() {
			return this.shakeEnd > this.time.frame;
		}

		// TODO: CAMERA SHAKE
		//public void GetCameraShakeOffsetX() {
		//	return Calc.lerpEaseBothDir( -this.shakeStrength* 2, this.shakeStrength* 2, (this.time.elapsed % 240) / 240 );
		//}

		//public void GetCameraShakeOffsetY() {
		//	return Calc.lerpEaseBothDir( -this.shakeStrength, this.shakeStrength, (this.time.elapsed - this.shakeStart) / (this.shakeEnd - this.shakeStart) );
		//}
	}
}
