using System;
using UnityEngine;

public class Item
{
    public ItemDataSO ItemData { get; protected set; }
    public Player Player { get; protected set; }

    public virtual void Initialize(ItemDataSO itemData, Player player)
    {
        ItemData = itemData;
        Player = player;
    }

    public virtual void OnSpawn()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void OnDestroy()
    {

    }

    public virtual void OnUpgrade()
    {

    }
}
