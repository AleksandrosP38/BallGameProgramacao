using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider: MonoBehaviour
{
    public Slider volumeSlider;  
    public AudioSource audioSource;  

    private const string VolumePrefsKey = "AudioVolume";

    void Start()
    {
        if (PlayerPrefs.HasKey(VolumePrefsKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey);
            volumeSlider.value = savedVolume;
            audioSource.volume = savedVolume;
        }
        else
        {
            volumeSlider.value = audioSource.volume;
        }

        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float volume)
    {
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(VolumePrefsKey, volume);
        PlayerPrefs.Save();
    }
}
