using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
    public void PlaySound(AudioSource clip, float volumeScale, float pitchModifier, Vector3 pos)
    {
        
        //audioSource.pitch = pitchModifier;

        //audioSource.PlayOneShot(clip, volumeScale);

        AudioSource current = Instantiate(clip, pos, Quaternion.identity);
        current.pitch = pitchModifier;
        
        current.volume = volumeScale;
        current.Play();

        Destroy(current, current.clip.length);

        //StartCoroutine(ResetPitch(pitchModifier, clip));

    }

    //Play a clip at volumeScale
    public void PlaySound(AudioSource clip, float volumeScale, Vector3 pos)
    {
        AudioSource current = Instantiate(clip, pos,Quaternion.identity);

        current.volume = volumeScale;
        current.Play();

        Destroy(current, current.clip.length);
    }

    /*public void PlaySoundInList(List<AudioClip> clipList, float volumeScale, float pitchModifier)
    {
        
        int n = Random.Range(0, clipList.Count);
        float originalPitch = audioSource.pitch;

        audioSource.pitch = pitchModifier;

        audioSource.PlayOneShot(clipList[n], volumeScale);

        //audioSource.pitch = originalPitch;
    }*/

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

    /*IEnumerator ResetPitch(float pitchModifier, AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length / pitchModifier);

        audioSource.pitch = 1f;
    }*/


}
