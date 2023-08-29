using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController: MonoBehaviour
{
    //References

    public Slider slider;
    [SerializeField] private float sliderValue;
    [SerializeField] private Image muteImage;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        AudioListener.volume = slider.value;
        CheckIfIMute();
    }

    public void ChangeSlider(float valor) 
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumeAudio", sliderValue);
        AudioListener.volume = slider.value;
        CheckIfIMute();
    }

    public void CheckIfIMute() 
    {
        if (sliderValue == 0) 
        {
            muteImage.enabled = true;
        }

        else 
        {
            muteImage.enabled = false;
        }
    }
}
