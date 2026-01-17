using Core.MpvUi;
using JetBrains.Annotations;
using R3;

namespace ZooWorld.Gameplay.Ui
{
	[UsedImplicitly]
	public class HudPresenter : BasePresenter<HudModel, HudView, HudViewConfig>
	{
		protected override void OnInitialize()
		{
			Model.Data.PredatorDieCount
			     .Subscribe(View.SetPredatorDieCount)
			     .AddTo(Disposables);
			
			Model.Data.PreyDieCount
			     .Subscribe(View.SetPreyDieCount)
			     .AddTo(Disposables);
		}
	}
}