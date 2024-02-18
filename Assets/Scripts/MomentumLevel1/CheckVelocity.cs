using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CheckVelocity : MonoBehaviour
{
    public Rigidbody2D block;
    public double velocityGoal;

    public GameObject win;
    public GameObject lose;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("here");
        if (collision.gameObject.name == "Block2")
        {
            Debug.Log("here2");
            float vel = (float)Math.Round(block.velocity.x,3);
            StartCoroutine(display(3f, block.velocity.x));
            
        } 
    }

    IEnumerator display(float delay, float vel)
    {
        yield return new WaitForSeconds(delay);
        if (vel == 4)
        {
            win.SetActive(true);
            PlayerPrefs.SetInt("Momentum1", 1);
        }
        else
        {
            lose.SetActive(true);
            PlayerPrefs.SetInt("Momentum1", 0);
        }
    }
}
