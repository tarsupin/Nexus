using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public enum CollideEnum : byte {
		Inactive = 0,               // The object is inactive, not visible, etc. No reason to run updates.
		NoCollide = 1,				// The object requires updates, but don't run collision.
		NoTileCollide = 2,			// The object requires updates, but doesn't collide with tiles.
		Standard = 4,				// The object is forced to coollide.
	}

	public enum CommonState : byte {
		Undefined,			// No current state.
		Move,				// Standard movement for the actor.
		Wait,				// Waiting motion; often occurs after a reaction (e.g. brief pause while charging up).
		MotionStart,		// An active motion caused by a creature's decision to act (e.g. jumping), even if that decision is based on a cycle (such as hopping).
		Motion,				// Indicates sustained movement, such as jumping.
		MotionEnd,			// Signals ending a motion, such as landing after a jump.
		Special,			// Special movement, decision, or action.
		SpecialWait,		// A wait or stall that is associated with the special action.
		Death,				// In the death state; the process of dying.
	}

	public class GameObject {

		public CollideEnum CollideVal { get; protected set; }

		// Metadata
		public int id { get; protected set; }
		public IMetaData Meta { get; protected set; }
		public RoomScene room;

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

		public GameObject(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList = null) {
			
			this.room = room;
			this.id = room == null ? 0 : room.nextId;
			this.subType = subType;
			this.posX = pos.X.RoundInt;
			this.posY = pos.Y.RoundInt;

			this.SetCollide(CollideEnum.Standard);
		}

		public virtual void RunTick() {

			// Standard Physics
			this.physics.RunPhysicsTick();
			
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

		// Run this method to change an actor's current collision type.
		public void SetCollide( CollideEnum collideVal ) {
			this.CollideVal = collideVal;
		}
		
		// Run this method to change an actor's facing direction.
		public void SetDirection( bool faceRight ) {
			this.FaceRight = faceRight;
			this.OnDirectionChange();
		}

		// Run this method when the actor's direction has changed.
		public virtual void OnDirectionChange() {}

		// Run this method when the actor's .state has changed. This may affect sprites, animations, or other custom behaviors.
		public virtual void OnStateChange() {}

		public short GridX { get { return (short)Math.Floor((double)((this.posX + this.bounds.Left) / (byte)TilemapEnum.TileWidth)); } }
		public short GridY { get { return (short)Math.Floor((double)((this.posY + this.bounds.Top) / (byte)TilemapEnum.TileHeight)); } }

		public short GridX2 { get { return (short)Math.Floor((double)((this.posX + this.bounds.Right -1) / (byte)TilemapEnum.TileWidth)); } }
		public short GridY2 { get { return (short)Math.Floor((double)((this.posY + this.bounds.Bottom -1) / (byte)TilemapEnum.TileHeight)); } }

		public void SetSpriteName(string spriteName) {
			this.SpriteName = spriteName;
			if(this.animate is Animate) { this.animate.DisableAnimation(); }
		}

		public void SetAnimationSprite(string spriteName) {
			this.SpriteName = spriteName;
		}

		public virtual void SetSubType(byte subType) {
			//this.Texture = "BaseTexture/" + System.Enum.GetName(typeof(GroundSubTypes), subType);
		}

		// Tile Collisions
		public virtual void CollidePosUp(int posY) {
			this.physics.touch.TouchUp();
			this.physics.StopY();
			this.physics.MoveToPosY(posY);
		}

		public virtual void CollidePosDown(int posY) {
			this.physics.touch.TouchDown();
			this.physics.StopY();
			this.physics.MoveToPosY(posY);
		}

		public virtual void CollidePosLeft(int posX) {
			this.physics.touch.TouchLeft();
			this.physics.StopX();
			this.physics.MoveToPosX(posX);
		}

		public virtual void CollidePosRight(int posX) {
			this.physics.touch.TouchRight();
			this.physics.StopX();
			this.physics.MoveToPosX(posX);
		}

		// Dynamic Object Collisions
		public virtual bool CollideObjUp(GameObject obj) {

			// Verify the object is moving Up. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.intend.Y >= 0) { return false; }

			this.physics.touch.TouchUp();
			this.physics.AlignDown(obj);
			this.physics.StopY();

			return true;
		}

		public virtual bool CollideObjDown(GameObject obj) {

			// Verify the object is moving Down. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.intend.Y <= 0) { return false; }

			this.physics.touch.TouchDown();
			this.physics.AlignUp(obj);
			this.physics.StopY();

			return true;
		}

		public virtual bool CollideObjLeft(GameObject obj) {

			// Verify the object is moving Left. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.intend.X >= 0) { return false; }

			this.physics.touch.TouchLeft();
			this.physics.AlignRight(obj);
			this.physics.StopX();

			return true;
		}

		public virtual bool CollideObjRight(GameObject obj) {

			// Verify the object is moving Right. If not, don't collide.
			// This prevents certain false collisions, e.g. if both objects are moving in the same direction.
			if(this.physics.intend.X <= 0) { return false; }

			this.physics.touch.TouchRight();
			this.physics.AlignLeft(obj);
			this.physics.StopX();

			return true;
		}

		// Render Object
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

		// Destroys the instance of this object.
		public virtual void Destroy() {
			this.room.RemoveFromScene(this);
		}

		public virtual void BounceUp(int midX, sbyte strengthMod = 4, byte maxX = 2, sbyte relativeMult = 3) {

			this.physics.velocity.Y = FInt.Create(-strengthMod);
			
			if(maxX > 0) {
				short xDiff = CollideRect.GetRelativeX(this, midX);
				FInt xAdjust = FInt.Create(Math.Min(maxX, Math.Abs(xDiff / relativeMult)));

				if(xDiff > 0) {
					this.physics.velocity.X += xAdjust;
				} else {
					this.physics.velocity.X -= xAdjust;
				}
			}
		}
	}
}
