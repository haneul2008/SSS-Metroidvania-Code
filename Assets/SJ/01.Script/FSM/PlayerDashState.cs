public class PlayerDashState : PlayerState
{
    public PlayerDashState(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _playerMovement.StopX();
        _playerMovement.TryToDash(_agentRenderer.ReturnFlip(), () => _stateMachine.ChangeState(PlayerStateEnum.Fall));
    }

    public override void Exit()
    {
        base.Exit();
        _playerMovement.StopX();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public override void Update()
    {
        base.Update();

        if (_playerMovement.IsGround || _endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
