using System;
using Domain.Model;

namespace Domain.DTO.Commands
{
  public record PlaceBuildingCommand(BuildingTypeId TypeId, GridPos Position)
  {
    public BuildingTypeId TypeId { get; } = TypeId;
    public GridPos Position { get; } = Position;
  }

  public record RemoveBuildingCommand(Guid BuildingId)
  {
    public Guid BuildingId { get; } = BuildingId;
  }

  public record MoveBuildingCommand(Guid BuildingId, GridPos ToPosition)
  {
    public Guid BuildingId { get; } = BuildingId;
    public GridPos ToPosition { get; } = ToPosition;
  }

  public record UpgradeBuildingCommand(Guid BuildingId)
  {
    public Guid BuildingId { get; } = BuildingId;
  }
  
  public record RotateBuildingCommand(Guid BuildingId)
  {
    public Guid BuildingId { get; } = BuildingId;
  }
  
  public record SelectBuildingTypeCommand(BuildingTypeId TypeId)
  {
    public BuildingTypeId TypeId { get; } = TypeId;
  }
}