using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Model;
using VContainer;

namespace Repositories
{
  public class UnityBuildingRepository : IBuildingRepository
  {
    private readonly Dictionary<Guid, BuildingModel> _buildings = new();
    
    [Inject] private readonly GridModel _gridModelModel;

    public IReadOnlyList<BuildingModel> GetAll()
    {
      return _buildings.Values.ToList().AsReadOnly();
    }

    public BuildingModel Get(Guid id) => _buildings.GetValueOrDefault(id);

    public void Add(BuildingModel buildingModel)
    {
      _buildings[buildingModel.Id] = buildingModel;
    }

    public void Remove(Guid id)
    {
      _buildings.Remove(id);
    }

    public void Update(BuildingModel buildingModel)
    {
      if (_buildings.ContainsKey(buildingModel.Id))
      {
        _buildings[buildingModel.Id] = buildingModel;
      }
    }
  }
}