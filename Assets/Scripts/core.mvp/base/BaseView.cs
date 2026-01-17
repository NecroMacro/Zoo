using System;
using UnityEngine;

namespace Core.MpvUi
{
	public interface IBaseView : IDisposable
	{
		public void Show();
		public void Close();
	}
	
	public abstract class BaseView : MonoBehaviour, IBaseView
	{
		public void Dispose()
		{
		}

		public virtual void Show()
		{
			gameObject.SetActive(true);
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
		}
	}
}