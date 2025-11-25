using System;

namespace Domain.DTO.Events
{
  public record BuildingPlacedEvent(BuildingModel BuildingModel)
  {
    public BuildingModel BuildingModel { get; } = BuildingModel;
  };
  
  public record BuildingRemovedEvent(Guid BuildingId)
  {
    public Guid BuildingId { get; } = BuildingId;
  };
  
  public record BuildingUpgradedEvent(BuildingModel BuildingModel)
  {
    public BuildingModel BuildingModel { get; } = BuildingModel;
  };
}