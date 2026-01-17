using R3;

namespace ZooWorld.Gameplay
{
	public interface IBattleHitProvider
	{
		Observable<BattleHitInfo> OnHit { get; }
	}
}