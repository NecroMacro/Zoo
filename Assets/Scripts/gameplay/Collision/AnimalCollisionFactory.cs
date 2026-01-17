using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using R3;

namespace ZooWorld.Gameplay.Collision
{
	[UsedImplicitly]
	public class AnimalCollisionFactory : IBattleHitProvider, IDisposable
	{
		private readonly Dictionary<AnimalPhysicsCollisionTracker, Animal> animalCollisionMap = new();
		private readonly CompositeDisposable disposables = new();
		
		private readonly Subject<BattleHitInfo> onHit = new();
		private readonly BattleHitInfo hitInfo = new();
		public Observable<BattleHitInfo> OnHit => onHit;

		public AnimalPhysicsCollisionTracker Create(Animal target)
		{
			var collisionTracker = target.View.AddComponent<AnimalPhysicsCollisionTracker>();
			animalCollisionMap.TryAdd(collisionTracker, target);
			
			collisionTracker.OnCollision.Subscribe(OnCollision).AddTo(disposables);
			
			return collisionTracker;
		}

		private void OnCollision((AnimalPhysicsCollisionTracker source, AnimalPhysicsCollisionTracker target) data)
		{
			var sourceAnimal = animalCollisionMap[data.source];
			var targetAnimal = animalCollisionMap[data.target];
			
			hitInfo.UpdateData(sourceAnimal, targetAnimal);
			
			onHit.OnNext(hitInfo);
		}

		public void Dispose()
		{
			disposables?.Dispose();
		}
	}
}