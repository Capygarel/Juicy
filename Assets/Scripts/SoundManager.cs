using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource audioSource, musicSource;

    //Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //Play a clip at volumeScale, pitchModifier not working
    public void PlaySound(AudioClip clip, float volumeScale, float pitchModifier)
    {
        float originalPitch = audioSource.pitch;
        Debug.Log(pitchModifier);
        audioSource.pitch -= pitchModifier;
        audioSource.PlayOneShot(clip, volumeScale);
        audioSource.pitch = originalPitch;
    }

    //Play a clip at volumeScale
    public void PlaySound(AudioClip clip, float volumeScale)
    {
        audioSource.PlayOneShot(clip, volumeScale);
    }

    public void PlaySoundInList(List<AudioClip> clipList, float volumeScale)
    {
        
        int n = Random.Range(0, clipList.Count);
        Debug.Log(clipList[n]);
        audioSource.PlayOneShot(clipList[n], volumeScale);
    }

    //Change background music
    public void SetMusicSource(AudioClip music)
    {
        musicSource.clip = music;
        SetMusicActive(true);
    }

    //Play or stop the music when in menus
    public void SetMusicActive(bool Active)
    {
        if (Active)
        {
            musicSource.Play();
        }
        else
            musicSource.Stop();
    }


}
