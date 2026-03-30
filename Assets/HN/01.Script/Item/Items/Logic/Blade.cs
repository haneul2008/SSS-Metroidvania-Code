using UnityEngine;

public class Blade : Item
{
    private AgentRenderer _agentRenderer;
    private BladeDataSO _bladeData;

    public override void Initialize(ItemDataSO itemData, Player player)
    {
        base.Initialize(itemData, player);

        _bladeData = itemData as BladeDataSO;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        _agentRenderer ??= Player.GetCompo<AgentRenderer>();

        Player.OnAttackEvent += HandleAttack;
    }

    private void HandleAttack(bool isAttackSuccess)
    {
        if (isAttackSuccess) Attack();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        Player.OnAttackEvent -= HandleAttack;
    }

    private void Attack()
    {
        bool isFlip = _agentRenderer.ReturnFlip();
        Vector2 dir = isFlip ? Vector2.left : Vector2.right;

        BladeObject bladeObject = PoolManager.Instance.Pop("BladeObject") as BladeObject;
        bladeObject.SetUp(this, Player.transform.position, dir, _bladeData.damage, _bladeData.speed, _bladeData.knockbackPower);
    }
}
