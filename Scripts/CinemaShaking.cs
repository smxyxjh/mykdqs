using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaShaking : MonoBehaviour//ÆÁÄ»Õð¶¯·½·¨
{
    private Cinemachine.CinemachineImpulseSource myImpulse;
    // Start is called before the first frame update
    void Start()
    {
      myImpulse = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

    // Update is called once per frame
    public void CinemaShake()
    {
        myImpulse.GenerateImpulse();
    }
    void Update()
    {
        
    }
}
