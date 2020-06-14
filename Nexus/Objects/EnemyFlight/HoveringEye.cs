using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.Objects {

	public enum HoveringEyeSubType : byte { Eye }

	public class HoveringEye : EnemyFlight {

		public HoveringEye(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.HoveringEye].meta;

			// Physics, Collisions, etc.
			this.physics = new Physics(this);

			// Assign Flight Behavior
			this.behavior = FlightBehavior.AssignFlightMotion(this, paramList);

			this.AssignSubType(subType);
			this.AssignBoundsByAtlas(6, 6, -6, -6);
		}
		
		
	//attackBolt( time: Timer ): boolean {
	//	let att = this.att;
	//	if(!att.count || !this.attack( time )) { return false; }
		
	//	const charPos = this.scene.character.pos;
	//	const attRadian = Radians.getRadiansBetween( this.pos.x + 16, this.pos.y + 16, charPos.x + 8, charPos.y + 10 );
		
	//	if(att.count !== 2) {
	//		this.shootBolt( attRadian );
	//	}
		
	//	// Spread Attack (if applicable)
	//	if(att.count > 1) {
	//		this.shootBolt( attRadian + att.spread );
	//		this.shootBolt( attRadian - att.spread );
	//	}
		
	//	this.scene.soundList.bolt.addToSoundPool(); // Track Sound
	//	return true;
	//}

	//shootBolt( rotation: number ) {
	//	const r = Radians.normalize( rotation );
	//	const attX = Radians.getXFromRotation( r, this.att.speed );
	//	const attY = Radians.getYFromRotation( r, this.att.speed );
		
	//	let projectile = Projectile.fire( this.scene, ProjectileBall, "Electric", this.pos.x + 16, this.pos.y + 12, attX, attY, "EnemyElectric" );
	//	projectile.render = projectile.renderBallRotation;
	//	projectile.rotRadian = Radians.getRadiansBetween( 0, 0, projectile.physics.velocity.x, projectile.physics.velocity.y );
	//}

		private void AssignSubType( byte subType ) {
			this.SpriteName = "Eye/Eye";
		}

		public override bool RunCharacterImpact(Character character) {
			character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			return true;
		}
	}
}
