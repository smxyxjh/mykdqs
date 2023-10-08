using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulOrb : MonoBehaviour
{
    Health health;
    [SerializeField]Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<Health>();
        //animator = GetComponent<Animator>();
    }
    public void DelayShowOrb(float  delay)
    {
        StartCoroutine(ShowOrb(delay));

    }
    IEnumerator ShowOrb(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Respawn");
    }
    public void HideOrd()
    {
        animator.SetTrigger("Hide");
    }
    public void ShowHealthItem()
    {
        StartCoroutine(health.ShowHealthItems());
    }
    public void HideHealthItem()
    {
        health.HideHealthItems();
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
