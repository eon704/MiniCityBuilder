using Domain.Model;
using UnityEngine;

namespace Domain.DTO.Events
{
  public record CameraMoveCommand(Vector2 Delta)
  {
    public Vector2 Delta { get; } = Delta;
  }
  
  public record CameraZoomCommand(float Delta)
  {
    public float Delta { get; } = Delta;
  }
}

