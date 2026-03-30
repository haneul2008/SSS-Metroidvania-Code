using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private HeartUI healthUI;

    private void Awake()
    {
        healthUI = GetComponentInChildren<HeartUI>();
    }

    public void InitializeMaxHealth(int maxHealth)
    {
        healthUI.Initialized(maxHealth);
    }
}   

