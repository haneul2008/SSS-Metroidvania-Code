using UnityEngine;

public enum EnemyStateEnum
{
    WalkAround,
    Appear,
    Idle,
    Chase,
    Hit,
    Attack,
    Dead
}

public class EnemyStateMachine : StateMachine<EnemyStateEnum, EnemyState>
{

}
