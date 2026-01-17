using System;
using JetBrains.Annotations;
using R3;
using VContainer.Unity;
// ReSharper disable MemberCanBeMadeStatic.Local

namespace ZooWorld.Gameplay
{
	[UsedImplicitly]
	public class BattleSystem : IBattleDieInfoProvider, IStartable, IDisposable
	{
		private readonly IBattleHitProvider hitProvider;

		private readonly CompositeDisposable disposables = new();

		private readonly ReactiveProperty<int> predatorDieCount = new();
		private readonly ReactiveProperty<int> preyDieCount = new();
		private readonly Subject<Animal> onKillPerformed = new();
		public ReadOnlyReactiveProperty<int> PredatorDieCount => predatorDieCount;
		public ReadOnlyReactiveProperty<int> PreyDieCount => preyDieCount;
		public Observable<Animal> OnKillPerformed => onKillPerformed;


		public BattleSystem(IBattleHitProvider hitProvider)
		{
			this.hitProvider = hitProvider;
		}
		
		public void Start()
		{
			hitProvider.OnHit.Subscribe(HandleHit).AddTo(disposables);
		}
		
		private void HandleHit(BattleHitInfo hitInfo)
		{
			var source = hitInfo.Source;
			var target = hitInfo.Target;
			
			var sourceKind = source.Config.Kind;
			var targetKind = target.Config.Kind;
			
			if (sourceKind == AnimalKind.Prey && targetKind == AnimalKind.Prey)
				return;

			Animal predator;
			Animal prey;

			if (sourceKind == AnimalKind.Predator && targetKind == AnimalKind.Predator)
			{
				predator = source;
				prey = target;
			}
			else
			{
				predator = sourceKind == AnimalKind.Predator ? source : target;
				prey = sourceKind == AnimalKind.Predator ? target : source;
			}

			prey.Die();
			
			onKillPerformed.OnNext(predator);
			
			IncreaseDieCount(prey);
		}

		private void IncreaseDieCount(Animal animal)
		{
			switch (animal.Config.Kind)
			{
				case AnimalKind.Prey:
					preyDieCount.OnNext(preyDieCount.CurrentValue + 1);
					break;
				case AnimalKind.Predator:
					predatorDieCount.OnNext(predatorDieCount.CurrentValue + 1);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void Dispose()
		{
			disposables?.Dispose();
		}
	}
}