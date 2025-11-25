using System;
using Domain.DTO.Commands;
using Domain.DTO.Events;
using Domain.Model;
using MessagePipe;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Input
{
  public class InputHandler : IInitializable, IDisposable
  {
    private readonly IPublisher<PlaceBuildingCommand> _placeBuildingPublisher;
    private readonly IPublisher<RemoveBuildingCommand> _removeBuildingPublisher;
    private readonly IPublisher<RotateBuildingCommand> _rotateBuildingPublisher;
    private readonly IPublisher<SelectBuildingTypeCommand> _selectBuildingTypePublisher;
    private readonly IPublisher<CameraMoveCommand> _cameraMovePublisher;
    private readonly IPublisher<CameraZoomCommand> _cameraZoomPublisher;
    
    private Keyboard _keyboard;
    private Mouse _mouse;
    private readonly CompositeDisposable _disposables = new();
    
    private Vector2 _lastMousePosition;
    
    [Inject]
    public InputHandler(
      IPublisher<PlaceBuildingCommand> placeBuildingPublisher,
      IPublisher<RemoveBuildingCommand> removeBuildingPublisher,
      IPublisher<RotateBuildingCommand> rotateBuildingPublisher,
      IPublisher<SelectBuildingTypeCommand> selectBuildingTypePublisher,
      IPublisher<CameraMoveCommand> cameraMovePublisher,
      IPublisher<CameraZoomCommand> cameraZoomPublisher)
    {
      _placeBuildingPublisher = placeBuildingPublisher;
      _removeBuildingPublisher = removeBuildingPublisher;
      _rotateBuildingPublisher = rotateBuildingPublisher;
      _selectBuildingTypePublisher = selectBuildingTypePublisher;
      _cameraMovePublisher = cameraMovePublisher;
      _cameraZoomPublisher = cameraZoomPublisher;
    }
    
    public void Initialize()
    {
      _keyboard = Keyboard.current;
      _mouse = Mouse.current;
      
      SetupKeyboardObservables();
      SetupMouseObservables();
    }
    
    private void SetupKeyboardObservables()
    {
      Observable.EveryUpdate()
        .Where(_ => _keyboard.digit1Key.wasPressedThisFrame)
        .Subscribe(_ => _selectBuildingTypePublisher.Publish(new SelectBuildingTypeCommand(new BuildingTypeId(1))))
        .AddTo(_disposables);
      
      Observable.EveryUpdate()
        .Where(_ => _keyboard.digit2Key.wasPressedThisFrame)
        .Subscribe(_ => _selectBuildingTypePublisher.Publish(new SelectBuildingTypeCommand(new BuildingTypeId(2))))
        .AddTo(_disposables);
      
      Observable.EveryUpdate()
        .Where(_ => _keyboard.digit3Key.wasPressedThisFrame)
        .Subscribe(_ => _selectBuildingTypePublisher.Publish(new SelectBuildingTypeCommand(new BuildingTypeId(3))))
        .AddTo(_disposables);
      
      Observable.EveryUpdate()
        .Where(_ => _keyboard.rKey.wasPressedThisFrame)
        .Subscribe(_ => _rotateBuildingPublisher.Publish(new RotateBuildingCommand(Guid.Empty)))
        .AddTo(_disposables);
      
      Observable.EveryUpdate()
        .Where(_ => _keyboard.deleteKey.wasPressedThisFrame)
        .Subscribe(_ => _removeBuildingPublisher.Publish(new RemoveBuildingCommand(Guid.Empty)))
        .AddTo(_disposables);
      
      Observable.EveryUpdate()
        .Select(_ =>
        {
          Vector2 move = Vector2.zero;
          if (_keyboard.wKey.isPressed) move.y += 1f;
          if (_keyboard.sKey.isPressed) move.y -= 1f;
          if (_keyboard.aKey.isPressed) move.x -= 1f;
          if (_keyboard.dKey.isPressed) move.x += 1f;
          return move;
        })
        .Where(move => move != Vector2.zero)
        .Subscribe(move => _cameraMovePublisher.Publish(new CameraMoveCommand(move * Time.deltaTime)))
        .AddTo(_disposables);
    }
    
    private void SetupMouseObservables()
    {
      Observable.EveryUpdate()
        .Select(_ => _mouse.rightButton.isPressed)
        .Pairwise()
        .Where(pair => pair.Previous && pair.Current)
        .Select(_ =>
        {
          Vector2 currentPos = _mouse.position.ReadValue();
          Vector2 delta = (_lastMousePosition - currentPos) * 0.005f;
          _lastMousePosition = currentPos;
          return delta;
        })
        .Subscribe(delta => _cameraMovePublisher.Publish(new CameraMoveCommand(delta)))
        .AddTo(_disposables);
      
      Observable.EveryUpdate()
        .Subscribe(_ => _lastMousePosition = _mouse.position.ReadValue())
        .AddTo(_disposables);
      
      Observable.EveryUpdate()
        .Select(_ => _mouse.scroll.ReadValue().y)
        .Where(scroll => Mathf.Abs(scroll) > 0.01f)
        .Subscribe(scroll => _cameraZoomPublisher.Publish(new CameraZoomCommand(scroll * 0.05f)))
        .AddTo(_disposables);
    }
    
    public void Dispose()
    {
      _disposables?.Dispose();
    }
  }
}

