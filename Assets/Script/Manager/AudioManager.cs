


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource musicAudio, sfxSource, BGMSource;
    [SerializeField] AudioLibary sfxClip, BGM;
    // [SerializeField] SongSO songSO;

    [SerializeField] float musicFadeDurrationSec = 1;
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
        PlaySFX(AudioClipName.SFX_Click);
    }

    private void OnDisable()
    {
        EventDefine.onLose.RemoveListener(StopPlay);
        EventDefine.onStart.RemoveListener(OnStart);
        EventDefine.onSuccessClickOnNote.RemoveListener(OnSuccessClick);
    }
    void StopPlay()
    {
        BGMSource.Stop();

    }
    void OnStart()
    {

        PlaySong(NoteManager.Instance.DataTransferSenceData.GetSelectedSong().SongAudio);
    }
    public void PlaySFX(string soundName)
    {
        Debug.Log("Play SFX" + soundName);
        sfxSource.pitch = Random.Range(.80f, 1.2f);
        sfxSource.PlayOneShot(sfxClip.GetAudioClipsFromName(soundName));
    }
    public void PlayBGM(string musicName)
    {
        AudioClip audioClip = BGM.GetAudioClipsFromName(musicName);
        BGMSource.loop = true;

        StartCoroutine(CrossFadeMusic(BGMSource, audioClip));
    }
    public void PlaySong(AudioClip audioClip)
    {

        BGMSource.loop = false;
        // StartCoroutine(CrossFadeMusic(BGMSource, audioClip));
        BGMSource.clip = audioClip;
        BGMSource.Play();
    }

    IEnumerator CrossFadeMusic(AudioSource audioSource, AudioClip audioClip)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / musicFadeDurrationSec;

            BGMSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;

        }

        audioSource.clip = audioClip;
        audioSource.Play();
        percent = 0;
        yield return null;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / musicFadeDurrationSec;

            audioSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;

        }
    }


}
