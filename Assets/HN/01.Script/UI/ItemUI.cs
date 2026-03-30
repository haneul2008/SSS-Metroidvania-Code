using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private DescUI _descUI;
    private Image _image;
    private Item _item;

    public void Initialize(Item item, DescUI descUI)
    {
        _image = GetComponent<Image>();
        _item = item;

        _descUI = descUI;

        _image.sprite = _item.ItemData.itemSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descUI.Active(true, _item.ItemData.itemDesc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _descUI.Active(false, _item.ItemData.itemDesc);
    }
}
