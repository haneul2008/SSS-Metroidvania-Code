using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    private Image image;
    public UnityEvent OnSpriteChange;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
