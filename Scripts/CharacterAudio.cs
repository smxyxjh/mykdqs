using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource mainAudio;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource landingAudio;
    [SerializeField] AudioSource fallingAudio;
    [SerializeField] AudioSource takeDamageAudio;

    public enum AudioType
    {
        jump,landing,falling,takedamage
    }
    public void Play(AudioType audioType,bool playstate)
    {
        AudioSource audioSource=null;
        switch (audioType)
        {
            case AudioType.jump:
                audioSource = jumpAudio;
                break;
            case AudioType.landing:
                audioSource = landingAudio;
                break;
            case AudioType.falling:
                audioSource = fallingAudio;
                break;
            case AudioType.takedamage:
                audioSource = takeDamageAudio;
                break;
        }
        if (audioSource!=null)
        {
            if (playstate)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
         
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
