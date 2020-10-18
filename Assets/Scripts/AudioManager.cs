using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _musicAudio;
    
    void Awake()
    {
        SetUpSingleton();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _musicAudio = FindObjectOfType<AudioSource>();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        _musicAudio.volume = volume;
    }

    public void MuteMusic()
    {
        _musicAudio.mute = true;
    }
    
    public void UnmuteMusic()
    {
        _musicAudio.mute = false;
    }
}
