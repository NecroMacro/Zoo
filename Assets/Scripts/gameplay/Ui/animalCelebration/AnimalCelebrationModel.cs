using Core.MpvUi;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace ZooWorld.Gameplay.Ui
{
	[UsedImplicitly]
	public class AnimalCelebrationModel : BaseModel<IBattleDieInfoProvider>
	{
		[Inject]
		public Canvas MainCanvas;
		[Inject]
		public ScreenService ScreenService;
	}
}