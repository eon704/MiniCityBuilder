using System;
using MessagePipe;
using VContainer.Unity;

namespace Application.UseCases
{
  public class PlaceBuildingUseCase : IInitializable, IDisposable, IMessageHandler<PlaceBuildingUseCase>
  {
    public void Initialize()
    {
      throw new NotImplementedException();
    }

    public void Handle(PlaceBuildingUseCase message)
    {
      throw new NotImplementedException();
    }
    
    public void Dispose()
    {
      
    }
  }
}