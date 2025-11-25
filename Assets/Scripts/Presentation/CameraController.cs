using System;
using Domain.DTO.Events;
using MessagePipe;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation
{
  public class CameraController : IInitializable, IDisposable
  {
    private readonly Camera _camera;
    private readonly ISubscriber<CameraMoveCommand> _cameraMoveSubscriber;
    private readonly ISubscriber<CameraZoomCommand> _cameraZoomSubscriber;

    private float _moveSpeed = 5f;
    private float _zoomSpeed = 2f;
    private float _minZoom = 5f;
    private float _maxZoom = 30f;
    
    private readonly CompositeDisposable _disposables = new();
    
    [Inject]
    public CameraController(
      Camera camera,
      ISubscriber<CameraMoveCommand> cameraMoveSubscriber,
      ISubscriber<CameraZoomCommand> cameraZoomSubscriber)
    {
      _camera = camera;
      _cameraMoveSubscriber = cameraMoveSubscriber;
      _cameraZoomSubscriber = cameraZoomSubscriber;
    }
    
    public void Initialize()
    {
      _cameraMoveSubscriber.Subscribe(HandleCameraMove)
        .AddTo(_disposables);
      
      _cameraZoomSubscriber.Subscribe(HandleCameraZoom)
        .AddTo(_disposables);
    }
    
    public void HandleCameraMove(CameraMoveCommand command)
    {
      if (_camera == null) return;
      
      Vector3 right = _camera.transform.right;
      Vector3 forward = _camera.transform.forward;
      
      right.y = 0f;
      forward.y = 0f;
      
      right.Normalize();
      forward.Normalize();
      
      Vector3 moveDirection = (right * command.Delta.x) + (forward * command.Delta.y);
      _camera.transform.position += moveDirection * _moveSpeed * Time.deltaTime;
    }
    
    public void HandleCameraZoom(CameraZoomCommand command)
    {
      if (_camera == null || !_camera.orthographic) return;
      
      float newSize = _camera.orthographicSize - (command.Delta * _zoomSpeed);
      _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);
    }
    
    public void Dispose()
    {
      _disposables?.Dispose();
    }
  }
}

