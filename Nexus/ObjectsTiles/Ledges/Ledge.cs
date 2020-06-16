﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class Ledge : TileObject {

		protected string[] Texture;

		public Ledge() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Ledge];
			this.title = "Ledge";
			this.description = "A background landscape that acts like a platform.";
		}

		public override bool RunImpact(RoomScene room, GameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Actor must cross the DOWN threshold for this ledge; otherwise, it shouldn't compute any collision.
			if(!actor.physics.CrossedThresholdDown(gridY * (byte)TilemapEnum.TileHeight)) { return false;  }

			if(actor is Projectile) {
				if(!CollideTileFacing.RunImpactTest(dir, DirCardinal.Up)) { return false; }
				return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
			}

			// Allow Dropdown Mechanic
			if(actor is Character) {
				Character character = (Character)actor;
				character.physics.touch.onMover = true;
				if(character.status.action is DropdownAction) { return false; }
			}

			bool collided = CollideTileFacing.RunImpact(actor, gridX, gridY, dir, DirCardinal.Up);

			if(collided) {
				if(actor is Projectile) {
					TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
				}
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		protected void BuildTextures(string baseName) {
			this.Texture = new string[16];
			this.Texture[(byte)GroundSubTypes.S] = baseName + "S";
			this.Texture[(byte)GroundSubTypes.FUL] = baseName + "FUL";
			this.Texture[(byte)GroundSubTypes.FU] = baseName + "FU";
			this.Texture[(byte)GroundSubTypes.FUR] = baseName + "FUR";
			this.Texture[(byte)GroundSubTypes.FL] = baseName + "FL";
			this.Texture[(byte)GroundSubTypes.FC] = baseName + "FC";
			this.Texture[(byte)GroundSubTypes.FR] = baseName + "FR";
			this.Texture[(byte)GroundSubTypes.FBL] = baseName + "FBL";
			this.Texture[(byte)GroundSubTypes.FB] = baseName + "FB";
			this.Texture[(byte)GroundSubTypes.FBR] = baseName + "FBR";
			this.Texture[(byte)GroundSubTypes.H1] = baseName + "H1";
			this.Texture[(byte)GroundSubTypes.H2] = baseName + "H2";
			this.Texture[(byte)GroundSubTypes.H3] = baseName + "H3";
			this.Texture[(byte)GroundSubTypes.V1] = baseName + "V1";
			this.Texture[(byte)GroundSubTypes.V2] = baseName + "V2";
			this.Texture[(byte)GroundSubTypes.V3] = baseName + "V3";
		}
	}
}
