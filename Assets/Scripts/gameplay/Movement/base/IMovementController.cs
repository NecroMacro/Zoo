using UnityEngine;

namespace ZooWorld.Gameplay.Movement
{
	public interface IMovementController
	{
		bool IsNeedChangeDirection(Transform target);
		Vector3 GetReturnDirection(Transform target);
	}
}