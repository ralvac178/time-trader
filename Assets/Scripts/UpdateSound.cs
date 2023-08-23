using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSound : MonoBehaviour
{
    private Slider slider;
    private AudioSource[] audioSources;
    // Start is called before the first frame update
    public void Start()
    {
        slider = GetComponent<Slider>();
        audioSources = GameObject.Find("GameData").GetComponents<AudioSource>();

        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            slider.value = PlayerPrefs.GetFloat("SoundVolume");
            for (int i = 1; i < audioSources.Length; i++)
            {
                audioSources[i].volume = PlayerPrefs.GetFloat("SoundVolume");
            }

        }
        else
        {
            slider.value = 0.25f;
        }

        UpdateSoundVolume();
    }

    // Update is called once per frame
    public void UpdateSoundVolume()
    {
        PlayerPrefs.SetFloat("SoundVolume", slider.value);

        for (int i = 1; i < audioSources.Length; i++)
        {
            audioSources[i].volume = slider.value;
        }
    }
}
