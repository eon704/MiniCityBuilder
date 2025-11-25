using System;

namespace Domain
{
  public struct GridPos
  {
    public int X;
    public int Y;
    
    public GridPos(int x, int y)
    {
      X = x;
      Y = y;
    }

    public static bool operator ==(GridPos a, GridPos b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(GridPos a, GridPos b) => !(a == b);
    public override bool Equals(object obj)
    {
      return obj is GridPos other && Equals(other);
    }
    
    public bool Equals(GridPos other)
    {
      return X == other.X && Y == other.Y;
    }
    
    public override int GetHashCode()
    {
      return HashCode.Combine(X, Y);
    }
  }
}