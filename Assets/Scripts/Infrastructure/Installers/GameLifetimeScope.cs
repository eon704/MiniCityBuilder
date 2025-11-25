using Infrastructure.Installers;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
  public class GameLifetimeScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      var installer = new GameSceneInstaller();
      installer.Install(builder);
      
      builder.RegisterComponentInHierarchy<Presentation.Views.Grid.GridView>();
    }
  }
}
