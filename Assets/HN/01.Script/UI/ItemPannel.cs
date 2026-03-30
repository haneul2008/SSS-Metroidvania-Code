using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPannel : MonoBehaviour
{
    [SerializeField] private ItemUI _itemUIPrefab;
    [SerializeField] private DescUI _descUI;

    private List<ItemUI> _currentUIs = new List<ItemUI>();
    private Image _image;
    private HorizontalLayoutGroup _layoutGroup;
    private float _rightPadding;
    private float _spacing;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _layoutGroup = GetComponent<HorizontalLayoutGroup>();
        _rightPadding = _layoutGroup.padding.right;
        _spacing = _layoutGroup.spacing;

        ItemManager.Instance.OnGetItem += HandleGetItem;
    }

    public void HandleGetItem(Item item)
    {
        ItemUI itemUI = Instantiate(_itemUIPrefab, transform);
        itemUI.Initialize(item, _descUI);
        _currentUIs.Add(itemUI);
    }

    private void GenerateSize()
    {
        float x = (_currentUIs.Count - 1) * _spacing + _rightPadding;
        _image.rectTransform.sizeDelta = new Vector2(x, _image.rectTransform.sizeDelta.y);
    }
}
