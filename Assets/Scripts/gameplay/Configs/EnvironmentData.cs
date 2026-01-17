using UnityEngine;

namespace ZooWorld.Gameplay
{
	public class EnvironmentData : MonoBehaviour
	{
		[SerializeField]
		private Camera environmentCamera;
		[SerializeField]
		private Collider groundCollider;

		public Camera EnvironmentCamera => environmentCamera;
		public Collider GroundCollider => groundCollider;
	}
}