using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource musicAudio, sfxAudio;
    [SerializeField] AudioClip[] musicClip, sfxClip;
    string _HYLT = "HYLT";
    string _Click = "Click";
    void OnEnable()
    {
        EventDefine.onLose.AddListener(StopPlay);
        EventDefine.onStart.AddListener(OnStart);
        EventDefine.onSuccessClickOnNote.AddListener(OnSuccessClick);
    }

    private void OnSuccessClick()
    {
        PlaySFX("Cling");
    }

    private void OnDisable()
    {
        EventDefine.onLose.RemoveListener(StopPlay);
        EventDefine.onStart.RemoveListener(OnStart);
        EventDefine.onSuccessClickOnNote.RemoveListener(OnSuccessClick);
    }
    void StopPlay()
    {
        musicAudio.Stop();

    }
    void OnStart()
    {

        PlayMusic(_HYLT);
    }
    void PlayMusic(string songName)
    {
        AudioClip audioClip = Array.Find(musicClip, x => x.name == songName);
        if (audioClip == null) return;
        musicAudio.clip = audioClip;
        musicAudio.Play();
    }
    void PlaySFX(string vfxName)
    {
        AudioClip audioClip = Array.Find(sfxClip, x => x.name == vfxName);
        if (audioClip == null) return;
        sfxAudio.Stop();
        sfxAudio.pitch = UnityEngine.Random.Range(1f, 3f);
        sfxAudio.PlayOneShot(audioClip);
    }


}
