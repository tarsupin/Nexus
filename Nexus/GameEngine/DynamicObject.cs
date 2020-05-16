using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public enum Activity : byte {
		Inactive = 0,               // The object is inactive, not visible, etc. No reason to run updates.
		NoCollide = 1,				// The object requires updates, but don't run collision.
		Active = 2,                 // The object is currently active; usually visible to a player (multiplayer-ready). Needs to run updates.
		ForceActive = 3,            // The object is forced to be active through the whole level. Always run updates.
	}

	public enum CommonState : byte {
		Undefined,                  // No current state.
		Move,                       // Standard movement for the actor.
		Wait,                       // Waiting motion; often occurs after a reaction (e.g. brief pause while charging up).
		MotionStart,                // An active motion caused by a creature's decision to act (e.g. jumping), even if that decision is based on a cycle (such as hopping).
		Motion,                     // Indicates sustained movement, such as jumping.
		MotionEnd,                  // Signals ending a motion, such as landing after a jump.
		SpecialStart,               // The start of a special movement or action.
		Special,                    // Special movement, decision, or action.
		SpecialEnd,                 // The end of a special action.
		SpecialWait,                // A wait or stall that is associated with the special action.
		OtherStart,                 // The start of a secondary special action.
		Other,                      // Secondary movement, decision, or action.
		OtherEnd,                   // The end of a secondary action.
		OtherWait,                  // A wait or stall that is associated with the secondary action.
		Death,                      // In the death state; the process of dying.
	}

	public class DynamicObject {

		public Activity Activity { get; protected set; }

		// TODO: -- last character touch direction; what relelvant for?
		// TODO: TrackInstructions (rules for dealing with tracks; not everything needs this, but... ???)

		// Metadata
		public readonly uint id;
		public IMetaData Meta { get; protected set; }
		public readonly RoomScene room;
		public readonly float texLayer;         // 0.0f is bottom layer, 1.0f is top layer

		// Data
		public byte subType;
		public int posX;
		public int posY;
		public Bounds bounds;

		// Rendering
		public string SpriteName { get; protected set; }

		// Components
		public Physics physics;
		public Animate animate;

		// State Changes (also see SetState() and OnStateChange() methods)
		public byte State { get; protected set; }			// Tracks the object's current state.
		public bool FaceRight { get; protected set; }		// TRUE if the actor is facing right.

		public DynamicObject(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList = null) {
			
			this.id = room.nextId;
			this.room = room;
			this.subType = subType;
			this.posX = pos.X.IntValue;
			this.posY = pos.Y.IntValue;

			this.SetActivity(Activity.Active);
		}

		public virtual void RunTick() {

			// Standard Physics
			this.physics.RunTick();
			
			// Animations, if applicable.
			if(this.animate is Animate) {
				this.animate.RunAnimationTick(Systems.timer);
			}
		}

		// Run this method to change an actor's state.
		public void SetState(byte state) {
			if(this.State != state) {
				this.State = state;
				this.OnStateChange();
			}
		}

		// Run this method to change an actor's facing direction.
		public void SetActivity( Activity activity ) {
			if(this.Activity != Activity.ForceActive) {
				this.Activity = activity;
			}
		}
		
		// Run this method to change an actor's facing direction.
		public void SetDirection( bool faceRight ) {
			if(this.FaceRight != faceRight) {
				this.FaceRight = faceRight;
				this.OnDirectionChange();
			}
		}

		// Run this method when the actor's direction has changed.
		public virtual void OnDirectionChange() {}

		// Run this method when the actor's .state has changed. This may affect sprites, animations, or other custom behaviors.
		public virtual void OnStateChange() {}

		public ushort GridX { get { return (ushort)Math.Floor((double)((this.posX + this.bounds.Left) / (byte)TilemapEnum.TileWidth)); } }
		public ushort GridY { get { return (ushort)Math.Floor((double)((this.posY + this.bounds.Top) / (byte)TilemapEnum.TileHeight)); } }

		public ushort GridX2 { get { return (ushort)Math.Floor((double)((this.posX + this.bounds.Right) / (byte)TilemapEnum.TileWidth)); } }
		public ushort GridY2 { get { return (ushort)Math.Floor((double)((this.posY + this.bounds.Bottom) / (byte)TilemapEnum.TileHeight)); } }

		public virtual void SetSpriteName(string spriteName) {
			this.SpriteName = spriteName;
		}

		public void SetSpriteName(string spriteName, bool isAnimation = false) {
			this.SpriteName = spriteName;
			if(!isAnimation && this.animate is Animate) { this.animate.DisableAnimation(); }
		}

		public virtual void SetnSubType(byte subType) {
			//this.Texture = "BaseTexture/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}

		public virtual void Draw(int camX, int camY) {
			this.Meta.Atlas.Draw(this.SpriteName, posX - camX, posY - camY);
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

		// Run Standard Impact
		// TODO HIGH PRIORITY: IMPLEMENT IMPACTS LIKE THIS
		// TODO HIGH PRIORITY: IMPLEMENT IMPACTS LIKE THIS
		// TODO HIGH PRIORITY: IMPLEMENT IMPACTS LIKE THIS
		// TODO HIGH PRIORITY: IMPLEMENT IMPACTS LIKE THIS
		public virtual bool RunImpact(DynamicObject actor, DirCardinal dir) {
			return true;
		}

		// Destroys the instance of this object.
		public virtual void Destroy() {
			this.room.RemoveFromScene(this);
		}

		// TODO: Does this get used? Character.BounceUp() does.
		// As of 11/3/2019, I don't think so.
		public virtual void BounceUp(DynamicObject obj, sbyte strengthMod = 4, byte maxX = 4, sbyte relativeMult = 3) {

			// Some dynamic archetypes shouldn't bounce.
			if(this.Meta.Archetype == Arch.Platform) { return; }

			this.physics.velocity.Y = FInt.Create(-strengthMod);
			short xDiff = CollideDetect.GetRelativeX(this, obj);

			if(xDiff < 0) {
				this.physics.velocity.X = FInt.Create(Math.Min(maxX, Math.Abs(xDiff / relativeMult)));
			}
			
			else {
				this.physics.velocity.X = FInt.Create(-Math.Min(maxX, xDiff / relativeMult));
			}
		}
	}
}
