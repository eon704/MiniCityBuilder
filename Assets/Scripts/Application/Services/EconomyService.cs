using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using R3;
using VContainer;
using VContainer.Unity;

namespace Application.Services
{
  public class EconomyService : IInitializable, IDisposable
  {
    public ReadOnlyReactiveProperty<int> CurrentGold => _currentGold;
        
    [Inject] private readonly IBuildingRepository _buildingRepository;
        
    private readonly ReactiveProperty<int> _currentGold = new(1000); // Initial gold
    private CancellationTokenSource _incomeCts;

    public void Initialize() { }
    public void Dispose() { _incomeCts?.Cancel(); }

    public bool TrySpend(Cost cost)
    {
      throw new NotImplementedException();
    }

    public void AddIncome(int amount)
    {
      throw new NotImplementedException();
    }

    public void StartPassiveIncomeLoop(float tickIntervalSeconds)
    {
      throw new NotImplementedException();
    }
        
    // UniTaskVoid is used for fire-and-forget background operations
    private async UniTaskVoid IncomeLoopInternal(float tickIntervalSeconds) { }
  }
}