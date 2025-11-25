using System;
using Domain.Model;
using R3;
using VContainer;
using VContainer.Unity;

namespace Application.Services
{
  public class BuildingSelectionService : IInitializable, IDisposable
  {
    private readonly ReactiveProperty<BuildingTypeId?> _selectedBuildingType = new(null);
    
    public ReadOnlyReactiveProperty<BuildingTypeId?> SelectedBuildingType => _selectedBuildingType;
    
    public void Initialize()
    {}
    
    public void Dispose()
    {
      _selectedBuildingType?.Dispose();
    }
    
    public void SelectBuildingType(BuildingTypeId typeId)
    {
      _selectedBuildingType.Value = typeId;
    }
    
    public void ClearSelection()
    {
      _selectedBuildingType.Value = null;
    }
  }
}

