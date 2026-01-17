using R3;
using UnityEngine;
using ZooWorld.Gameplay.Configs;
using ZooWorld.Gameplay.Movement;

namespace ZooWorld.Gameplay
{
	public class Animal
	{
		public AnimalConfig Config { get; set; }
		public GameObject View { get; set; }
		private BaseMovement Movement { get; set; }

		private readonly Subject<Animal> dieSubject = new();
		public Observable<Animal> OnDie => dieSubject;
		
		public Animal(AnimalConfig animalConfig, GameObject view, BaseMovement movement)
		{
			Config = animalConfig;
			View = view;
			Movement = movement;
		}

		public void SetPosition(Vector3 position)
		{
			View.transform.position = position;
		}

		public void Die()
		{
			dieSubject.OnNext(this);
			View.gameObject.SetActive(false);
		}

		public void Reset()
		{
			Movement.Reset();
		}
	}
}