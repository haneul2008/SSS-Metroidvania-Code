using UnityEngine;

public enum PlayerStateEnum
{
    Idle,
    Move,
    Attack1,
    Attack2,
    Attack3,
    Jump,
    Fall,
    Climb,
    Dead,
    Roll,
    Dash
}

public abstract class PlayerState : State<PlayerStateEnum, PlayerState>
{
    protected Player _player;
    protected InputReader _inputReader;
    protected PlayerMovement _playerMovement;
    protected AgentRenderer _agentRenderer;
    protected bool _endTriggerCalled;
    protected bool _isFlip;

    public PlayerState(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _player = agent as Player;
        _stateMachine = stateMachine;
        _inputReader = _player.GetCompo<InputReader>();
        _playerMovement = _player.GetCompo<PlayerMovement>();
        _agentRenderer = _player.GetCompo<AgentRenderer>();
    }

    public override void Enter()
    {
        base.Enter();

        _inputReader.OnMove += HandleMovement;
        _inputReader.OnJumpEvent += HandleJump;
        _inputReader.OnAttackEvent += HandleAttack;
        _inputReader.OnRollEvent += HandleRoll;
        _endTriggerCalled = false;
    }

    protected virtual void HandleRoll()
    {


    }

    protected virtual void HandleAttack()
    {

    }

    protected virtual void HandleJump()
    {
        if (_playerMovement.IsGround)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _inputReader.OnMove -= HandleMovement;
        _inputReader.OnJumpEvent -= HandleJump;
        _inputReader.OnAttackEvent -= HandleAttack;
        _inputReader.OnRollEvent -= HandleRoll;

    }



    protected virtual void HandleMovement(Vector2 vector)
    {

    }

    public void AnimationEndTrigger()
    {
        _endTriggerCalled = true;
    }



    protected virtual void Flip()
    {

        if (_inputReader.InputVector.x != 0)
        {
            _agentRenderer.RightFlip(_inputReader.InputVector.x > 0);
        }
    }
}
