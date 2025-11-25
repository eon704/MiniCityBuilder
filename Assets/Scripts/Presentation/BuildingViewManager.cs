using System;
using System.Collections.Generic;
using Domain;
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
  public class BuildingViewManager : IInitializable, IDisposable
  {
    private readonly Dictionary<Guid, IBuildingView> _buildingViews = new();
    private readonly Dictionary<BuildingTypeId, GameObject> _buildingPrefabs = new();
    
    private readonly CompositeDisposable _disposables = new();
    private readonly ISubscriber<BuildingPlacedEvent> _placedSubscriber;
    private readonly ISubscriber<BuildingRemovedEvent> _removedSubscriber;
    private readonly ISubscriber<BuildingRotatedEvent> _rotatedSubscriber;
    private readonly GridModel _gridModel;
    
    private const float CellSize = 1f; // Must match GridView._cellSize
    
    [Inject]
    public BuildingViewManager(
      ISubscriber<BuildingPlacedEvent> placedSubscriber,
      ISubscriber<BuildingRemovedEvent> removedSubscriber,
      ISubscriber<BuildingRotatedEvent> rotatedSubscriber,
      GridModel gridModel)
    {
      _placedSubscriber = placedSubscriber;
      _removedSubscriber = removedSubscriber;
      _rotatedSubscriber = rotatedSubscriber;
      _gridModel = gridModel;
    }
    
    public void Initialize()
    {
      _placedSubscriber.Subscribe(evt => SpawnBuilding(evt.BuildingId, evt.TypeId, evt.Position, 0f))
        .AddTo(_disposables);
      
      _removedSubscriber.Subscribe(evt => RemoveBuilding(evt.BuildingId))
        .AddTo(_disposables);
      
      _rotatedSubscriber.Subscribe(evt => UpdateBuildingRotation(evt.BuildingId, evt.NewRotation))
        .AddTo(_disposables);
    }
    
    public void SetPrefabs(GameObject housePrefab, GameObject farmPrefab, GameObject minePrefab)
    {
      if (housePrefab != null)
      {
        _buildingPrefabs[new BuildingTypeId(1)] = housePrefab;
      }
      if (farmPrefab != null)
      {
        _buildingPrefabs[new BuildingTypeId(2)] = farmPrefab;
      }
      if (minePrefab != null)
      {
        _buildingPrefabs[new BuildingTypeId(3)] = minePrefab;
      }
    }
    
    public void Dispose()
    {
      _disposables?.Dispose();
      
      foreach (var view in _buildingViews.Values)
      {
        if (view is MonoBehaviour mb && mb != null)
        {
          UnityEngine.Object.Destroy(mb.gameObject);
        }
      }
      _buildingViews.Clear();
    }
    
    public void SpawnBuilding(Guid buildingId, BuildingTypeId typeId, GridPos position, float rotation)
    {
      if (!_buildingPrefabs.TryGetValue(typeId, out var prefab))
      {
        return;
      }
      
      var instance = UnityEngine.Object.Instantiate(prefab);
      var buildingView = instance.GetComponent<IBuildingView>();
      
      buildingView.SetPosition(GridToWorldPosition(position));
      buildingView.SetRotation(rotation);
      
      _buildingViews[buildingId] = buildingView;
    }
    
    public void RemoveBuilding(Guid buildingId)
    {
      if (_buildingViews.TryGetValue(buildingId, out var view))
      {
        if (view is MonoBehaviour mb && mb != null)
        {
          UnityEngine.Object.Destroy(mb.gameObject);
        }
        _buildingViews.Remove(buildingId);
      }
    }
    
    public void UpdateBuildingRotation(Guid buildingId, float rotation)
    {
      if (_buildingViews.TryGetValue(buildingId, out var view))
      {
        view.SetRotation(rotation);
      }
    }
    
    private Vector3 GridToWorldPosition(GridPos gridPos)
    {
      float offsetX = (_gridModel.SizeX - 1) * CellSize * 0.5f;
      float offsetY = (_gridModel.SizeY - 1) * CellSize * 0.5f;
      
      float worldX = gridPos.X * CellSize - offsetX;
      float worldZ = gridPos.Y * CellSize - offsetY;
      
      return new Vector3(worldX, 0, worldZ);
    }
  }
}

