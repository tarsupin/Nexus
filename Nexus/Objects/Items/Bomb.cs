using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Bomb : Item {

		public enum BombSubType : byte {
			Bomb
		}

		public string baseName = "Items/Bomb";
		public int startFrame;
		public int endFrame;

		public Bomb(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Bomb].meta;
			this.ThrowStrength = 12;
			this.KickStrength = 7;

			// Grip Points (When Held)
			this.gripLeft = -35;
			this.gripRight = 25;
			this.gripLift = -8;

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(4, 4, -4, 0);

			// Is Active
			if(paramList != null && paramList.ContainsKey("on")) {
				this.BeginTimer();
			}
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) BombSubType.Bomb) { this.SpriteName = "Items/Bomb1"; }
		}

		public override void RunTick() {

			// Check if the bomb explodes.
			if(this.endFrame == Systems.timer.Frame && this.endFrame > this.startFrame) {
				Bomb.Detonate(this.room, this.posX + this.bounds.MidX, this.posY + this.bounds.MidY);
				this.Destroy();
				return;
			}

			base.RunTick();
		}

		public void BeginTimer() {
			if(this.startFrame > 0) { return; }
			this.startFrame = Systems.timer.Frame;
			this.endFrame = Systems.timer.Frame + 180;
		}

		public override void ActivateItem(Character character) { }

		public void HorizontalCollide(GameObject obj, int bouncePosX, DirCardinal dir) {

			// Enemies die when from side.
			if(obj is EnemyLand) {
				if(this.physics.velocity.X != 0) {
					((Enemy)obj).ReceiveWound();
				}
			}
		}

		public override bool CollideObjLeft(GameObject obj) {
			this.HorizontalCollide(obj, obj.posX + obj.bounds.Right - this.bounds.Left, DirCardinal.Left);
			return base.CollideObjLeft(obj);
		}

		public override bool CollideObjRight(GameObject obj) {
			this.HorizontalCollide(obj, obj.posX + obj.bounds.Left - this.bounds.Right, DirCardinal.Right);
			return base.CollideObjRight(obj);
		}

		// Something Tapped Bomb From Above
		public override bool CollideObjUp(GameObject obj) {

			// If the bomb hasn't been activated, it makes the activator bounce upward.
			if(this.startFrame == 0) {
				if(obj is Character) {
					Character character = (Character)obj;
					ActionMap.Jump.StartAction(character, 0, 0, 4, true, false);
					this.room.PlaySound(Systems.sounds.thudTap, 1f, this.posX + 16, this.posY + 16);
					this.BeginTimer();
				}
				
				else if(obj is Enemy) {
					Enemy enemy = (Enemy)obj;
					enemy.BounceUp(enemy.posX + enemy.bounds.MidX, 2);
					this.room.PlaySound(Systems.sounds.thudTap, 1f, this.posX + 16, this.posY + 16);
					this.BeginTimer();
				}
			}

			return base.CollideObjUp(obj);
		}

		public static void Detonate(RoomScene room, int midX, int midY) {
			TilemapLevel tilemap = room.tilemap;

			int gridX = (int)Math.Floor((double)(midX / (byte)TilemapEnum.TileWidth));
			int gridY = (int)Math.Floor((double)(midY / (byte)TilemapEnum.TileHeight));

			// Destroy Enemies Within Area of Effect
			List<GameObject> enemiesFound = CollideRect.FindAllObjectsTouchingArea(room.objects[(byte)LoadOrder.Enemy], midX - 144, midY - 96, 288, 192);

			foreach(GameObject enemy in enemiesFound) {
				((Enemy)enemy).Die(DeathResult.Knockout);
			}

			// Heavily Damage Characters Within Area of Effect
			List<GameObject> charsFound = CollideRect.FindAllObjectsTouchingArea(room.objects[(byte)LoadOrder.Character], midX - 144, midY - 96, 288, 192);

			foreach(GameObject character in charsFound) {
				((Character)character).wounds.ReceiveWoundDamage(DamageStrength.Major, true);
				((Character)character).wounds.ReceiveWoundDamage(DamageStrength.Major, true);
				((Character)character).wounds.ReceiveWoundDamage(DamageStrength.Major, true);
			}

			// Destroy Certain Tiles Within Area of Effect
			short startX = (short)Math.Max(0, gridX - 3);
			short startY = (short)Math.Max(0, gridY - 2);
			short endX = (short)Math.Min(tilemap.XCount + (byte)TilemapEnum.GapLeft - 1, gridX + 3);
			short endY = (short)Math.Min(tilemap.YCount + (byte)TilemapEnum.GapUp - 1, gridY + 2);

			// Locate Tiles: Chompers, Boxes, Bricks, Leafs, etc.
			var tilesFound = tilemap.GetTilesByMainIDsWithinArea(new byte[7] { (byte)TileEnum.ChomperFire, (byte)TileEnum.ChomperGrass, (byte)TileEnum.ChomperMetal, (byte)TileEnum.Plant, (byte)TileEnum.Box, (byte)TileEnum.Brick, (byte)TileEnum.Leaf }, startX, startY, endX, endY);

			var TileDict = Systems.mapper.TileDict;

			foreach(var tileInfo in tilesFound) {
				TileObject tile = TileDict[tileInfo.tileId];

				if(tile is Chomper) { ((Chomper)tile).DestroyChomper(room, tileInfo.gridX, tileInfo.gridY); } else if(tile is Brick) { ((Brick)tile).DestroyBrick(room, tileInfo.gridX, tileInfo.gridY); } else if(tile is Box) { ((Box)tile).DestroyBox(room, tileInfo.gridX, tileInfo.gridY); } else if(tile is Leaf) { ((Leaf)tile).TriggerEvent(room, tileInfo.gridX, tileInfo.gridY, 0); }
			}

			// Display Particle Effect
			ExplodeEmitter.BoxExplosion(room, "Particles/GrenadeFrag", midX, midY, 20, 15);
			ExplodeEmitter.BoxExplosion(room, "Particles/GrenadeFrag", midX, midY, 70, 50);

			room.PlaySound(Systems.sounds.explosion, 0.4f, midX, midY);

			// Camera Shake
			Systems.camera.BeginCameraShake(35, 6);
		}

		public override void Draw(int camX, int camY) {

			// Increase Flashing Speed after 2 Seconds
			if(this.startFrame == 0) {
				this.Meta.Atlas.Draw(this.SpriteName, this.posX - camX, this.posY - camY);
				return;
			}
			
			if(Systems.timer.Frame - this.startFrame > 120) {
				this.Meta.Atlas.Draw(this.baseName + (Systems.timer.frame16Modulus % 8 < 4 ? "2" : "1"), this.posX - camX, this.posY - camY);
			} else {
				this.Meta.Atlas.Draw(this.baseName + (Systems.timer.frame16Modulus < 4 ? "2" : "1"), this.posX - camX, this.posY - camY);
			}
		}
	}
}
