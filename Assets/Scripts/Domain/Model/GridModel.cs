using System.Collections.Generic;

namespace Domain.Model
{
  public class GridModel
  {
    public int SizeX { get; }
    public int SizeY { get; }
    
    private readonly Dictionary<GridPos, BuildingModel> _occupiedCells;

    public GridModel(int sizeX, int sizeY)
    {
      SizeX = sizeX;
      SizeY = sizeY;
      _occupiedCells = new Dictionary<GridPos, BuildingModel>();
    }

    public bool IsInBounds(GridPos pos)
    {
      return pos.X >= 0 && pos.X < SizeX && pos.Y >= 0 && pos.Y < SizeY;
    }
    
    public bool IsOccupied(GridPos pos)
    {
      return _occupiedCells.ContainsKey(pos);
    }
    
    public BuildingModel GetBuildingAt(GridPos pos)
    {
      return _occupiedCells.GetValueOrDefault(pos);
    }
    
    public void PlaceBuilding(BuildingModel buildingModel)
    {
      var pos = buildingModel.Position.CurrentValue;
      if (!IsInBounds(pos))
      {
        return;
      }
      
      if (IsOccupied(pos))
      {
        return;
      }
      
      _occupiedCells[pos] = buildingModel;
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