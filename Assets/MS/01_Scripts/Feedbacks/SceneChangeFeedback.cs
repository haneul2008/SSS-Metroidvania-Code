using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SceneChangeFeedback : Feedback
{
    public string sceneName;
    public float time;
    public override void PlayFeedback()
    {
        StartCoroutine(TimeScene());
            
    }

    private IEnumerator TimeScene()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }

    public override void StopFeedback()
    {

    }
}