using JetBrains.Annotations;
using VContainer.Unity;
using ZooWorld.Gameplay.Factory;
using ZooWorld.Gameplay.Ui;

namespace ZooWorld.Gameplay
{
	[UsedImplicitly]
	public class GameplayEntryPoint : IStartable
	{
		private readonly MovementFactory movementFactory;
		private readonly AnimalSpawnService animalSpawnService;
		private readonly BattleSystem battleSystem;
		private readonly MvpUiService uiService;
		
		public GameplayEntryPoint(
			MovementFactory movementFactory,
			AnimalSpawnService animalSpawnService,
			BattleSystem battleSystem,
			MvpUiService uiService
			)
		{
			this.movementFactory = movementFactory;
			this.animalSpawnService = animalSpawnService;
			this.battleSystem = battleSystem;
			this.uiService = uiService;
		}
		
		public void Start()
		{
			movementFactory.Start();
			animalSpawnService.Start();
			battleSystem.Start();
			uiService.Start();

			uiService.Show<HudPresenter>();
			uiService.Show<AnimalCelebrationPresenter>();
		}
	}
}