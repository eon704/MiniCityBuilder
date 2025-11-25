using Application.Services;
using Application.UseCases;
using Domain.DTO.Commands;
using Domain.DTO.Events;
using Domain.Interfaces;
using Domain.Model;
using Infrastructure.Input;
using MessagePipe;
using MessagePipe.VContainer;
using Presentation;
using Presentation.Views.Grid;
using Presentation.Views.Interfaces;
using Repositories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
  public class GameSceneInstaller : IInstaller
  {
    private const int GridSizeX = 32;
    private const int GridSizeY = 32;
    
    public void Install(IContainerBuilder builder)
    {
      var options = builder.RegisterMessagePipe();
      builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
      builder.Register<ILogger>(container => Debug.unityLogger, Lifetime.Singleton);
      builder.Register<GridModel>(container => new GridModel(GridSizeX, GridSizeY), Lifetime.Singleton);
      builder.Register<Domain.Interfaces.IBuildingRepository, UnityBuildingRepository>(Lifetime.Singleton);
      builder.Register<Domain.Interfaces.IBuildingConfigRepository, InMemoryBuildingConfigRepository>(Lifetime.Singleton);
      
      builder.Register<BuildingSelectionService>(Lifetime.Singleton);
      
      builder.Register<PlaceBuildingUseCase>(Lifetime.Singleton);
      builder.Register<RemoveBuildingUseCase>(Lifetime.Singleton);
      builder.Register<RotateBuildingUseCase>(Lifetime.Singleton);
      builder.Register<SelectBuildingTypeUseCase>(Lifetime.Singleton);
      
      builder.RegisterBuildCallback(container =>
      {
        var placeHandler = container.Resolve<PlaceBuildingUseCase>();
        var removeHandler = container.Resolve<RemoveBuildingUseCase>();
        var rotateHandler = container.Resolve<RotateBuildingUseCase>();
        var selectHandler = container.Resolve<SelectBuildingTypeUseCase>();
        
        var placeSubscriber = container.Resolve<ISubscriber<PlaceBuildingCommand>>();
        var removeSubscriber = container.Resolve<ISubscriber<RemoveBuildingCommand>>();
        var rotateSubscriber = container.Resolve<ISubscriber<RotateBuildingCommand>>();
        var selectSubscriber = container.Resolve<ISubscriber<SelectBuildingTypeCommand>>();
        
        placeSubscriber.Subscribe(placeHandler);
        removeSubscriber.Subscribe(removeHandler);
        rotateSubscriber.Subscribe(rotateHandler);
        selectSubscriber.Subscribe(selectHandler);
      });
      builder.RegisterEntryPoint<InputHandler>(Lifetime.Scoped);
      builder.Register<IGridView>(container => container.Resolve<GridView>(), Lifetime.Scoped);
      builder.RegisterEntryPoint<GridPresenter>(Lifetime.Scoped);
      builder.RegisterEntryPoint<BuildingPresenter>(Lifetime.Scoped);
      builder.RegisterEntryPoint<InputPresenter>(Lifetime.Scoped);
      builder.Register<BuildingViewManager>(Lifetime.Singleton);
      
      var buildingViewManagerMono = Object.FindObjectOfType<BuildingViewManagerMono>();
      builder.RegisterComponent(buildingViewManagerMono);

      var camera = Camera.main ?? Object.FindObjectOfType<Camera>();
      if (camera != null)
      {
        builder.RegisterInstance(camera);
      }

      builder.RegisterEntryPoint<CameraController>(Lifetime.Singleton);
    }
  }
}
