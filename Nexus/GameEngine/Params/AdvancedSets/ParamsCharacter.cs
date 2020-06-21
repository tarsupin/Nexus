
namespace Nexus.GameEngine {

	public class ParamsCharacter : Params {

		public string[] teams = new string[5] { "No Team", "Team #1", "Team #2", "Team #3", "Team #4" };
		public string[] faceOpt = new string[3] { "Ryu", "Poo", "Carl" };

		public ParamsCharacter() {
			this.rules.Add(new LabeledParam("team", "Team", this.teams, (byte) 0));
			this.rules.Add(new IntParam("num", "Team Position", 0, 8, 1, 0, ""));
			this.rules.Add(new LabeledParam("face", "Face", this.faceOpt, (byte) 0));
			this.rules.Add(new LabeledParam("dir", "Facing Direction", new string[2] { "Right", "Left" }, (byte) 0));
		}
	}
}
