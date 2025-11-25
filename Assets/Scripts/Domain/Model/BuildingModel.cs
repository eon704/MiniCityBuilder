using System;
using Domain.Model;
using R3;

namespace Domain
{
  public class BuildingModel
  {
    public Guid Id { get; }
    public BuildingConfig Config { get; }
    
    public ReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
    public ReadOnlyReactiveProperty<GridPos> Position => _position;
    public ReadOnlyReactiveProperty<float> RotationAngle => _rotationAngle;
    
    private readonly ReactiveProperty<int> _currentLevel = new();
    private readonly ReactiveProperty<GridPos> _position = new();
    private readonly ReactiveProperty<float> _rotationAngle = new();

    public BuildingModel(BuildingConfig config, GridPos position, float rotationAngle)
    {
      Id = Guid.NewGuid();
      Config = config;
      _currentLevel.Value = 1;
      _position.Value = position;
      _rotationAngle.Value = rotationAngle;
    }

    public BuildingLevel GetCurrentLevelData => Config.Levels[_currentLevel.Value - 1];
    public BuildingLevel GetNextLevelData()
    {
      if (_currentLevel.Value >= Config.Levels.Count)
      {
        return null;
      }
      
      return Config.Levels[_currentLevel.Value + 1]; 
    }

    public bool CanUpgrade() => _currentLevel.Value < Config.Levels.Count;

    public void Upgrade()
    {
      // TODO: revisit
    }
    
    public void SetPosition(GridPos newPosition)
    {
      _position.Value = newPosition;
    }
    
    public void SetRotationAngle(float newAngle)
    {
      _rotationAngle.Value = newAngle;
    }
  }
}