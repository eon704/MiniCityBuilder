using System.Collections.Generic;
using Domain.Model;

namespace Domain.Interfaces
{
  public interface IBuildingConfigRepository
  {
    BuildingConfig GetConfig(BuildingTypeId id);
    IReadOnlyList<BuildingConfig> GetAllConfigs();
  }
}
