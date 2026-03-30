using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static KeyAction;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerInputActions, IAgentComponent<Player>
{
    public event Action<Vector2> OnMove;
    public event Action OnJumpEvent;
    public event Action OnAttackEvent;
    public event Action OnRollEvent;
    public event Action OnDashEvnet;

    public Vector2 InputVector { get; private set; }

    private KeyAction _playerInputAction;
    private Player _player;

    public void Initialize(Agent agent) => Initialize(agent as Player);

    public void Initialize(Player player)
    {
        _player = player;
    }

    private void OnEnable()
    {
        if (_playerInputAction == null)
        {
            _playerInputAction = new KeyAction();
            _playerInputAction.PlayerInput.SetCallbacks(this);
        }
        _playerInputAction.PlayerInput.Enable();
    }



    private void OnDisable()
    {
        _playerInputAction.PlayerInput.Disable();
    }

    public void OnOnMove(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
        OnMove?.Invoke(InputVector);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnJumpEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnAttackEvent?.Invoke();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnRollEvent?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnRollEvent?.Invoke();
    }
}
