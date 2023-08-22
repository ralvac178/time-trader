using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMusic : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    public void Start()
    {
        slider = this.GetComponent<Slider>();

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            slider.value = PlayerPrefs.GetFloat("MusicVolume");
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            slider.value = 0.15f;  
        }

        UpdateMusicVolume();
    }

    public void UpdateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
    }
}
