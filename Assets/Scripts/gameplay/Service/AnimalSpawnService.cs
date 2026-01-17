using System;
using System.Threading;
using JetBrains.Annotations;
using VContainer.Unity;
using ZooWorld.Gameplay;
using Cysharp.Threading.Tasks;
using ZooWorld.Gameplay.Configs;

[UsedImplicitly]
public class AnimalSpawnService : IStartable, IDisposable
{
	private readonly GameplayConfig gameplayConfig;
	private readonly AnimalFactory animalFactory;
	private readonly ScreenService screenService;

	private CancellationTokenSource spawnLoopCancellationToken;

	public AnimalSpawnService(
		GameplayConfig gameplayConfig,
		AnimalFactory animalFactory,
		ScreenService screenService
	)
	{
		this.gameplayConfig = gameplayConfig;
		this.animalFactory = animalFactory;
		this.screenService = screenService;
	}

	public void Start()
	{
		spawnLoopCancellationToken = new CancellationTokenSource();
		Tick(spawnLoopCancellationToken.Token).Forget();
	}

	private async UniTask Tick(CancellationToken token)
	{
		while (!token.IsCancellationRequested)
		{
			foreach (var animalConfig in gameplayConfig.animalConfigs)
			{
				SpawnAnimal(animalConfig);

				float waitTime = gameplayConfig.animalSpawnInterval.RandomValue;
				await UniTask.WaitForSeconds(waitTime, cancellationToken: token);
			}
		}
	}

	private void SpawnAnimal(AnimalConfig animalConfig)
	{
		var animal = animalFactory.Spawn(animalConfig);
		animal.SetPosition(screenService.GetRandomGroundPoint());
	}

	public void Dispose()
	{
		if (spawnLoopCancellationToken == null)
			return;

		spawnLoopCancellationToken.Cancel();
		spawnLoopCancellationToken.Dispose();
		spawnLoopCancellationToken = null;
	}
}