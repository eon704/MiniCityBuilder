using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
  public interface IBuildingRepository
  {
    IReadOnlyList<Building> GetAll();
    Building Get(Guid id);
    void Add(Building building);
    void Remove(Guid id);
    void Update(Building building);
  }
}