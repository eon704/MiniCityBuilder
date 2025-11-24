using System.Collections.Generic;

namespace Domain.Model
{
  public class BuildingConfig
  {
    public BuildingTypeId TypeId { get; }
    public string Name;
    public IReadOnlyList<BuildingLevel> Levels;

    public BuildingConfig(BuildingTypeId typeId, string name, IReadOnlyList<BuildingLevel> levels)
    {
      TypeId = typeId;
      Name = name;
      Levels = levels;
    }
  }
}