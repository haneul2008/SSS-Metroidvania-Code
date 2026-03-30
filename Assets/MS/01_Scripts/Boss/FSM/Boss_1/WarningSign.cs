using UnityEngine;

public class WarningSign : MonoBehaviour, IAgentComponent<LightningBoss>
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rbCompo;

    private float _startTime;
    private float DEALAY_TIME = 2.75f;

    private LightningBoss _lightningBoss;

    public void Initialize(LightningBoss agent)
    {
        _lightningBoss = agent;
        _startTime = Time.time;
    }
    public void Initialize(Agent agent) => Initialize(agent as LightningBoss);

    private void FixedUpdate()
    {
        if (_startTime + DEALAY_TIME < Time.time)
        {

            GetComponent<SpriteRenderer>().color = Color.red;
            return;
        };
        _rbCompo.linearVelocity = (UtilityClass.TargetDirectionXORY(new Vector2(1,0),_lightningBoss.targetTrm,transform) * _speed) + new Vector2(0,0.5f);
    }
}
