using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeoCollect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator collectAni;
    [SerializeField] AudioClip[] geoCollect;
    [SerializeField] AudioSource audioSource;
    [SerializeField] int geoCount = 0;
    [SerializeField] Text geoText;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        geoText.text = geoCount.ToString();

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)//¼ñµ½½ð±Ò
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Geo"))
        {
            collectAni.SetTrigger("collect");
            int index = Random.Range(0, geoCollect.Length);
            audioSource.PlayOneShot(geoCollect[index]);
            geoCount++;
            geoText.text = geoCount.ToString();
            Destroy(collision.gameObject);
        }
    }
    void Update()
    {
        
    }
}
