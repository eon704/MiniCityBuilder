using System;
using Domain.DTO.Commands;
using Domain.DTO.Events;
using Domain.Interfaces;
using Domain.Model;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases
{
  public class RemoveBuildingUseCase : IMessageHandler<RemoveBuildingCommand>, IInitializable, IDisposable
  {
    private readonly IPublisher<BuildingRemovedEvent> _buildingRemovedPublisher;
    private readonly Domain.Interfaces.IBuildingRepository _buildingRepository;
    private readonly GridModel _gridModel;
    
    [Inject]
    public RemoveBuildingUseCase(
      IPublisher<BuildingRemovedEvent> buildingRemovedPublisher,
      Domain.Interfaces.IBuildingRepository buildingRepository,
      GridModel gridModel)
    {
      _buildingRemovedPublisher = buildingRemovedPublisher;
      _buildingRepository = buildingRepository;
      _gridModel = gridModel;
    }
    
    public void Initialize() { }
    public void Dispose() { }
    
    public void Handle(RemoveBuildingCommand command)
    {
      var building = _buildingRepository.Get(command.BuildingId);
      if (building == null)
        return;
      
      var position = building.Position.CurrentValue;
      _buildingRepository.Remove(command.BuildingId);
      _gridModel.RemoveBuilding(position);
      
      _buildingRemovedPublisher.Publish(new BuildingRemovedEvent(command.BuildingId));
    }
  }
}
