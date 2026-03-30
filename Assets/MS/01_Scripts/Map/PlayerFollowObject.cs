using UnityEngine;

public class PlayerFollowObject : MonoBehaviour 
{
    private void Update()
    {
        transform.position = GameManager.Instance.Player.transform.position;
    }
}