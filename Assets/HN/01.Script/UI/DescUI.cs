using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DescUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descText;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Active(bool active, string text)
    {
        if(active) _descText.text = text;

        gameObject.SetActive(active);
    }

    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.value;

        transform.position = mousePos;
    }
}
