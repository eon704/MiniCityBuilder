using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Presentation.Views.Interfaces
{
  public interface IBuildingView
  {
    UniTask ShowAsync();
    UniTask HideAsync();
    void SetPosition(Vector3 worldPosition);
    void SetVisualLevel(int level);
    void SetRotation(float angle);
    // Use R3 Observable for UI events
    IObservable<Unit> OnUpgradeClickedAsObservable(); 
    IObservable<Unit> OnDeleteClickedAsObservable();
  }
}

