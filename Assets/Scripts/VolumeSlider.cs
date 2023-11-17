using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider: MonoBehaviour
{
    public Slider volumeSlider;  // Reference to the Slider UI element
    public AudioSource audioSource;  // Reference to the AudioSource component

    private const string VolumePrefsKey = "AudioVolume";

    void Start()
    {
        // Load the saved volume from PlayerPrefs
        if (PlayerPrefs.HasKey(VolumePrefsKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey);
            volumeSlider.value = savedVolume;
            audioSource.volume = savedVolume;
        }
        else
        {
            // Set the initial slider value to the current audio volume
            volumeSlider.value = audioSource.volume;
        }

        // Add a listener to the slider value change event
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    // Method called when the slider value changes
    void ChangeVolume(float volume)
    {
        // Update the audio volume based on the slider value
        audioSource.volume = volume;

        // Save the volume to PlayerPrefs for persistence
        PlayerPrefs.SetFloat(VolumePrefsKey, volume);
        PlayerPrefs.Save();
    }
}
