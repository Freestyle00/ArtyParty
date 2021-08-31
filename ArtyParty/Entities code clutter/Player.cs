using FlatRedBall.Input;
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
		void Inputcode()
		{
			this.HorizontalInput = InputManager.Keyboard.Get1DInput(Keys.Left, Keys.Right).Or(InputManager.Keyboard.Get1DInput(Keys.A, Keys.D));
			this.VerticalInput   = InputManager.Keyboard.Get1DInput(Keys.Down, Keys.Up)   .Or(InputManager.Keyboard.Get1DInput(Keys.S, Keys.W));
		}
		void TurretMoving()
		{
			///<summary>
			///This function serves the purpose of letting the player move the turret of the entity
			///</summary>
			if (VerticalInput.Value > 0 && InputManager.Keyboard.KeyDown(Keys.LeftControl)) Turret.RelativeRotationZVelocity = 0.4f;
			else if (VerticalInput.Value > 0) Turret.RelativeRotationZVelocity = 0.2f;
			else if (VerticalInput.Value < 0 && InputManager.Keyboard.KeyDown(Keys.LeftControl)) Turret.RelativeRotationZVelocity = -0.4f;
			else if (VerticalInput.Value < 0) Turret.RelativeRotationZVelocity = -0.2f;
			else Turret.RelativeRotationZVelocity = 0;
		}
	}
}
