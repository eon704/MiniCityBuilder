using System.Collections.Generic;

namespace Domain.Model
{
  public class Grid
  {
    public int SizeX { get; private set; }
    public int SizeY { get; private set; }
    
    private readonly Dictionary<GridPos, Building> _occupiedCells = new();

    public Grid(int sizeX, int sizeY)
    {
      SizeX = sizeX;
      SizeY = sizeY;
    }
    
    public bool IsInBounds(GridPos pos)
    {
      return pos.X >= 0 && pos.X < SizeX && pos.Y >= 0 && pos.Y < SizeY;
    }
    
    public bool IsOccupied(GridPos pos)
    {
      return _occupiedCells.ContainsKey(pos);
    }
    
    public Building GetBuildingAt(GridPos pos)
    {
      return _occupiedCells.GetValueOrDefault(pos);
    }
    
    public void PlaceBuilding(Building building)
    {
      var pos = building.Position.CurrentValue;
      if (!IsInBounds(pos))
      {
        return;
      }
      
      if (IsOccupied(pos))
      {
        return;
      }
      
      _occupiedCells[pos] = building;
    }

    public void RemoveBuilding(GridPos pos)
    {
      if (!IsInBounds(pos))
      {
        return;
      }

      if (!IsOccupied(pos))
      {
        return;
      }

      _occupiedCells[pos] = null;
    }
  }
}