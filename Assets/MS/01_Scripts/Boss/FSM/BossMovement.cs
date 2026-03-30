using UnityEngine;

public class BossMovement : AgentMovement, IAgentComponent<Boss>
{
    private Boss _boss;

    public override void Initialize(Agent agent)
    {
        base.Initialize(agent);

        Initialize(agent as Boss);
    }

    public void Initialize(Boss boss)
    {
        _boss = boss;
    }

    public void CanMoveSet(bool canMove)
    {
        base.CanMove = canMove;
    }
}
