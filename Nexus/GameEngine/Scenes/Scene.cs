﻿
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class Scene {

		public uint idCounter;           // Tracks the last ID that was generated in the scene.

		public readonly Systems systems;
		public readonly TimerGlobal timer;
		public readonly ScreenSys screen;
		public readonly GameMapper mapper;
		public Camera camera { get; protected set; }

		public Scene( Systems systems ) {

			// Systems
			this.systems = systems;
			this.timer = systems.timer;
			this.screen = systems.screen;
			this.mapper = systems.mapper;
		}

		public virtual int Width { get { return 0; } } // Placeholder for Camera
		public virtual int Height { get { return 0; } } // Placeholder for Camera

		public virtual void StartScene() { }
		public virtual void ResetScene() { }
		public virtual void EndScene() { }

		public virtual void RunTick() { throw new ArgumentNullException("Must implement RunTick() in Child Scene."); }
		public virtual void Draw() { throw new ArgumentNullException("Must implement Draw() in Child Scene."); }

		public uint nextId {
			get {
				this.idCounter++;
				return this.idCounter;
			}
		}
	}
}
