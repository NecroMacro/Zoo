using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using R3;
using ZooWorld.Gameplay;
using ZooWorld.Gameplay.Collision;
using ZooWorld.Gameplay.Configs;
using ZooWorld.Gameplay.Factory;

[UsedImplicitly]
public class AnimalFactory : IDisposable
{
	private readonly MovementFactory movementFactory;
	private readonly AnimalCollisionFactory collisionFactory;
	private readonly CompositeDisposable disposables = new();

	private readonly HashSet<Animal> animals = new(100);
	private readonly HashSet<Animal> freePool = new(100);
	
	public AnimalFactory(
		MovementFactory movementFactory, 
		AnimalCollisionFactory collisionFactory)
	{
		this.movementFactory = movementFactory;
		this.collisionFactory = collisionFactory;
	}
	
	public Animal Spawn(AnimalConfig animalConfig)
	{
		var animalFromPool = TryGetGromPool(animalConfig);
		
		if (animalFromPool != null) return animalFromPool;
		
		var view = UnityEngine.Object.Instantiate(animalConfig.Prefab);
		var movement = movementFactory.Create(view, animalConfig.Movement);
		var animal = new Animal(animalConfig, view, movement);		
		collisionFactory.Create(animal);
		animals.Add(animal);

		animal.OnDie.Subscribe(OnAnimalDie).AddTo(disposables);
		
		return animal;
	}

	private Animal TryGetGromPool(AnimalConfig animalConfig)
	{
		var animalFromPool = freePool.FirstOrDefault(animal => animal.Config == animalConfig);
		
		if (animalFromPool == null) return null;
		
		animals.Add(animalFromPool);
		freePool.Remove(animalFromPool);
		
		animalFromPool.View.gameObject.SetActive(true);
		
		animalFromPool.Reset();
		
		return animalFromPool;
	}
	
	private void OnAnimalDie(Animal animal)
	{
		animals.Remove(animal);
		freePool.Add(animal);
	}

	public void Dispose()
	{
		disposables?.Dispose();
	}
}