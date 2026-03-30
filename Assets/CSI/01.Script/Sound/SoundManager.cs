using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    
    [SerializeField] private SoundListSo _soundListSo;
    [SerializeField] private AudioMixer _mixer;

    public void PlaySound(SoundSo sound)
    {
        SetUpSound(sound);
    }

    public void PlaySound(string soundName)
    {
        if(_soundListSo.SoundsDictionary.TryGetValue(soundName, out SoundSo soundSo))
        {
            SetUpSound(soundSo);
        };
    }

    private void SetUpSound(SoundSo soundSO)
    {
        GameObject obj = new GameObject();
        obj.name = soundSO.soundName + " Sound";
        AudioSource source = obj.AddComponent<AudioSource>();
        if (soundSO.soundType == SoundType.SFX)
            source.outputAudioMixerGroup = _mixer.FindMatchingGroups("Master")[2];
        else if (soundSO.soundType == SoundType.BGM)
        {
            source.outputAudioMixerGroup = _mixer.FindMatchingGroups("Master")[1];
        }
        else
        {
            Debug.LogWarning("Type이 없습니다");
            source.outputAudioMixerGroup = _mixer.FindMatchingGroups("Master")[0];

        }
        SetAudio(source, soundSO);
    }

    private void SetAudio(AudioSource source,SoundSo sounds)
    {
        source.clip = sounds.clip;
        source.loop = sounds.loop;
        source.priority = sounds.Priority;
        source.volume = sounds.volume;

        float randomizePitch = 0;

        if (sounds.randomizePitch != 0)
        {
            randomizePitch = UnityEngine.Random.Range(-sounds.randomizePitch, sounds.randomizePitch);
        }

        source.pitch = sounds.pitch + randomizePitch;

        source.panStereo = sounds.stereoPan;
        source.spatialBlend = sounds.SpatialBlend;
        source.Play();
        if (!sounds.loop) { StartCoroutine(DestroyCo(source.clip.length,source.gameObject)); }

    }

    IEnumerator DestroyCo(float endTime,GameObject obj)
    {
        yield return new WaitForSecondsRealtime(endTime);
        Destroy(obj);
    }
}

public enum SoundType
{
    BGM,
    SFX
}