using System;
using Domain.Model;

namespace Domain.DTO.Events
{
  public record BuildingPlacedEvent(Guid BuildingId, BuildingTypeId TypeId, GridPos Position)
  {
    public Guid BuildingId { get; } = BuildingId;
    public BuildingTypeId TypeId { get; } = TypeId;
    public GridPos Position { get; } = Position;
  };
  
  public record BuildingRemovedEvent(Guid BuildingId)
  {
    public Guid BuildingId { get; } = BuildingId;
  };
  
  public record BuildingUpgradedEvent(Guid BuildingId)
  {
    public Guid BuildingId { get; } = BuildingId;
  };
  
  public record BuildingRotatedEvent(Guid BuildingId, float NewRotation)
  {
    public Guid BuildingId { get; } = BuildingId;
    public float NewRotation { get; } = NewRotation;
  };
}