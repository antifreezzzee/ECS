using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;
    private InputAction _moveAction;
    private float2 _moveInput;
    private InputAction _rushAction;
    private float _rushInput;
    private InputAction _shootAction;
    private float _shootInput;


    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        _moveAction = new InputAction("move", binding: "<Gamepad>/rightstick");
        _moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.Enable();

        _shootAction = new InputAction("shoot", binding: "<Keyboard>/Space");
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.Enable();

        _rushAction = new InputAction("rush", binding: "<Keyboard>/z");
        _rushAction.performed += context => { _rushInput = context.ReadValue<float>(); };
        _rushAction.started += context => { _rushInput = context.ReadValue<float>(); };
        _rushAction.canceled += context => { _rushInput = context.ReadValue<float>(); };
        _rushAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _shootAction.Disable();
        _rushAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery)
            .ForEach((Entity entity, ref InputData inputData) =>
            {
                inputData.Move = _moveInput;
                inputData.Shoot = _shootInput;
                inputData.Rush = _rushInput;
            });
    }
}