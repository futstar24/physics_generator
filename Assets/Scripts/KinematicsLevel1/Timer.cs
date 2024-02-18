using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{

    public float t;
    public bool timing;
    public double timeGoal;
    public GameObject win;
    public GameObject lose;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        float runTime = Time.realtimeSinceStartup - t - (float)0.03;
        timing = false;
        float yourTime = (float)Math.Round(runTime, 2);
        if (Math.Abs(Math.Round(runTime, 2)-timeGoal) < 0.03)
        {
            win.SetActive(true);
        } else
        {
            lose.SetActive(true);
        }
    }

    public void Update()
    {
        if (timing)
        {
            float yourTime = (float)Math.Round((Time.realtimeSinceStartup - t), 2);
        }
    }
}

