using System;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource MusicAudio;
    [SerializeField] private AudioSource SoundAudio;

    private void OnEnable()
    {
        MusicAudio.volume = StaticPrefs.MusicVolume;
        SoundAudio.volume = StaticPrefs.SoundVolume;
    }

    private void FixedUpdate()
    {
        //Решил не партиься
        MusicAudio.volume = StaticPrefs.MusicVolume;
        SoundAudio.volume = StaticPrefs.SoundVolume;
    } 
}
