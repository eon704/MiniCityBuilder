using System;
using Domain.DTO.Commands;
using Domain.DTO.Events;
using MessagePipe;
using R3;
using VContainer;
using VContainer.Unity;

namespace Presentation
{
  public class InputPresenter : IInitializable, IDisposable
  {
    private readonly IPublisher<SelectBuildingTypeCommand> _selectBuildingTypePublisher;
    private readonly IPublisher<RotateBuildingCommand> _rotateBuildingPublisher;
    private readonly IPublisher<RemoveBuildingCommand> _removeBuildingPublisher;
    private readonly BuildingPresenter _buildingPresenter;
    private readonly CameraController _cameraController;
    private readonly CompositeDisposable _disposables = new();
    
    [Inject]
    public InputPresenter(
      IPublisher<SelectBuildingTypeCommand> selectBuildingTypePublisher,
      IPublisher<RotateBuildingCommand> rotateBuildingPublisher,
      IPublisher<RemoveBuildingCommand> removeBuildingPublisher,
      BuildingPresenter buildingPresenter,
      CameraController cameraController)
    {
      _selectBuildingTypePublisher = selectBuildingTypePublisher;
      _rotateBuildingPublisher = rotateBuildingPublisher;
      _removeBuildingPublisher = removeBuildingPublisher;
      _buildingPresenter = buildingPresenter;
      _cameraController = cameraController;
    }
    
    public void Initialize()
    {
    }
    
    public void HandleSelectBuildingType(SelectBuildingTypeCommand command)
    {
      _selectBuildingTypePublisher.Publish(command);
    }
    
    public void HandleRotateBuilding(RotateBuildingCommand command)
    {
      _buildingPresenter.RotateSelectedBuilding();
    }
    
    public void HandleDeleteBuilding(RemoveBuildingCommand command)
    {
      _buildingPresenter.DeleteSelectedBuilding();
    }
    
    public void HandleCameraMove(CameraMoveCommand command)
    {
      _cameraController.HandleCameraMove(command);
    }
    
    public void HandleCameraZoom(CameraZoomCommand command)
    {
      _cameraController.HandleCameraZoom(command);
    }
    
    public void Dispose()
    {
      _disposables?.Dispose();
    }
  }
}

