﻿using Nexus.Gameplay;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.GameEngine {

	public class ParamsFlight : Params {

		public ParamsFlight() {
			ParamsFlight.ApplyRules(ref this.rules);
		}

		public static void ApplyRules(ref List<ParamGroup> rules) {
			rules.Add(new LabeledParam("fly", "Movement Type", new string[6] { "None", "Axis", "Quadratic", "Circle", "To Direction", "Follows Track", }, (byte)FlightMovement.Axis));

			rules.Add(new IntParam("duration", "Move Duration", 100, 6000, 25, 300, "0 ms"));
			rules.Add(new IntParam("durationOffset", "Timer Offset", 0, 6000, 25, 0, "0 ms"));

			rules.Add(new IntParam("x", "X Movement", -20, 20, 1, 0, " tiles(s)"));
			rules.Add(new IntParam("y", "Y Movement", -20, 20, 1, 0, " tiles(s)"));

			rules.Add(new IntParam("midX", "X Midpoint", -20, 20, 1, 0, " tiles(s)"));
			rules.Add(new IntParam("midY", "Y Midpoint", -20, 20, 1, 0, " tiles(s)"));

			rules.Add(new IntParam("diameter", "Diameter", 0, 20, 1, 3, " tiles(s)"));

			rules.Add(new LabeledParam("reverse", "Reverses Direction", new string[2] { "False", "True" }, (byte)0));
			rules.Add(new IntParam("countdown", "Countdown", 0, 60, 1, 0, " second(s)"));

			rules.Add(new IntParam("toTrack", "To Track #", 0, 100, 1, 1, " (0 to ignore)"));

			rules.Add(new IntParam("clusterId", "Act As Cluster ID", 0, 10, 1, 0, " (0 to ignore)"));
			rules.Add(new IntParam("toCluster", "Link To Cluster ID", 0, 10, 1, 0, " (0 to ignore)"));
		}

		// This override will check the "fly" group param, and show the appropriate rule accordingly.
		public override bool RunCustomMenuUpdate() {
			short flightVal = WandData.GetParamVal(WandData.moveParamSet, "fly");

			List<byte> rulesToShow = new List<byte>();

			// Add Rules that are present for this menu:
			this.AddRulesToShow(new string[] { "fly" }, ref rulesToShow);

			if(flightVal != 0) {
				this.AddRulesToShow(new string[] { "duration", "durationOffset" }, ref rulesToShow);
			}

			switch(flightVal) {

				// X, Y, Reverse, Cluster Link, Cluster ID
				case (byte)FlightMovement.Axis:
					this.AddRulesToShow(new string[] { "x", "y", "reverse", "clusterId", "toCluster" }, ref rulesToShow);
					break;

				// X, Y, MidX, MidY, Reverse, Cluster Link, Cluster ID
				case (byte)FlightMovement.Quadratic:
					this.AddRulesToShow(new string[] { "x", "y", "midX", "midY", "reverse", "clusterId", "toCluster" }, ref rulesToShow);
					break;

				// Diameter, Reverse, Cluster Link, Cluster ID
				case (byte)FlightMovement.Circle:
					this.AddRulesToShow(new string[] { "diameter", "reverse", "clusterId", "toCluster" }, ref rulesToShow);
					break;

				// X, Y, Countdown, Cluster ID
				case (byte)FlightMovement.To:
					this.AddRulesToShow(new string[] { "x", "y", "countdown", "clusterId" }, ref rulesToShow);
					break;

				// Track, Cluster ID
				case (byte)FlightMovement.Track:
					this.AddRulesToShow(new string[] { "toTrack", "clusterId" }, ref rulesToShow);
					break;
			}

			byte[] ruleIdsToShow = rulesToShow.ToArray();
			WandData.moveParamMenu.UpdateMenuOptions((byte) ruleIdsToShow.Length, ruleIdsToShow);
			return true;
		}

		public void AddRulesToShow(string[] ruleKeys, ref List<byte> rulesToShow) {
			byte index = 0;
			foreach(var rule in this.rules) {
				if(ruleKeys.Contains(rule.key)) {
					rulesToShow.Add(index);
				}
				index++;
			}
		}
	}
}
