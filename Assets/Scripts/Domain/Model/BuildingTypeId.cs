namespace Domain.Model
{
  public readonly struct BuildingTypeId : System.IEquatable<BuildingTypeId>
  {
    private readonly int _value;

    public BuildingTypeId(int value)
    {
      _value = value;
    }

    public bool Equals(BuildingTypeId other)
    {
      return _value == other._value;
    }

    public override bool Equals(object obj)
    {
        return obj is BuildingTypeId other && Equals(other);
    }

    public override int GetHashCode() => _value.GetHashCode();
  }
}