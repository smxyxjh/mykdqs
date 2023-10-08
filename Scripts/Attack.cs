using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject slash;
    public GameObject altslash;
    public GameObject upslash;
    public GameObject downslash;
    public ContactFilter2D enemyContactFilter;
    
    public enum AttackType
    {
        Slash,
        AltSlash,
        DownSlash,
        UpSlash
    }
    public void Play(AttackType attackType,ref List<Collider2D> colliders)
    {
        switch (attackType)
        {
            case AttackType.Slash:
                Physics2D.OverlapCollider(slash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                //音效
                slash.GetComponent<AudioSource>().Play();
                break;
            case AttackType.AltSlash:
                Physics2D.OverlapCollider(altslash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                //音效
                altslash.GetComponent<AudioSource>().Play();
                break;
            case AttackType.UpSlash:
                Physics2D.OverlapCollider(upslash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                //音效
                upslash.GetComponent<AudioSource>().Play();
                break;
            case AttackType.DownSlash:
                Physics2D.OverlapCollider(downslash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                //音效
                downslash.GetComponent<AudioSource>().Play();
                break;
        }

    }
}
