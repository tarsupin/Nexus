﻿
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class GameObject {

		// References
		// TODO: Pool (remove from game objects, put on projectiles exclusively)

		// Metadata
		public readonly uint id;
		public IMetaData Meta { get; protected set; }
		public readonly Scene scene;
		public readonly float texLayer;         // 0.0f is bottom layer, 1.0f is top layer

		// Data
		public byte subType;
		public FVector pos;
		public Bounds bounds;

		// Rendering
		public string Texture { get; protected set; }

		// Facing
		// TODO: RotationFacing, RotationSpeed, RotationRadian

		// Object Properties
		//protected byte _state;           // State of object. 0 if state never changed. All state changes must go through setState(), for Multiplayer integrity.
		//protected byte[] _flags;         // Any flags assigned to the object. Only use if state cannot contain all status for an object.

		// Object Physics
		// TODO: Collision, Physics, Sector (Tile Position; this exists in static, not needed here? might be.)

		public GameObject(LevelScene scene, byte subType, FVector pos, object[] paramList = null) {
			this.id = scene.nextId;
			this.scene = scene;
			this.subType = subType;
			this.pos = pos;

			// TODO HIGH PRIORITY: Assign Bounds
			this.bounds = new Bounds(0, 0, 48, 48);
		}

		public ushort GridX { get { return (ushort) Math.Floor((double) (this.pos.X.IntValue / 48)); } }
		public ushort GridY { get { return (ushort) Math.Floor((double) (this.pos.Y.IntValue / 48)); } }

		public virtual void SetSubType(byte subType) {
			//this.Texture = "BaseTexture/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}
		
		public virtual void Draw( int camX, int camY ) {
			this.Meta.Atlas.Draw(this.Texture, pos.X.IntValue - camX, pos.Y.IntValue - camY);
		}
	}
}
