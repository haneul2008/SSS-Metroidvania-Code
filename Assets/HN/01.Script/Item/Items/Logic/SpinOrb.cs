using System.Collections.Generic;
using UnityEngine;

public class SpinOrb : Item
{
    private SpinOrbDataSO _spinOrbData;
    private static List<SpinOrbObject> _orbs = new List<SpinOrbObject>();
    private const int _defalutOrbCnt = 2;

    public override void Initialize(ItemDataSO itemData, Player player)
    {
        base.Initialize(itemData, player);

        _spinOrbData = itemData as SpinOrbDataSO;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        SpawnOrb(_defalutOrbCnt);
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        int orbCnt = 0;

        foreach(Item item in ItemManager.Instance.CollectedItems)
        {
            if(item.GetType() == typeof(SpinOrb))
                orbCnt++;
        }

        int maxCnt = (orbCnt + 1) * _defalutOrbCnt;

        SpawnOrb(maxCnt);
    }

    private void SpawnOrb(int maxCnt)
    {
        foreach(SpinOrbObject obj in _orbs)
        {
            GameObject.Destroy(obj.gameObject);
        }

        _orbs.Clear();

        for (int i = 0; i < maxCnt; i++)
        {
            SpinOrbObject spinOrb = GameObject.Instantiate(_spinOrbData.spinOrbObject);
            spinOrb.SetUp(this, Player.transform, i, maxCnt);

            _orbs.Add(spinOrb);
        }
    }
}
