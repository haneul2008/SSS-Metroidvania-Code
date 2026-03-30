using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private void Start()
    {
        SaveManager.Instance.FileReset();
    }

    public void MoveTitle()
    {
        SceneManager.LoadScene("TitleUi");
    }
}
