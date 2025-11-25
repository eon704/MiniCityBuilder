using System;
using Domain;
using Domain.DTO.Commands;
using Domain.DTO.Events;
using Domain.Model;
using MessagePipe;
using Presentation.Views.Interfaces;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation
{
  public class BuildingPresenter : IInitializable, IDisposable
  {
    private readonly IPublisher<PlaceBuildingCommand> _placeBuildingPublisher;
    private readonly IPublisher<SelectBuildingTypeCommand> _selectBuildingTypePublisher;
    private readonly IPublisher<RemoveBuildingCommand> _removeBuildingPublisher;
    private readonly IPublisher<RotateBuildingCommand> _rotateBuildingPublisher;
    private readonly Application.Services.BuildingSelectionService _selectionService;
    private readonly IGridView _gridView;
    private readonly ILogger _logger;
    
    private readonly CompositeDisposable _disposables = new();
    private Guid? _selectedBuildingId;
    
    [Inject]
    public BuildingPresenter(
      IPublisher<PlaceBuildingCommand> placeBuildingPublisher,
      IPublisher<SelectBuildingTypeCommand> selectBuildingTypePublisher,
      IPublisher<RemoveBuildingCommand> removeBuildingPublisher,
      IPublisher<RotateBuildingCommand> rotateBuildingPublisher,
      Application.Services.BuildingSelectionService selectionService,
      IGridView gridView,
      ILogger logger)
    {
      _placeBuildingPublisher = placeBuildingPublisher;
      _selectBuildingTypePublisher = selectBuildingTypePublisher;
      _removeBuildingPublisher = removeBuildingPublisher;
      _rotateBuildingPublisher = rotateBuildingPublisher;
      _selectionService = selectionService;
      _gridView = gridView;
      _logger = logger;
    }
    
    public void Initialize()
    {
      _gridView.OnCellClickedAsObservable()
        .Subscribe(HandleCellClicked)
        .AddTo(_disposables);

      _selectionService.SelectedBuildingType
        .Subscribe(HandleBuildingTypeSelected)
        .AddTo(_disposables);
    }
    
    public void Dispose()
    {
      _disposables?.Dispose();
    }
    
    private void HandleCellClicked(GridPos pos)
    {
      var selectedType = _selectionService.SelectedBuildingType.CurrentValue;
      
      if (selectedType.HasValue)
      {
        _placeBuildingPublisher.Publish(new PlaceBuildingCommand(selectedType.Value, pos));
        _logger.Log($"Placing building type {selectedType.Value.Value} at ({pos.X}, {pos.Y})");
      }
      else
      {
        _logger.Log($"Cell clicked at ({pos.X}, {pos.Y}) but no building type selected. Press 1, 2, or 3 to select a building.");
      }
    }
    
    private void HandleBuildingTypeSelected(BuildingTypeId? typeId)
    {
      if (typeId.HasValue)
      {
        _logger.Log($"Building type selected: {typeId.Value.Value}");
      }
      else
      {
        _logger.Log("Building selection cleared");
      }
    }
    
    public void SelectBuilding(Guid buildingId)
    {
      _selectedBuildingId = buildingId;
    }
    
    public void RotateSelectedBuilding()
    {
      if (_selectedBuildingId.HasValue)
      {
        _rotateBuildingPublisher.Publish(new RotateBuildingCommand(_selectedBuildingId.Value));
      }
    }
    
    public void DeleteSelectedBuilding()
    {
      if (_selectedBuildingId.HasValue)
      {
        _removeBuildingPublisher.Publish(new RemoveBuildingCommand(_selectedBuildingId.Value));
        _selectedBuildingId = null;
      }
    }
  }
}

