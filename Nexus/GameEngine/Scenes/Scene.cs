﻿
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class Scene {

		public uint idCounter;           // Tracks the last ID that was generated in the scene.

		public readonly Systems systems;
		public readonly TimerGlobal time;
		public readonly Camera camera;
		public readonly ScreenSys screen;
		public readonly GameMapper mapper;

		public Scene( Systems systems ) {

			// Systems
			this.systems = systems;
			this.time = systems.timer;
			this.screen = systems.screen;
			this.camera = new Camera(this);
			this.mapper = systems.mapper;
		}

		public virtual void RunTick() { throw new ArgumentNullException("Must implement RunTick() in Child Scene."); }
		public virtual void Draw() { throw new ArgumentNullException("Must implement Draw() in Child Scene."); }
		public virtual void EndScene() { }

		public uint nextId {
			get {
				this.idCounter++;
				return this.idCounter;
			}
		}
	}
}
