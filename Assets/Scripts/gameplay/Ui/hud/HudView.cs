using Core.MpvUi;
using UnityEngine;
using UnityEngine.UI;

namespace ZooWorld.Gameplay.Ui
{
	public class HudView : BaseView
	{
		[SerializeField]
		private Text predatorDieCountText;
		
		[SerializeField]
		private Text preyDieCountText;

		public void SetPredatorDieCount(int value)
		{
			predatorDieCountText.text = value.ToString();
		}
		
		public void SetPreyDieCount(int value)
		{
			preyDieCountText.text = value.ToString();
		}
	}
}