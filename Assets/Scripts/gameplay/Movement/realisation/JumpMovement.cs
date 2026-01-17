using UnityEngine;
using ZooWorld.Gameplay.Configs;

namespace ZooWorld.Gameplay.Movement
{
	public class JumpMovement : BaseMovement<JumpMovementConfig>
	{
		private float nextJumpTime;
		
		protected override void OnStart()
		{
		}

		protected override void OnRestart()
		{
			nextJumpTime = 0;
		}

		public override void Move()
		{
			if (nextJumpTime > Time.time) return;

			if (IsOutOfBounds)
			{
				TargetDirection = GetReturnDirection();
			} else
			{
				SetNewRandomDirection();
			}
			
			LookAtDirection(TargetDirection);
			
			Jump(TargetDirection, Config.Distance, Config.MaxHeight);
			
			nextJumpTime = Time.time + Config.Interval;
		}
		
		private void LookAtDirection(Vector3 direction)
		{
			Vector3 flatDir = new Vector3(direction.x, 0f, direction.z);
			if (flatDir.sqrMagnitude < 0.0001f)
				return;

			Quaternion targetRot = Quaternion.LookRotation(flatDir, Vector3.up);
			transform.rotation = targetRot;
		}
		
		private void Jump(Vector3 direction, float distance, float height)
		{
			if (direction == Vector3.zero) direction = Vector3.forward;

			Vector3 horizontalDir = new Vector3(direction.x, 0f, direction.z).normalized;

			float g = Mathf.Abs(Physics.gravity.y);

			float v0y = Mathf.Sqrt(2f * g * height);

			float t_total = 2f * v0y / g;

			Vector3 v_horizontal = horizontalDir * (distance / t_total);

			Vector3 jumpVelocity = v_horizontal + Vector3.up * v0y;

			RigidBody.linearVelocity = jumpVelocity;
		}
	}
}