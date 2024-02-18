using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PullForceLevel : MonoBehaviour
{
    private bool result = false;
    public TMP_Text resultText;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x > 3 && !result)
        {
            result = true;
            resultText.text = "Your Velocity: " + Math.Round(this.gameObject.GetComponent<Rigidbody2D>().velocity.x, 2);
            if (Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x-15) <= 5)
            {
                Debug.Log("Win");
                Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().velocity.x);
            } else
            {
                Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().velocity.x);
            }

        }
    }
}
