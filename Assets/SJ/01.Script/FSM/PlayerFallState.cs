using UnityEngine;

public class PlayerFallState : PlayerState
{
    private Vector2 _beforeVelocity;

    public PlayerFallState(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Update()
    {
        base.Update();

        if (_playerMovement.IsGround)
        {
            _playerMovement.Rigid.linearVelocity = Vector2.zero;
            _playerMovement.OnLandingEvent?.Invoke(_beforeVelocity);

            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        _beforeVelocity = _playerMovement.Rigid.linearVelocity;

        Flip();
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
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _playerMovement.MoveX(_inputReader.InputVector.x);
    }
}
