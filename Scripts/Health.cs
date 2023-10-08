using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator[] healthItem;
    public Animator geo;
    private CharactorData charactorData;
    // Start is called before the first frame update
    void Start()
    {
        charactorData = FindObjectOfType<CharactorData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hurt()// ‹…À∑Ω∑®
    {
        if (charactorData.GetDeadStatement())
            return;
        charactorData.LoseHealth(1);
        int health = charactorData.GetCurrentHealt();
        if (health < 0)
        {
            health = 0;
        }
        healthItem[health].SetTrigger("Hurt");
    }
    public IEnumerator ShowHealthItems()
    {
        for(int i = 0; i < healthItem.Length; i++)
        {
            healthItem[i].SetTrigger("ResPawn");
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.2f);
        geo.Play("Enter");
    }
    public void HideHealthItems()
    {
        geo.Play("Exit");
        for (int i = 0; i < healthItem.Length; i++)
        {
            healthItem[i].SetTrigger("Hide");
           
        }
    }
}
