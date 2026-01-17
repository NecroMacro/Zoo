using System;
using VContainer;

namespace Core.MpvUi
{
    public interface IBaseModel : IDisposable { }

	public abstract class BaseModel<TData> : IBaseModel 
	{
		[Inject] public readonly TData Data;
		public void Dispose() { }
	}
}