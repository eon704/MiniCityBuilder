using System;
using Domain;
using Domain.DTO.Commands;
using Domain.DTO.Events;
using Domain.Interfaces;
using Domain.Model;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases
{
  public class PlaceBuildingUseCase : IMessageHandler<PlaceBuildingCommand>, IInitializable, IDisposable
  {
    private readonly IPublisher<BuildingPlacedEvent> _buildingPlacedPublisher;
    private readonly IBuildingRepository _buildingRepository;
    private readonly GridModel _gridModel;
    private readonly IBuildingConfigRepository _configRepository;
    
    [Inject]
    public PlaceBuildingUseCase(
      IPublisher<BuildingPlacedEvent> buildingPlacedPublisher,
      IBuildingRepository buildingRepository,
      GridModel gridModel,
      IBuildingConfigRepository configRepository)
    {
      _buildingPlacedPublisher = buildingPlacedPublisher;
      _buildingRepository = buildingRepository;
      _gridModel = gridModel;
      _configRepository = configRepository;
    }
    
    public void Initialize()
    {
    }
    
    public void Dispose()
    {
    }
    
    public void Handle(PlaceBuildingCommand command)
    {
      if (!_gridModel.IsInBounds(command.Position))
      {
        return;
      }
      
      if (_gridModel.IsOccupied(command.Position))
      {
        return;
      }
      
      var config = _configRepository.GetConfig(command.TypeId);
      if (config == null)
      {
        return;
      }
      
      var building = new BuildingModel(config, command.Position, 0f);
      _buildingRepository.Add(building);
      _gridModel.PlaceBuilding(building);
      _buildingPlacedPublisher.Publish(new BuildingPlacedEvent(building.Id, building.Config.TypeId, command.Position));
    }
  }
}
