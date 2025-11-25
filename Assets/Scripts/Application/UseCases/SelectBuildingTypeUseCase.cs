using System;
using Domain.DTO.Commands;
using Domain.Model;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases
{
  public class SelectBuildingTypeUseCase : IMessageHandler<SelectBuildingTypeCommand>, IInitializable, IDisposable
  {
    private readonly Services.BuildingSelectionService _selectionService;
    
    [Inject]
    public SelectBuildingTypeUseCase(Services.BuildingSelectionService selectionService)
    {
      _selectionService = selectionService;
    }
    
    public void Initialize() { }
    public void Dispose() { }
    
    public void Handle(SelectBuildingTypeCommand command)
    {
      _selectionService.SelectBuildingType(command.TypeId);
    }
  }
}

