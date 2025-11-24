using System;

namespace Domain.DTO.Events
{
  public record BuildingPlacedEvent(Building Building)
  {
    public Building Building { get; } = Building;
  };
  
  public record BuildingRemovedEvent(Guid BuildingId)
  {
    public Guid BuildingId { get; } = BuildingId;
  };
  
  public record BuildingUpgradedEvent(Building Building)
  {
    public Building Building { get; } = Building;
  };
}