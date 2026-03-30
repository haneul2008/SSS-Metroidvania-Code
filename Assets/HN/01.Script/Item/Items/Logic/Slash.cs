using UnityEngine;

public class Slash : Item
{
    private SlashDataSO _slashData;
    private float _lastAttackTime;
    private AgentRenderer _agentRenderer;
    private Collider2D[] _result;
    private SlashPossessionEffect _effect;
    private bool _isEffectPlay;

    public override void Initialize(ItemDataSO itemData, Player player)
    {
        base.Initialize(itemData, player);

        _slashData = itemData as SlashDataSO;
        _result = new Collider2D[_slashData.detectCnt];
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        _agentRenderer ??= Player.GetCompo<AgentRenderer>();

        _effect ??= PoolManager.Instance.Pop("SlashPossessionEffect") as SlashPossessionEffect;
        _effect.PlayEffect(Player.transform.position, Player.transform);
        _isEffectPlay = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Time.time < _lastAttackTime + _slashData.slashCooltime) return;

        PlayEffect();
        CheckTarget();
    }

    private void PlayEffect()
    {
        if (!_isEffectPlay)
        {
            _isEffectPlay = true;
            _effect.PlayEffect(Player.transform.position, Player.transform);
        }
    }

    private void CheckTarget()
    {
        Vector2 playerPos = Player.transform.position;

        int cnt = Physics2D.OverlapCircle(playerPos, _slashData.radius, _slashData.contactFilter, _result);

        if (cnt == 0) return;

        for (int i = 0; i < cnt; i++)
        {
            for (int j = 0; j < _slashData.slashcnt; j++)
            {
                int xDir = _agentRenderer.ReturnFlip() ? -1 : 1;
                Vector2 pos = _result[i].transform.position;

                SlashObject slashObject = PoolManager.Instance.Pop("Slash") as SlashObject;
                slashObject.SetUp(this, pos, _slashData.damage, j * _slashData.slashDelay);

                _lastAttackTime = Time.time;
            }
        }

        _isEffectPlay = false;
        _effect.StopEffect();
    }
}
