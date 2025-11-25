using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Model;
using VContainer;

namespace Repositories
{
  public class UnityBuildingRepository : IBuildingRepository
  {
    private readonly Dictionary<Guid, Building> _buildings = new();
    
    [Inject] private readonly Grid _gridModel;

    public IReadOnlyList<Building> GetAll()
    {
      return _buildings.Values.ToList().AsReadOnly();
    }

    public Building Get(Guid id) => _buildings.GetValueOrDefault(id);

    public void Add(Building building)
    {
      _buildings[building.Id] = building;
    }

    public void Remove(Guid id)
    {
      _buildings.Remove(id);
    }
  }
}