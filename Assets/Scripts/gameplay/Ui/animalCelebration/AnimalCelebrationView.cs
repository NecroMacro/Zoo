using Core.MpvUi;
using UnityEngine;

namespace ZooWorld.Gameplay.Ui
{
	public class AnimalCelebrationView : BaseView
	{
		[SerializeField]
		private RectTransform celebrationPrefab;

		private void Start()
		{
			celebrationPrefab.gameObject.SetActive(false);
		}

		public GameObject ShowCelebration(Vector2 position)
		{
			var celebration = Instantiate(celebrationPrefab, celebrationPrefab.parent);
			celebration.anchoredPosition = position;
			celebration.gameObject.SetActive(true);
			return celebration.gameObject;
		}
	}
}