namespace Domain.Model
{
  [System.Serializable]
  public struct BuildingTypeId : System.IEquatable<BuildingTypeId>
  {
    public int Value;

    public BuildingTypeId(int value)
    {
      Value = value;
    }

    public bool Equals(BuildingTypeId other)
    {
      return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return obj is BuildingTypeId other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();
  }
}