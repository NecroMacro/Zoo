using R3;

namespace ZooWorld.Gameplay
{
	public interface IBattleDieInfoProvider
	{
		ReadOnlyReactiveProperty<int> PredatorDieCount { get; }
		ReadOnlyReactiveProperty<int> PreyDieCount { get; }
		Observable<Animal> OnKillPerformed { get; }
	}
}