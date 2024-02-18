using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private float particleTime = 0f;
    private float particleLifetime = 0.2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particleTime = Time.realtimeSinceStartup;
            particleSystem.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0); ;
            particleSystem.Play();
        }
        if(Time.realtimeSinceStartup - particleTime > particleLifetime)
        {
            particleSystem.Stop();
        }
    }
}
