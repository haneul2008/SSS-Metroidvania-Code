using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (!_playerMovement.CanMove)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }

        _playerMovement.StopX();
        _playerMovement.TryToJump();
    }

    public override void Exit()
    {
        base.Exit();
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
    protected override void HandleAttack()
    {
        base.HandleAttack();
        _stateMachine.ChangeState(PlayerStateEnum.Attack1);
    }

    public override void Update()
    {
        base.Update();
        if (_playerMovement.IsGround && _playerMovement.Rigid.linearVelocityY < 0.8f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        Flip();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!_playerMovement.IsGround && _playerMovement.Rigid.linearVelocityY < 0)//!_playerMovement.IsGround && 
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
        _playerMovement.MoveX(_inputReader.InputVector.x);
    }
}



/*
 * <size=80><b>Echos of Ruin</b></size>

<size=40><size=60><b>S</b></size>kill <size=60><b>S</b></size>culptor <size=60><b>S</b></size>tudio</size>


게발자 :  박하늘
\t\t장준서
\t\t고민수
\t\t박시우
\t\t박성제

보스 | 고민수
플레이어 | 박하늘
음향 찾기 | 박성제
플레이어 버그 수정 | 장준서
플레이어 | 박성제
맵 | 박시우
아이템 | 박하늘
잡다한 거 | 장준서
적 | 박하늘
쉐이더 | 박하늘
타이틀 UI | 박시우
카메라 이동 | 고민수
음향 | 장준서
타이틀 세부 조정 | 장준서
이팩트 | 고민수


 */