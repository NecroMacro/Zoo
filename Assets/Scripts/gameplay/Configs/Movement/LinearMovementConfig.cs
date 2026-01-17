using UnityEngine;

namespace ZooWorld.Gameplay.Configs
{
	[CreateAssetMenu(
		fileName = nameof(LinearMovementConfig),
		menuName = "ZooWorld/Movement/" + nameof(LinearMovementConfig)
	)]
	public class LinearMovementConfig : MovementConfig
	{
		[SerializeField]
		private float speed;
		[SerializeField]
		private float rotationSpeed;
		[SerializeField]
		private float changeDirectionTime;
		
		public float Speed => speed;
		public float RotationSpeed => rotationSpeed;
		public float ChangeDirectionTime => changeDirectionTime;
	}
}	