using FlatRedBall.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyParty.Entities
{
	public partial class Player
	{
		const float UPPER_MAXIMUM_TURRET_ROTATION_POINT = 1.396263f; //(# ah yes python) This is RAD (and this is a stupid joke i made after needing 30minutes of debugiing to find out that the Z value is RAD not DEG)
		const float LOWER_MAXIMUM_TURRET_ROTATION_POINT = 0.01745329f;
		const float NORMAL_TURRET_SPEED				    = 0.2f;
		const float FASTER_TURRET_SPEED					= 0.4f;
		const float TURRET_ROTATION_DIFFERENCE			= 1.570796f;
		void Inputcode()
		{
			this.HorizontalInput = InputManager.Keyboard.Get1DInput(Keys.Left, Keys.Right).Or(InputManager.Keyboard.Get1DInput(Keys.A, Keys.D));
			this.VerticalInput   = InputManager.Keyboard.Get1DInput(Keys.Down, Keys.Up)   .Or(InputManager.Keyboard.Get1DInput(Keys.S, Keys.W));
			this.JumpInput		 = InputManager.Keyboard.GetKey(Keys.None);
		}
		void TurretMoving()
		{
			///<summary>
			///This function serves the purpose of letting the player move the turret of the entity
			///</summary>
			if ((VerticalInput.Value > 0 && InputManager.Keyboard.KeyDown(Keys.LeftControl)) && (this.Turret.RelativeRotationZ < UPPER_MAXIMUM_TURRET_ROTATION_POINT)) Turret.RelativeRotationZVelocity = FASTER_TURRET_SPEED;
			else if (VerticalInput.Value > 0 && this.Turret.RelativeRotationZ < UPPER_MAXIMUM_TURRET_ROTATION_POINT) Turret.RelativeRotationZVelocity = NORMAL_TURRET_SPEED;
			else if ((VerticalInput.Value < 0 && InputManager.Keyboard.KeyDown(Keys.LeftControl)) && this.Turret.RelativeRotationZ > LOWER_MAXIMUM_TURRET_ROTATION_POINT) Turret.RelativeRotationZVelocity = -FASTER_TURRET_SPEED;
			else if (VerticalInput.Value < 0 && this.Turret.RelativeRotationZ > LOWER_MAXIMUM_TURRET_ROTATION_POINT) Turret.RelativeRotationZVelocity = -NORMAL_TURRET_SPEED;
			else Turret.RelativeRotationZVelocity = 0;
		}
		void Shooting()
		{
			if (InputManager.Keyboard.KeyPushed(Keys.Space))
			{
				var position = Turret.AbsolutePointPosition(2) + new Vector3(1, 0, 0);
				var bullet = Factories.Armor_Piercing_roundFactory.CreateNew(position);
				bullet.__init__(Turret.RelativeRotationZ - TURRET_ROTATION_DIFFERENCE, position);
				Mortar_Blast_Sound.Play(1, 0, 0);
			}
		}
	}
}