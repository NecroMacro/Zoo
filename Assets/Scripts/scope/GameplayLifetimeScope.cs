using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZooWorld.Gameplay;
using ZooWorld.Gameplay.Collision;
using ZooWorld.Gameplay.Factory;
using ZooWorld.Gameplay.Movement;
using ZooWorld.Gameplay.Ui;

namespace ZooWorld.Scope
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameplayConfig gameplayConfig;
        
        [SerializeField]
        private EnvironmentData environmentData;
        
        [SerializeField]
        private Canvas mainCanvas;
        
        [SerializeField]
        private UiRegistryConfig uiRegistryConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(gameplayConfig);
            builder.RegisterInstance(environmentData);
            builder.RegisterInstance(mainCanvas);
            builder.RegisterInstance(uiRegistryConfig);
            
            builder.Register<ScreenService>(Lifetime.Scoped).As<IMovementController>().AsSelf();
            builder.Register<MovementFactory>(Lifetime.Scoped);
            builder.Register<AnimalCollisionFactory>(Lifetime.Scoped).As<IBattleHitProvider>().AsSelf();
            builder.Register<AnimalFactory>(Lifetime.Scoped);
            builder.Register<AnimalSpawnService>(Lifetime.Scoped);
            builder.Register<BattleSystem>(Lifetime.Scoped).As<IBattleDieInfoProvider>().AsSelf();
            builder.Register<MvpUiService>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<GameplayEntryPoint>();
        }
    }
}