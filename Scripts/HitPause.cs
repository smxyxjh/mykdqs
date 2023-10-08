using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPause : MonoBehaviour
{
  
    bool waiting = false;
    public void Stop(float duration,float timeScale)
    {
        if (waiting)
        {
            return;
        }
        else
        {
            Time.timeScale = timeScale;
            StartCoroutine(wait(duration));
        }
    }
    IEnumerator wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
