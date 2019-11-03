using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class WhiteWizard : Suit {

		public WhiteWizard() : base(SuitRank.PowerSuit, "WhiteWizard", HatMap.WizardWhiteHat) {

		}

		public override void UpdateCharacterStats(Character character) {
			character.stats.CanFastCast = true;
			
			// TODO SOUND: char.scene.soundList.shield.play(); // Play Shield Sound; actually, this should go into the shield class.
			// TODO HIGH PRIORITY: Add Shield Attachment
			// char.attachments.shield.generateShield( 3, 90, 1 );
		}
	}
}
