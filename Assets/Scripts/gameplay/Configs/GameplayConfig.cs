using Core;
using UnityEngine;
using ZooWorld.Gameplay.Configs;

namespace ZooWorld.Gameplay
{
	[CreateAssetMenu(
		fileName = nameof(GameplayConfig),
		menuName = "ZooWorld/" + nameof(GameplayConfig)
	)]
	public class GameplayConfig : ScriptableObject
	{
		public FloatRange animalSpawnInterval;
		public AnimalConfig[] animalConfigs;
	}
}