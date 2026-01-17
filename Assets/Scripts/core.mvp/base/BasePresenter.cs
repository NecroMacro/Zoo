using System;
using R3;

namespace Core.MpvUi
{
	public interface IBasePresenter : IDisposable
	{
		void Initialize(IBaseModel model, BaseView view, BaseViewConfig config);
	}

	public abstract class BasePresenter<TModel, TView, TConfig> : IBasePresenter
		where TModel : IBaseModel
		where TView : BaseView
		where TConfig : BaseViewConfig
	{
		protected TModel Model;
		protected TView View;
		protected TConfig Config;

		protected readonly CompositeDisposable Disposables = new();
		
		public void Initialize(IBaseModel model, BaseView view, BaseViewConfig config)
		{
			Model = (TModel)model;
			View = view as TView;
			Config = (TConfig)config;
			
			if (View == null)
				throw new Exception($"Wrong view type. Expected {typeof(TView)}");

			OnInitialize();
		}

		protected virtual void OnInitialize() { }

		public void Dispose()
		{
			Disposables.Dispose();
		}
	}
}