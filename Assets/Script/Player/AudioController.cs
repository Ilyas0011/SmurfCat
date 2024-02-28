using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource deathAudio;
    [SerializeField] private AudioSource runAudio;
    [SerializeField] private AudioSource fallAudio;
    [SerializeField] private AudioSource pickingAudio;
    [SerializeField] private AudioSource forestAudio;

    void Start()
    {
        GameObject forestMusicObject = GameObject.FindGameObjectWithTag("Music");
        if (forestMusicObject != null)
        {
            // Получить доступ к АудиоСоурсу на найденном объекте
            forestAudio = forestMusicObject.GetComponent<AudioSource>();
        }
    }

    public void StopForestAudio()
    {
        if (forestAudio != null && forestAudio.isPlaying)
        {
            forestAudio.Stop();
        }
    }

    public void PlayJumpAudio()
    {
        if (jumpAudio != null && !jumpAudio.isPlaying)
        {
            jumpAudio.Play();
        }
    }

    public void PlayMusic()
    {
        if (musicAudio != null)
        {
            musicAudio.Play();
        }
    }

    public void StopMusic()
    {
        if (musicAudio != null && musicAudio.isPlaying)
        {
            musicAudio.Stop();
        }
    }

    public void PlayDeathSound()
    {
        if (deathAudio != null)
        {
            deathAudio.Play();
        }
    }

    public void PlayRunAudio()
    {
        if (runAudio != null && !runAudio.isPlaying)
        {
            runAudio.Play();
        }
    }

    public void StopRunAudio()
    {
        if (runAudio != null && runAudio.isPlaying)
        {
            runAudio.Stop();
        }
    }

    public void PlayFallAudio()
    {
        if (fallAudio != null)
        {
            fallAudio.Play();
        }
    }

    public void StopFallAudio()
    {
        if (fallAudio != null && fallAudio.isPlaying)
        {
            fallAudio.Stop();
        }
    }

    public void PlayPickingAudio()
    {
        if (pickingAudio != null && !pickingAudio.isPlaying)
        {
            pickingAudio.Play();
        }
    }
}
