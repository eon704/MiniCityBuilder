using System.Linq;
using Domain;
using Domain.Model;
using UnityEngine;

namespace Repositories
{
  [CreateAssetMenu(fileName = "BuildingConfig", menuName = "Game/Building Config")]
  [System.Serializable]
  public class BuildingConfigSO : ScriptableObject
  {
    public BuildingTypeId Id;
    public string Name;
    public BuildingLevel[] Levels;

    public BuildingConfig ToDomainModel() => new(Id, Name, Levels.ToList());
  }
}