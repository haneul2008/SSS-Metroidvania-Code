using System.Threading;
using UnityEngine;

public class PlayerRollState : PlayerState
{
   
    public PlayerRollState(StateMachine<PlayerStateEnum, PlayerState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _playerMovement.StopX();
        _playerMovement.TryToRoll(_agentRenderer.ReturnFlip());
    }

    public override void Exit()
    {
        base.Exit();
        _playerMovement.StopX();

    }



    public override void Update()
    {
        if(_endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
            
        }

        base.Update();

        
    }

}
