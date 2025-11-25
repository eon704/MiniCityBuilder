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
    
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private GameObject _farmPrefab;
    [SerializeField] private GameObject _minePrefab;
    
    private readonly CompositeDisposable _disposables = new();
    private readonly ISubscriber<BuildingPlacedEvent> _placedSubscriber;
    private readonly ISubscriber<BuildingRemovedEvent> _removedSubscriber;
    private readonly ISubscriber<BuildingRotatedEvent> _rotatedSubscriber;
    
    [Inject]
    public BuildingViewManager(
      ISubscriber<BuildingPlacedEvent> placedSubscriber,
      ISubscriber<BuildingRemovedEvent> removedSubscriber,
      ISubscriber<BuildingRotatedEvent> rotatedSubscriber)
    {
      _placedSubscriber = placedSubscriber;
      _removedSubscriber = removedSubscriber;
      _rotatedSubscriber = rotatedSubscriber;
    }
    
    public void Initialize()
    {
      if (_housePrefab != null)
        _buildingPrefabs[new BuildingTypeId(1)] = _housePrefab;
      if (_farmPrefab != null)
        _buildingPrefabs[new BuildingTypeId(2)] = _farmPrefab;
      if (_minePrefab != null)
        _buildingPrefabs[new BuildingTypeId(3)] = _minePrefab;

      _placedSubscriber.Subscribe(evt => SpawnBuilding(evt.BuildingId, evt.TypeId, evt.Position, 0f))
        .AddTo(_disposables);
      
      _removedSubscriber.Subscribe(evt => RemoveBuilding(evt.BuildingId))
        .AddTo(_disposables);
      
      _rotatedSubscriber.Subscribe(evt => UpdateBuildingRotation(evt.BuildingId, evt.NewRotation))
        .AddTo(_disposables);
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
        Debug.LogError($"BuildingViewManager: No prefab found for type {typeId}");
        return;
      }
      
      var instance = UnityEngine.Object.Instantiate(prefab);
      var buildingView = instance.GetComponent<IBuildingView>();
      
      if (buildingView == null)
      {
        Debug.LogError($"BuildingViewManager: Prefab {prefab.name} doesn't have IBuildingView component");
        UnityEngine.Object.Destroy(instance);
        return;
      }
      
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
      return new Vector3(gridPos.X, 0, gridPos.Y);
    }
  }
}

