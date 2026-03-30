using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Update()
    {
        base.Update();

        if (_playerMovement.CanMove)
            _player.OnPlayerNowMove();

        Flip();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_playerMovement.Rigid.linearVelocityY < 0)//!_playerMovement.IsGround && 
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
        _playerMovement.MoveX(_inputReader.InputVector.x);
    }

    protected override void HandleMovement(Vector2 vector)
    {
        if (vector.x == 0)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
    protected override void HandleAttack()
    {
        base.HandleAttack();
        _stateMachine.ChangeState(PlayerStateEnum.Attack1);
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
