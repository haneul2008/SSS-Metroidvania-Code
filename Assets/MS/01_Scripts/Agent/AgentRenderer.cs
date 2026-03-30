using UnityEngine;

public class AgentRenderer : MonoBehaviour, IAgentComponent
{
    private Agent _agent;
    public SpriteRenderer SpriteRenderer { get; private set; }

    public void Initialize(Agent agent)
    {
        _agent = agent;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool ReturnFlip()
    {
        return Mathf.Approximately(_agent.transform.eulerAngles.y, 180f);
    }

    public void RightFlip(bool isCanFlip = true)
    {
        if(isCanFlip)
            _agent.gameObject.transform.eulerAngles = Vector3.zero;
        if (!isCanFlip)
            _agent.gameObject.transform.eulerAngles = new Vector3(0, -180f, 0);
    }
}
