using Domain;

namespace Domain.Model
{
  [System.Serializable]
  public class BuildingLevel
  {
    public int Level;
    public Cost UpgradeCost;
    public Income Income;
    public string Description;
    
    public BuildingLevel(int level, Cost upgradeCost, Income income, string description)
    {
      Level = level;
      UpgradeCost = upgradeCost;
      Income = income;
      Description = description;
    }

    // Parameterless constructor required for Unity serialization
    public BuildingLevel()
    {
      Level = 1;
      UpgradeCost = new Cost(0);
      Income = new Income(0);
      Description = string.Empty;
    }
  }
}