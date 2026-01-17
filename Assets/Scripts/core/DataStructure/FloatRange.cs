using UnityEngine;

namespace Core
{
	[System.Serializable]
	public struct FloatRange
	{
		public float Min;
		public float Max;

		public float RandomValue => Random.Range(Min, Max);

		public float Clamp(float value) => Mathf.Clamp(value, Min, Max);
	}
}