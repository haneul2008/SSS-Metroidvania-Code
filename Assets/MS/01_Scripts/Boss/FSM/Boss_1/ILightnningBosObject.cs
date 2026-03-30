using UnityEngine;

public class ILightnningBosObject : IAgentComponent<LightningBoss>
{
    public void Initialize(LightningBoss agent)
    {

    }

    public void Initialize(Agent agent) => Initialize(agent as  LightningBoss);
}
