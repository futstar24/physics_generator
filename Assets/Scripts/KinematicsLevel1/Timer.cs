using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{

    public float t;
    public bool timing;
    public TMP_Text yourTimeText;
    public double timeGoal;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        float runTime = Time.realtimeSinceStartup - t - (float)0.03;
        timing = false;
        yourTimeText.text = "Your time: " + Math.Round(runTime, 2).ToString();
        if (Math.Abs(Math.Round(runTime, 2)-timeGoal) < 0.03)
        {
            Debug.Log("W");
        }
    }

    public void Update()
    {
        if (timing)
        {
            yourTimeText.text = "Your time: "+ Math.Round((Time.realtimeSinceStartup - t), 2).ToString();
        }
    }
}

