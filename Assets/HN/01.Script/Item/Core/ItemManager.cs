using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    public Action<Item> OnGetItem;
    public List<Item> CollectedItems { get; private set; } = new List<Item>();

    [SerializeField] private ItemDataListSO _itemList;

    private Dictionary<string, Item> _itemPairs = new Dictionary<string, Item>();

    private void Awake()
    {
        Player player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Debug.LogWarning("no player");
            return;
        }

        foreach (ItemDataSO itemData in _itemList.list)
        {
            Type type = Type.GetType(itemData.className);
            Item item = Activator.CreateInstance(type) as Item;
            item.Initialize(itemData, player);

            _itemPairs.Add(itemData.itemKey, item);
        }
    }

    public void LoadCollectedItem(List<string> itemKeys)
    {
        CollectedItems.Clear();

        foreach (string key in itemKeys)
        {
            CollectItem(key);
        }
    }

    public List<string> GetCollectedItemsKey()
    {
        List<string> keys = new List<string>();
        CollectedItems.ForEach((item) =>
        {
            keys.Add(item.ItemData.itemKey);
        });

        return keys;
    }

    public void GetItem(ItemDataSO itemData)
    {
        CollectItem(itemData.itemKey);

        SaveManager.Instance.JsonSave();
    }

    private void CollectItem(string itemKey)
    {
        if (_itemPairs.TryGetValue(itemKey, out Item item))
        {
            item.OnSpawn();

            foreach(Item collectedItem in CollectedItems)
            {
                if (collectedItem.GetType() == item.GetType())
                    item.OnUpgrade();
            }

            CollectedItems.Add(item);
            OnGetItem?.Invoke(item);
        }
    }

    public void Update()
    {
        if (CollectedItems.Count == 0) return;

        foreach (Item item in CollectedItems)
        {
            item.Update();
        }
    }

    private void FixedUpdate()
    {
        if (CollectedItems.Count == 0) return;

        foreach (Item item in CollectedItems)
        {
            item.FixedUpdate();
        }
    }

    [ContextMenu("GetAllItem")]
    public void GetAllItem()
    {
        foreach (ItemDataSO itemData in _itemList.list)
        {
            print(itemData);
            Item item = _itemPairs.GetValueOrDefault(itemData.itemKey);

            if (item == null) return;

            CollectedItems.Add(item);
            item.OnSpawn();
        }
    }

    private void OnDestroy()
    {
        foreach (Item item in CollectedItems)
        {
            item.OnDestroy();
        }

        OnGetItem = null;
    }
}
