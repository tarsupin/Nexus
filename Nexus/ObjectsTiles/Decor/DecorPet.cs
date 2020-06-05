﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;

namespace Nexus.Objects {

	public class DecorPet : Decor {

		public enum PetSubType : byte {
			BunnyLeft = 0,
			BunnyRight = 1,
			CowLeft = 2,
			CowRight = 3,
			DeerLeft = 4,
			DeerRight = 5,
			DogLeft = 6,
			DogRight = 7,
			DuckLeft = 8,
			DuckRight = 9,
			FoxLeft = 10,
			FoxRight = 11,
			GoatLeft = 12,
			GoatRight = 13,
			HenLeft = 14,
			HenRight = 15,
			PigLeft = 16,
			PigRight = 17,
			RaccoonLeft = 18,
			RaccoonRight = 19,
			SheepLeft = 20,
			SheepRight = 21,
			SquirrelLeft = 22,
			SquirrelRight = 23,
		}

		public DecorPet() : base() {
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
			this.BuildTextures();
			this.tileId = (byte)TileEnum.DecorPet;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {

			byte jumpTrack = (byte) (Systems.timer.Frame + subType * 47);

			if(jumpTrack > 145 && jumpTrack <= 185) {
				byte offSet = (byte)(5 * Math.Sin(jumpTrack * 0.3) + 5);
				this.atlas.Draw(this.Texture[subType], posX, posY - offSet);
			} else {
				this.atlas.Draw(this.Texture[subType], posX, posY);
			}
		}

		public void BuildTextures() {
			this.Texture = new string[24];
			this.Texture[(byte)PetSubType.BunnyLeft] = "Pets/Bunny/Left";
			this.Texture[(byte)PetSubType.BunnyRight] = "Pets/Bunny/Right";
			this.Texture[(byte)PetSubType.CowLeft] = "Pets/Cow/Left";
			this.Texture[(byte)PetSubType.CowRight] = "Pets/Cow/Right";
			this.Texture[(byte)PetSubType.DeerLeft] = "Pets/Deer/Left";
			this.Texture[(byte)PetSubType.DeerRight] = "Pets/Deer/Right";
			this.Texture[(byte)PetSubType.DogLeft] = "Pets/Dog/Left";
			this.Texture[(byte)PetSubType.DogRight] = "Pets/Dog/Right";
			this.Texture[(byte)PetSubType.DuckLeft] = "Pets/Duck/Left";
			this.Texture[(byte)PetSubType.DuckRight] = "Pets/Duck/Right";
			this.Texture[(byte)PetSubType.FoxLeft] = "Pets/Fox/Left";
			this.Texture[(byte)PetSubType.FoxRight] = "Pets/Fox/Right";
			this.Texture[(byte)PetSubType.GoatLeft] = "Pets/Goat/Left";
			this.Texture[(byte)PetSubType.GoatRight] = "Pets/Goat/Right";
			this.Texture[(byte)PetSubType.HenLeft] = "Pets/Hen/Left";
			this.Texture[(byte)PetSubType.HenRight] = "Pets/Hen/Right";
			this.Texture[(byte)PetSubType.PigLeft] = "Pets/Pig/Left";
			this.Texture[(byte)PetSubType.PigRight] = "Pets/Pig/Right";
			this.Texture[(byte)PetSubType.RaccoonLeft] = "Pets/Raccoon/Left";
			this.Texture[(byte)PetSubType.RaccoonRight] = "Pets/Raccoon/Right";
			this.Texture[(byte)PetSubType.SheepLeft] = "Pets/Sheep/Left";
			this.Texture[(byte)PetSubType.SheepRight] = "Pets/Sheep/Right";
			this.Texture[(byte)PetSubType.SquirrelLeft] = "Pets/Squirrel/Left";
			this.Texture[(byte)PetSubType.SquirrelRight] = "Pets/Squirrel/Right";
		}
	}
}
