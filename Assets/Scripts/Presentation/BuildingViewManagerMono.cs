using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation
{
  public class BuildingViewManagerMono : MonoBehaviour, IInitializable
  {
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private GameObject _farmPrefab;
    [SerializeField] private GameObject _minePrefab;
    
    private BuildingViewManager _manager;
    
    [Inject]
    public void Construct(BuildingViewManager manager)
    {
      _manager = manager;
    }
    
    public void Initialize()
    {
      if (_manager == null) return;
      
      _manager.SetPrefabs(_housePrefab, _farmPrefab, _minePrefab);
    }
  }
}

