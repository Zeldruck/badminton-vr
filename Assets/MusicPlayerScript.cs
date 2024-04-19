using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    AudioSource musicSource;
    public AudioClip[] clip_list;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        // PlayRandomOrShut();
    }

    public void PlayRandomOrShut() {
        if (musicSource.isPlaying)
        {
            Debug.Log("Stop");
            musicSource.Stop();
        }
        else
        {
            Debug.Log("Play");
            musicSource.clip = clip_list[Random.Range(0, clip_list.Length)];
            musicSource.PlayOneShot(musicSource.clip);
        }
    }
}
