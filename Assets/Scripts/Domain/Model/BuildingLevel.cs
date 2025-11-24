namespace Domain
{
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
  }
}