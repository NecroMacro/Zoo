using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;
using ZooWorld.Gameplay.Configs;
using ZooWorld.Gameplay.Movement;

namespace ZooWorld.Gameplay.Factory
{
	[UsedImplicitly]
	public class MovementFactory : IStartable
	{
		private readonly IMovementController movementController;
		
		private readonly Dictionary<Type, Type> typeMap = new();

		public MovementFactory(IMovementController movementController)
		{
			this.movementController = movementController;
		}
		
		public void Start()
		{
			Initialize();
		}
		
		private void Initialize()
		{
			var baseMovementType = typeof(BaseMovement);
			var assembly = Assembly.GetAssembly(typeof(BaseMovement));
			var types = assembly.GetTypes();

			foreach (var type in types)
			{
				if (type.IsAbstract || type.IsInterface)
					continue;

				if (!baseMovementType.IsAssignableFrom(type))
					continue;

				var current = type.BaseType;
				while (current != null)
				{
					if (current.IsGenericType &&
					    current.GetGenericTypeDefinition() == typeof(BaseMovement<>))
					{
						var configType = current.GetGenericArguments()[0];
						typeMap[configType] = type;
						break;
					}

					current = current.BaseType;
				}
			}
		}
		
		public BaseMovement Create(GameObject target, MovementConfig config)
		{
			var configType = config.GetType();

			if (!typeMap.TryGetValue(configType, out var movementType))
				throw new Exception($"No movement registered for {configType}");

			var movement = target.AddComponent(movementType) as BaseMovement;
			movement!.ApplyConfig(config);
			movement.ApplyDirectionChanger(movementController);
			return movement;
		}
	}
}