using System;
using System.Collections.Generic;
using Domain;

namespace Repositories
{
  public interface IBuildingRepository
  {
    IReadOnlyList<Building> GetAll();
    Building Get(Guid id);
    void Add(Building building);
    void Remove(Guid id);
  }
}