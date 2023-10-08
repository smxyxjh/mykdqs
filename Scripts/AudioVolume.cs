using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Text Master;
    public Text Sound;
    public Text Music;
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master",volume);
        Master.text = volume+"";
    }
    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("Sound", volume);
        Sound.text = volume + "";
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        Music.text = volume + "";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
