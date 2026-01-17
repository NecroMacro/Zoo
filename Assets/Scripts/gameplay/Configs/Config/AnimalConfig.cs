using UnityEngine;

namespace ZooWorld.Gameplay.Configs
{
	[CreateAssetMenu(
		fileName = nameof(AnimalConfig),
		menuName = "ZooWorld/Animal/" + nameof(AnimalConfig)
	)]
	public class AnimalConfig : ScriptableObject
	{
		[SerializeField]
		private string animalName;
		[SerializeField]
		private AnimalKind kind;
		[SerializeField]
		private MovementConfig movement;
		[SerializeField]
		private GameObject prefab;
		
		public string AnimalName => animalName;
		public AnimalKind Kind => kind;
		public MovementConfig Movement => movement;
		public GameObject Prefab => prefab;
	}
}