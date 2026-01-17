using System;
using Core.MpvUi;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace ZooWorld.Gameplay.Ui
{
	[UsedImplicitly]
	public class AnimalCelebrationPresenter : BasePresenter<AnimalCelebrationModel, AnimalCelebrationView,
		AnimalCelebrationViewConfig>
	{
		private RectTransform canvasRect;

		protected override void OnInitialize()
		{
			Model.Data.OnKillPerformed
			     .Subscribe(OnKillPerformed)
			     .AddTo(Disposables);

			canvasRect = Model.MainCanvas.GetComponent<RectTransform>();
		}

		private void OnKillPerformed(Animal killer)
		{
			var worldPosition = killer.View.transform.position;

			Vector2 screenPoint = Model.ScreenService.CurrentCamera.WorldToScreenPoint(worldPosition);

			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				canvasRect,
				screenPoint,
				null,
				out Vector2 anchoredPosition
			);

			var celebration = View.ShowCelebration(anchoredPosition);

			DestroyCelebration(celebration).Forget();
		}

		private async UniTask DestroyCelebration(GameObject celebration)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(2));
			UnityEngine.Object.Destroy(celebration);
		}
	}
}