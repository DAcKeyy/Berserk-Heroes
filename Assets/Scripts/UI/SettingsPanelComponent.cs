using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelComponent : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    private void OnEnable()
    {
        musicSlider.value = StaticPrefs.MusicVolume;
        soundSlider.value = StaticPrefs.SoundVolume;
    }

    public void SetSoundValueToPrefs()
    {
        StaticPrefs.MusicVolume = musicSlider.value;
        StaticPrefs.SoundVolume = soundSlider.value;
    }
}
