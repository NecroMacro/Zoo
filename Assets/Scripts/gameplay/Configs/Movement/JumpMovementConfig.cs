using UnityEngine;

namespace ZooWorld.Gameplay.Configs
{
	[CreateAssetMenu(
		fileName = nameof(JumpMovementConfig),
		menuName = "ZooWorld/Movement/" + nameof(JumpMovementConfig)
	)]	
	public class JumpMovementConfig : MovementConfig
	{
		[SerializeField]
		private float distance;

		[SerializeField]
		private float maxHeight;
		
		[SerializeField]
		private float interval;
		
		public float Distance => distance;
		public float Interval => interval;
		public float MaxHeight => maxHeight;
	}
}
