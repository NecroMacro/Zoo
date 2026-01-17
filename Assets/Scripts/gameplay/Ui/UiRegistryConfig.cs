using System.Collections.Generic;
using Core.MpvUi;
using UnityEngine;

namespace ZooWorld.Gameplay.Ui
{
	[CreateAssetMenu(
		fileName = nameof(HudViewConfig),
		menuName = "UiMVP/" + nameof(UiRegistryConfig), order = 0
	)]
	public class UiRegistryConfig : ScriptableObject
	{
		public List<BaseViewConfig> registry;
	}
}