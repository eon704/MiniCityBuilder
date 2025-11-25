using System;
using Cysharp.Threading.Tasks;
using Presentation.Views.Interfaces;
using R3;
using UnityEngine;

namespace Presentation.Views.Buildings
{
  public class FarmView : MonoBehaviour, IBuildingView
  {
    public UniTask ShowAsync()
    {
      throw new NotImplementedException();
    }

    public UniTask HideAsync()
    {
      throw new NotImplementedException();
    }

    public void SetPosition(Vector3 worldPosition)
    {
      throw new NotImplementedException();
    }

    public void SetVisualLevel(int level)
    {
      throw new NotImplementedException();
    }

    public void SetRotation(float angle)
    {
      throw new NotImplementedException();
    }

    public IObservable<Unit> OnUpgradeClickedAsObservable()
    {
      throw new NotImplementedException();
    }

    public IObservable<Unit> OnDeleteClickedAsObservable()
    {
      throw new NotImplementedException();
    }
  }
}

