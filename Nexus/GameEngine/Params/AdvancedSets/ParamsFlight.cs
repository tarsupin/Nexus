using Newtonsoft.Json.Linq;
using Nexus.Gameplay;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ParamsFlight : Params {

		public ParamsFlight() {
			this.rules = new ParamGroup[13];
			this.rules[0] = new LabeledParam("fly", "Movement Type", new string[6] { "None", "Axis", "Quadratic", "Circle", "To Direction", "Follows Track", }, (byte) FlightMovement.Axis );

			this.rules[1] = new IntParam("duration", "Move Duration", 60, 3600, 15, 180, " frame(s)");
			this.rules[2] = new IntParam("durationOffset", "Timer Offset", 0, 3600, 15, 0, " frame(s)");

			this.rules[3] = new IntParam("x", "X Movement", -20, 20, 1, 0, " tiles(s)");
			this.rules[4] = new IntParam("y", "Y Movement", -20, 20, 1, 0, " tiles(s)");

			this.rules[5] = new IntParam("midX", "X Midpoint", -20, 20, 1, 0, " tiles(s)");
			this.rules[6] = new IntParam("midY", "Y Midpoint", -20, 20, 1, 0, " tiles(s)");

			this.rules[7] = new IntParam("diameter", "Diameter", 0, 20, 1, 0, " tiles(s)");

			this.rules[8] = new LabeledParam("reverse", "Reverses Direction", new string[2] { "False", "True" }, (byte) 0);
			this.rules[9] = new IntParam("countdown", "Countdown", 0, 60, 1, 0, " second(s)");

			this.rules[10] = new IntParam("toTrack", "To Track #", 0, 100, 1, 1, " (0 to ignore)");

			this.rules[11] = new IntParam("clusterId", "Act As Cluster ID", 0, 10, 1, 0, " (0 to ignore)");
			this.rules[12] = new IntParam("toCluster", "Link To Cluster ID", 0, 10, 1, 0, " (0 to ignore)");
		}

		// This override will check the "fly" group param, and show the appropriate rule accordingly.
		public override void UpdateMenu() {
			short flightVal = WandData.GetParamVal("fly");

			List<byte> rulesToShow = new List<byte>();

			// Add Rules that are present for this menu:
			rulesToShow.Add(0); // Movement Type ("fly")

			if(flightVal != 0) {
				rulesToShow.Add(1); // Duration
				rulesToShow.Add(2); // Duration Offset
			}

			switch(flightVal) {

				// X, Y, Reverse, Cluster Link, Cluster ID
				case (byte)FlightMovement.Axis:
					rulesToShow.Add(3); // X Movement
					rulesToShow.Add(4); // Y Movement
					rulesToShow.Add(8); // Reverse
					rulesToShow.Add(11); // Cluster ID
					rulesToShow.Add(12); // Cluster Link
					break;

				// X, Y, MidX, MidY, Reverse, Cluster Link, Cluster ID
				case (byte)FlightMovement.Quadratic:
					rulesToShow.Add(3); // X Movement
					rulesToShow.Add(4); // Y Movement
					rulesToShow.Add(5); // MidX Movement
					rulesToShow.Add(6); // MidY Movement
					rulesToShow.Add(8); // Reverse
					rulesToShow.Add(11); // Cluster ID
					rulesToShow.Add(12); // Cluster Link
					break;

				// Diameter, Reverse, Cluster Link, Cluster ID
				case (byte)FlightMovement.Circle:
					rulesToShow.Add(7); // Diameter
					rulesToShow.Add(8); // Reverse
					rulesToShow.Add(11); // Cluster ID
					rulesToShow.Add(12); // Cluster Link
					break;

				// X, Y, Countdown, Cluster ID
				case (byte)FlightMovement.To:
					rulesToShow.Add(3); // X Movement
					rulesToShow.Add(4); // Y Movement
					rulesToShow.Add(9); // Countdown
					rulesToShow.Add(11); // Cluster ID
					break;

				// Track, Cluster ID
				case (byte)FlightMovement.Track:
					rulesToShow.Add(10); // Track ID
					rulesToShow.Add(11); // Cluster ID
					break;
			}

			byte[] ruleIdsToShow = rulesToShow.ToArray();
			WandData.UpdateMenuOptions((byte) ruleIdsToShow.Length, ruleIdsToShow);
		}
	}
}
