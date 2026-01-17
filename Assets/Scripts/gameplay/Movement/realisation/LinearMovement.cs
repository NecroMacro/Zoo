using UnityEngine;
using ZooWorld.Gameplay.Configs;

namespace ZooWorld.Gameplay.Movement
{
	public class LinearMovement : BaseMovement<LinearMovementConfig>
	{
		private float nextTimeChangeDirection;
		protected override void OnStart()
		{
			SetNewRandomDirection();
		}

		protected override void OnRestart()
		{
			nextTimeChangeDirection = 0;
		}

		public override void Move()
		{
			UpdateDirection();
			RotateToDirection();
			MoveForward();
		}

		private void UpdateDirection()
		{
			if (IsOutOfBounds)
			{
				TargetDirection = GetReturnDirection();
				nextTimeChangeDirection = Time.time + Config.ChangeDirectionTime;
				return;
			}

			if (nextTimeChangeDirection > Time.time) return;

			SetNewRandomDirection();
			nextTimeChangeDirection = Time.time + Config.ChangeDirectionTime;
		}

		private void RotateToDirection()
		{
			if (TargetDirection == Vector3.zero)
				return;

			Quaternion targetRotation = Quaternion.LookRotation(TargetDirection, Vector3.up);

			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				targetRotation,
				Config.RotationSpeed * Time.deltaTime
			);
		}
		
		private void MoveForward()
		{
			RigidBody.linearVelocity = transform.forward * Config.Speed;
		}
	}
}