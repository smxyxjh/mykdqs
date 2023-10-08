using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]ParticleSystem falltrail;
    [SerializeField] ParticleSystem burstRocks;
    [SerializeField] ParticleSystem lowfheakth;
    [SerializeField] ParticleSystem hitL;
    [SerializeField] ParticleSystem hitR;
    [SerializeField] ParticleSystem dustJump;
    public enum EffectType
    {
        Falltrail, BurstRocks, Lowfheakth,HitL,HitR, DustJump
    }
    public void DoEffect(EffectType effectType,bool enabled)
    {
        ParticleSystem particleSystem=null;
        switch (effectType)
        {
            case EffectType.Falltrail:
                particleSystem = falltrail;
                break;
            case EffectType.BurstRocks:
                particleSystem = burstRocks;
                break;
            case EffectType.Lowfheakth:
                particleSystem = lowfheakth;
                break;
            case EffectType.HitL:
                particleSystem = hitL;
                break;
            case EffectType.HitR:
                particleSystem = hitR;
                break;
            case EffectType.DustJump:
                particleSystem = dustJump;
                break;
        }
        if (particleSystem != null)
        {
            if (enabled)
            {
                particleSystem.Play();
            }
            else
            {
                particleSystem.Stop();
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
