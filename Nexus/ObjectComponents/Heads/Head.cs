using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Head {

		protected readonly Character character;
		protected readonly string SpriteName;	// Name of the sprite to draw.

		public Head( Character character, string headName ) {
			this.character = character;
			this.SpriteName = "Head/" + headName + "/";
		}

		// A default, Cosmetic Hat associated with the Head (such as PooBear's Hat for PooBear).
		public string DefaultCosmeticHat { get; protected set; }

		// Some Heads come with default hats.
		public void AssignHeadDefaultHat() {
			if(this.DefaultCosmeticHat != "") {
				this.character.hat = new CosmeticHat(this.character, this.DefaultCosmeticHat);
			}
		}

		public void Draw(int camX, int camY) {
			Systems.mapper.atlas[(byte) AtlasGroup.Objects].Draw(this.SpriteName + (this.character.FaceRight ? "Right" : "Left"), this.character.posX - camX, this.character.posY - camY);
		}
	}
}
