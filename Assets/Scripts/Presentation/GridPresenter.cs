using System;
using Domain;
using Domain.Model;
using Presentation.Views.Interfaces;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation
{
  public class GridPresenter : IInitializable, IDisposable
  {
    [Inject] private readonly GridModel _gridModel;
    [Inject] private readonly IGridView _gridView;
    [Inject] private ILogger _logger;
    
    private readonly CompositeDisposable _disposables = new();

    public void Initialize()
    {
      _gridView.InitializeMap(_gridModel.SizeX, _gridModel.SizeY);
      
      _gridView.OnCellClickedAsObservable()
        .Subscribe(HandleCellClicked)
        .AddTo(_disposables);
    }

    public void Dispose()
    {
      _disposables?.Dispose();
    }
    
    private void HandleCellClicked(GridPos pos)
    {
      if (!_gridModel.IsInBounds(pos)) 
        return;
      
      _logger.Log($"Grid Clicked at: ({pos.X}, {pos.Y})");
        
      if (_gridModel.IsOccupied(pos))
      {
        var building = _gridModel.GetBuildingAt(pos);
        _logger.LogWarning("Grid.HandleCellClicked", $"Cell occupied by Building ID: {building.Id}");
      }
      else
      {
        _logger.Log($"Cell is free.");
      }
    }
  }
}