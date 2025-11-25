namespace Domain
{
  [System.Serializable]
  public struct Cost
  {
    public int Gold;
    
    public Cost(int gold)
    {
      Gold = gold;
    }
  }
}