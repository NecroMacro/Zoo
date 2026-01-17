using UnityEngine;

namespace Core.MpvUi
{
	public class BaseViewConfig : ScriptableObject
	{
		[SerializeField]
		private BaseView asset;
		public BaseView Asset => asset;
	}
}