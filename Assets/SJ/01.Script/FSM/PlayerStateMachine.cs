using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateEnum, PlayerState>
{
    private Player _player;

    public override void Initialize(PlayerStateEnum initEnum, Agent agent)
    {
        base.Initialize(initEnum, agent);

        _player = agent as Player;
    }

    public override void ChangeState(PlayerStateEnum newStateEnum)
    {
        base.ChangeState(newStateEnum);

        _player.OnPlayerStateChange?.Invoke(CurrentState);
    }
}
