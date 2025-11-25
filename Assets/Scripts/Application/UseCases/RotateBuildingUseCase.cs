using System;
using Application.Services;
using Domain;
using Domain.DTO.Commands;
using Domain.DTO.Events;
using Domain.Interfaces;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases
{
  public class RotateBuildingUseCase : IMessageHandler<RotateBuildingCommand>, IInitializable, IDisposable
  {
    private readonly IPublisher<BuildingRotatedEvent> _buildingRotatedPublisher;
    private readonly Domain.Interfaces.IBuildingRepository _buildingRepository;
    private readonly BuildingSelectionService _selectionService;
    
    [Inject]
    public RotateBuildingUseCase(
      IPublisher<BuildingRotatedEvent> buildingRotatedPublisher,
      Domain.Interfaces.IBuildingRepository buildingRepository,
      BuildingSelectionService selectionService)
    {
      _buildingRotatedPublisher = buildingRotatedPublisher;
      _buildingRepository = buildingRepository;
      _selectionService = selectionService;
    }
    
    public void Initialize() { }
    public void Dispose() { }
    
    public void Handle(RotateBuildingCommand command)
    {
      var building = _buildingRepository.Get(command.BuildingId);
      if (building == null)
        return;
      
      float currentRotation = building.RotationAngle.CurrentValue;
      float newRotation = (currentRotation + 90f) % 360f;
      
      building.SetRotationAngle(newRotation);
      _buildingRepository.Update(building);
      
      _buildingRotatedPublisher.Publish(new BuildingRotatedEvent(building.Id, newRotation));
    }
  }
}

