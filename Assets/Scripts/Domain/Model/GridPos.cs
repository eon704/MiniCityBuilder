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
      throw new NotImplementedException();
    }
    public override int GetHashCode()
    {
      throw new NotImplementedException();
    }
  }
}