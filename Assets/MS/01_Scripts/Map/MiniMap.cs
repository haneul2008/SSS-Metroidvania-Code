using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private RawImage miniMap;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            miniMap.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            miniMap.gameObject.SetActive(false);
        }
    }
}