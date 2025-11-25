using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Model;

namespace Repositories
{
  /// <summary>
  /// Simple in-memory implementation of IBuildingConfigRepository.
  /// Creates default building configs for testing.
  /// Can be replaced with ScriptableObjectBuildingConfigRepository later.
  /// </summary>
  public class InMemoryBuildingConfigRepository : IBuildingConfigRepository
  {
    private readonly Dictionary<BuildingTypeId, BuildingConfig> _configs = new();

    public InMemoryBuildingConfigRepository()
    {
      // Create default building configs
      // These can be replaced with ScriptableObject-based configs later
      
      // House (TypeId = 1)
      var houseLevel = new BuildingLevel(
        level: 1,
        upgradeCost: new Cost(100),
        income: new Income(10),
        description: "A basic house that provides shelter"
      );
      _configs[new BuildingTypeId(1)] = new BuildingConfig(
        typeId: new BuildingTypeId(1),
        name: "House",
        levels: new List<BuildingLevel> { houseLevel }
      );

      // Farm (TypeId = 2)
      var farmLevel = new BuildingLevel(
        level: 1,
        upgradeCost: new Cost(150),
        income: new Income(15),
        description: "A farm that produces food"
      );
      _configs[new BuildingTypeId(2)] = new BuildingConfig(
        typeId: new BuildingTypeId(2),
        name: "Farm",
        levels: new List<BuildingLevel> { farmLevel }
      );

      // Mine (TypeId = 3)
      var mineLevel = new BuildingLevel(
        level: 1,
        upgradeCost: new Cost(200),
        income: new Income(20),
        description: "A mine that extracts resources"
      );
      _configs[new BuildingTypeId(3)] = new BuildingConfig(
        typeId: new BuildingTypeId(3),
        name: "Mine",
        levels: new List<BuildingLevel> { mineLevel }
      );
    }

    public BuildingConfig GetConfig(BuildingTypeId id)
    {
      return _configs.TryGetValue(id, out var config) ? config : null;
    }

    public IReadOnlyList<BuildingConfig> GetAllConfigs()
    {
      return _configs.Values.ToList();
    }
  }
}

