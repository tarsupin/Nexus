﻿using Microsoft.Xna.Framework;
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
		public readonly LevelScene scene;
		public readonly float texLayer;         // 0.0f is bottom layer, 1.0f is top layer

		// Data
		public byte subType;
		public FVector pos;
		public Bounds bounds;

		// Rendering
		public string SpriteName { get; protected set; }

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
		}

		public ushort GridX { get { return (ushort) Math.Floor((double) ((this.pos.X.IntValue + this.bounds.Left) / (byte) TilemapEnum.TileWidth)); } }
		public ushort GridY { get { return (ushort) Math.Floor((double) ((this.pos.Y.IntValue + this.bounds.Top) / (byte) TilemapEnum.TileHeight)); } }

		public ushort GridX2 { get { return (ushort) Math.Floor((double) ((this.pos.X.IntValue + this.bounds.Right) / (byte) TilemapEnum.TileWidth)); } }
		public ushort GridY2 { get { return (ushort) Math.Floor((double) ((this.pos.Y.IntValue + this.bounds.Bottom) / (byte) TilemapEnum.TileHeight)); } }

		public virtual void SetSubType(byte subType) {
			//this.Texture = "BaseTexture/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}
		
		public virtual void Draw( int camX, int camY ) {
			this.Meta.Atlas.Draw(this.SpriteName, pos.X.IntValue - camX, pos.Y.IntValue - camY);
		}

		// Note: Apply -1 to RIGHT and BOTTOM. Otherwise inaccurate overlaps (e.g. pos 0 + bound 1 would cover 2 pixels).
		public void AssignBounds(byte top = 0, byte left = 0, byte right = 0, byte bottom = 0) {
			this.bounds = new Bounds(top, left, right, bottom);
		}

		// Note: Texture Packer correctly applies -1 to RIGHT and BOTTOM. Otherwise inaccurate overlaps (e.g. pos 0 + bound 1 would cover 2 pixels).
		public void AssignBoundsByAtlas(sbyte top = 0, sbyte left = 0, sbyte right = 0, sbyte bottom = 0) {
			AtlasBounds quickBounds = this.Meta.Atlas.GetBounds(this.SpriteName);
			this.bounds = new Bounds((byte)(quickBounds.Top + top), (byte)(quickBounds.Left + left), (byte)(quickBounds.Right + right), (byte)(quickBounds.Bottom + bottom));
		}
	}
}
