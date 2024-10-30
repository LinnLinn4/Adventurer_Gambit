using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXButtonToggle : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource; 
    [SerializeField] private Button sfxButton; 
    [SerializeField] private Image soundOnIcon; 
    [SerializeField] private Image soundOffIcon; 

    private bool isMuted;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SFXMuted"))
        {
            PlayerPrefs.SetInt("SFXMuted", 0); 
        }

        Load();
        UpdateButtonIcon();
    }

    public void ToggleSFX()
    {
        isMuted = !isMuted;
        sfxSource.mute = isMuted;

        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
       
        soundOnIcon.enabled = !isMuted;
        soundOffIcon.enabled = isMuted;
    }

    private void Load()
    {
        isMuted = PlayerPrefs.GetInt("SFXMuted") == 1;
        sfxSource.mute = isMuted;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("SFXMuted", isMuted ? 1 : 0);
    }
}

