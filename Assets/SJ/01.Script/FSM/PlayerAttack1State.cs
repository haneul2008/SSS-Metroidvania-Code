public class PlayerAttack1State : PlayerState
{
    public PlayerAttack1State(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    private float moveSpeedSave;

    public override void Enter()
    {
        base.Enter();

        _playerMovement.StopX();
        moveSpeedSave = _playerMovement.moveSpeed;
        if (_playerMovement.IsGround)
        {
            _playerMovement.moveSpeed = moveSpeedSave/3; 
        }
        else
        {
            _playerMovement.moveSpeed = moveSpeedSave; 
        }
    }


    public override void Exit()
    {
        base.Exit();

        _playerMovement.moveSpeed = moveSpeedSave;
    }

    public override void Update()
    {
        base.Update();

        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        Flip();
    }

    public override void FixedUpdate()
    {
        _playerMovement.MoveX(_inputReader.InputVector.x);
    }
}
