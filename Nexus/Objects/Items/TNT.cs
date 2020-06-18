using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class TNT : Item {

		public enum TNTSubType : byte {
			TNT
		}

		public TNT(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.TNT].meta;
			this.ThrowStrength = 14;

			// Grip Points (When Held)
			this.gripLeft = -35;
			this.gripRight = 25;
			this.gripLift = -8;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(6, 2, -2, 0);
		}

		public override void ActivateItem(Character character) {
			this.Destroy();
			character.heldItem.ResetHeldItem();
			TNT.DetonateTNT(character);
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) TNTSubType.TNT) {
				this.SpriteName = "Items/TNT";
			}
		}

		public static void DetonateTNT(Character character) {
			int posX = Math.Min(Math.Max(0, character.posX - 800), character.room.tilemap.InnerWidth - 1600);
			int posY = Math.Min(Math.Max(0, character.posY - 500), character.room.tilemap.InnerHeight - 1000);
			TNT.DetonateTNT(character.room, posX, posY, 1600, 1000);
		}

		public static void DetonateTNT(RoomScene room, int posX, int posY, short width, short height) {
			Systems.camera.BeginCameraShake(13, 7);
			Systems.sounds.explosion.Play();

			List<GameObject> objects = CollideRect.FindAllObjectsTouchingArea(
				room.objects[(byte)LoadOrder.Enemy], posX, posY, width, height
			);

			// Loop through Enemies and destroy any that can be destroyed by TNT.
			foreach(GameObject obj in objects) {
				Enemy enemy = (Enemy)obj;
				enemy.DamageByTNT();
			};

			TilemapLevel tilemap = room.tilemap;

			// Destroy Chompers Within Area of Effect
			short startX = Math.Max((short) 0, (short)Math.Floor((double)(posX / (byte)TilemapEnum.TileWidth)));
			short startY = Math.Max((short) 0, (short)Math.Floor((double)(posY / (byte)TilemapEnum.TileHeight)));
			short endX = Math.Min(tilemap.XCount, (short)Math.Floor((double)((posX + width) / (byte)TilemapEnum.TileWidth)));
			short endY = Math.Min(tilemap.YCount, (short)Math.Floor((double)((posY + height) / (byte)TilemapEnum.TileHeight)));

			// Locate Chompers
			var tilesFound = tilemap.GetTilesByMainIDsWithinArea(new byte[7] { (byte)TileEnum.ChomperFire, (byte)TileEnum.ChomperGrass, (byte)TileEnum.ChomperMetal, (byte)TileEnum.Plant, (byte)TileEnum.Box, (byte)TileEnum.Brick, (byte)TileEnum.Leaf }, startX, startY, endX, endY);

			var TileDict = Systems.mapper.TileDict;

			foreach(var tileInfo in tilesFound) {
				TileObject tile = TileDict[tileInfo.tileId];
				if(tile is Chomper) { ((Chomper)tile).DestroyChomper(room, tileInfo.gridX, tileInfo.gridY); }
			}
		}
	}
}
