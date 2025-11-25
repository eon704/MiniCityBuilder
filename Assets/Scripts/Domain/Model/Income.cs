namespace Domain
{
  [System.Serializable]
  public class Income
  {
    public int GoldPerTick;
    
    public Income(int goldPerTick)
    {
      GoldPerTick = goldPerTick;
    }
    
    // Parameterless constructor required for Unity serialization
    public Income()
    {
      GoldPerTick = 0;
    }
  }
}