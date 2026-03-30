using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundListSo", menuName = "SO/Sound/SoundListSo")]
public class SoundListSo : ScriptableObject
{
    [SerializeField] private List<SoundSo> Sounds;

    public Dictionary<string, SoundSo> SoundsDictionary;
    private void OnValidate()
    {
        SoundsDictionary = new Dictionary<string, SoundSo>();
        foreach (SoundSo soundSo in Sounds)
        {
            SoundsDictionary[soundSo.soundName] = soundSo;
        }
    }
}
