using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyParty.Entities.Projectiles
{ 
	public partial class Armor_Piercing_round
	{
		const float MAXIMUM_SPEED = 350;
		const float GRAVITY = 200;
		const float TURRET_ROTATION_DIFFERENCE = 1.570796f;
		public void __init__(float Rotation, Vector3 Position)
		{
			/// <summary>
			/// This function assigns the flight path and the correct display angle 
			/// of the bullet
			/// </summary>
			this.RotationZ = Rotation;
			this.YAcceleration = -GRAVITY;
			Rotation += TURRET_ROTATION_DIFFERENCE; //As the turret rotation difference had to be subtracted so that the bullet will show correctly I have to readd it so that the math is correct (and im 100% sure this is gonna be a clusterfuck later)
			this.Position = Position;
			this.Velocity.X = (float)Math.Cos(Rotation) * MAXIMUM_SPEED;
			this.Velocity.Y = (float)Math.Sin(Rotation) * MAXIMUM_SPEED;
		}
		void Bullet_Rotation()
		{
			this.RotationZ = Velocity.Angle() - TURRET_ROTATION_DIFFERENCE ?? this.RotationZ;
		}
	}
}
