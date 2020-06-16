﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	class CollectableHat : Collectable {

		public CollectableHat() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.CollectableHat;
			this.moveParamSet = Params.ParamMap["Collectable"];

			// Helper Texts
			this.titles = new string[11];
			this.titles[(byte)HatSubType.AngelHat] = "Angel Hat";
			this.titles[(byte)HatSubType.BambooHat] = "Bamboo Hat";
			this.titles[(byte)HatSubType.CowboyHat] = "Cowboy Hat";
			this.titles[(byte)HatSubType.FeatheredHat] = "Feathered Hat";
			this.titles[(byte)HatSubType.Fedora] = "Fedora";
			this.titles[(byte)HatSubType.HardHat] = "Hard Hat";
			this.titles[(byte)HatSubType.RangerHat] = "Ranger Hat";
			this.titles[(byte)HatSubType.SpikeyHat] = "Spikey Hat";
			this.titles[(byte)HatSubType.TopHat] = "Top Hat";
			this.titles[(byte)HatSubType.RandomPowerHat] = "Random Hat";

			this.descriptions = new string[11];
			this.descriptions[(byte)HatSubType.AngelHat] = "Grants +1 air jump and increases jump height.";
			this.descriptions[(byte)HatSubType.BambooHat] = "Grants protection from shells. Can grab shells while they're moving.";
			this.descriptions[(byte)HatSubType.CowboyHat] = "Grants wall jump and wall slide.";
			this.descriptions[(byte)HatSubType.FeatheredHat] = "Increases jump strength and duration.";
			this.descriptions[(byte)HatSubType.Fedora] = "Can pass through platforms, but can still land on them.";
			this.descriptions[(byte)HatSubType.HardHat] = "Protected against projectiles from above.";
			this.descriptions[(byte)HatSubType.RangerHat] = "Can attack faster with projectiles.";
			this.descriptions[(byte)HatSubType.SpikeyHat] = "Protected against projectiles from above. Can damage things above.";
			this.descriptions[(byte)HatSubType.TopHat] = "Gain extra invulnerability time when the hat is lost.";
			this.descriptions[(byte)HatSubType.RandomPowerHat] = "Gain a random hat.";
		}

		public override void Collect( RoomScene room, Character character, short gridX, short gridY ) {

			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			Hat.AssignToCharacter(character, subType, true);

			Systems.sounds.collectBweep.Play();
			base.Collect(room, character, gridX, gridY);
		}

		private void CreateTextures() {
			this.Texture = new string[11];
			this.Texture[(byte)HatSubType.AngelHat] = "HatCollect/AngelHat";
			this.Texture[(byte)HatSubType.BambooHat] = "HatCollect/BambooHat";
			this.Texture[(byte)HatSubType.CowboyHat] = "HatCollect/CowboyHat";
			this.Texture[(byte)HatSubType.FeatheredHat] = "HatCollect/FeatheredHat";
			this.Texture[(byte)HatSubType.Fedora] = "HatCollect/FedoraHat";
			this.Texture[(byte)HatSubType.HardHat] = "HatCollect/HardHat";
			this.Texture[(byte)HatSubType.RangerHat] = "HatCollect/RangerHat";
			this.Texture[(byte)HatSubType.SpikeyHat] = "HatCollect/SpikeyHat";
			this.Texture[(byte)HatSubType.TopHat] = "HatCollect/TopHat";
			this.Texture[(byte)HatSubType.RandomPowerHat] = "HatCollect/RandomHat";
		}
	}
}
