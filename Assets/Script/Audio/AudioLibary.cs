using UnityEngine;
[CreateAssetMenu(fileName = "AudioLibary", menuName = "AudioLibary")]
public class AudioLibary : ScriptableObject
{
    // Start is called before the first frame update
    public Sound[] audioSource;
    public AudioClip GetAudioClipsFromName(string name)
    {
        foreach (var audioClip in audioSource)
        {
            if (audioClip.groupID == name)
            {
                return audioClip.audioClip[Random.Range(0, audioClip.audioClip.Length)];
            }
        }
        return null;
    }
}

[System.Serializable]
public class Sound
{
    public string groupID;
    public AudioClip[] audioClip;

}