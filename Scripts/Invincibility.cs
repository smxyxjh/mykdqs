using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer render;
    public Color normalCorlor;
    public Color flashCorlor;
    public int duration;// ‹…À…¡À∏¥Œ ˝
    public bool isInvincible;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator SetInvincibility()
    {
        isInvincible = true;
        for(int i=0;i< duration; i++)
        {
            yield return new WaitForSeconds(0.1f);
            render.color = flashCorlor;
            yield return new WaitForSeconds(0.1f);
            render.color = normalCorlor;
        }
        isInvincible = false;
    }
}
