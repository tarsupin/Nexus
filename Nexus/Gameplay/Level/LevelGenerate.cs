using Nexus.Engine;
using Nexus.GameEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nexus.Gameplay {

	public class LevelGenerate {

		// References
		private readonly Systems systems;
		private readonly LevelContent level;
		private Scene scene;

		public LevelGenerate(LevelContent level, GameHandler gameHandler) {
			this.systems = gameHandler.systems;
			this.level = level;
		}

		public void GenerateRoom(Scene scene, string roomNum) {
			this.scene = scene;

			RoomFormat room = this.level.data.room[roomNum];

			this.GenerateLayer(room.bgLayer);
			this.GenerateLayer(room.mainLayer);
			this.GenerateLayer(room.cosmeticLayer);
		}

		public void GenerateLayer(Dictionary<string, Dictionary<string, ArrayList>> layer) {

			// Prepare Minimum Width and Height for Level
			ushort levelWidth = 24;
			ushort levelHeight = 16;

			// Loop through YData within the Layer Provided:
			foreach(KeyValuePair<string, Dictionary<string, ArrayList>> yData in layer) {
				ushort gridY = ushort.Parse(yData.Key);

				if(gridY > levelHeight) { levelHeight = gridY; }

				// Loop through XData
				foreach(KeyValuePair<string, ArrayList> xData in yData.Value) {
					ushort gridX = ushort.Parse(xData.Key);

					if(gridX > levelWidth) { levelWidth = gridX; }

					if(xData.Value.Count == 2) {
						this.AddObjectToScene(gridX, gridY, Convert.ToUInt16(xData.Value[0]), Convert.ToByte(xData.Value[1]));
					} else if(xData.Value.Count > 2) {
						this.AddObjectToScene(gridX, gridY, Convert.ToUInt16(xData.Value[0]), Convert.ToByte(xData.Value[1]), xData.Value[2]);
					}
				}
			}
		}

		public void AddObjectToScene(ushort gridX, ushort gridY, ushort type, byte subType = 0, dynamic paramList = null) {

			// Skip Certain Flags
			// TODO: Might need to adjust how "Spawn" flags work here.
			if(type == 95) { return; }      // "Character" flag should be ignored.

			GameMapper mapper = this.systems.mapper;

			// Identify Object Class Type
			Type classType;
			bool hasType = mapper.ObjectMap.TryGetValue(type, out classType);
			if(!hasType|| classType == null) { return; }

			// Prepare Position
			FVector pos = FVector.Create(
				Snap.GridToPos((ushort)TilemapEnum.TileWidth, gridX),
				Snap.GridToPos((ushort)TilemapEnum.TileHeight, gridY)
			);

			// If the Object has a "Generate" method, run its special generation rules:
			// TODO: See if we can eliminate this; removing reflection would be a good idea. This effect only really benefits platforms, and that was on web.
			if(classType.GetMethod("Generate") != null) {
				classType.GetMethod("Generate", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { (Scene)this.scene, (byte)subType, (FVector)pos, (object[])paramList });
			} else {
				var gameObj = Activator.CreateInstance(classType, new object[] { (Scene) this.scene, (byte) subType, (FVector) pos, (object[]) paramList });
			}
		}

		// TODO HIGH PRIORITY: See generateCharacter() in LevelGenerator.ts
		//public void GenerateCharacter( FVector pos ) {

		//}
	}
}
