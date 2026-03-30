using UnityEngine;

public interface IAgentComponent
{
    public void Initialize(Agent agent);
}

public interface IAgentComponent<T> : IAgentComponent
{
    public void Initialize(T agent);
}
