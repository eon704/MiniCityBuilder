using Domain.Model;
using MessagePipe;
using Presentation;
using Presentation.Views.Grid;
using Presentation.Views.Interfaces;
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
      
      builder.Register<IGridView>(container => container.Resolve<GridView>(), Lifetime.Scoped);
      
      builder.RegisterEntryPoint<GridPresenter>(Lifetime.Scoped);
    }
  }
}
