﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightCircle : FlightBehavior {

		private float radius;           // The radius of the flight circle.
		
		// Identifies relation to cluster, enabling 'center around cluster' as an option.
		private bool centered;

		public FlightCircle(GameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.centered = paramList.ContainsKey("rel") ? (paramList["rel"] == 0 ? true : false) : (bool)true;
			byte diameter = paramList.ContainsKey("diameter") ? (byte) paramList["diameter"] : (byte) 3;
			this.radius = (float) Math.Round((double) diameter / 2, 1) * (byte) TilemapEnum.TileWidth;
		}

		// TODO OPTIMIZE: This section has a huge opportunity for optimization, since we're currently using large calculations twice.
		public override void RunTick() {

			// Identify Position based on Global Timing
			float weight = (float)((float)(Systems.timer.Frame + this.offset) % this.duration) / (float)this.duration;

			// Clockwise or Counter-Clockwise
			if(this.reverse) { weight = -weight; }

			// Set Position
			float posX, posY;

			if(this.cluster is Cluster) {

				// Allow the Actor to Rotate directly around the cluster, rather than having to rig up special clusters.
				if(this.centered) {
					posX = Circle.GetEdgePointByRadianX(this.cluster.actor.posX, this.radius, (float)(weight * Math.PI * 2));
					posY = Circle.GetEdgePointByRadianY(this.cluster.actor.posY, this.radius, (float)(weight * Math.PI * 2));
				}
				
				// Standard Cluster Mechanics
				else {
					posX = Circle.GetEdgePointByRadianX(this.startX, this.radius, (float)(weight * Math.PI * 2));
					posY = Circle.GetEdgePointByRadianY(this.startY, this.radius, (float)(weight * Math.PI * 2));
					posX += this.cluster.actor.posX - this.cluster.startX;
					posY += this.cluster.actor.posY - this.cluster.startY;
				}
			}
			
			// Standard Flight Mechanics without Cluster
			else {
				posX = Circle.GetEdgePointByRadianX(this.startX, this.radius, (float)(weight * Math.PI * 2));
				posY = Circle.GetEdgePointByRadianY(this.startY, this.radius, (float)(weight * Math.PI * 2));
			}

			this.physics.velocity.X = FInt.Create(posX - this.actor.posX);
			this.physics.velocity.Y = FInt.Create(posY - this.actor.posY);
		}
	}
}
