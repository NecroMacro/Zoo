using System;
using System.Collections.Generic;
using Core.MpvUi;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZooWorld.Gameplay.Ui;

[UsedImplicitly]
public class MvpUiService : IStartable
{
	private readonly IObjectResolver resolver;
	private readonly UiRegistryConfig registryConfig;
	private readonly Canvas mainCanvas;
	
	private readonly Dictionary<Type, BaseViewConfig> viewConfigs = new();
	
	public MvpUiService(IObjectResolver resolver, Canvas mainCanvas, UiRegistryConfig registryConfig)
	{
		this.resolver = resolver;
		this.mainCanvas = mainCanvas;
		this.registryConfig = registryConfig;
	}
	
	public void Start()
	{
		foreach (var config in registryConfig.registry)
			viewConfigs[config.GetType()] = config;
	}
	
	public TPresenter Show<TPresenter>() where TPresenter : IBasePresenter
	{
		var presenterType = typeof(TPresenter);
		var baseType = presenterType.BaseType;
		var configType = baseType!.GetGenericArguments()[2];

		if (!viewConfigs.TryGetValue(configType, out var config))
			throw new Exception($"Config of type {configType} not found");

		var view = UnityEngine.Object.Instantiate(config.Asset, mainCanvas.transform);
		
		var modelType = baseType.GetGenericArguments()[0];
		var model = (IBaseModel)Activator.CreateInstance(modelType);

		var presenter = Activator.CreateInstance<TPresenter>();

		resolver.Inject(model);
		resolver.Inject(presenter);

		presenter.Initialize(model, view, config);

		return presenter;
	}
}