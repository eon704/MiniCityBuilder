using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
  public interface IBuildingRepository
  {
    IReadOnlyList<BuildingModel> GetAll();
    BuildingModel Get(Guid id);
    void Add(BuildingModel buildingModel);
    void Remove(Guid id);
    void Update(BuildingModel buildingModel);
  }
}