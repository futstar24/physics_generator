using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CheckVelocity : MonoBehaviour
{
    public Rigidbody2D block;
    public TMP_Text yourVelocityText;
    public double velocityGoal;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("here");
        if (collision.gameObject.name == "bigBlock")
        {
            Debug.Log("here2");
            yourVelocityText.text = "Your velocity: " + Math.Round(block.velocity.x,3);
            if (block.velocity.x == velocityGoal)
            {
                Debug.Log("Win");
            }
        }
    }
}
