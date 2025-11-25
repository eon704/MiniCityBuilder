using System;
using Cysharp.Threading.Tasks;
using Presentation.Views.Interfaces;
using R3;
using UnityEngine;

namespace Presentation.Views.Buildings
{
  public class FarmView : MonoBehaviour, IBuildingView
  {
    private readonly Subject<Unit> _onUpgradeClicked = new();
    private readonly Subject<Unit> _onDeleteClicked = new();

    private void OnDestroy()
    {
      _onUpgradeClicked?.Dispose();
      _onDeleteClicked?.Dispose();
    }

    public UniTask ShowAsync()
    {
      gameObject.SetActive(true);
      return UniTask.CompletedTask;
    }

    public UniTask HideAsync()
    {
      gameObject.SetActive(false);
      return UniTask.CompletedTask;
    }

    public void SetPosition(Vector3 worldPosition)
    {
      transform.position = worldPosition;
    }

    public void SetVisualLevel(int level)
    {
      // TODO: Update visual appearance based on level
    }

    public void SetRotation(float angle)
    {
      transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public Observable<Unit> OnUpgradeClickedAsObservable()
    {
      return _onUpgradeClicked;
    }

    public Observable<Unit> OnDeleteClickedAsObservable()
    {
      return _onDeleteClicked;
    }
  }
}

