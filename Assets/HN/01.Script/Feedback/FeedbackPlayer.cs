using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbacks;

    private void Awake()
    {
        _feedbacks = GetComponents<Feedback>().ToList();
    }

    public void PlayFeedbacks()
    {
        StopFeedbacks();
        _feedbacks.ForEach(f => f.PlayFeedback());
    }

    public void StopFeedbacks()
    {
        _feedbacks.ForEach(f => f.StopFeedback());
    }
}
