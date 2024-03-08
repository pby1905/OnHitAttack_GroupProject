using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2_AudioManager : MonoBehaviour
{
    [Header ("-------------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------------Audio Clip---------")]
    public AudioClip background;
    public AudioClip attack;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
