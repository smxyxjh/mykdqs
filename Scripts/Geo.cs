using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geo : MonoBehaviour
{
    [SerializeField] private AudioClip[] geoHitGround;
    AudioSource audio;
    public bool isGround;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isGround && collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            isGround = true;
            int index = Random.RandomRange(0, geoHitGround.Length);
            audio.PlayOneShot(geoHitGround[index]);
        }
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
