using System;
using Domain.DTO.Events;
using MessagePipe;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation
{
  /// <summary>
  /// Handles presentation-level responses to input commands.
  /// Currently minimal - most input is handled directly by InputHandler publishing commands.
  /// This can be extended for UI feedback, visual effects, etc.
  /// </summary>
  public class InputPresenter : IInitializable, IDisposable
  {
    private readonly ISubscriber<CameraMoveCommand> _cameraMoveSubscriber;
    private readonly ISubscriber<CameraZoomCommand> _cameraZoomSubscriber;
    private readonly ILogger _logger;
    
    private readonly CompositeDisposable _disposables = new();
    
    [Inject]
    public InputPresenter(
      ISubscriber<CameraMoveCommand> cameraMoveSubscriber,
      ISubscriber<CameraZoomCommand> cameraZoomSubscriber,
      ILogger logger)
    {
      _cameraMoveSubscriber = cameraMoveSubscriber;
      _cameraZoomSubscriber = cameraZoomSubscriber;
      _logger = logger;
    }
    
    public void Initialize()
    {
      _cameraMoveSubscriber.Subscribe(cmd => 
      {
      }).AddTo(_disposables);
      
      _cameraZoomSubscriber.Subscribe(cmd => 
      {
      }).AddTo(_disposables);
    }
    
    public void Dispose()
    {
      _disposables?.Dispose();
    }
  }
}

