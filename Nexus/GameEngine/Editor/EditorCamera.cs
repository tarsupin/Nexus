using Microsoft.Xna.Framework;
using Nexus.Engine;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class EditorCamera {

		public static Dictionary<byte, Vector2> cameraMemory;

		public static void SaveCameraMemory( byte roomId, int posX, int posY ) {

			// Ignore any rooms higher than the allowed number.
			if(roomId > 9) { return; }

			// Set the camera's position.
			EditorCamera.cameraMemory[roomId] = new Vector2(posX, posY);
		}

		public static void LoadCameraMemory( byte roomId ) {

			// Ignore any rooms higher than the allowed number.
			if(roomId > 9) { return; }

			// If the camera memory does not exist for this room, create it at the default.
			if(!EditorCamera.cameraMemory.ContainsKey(roomId)) {
				EditorCamera.SaveCameraMemory(roomId, 0, 0);
			}

			// Update the Camera:
			Systems.camera.posX = (int) EditorCamera.cameraMemory[roomId].X;
			Systems.camera.posX = (int) EditorCamera.cameraMemory[roomId].Y;
		}
	}
}
