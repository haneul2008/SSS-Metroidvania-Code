using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _playerMovement.StopX();

        if(_inputReader.InputVector.x != 0)
            _stateMachine.ChangeState(PlayerStateEnum.Move);
    }

    public override void Update()
    {
        base.Update();
        if (_playerMovement.Rigid.linearVelocityY < 0)//!_playerMovement.IsGround && 
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    protected override void HandleAttack()
    {
        base.HandleAttack();
        _stateMachine.ChangeState(PlayerStateEnum.Attack1);
    }

    protected override void HandleMovement(Vector2 vector)
    {
        if(_playerMovement.IsGround)
            _stateMachine.ChangeState(PlayerStateEnum.Move);
    }
    
    protected override void HandleRoll()
    {
        base.HandleRoll();        
        if (_player.RollTimer())
        {
            if (_playerMovement.IsGround)
            {
                _stateMachine.ChangeState(PlayerStateEnum.Roll);
            }
            else
            {
                _stateMachine.ChangeState(PlayerStateEnum.Dash);
            }
        }
    }
}
