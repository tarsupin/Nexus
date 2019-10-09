using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class WhiteWizard : Suit {

		public WhiteWizard( Character character ) : base(character, SuitRank.PowerSuit) {

		}

		public override void AssignSuitDefaultHat() {
			this.character.hat = new WhiteWizardHat(this.character);
		}

		public override void UpdateCharacterStats() {
			this.character.stats.CanFastCast = true;
			
			// TODO SOUND: char.scene.soundList.shield.play(); // Play Shield Sound; actually, this should go into the shield class.
			// TODO HIGH PRIORITY: Add Shield Attachment
			// char.attachments.shield.generateShield( 3, 90, 1 );
		}
	}
}
