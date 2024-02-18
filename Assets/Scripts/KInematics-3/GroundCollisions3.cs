using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GroundCollision3 : MonoBehaviour
{

    private bool resultGiven = false;
    public TMP_Text resultText;
    public GameObject BallFrame;

    private float leaveGroundX;

    private void Start()
    {
        Debug.Log(this.gameObject.transform.position.x);
        leaveGroundX = this.gameObject.transform.position.x;
        Debug.Log(leaveGroundX);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "badGround" && !resultGiven)
        {
            Debug.Log("Lose");
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            resultGiven = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            resultText.text = "Distance Traveled:" + Math.Round((this.gameObject.transform.position.x - leaveGroundX - 0.3), 1);
        }
        if (collision.gameObject.tag == "winGround" && !resultGiven)
        {
            Debug.Log("Win");
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            resultGiven = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            resultText.text = "Distance Traveled:" + Math.Round((this.gameObject.transform.position.x - leaveGroundX - 0.3), 1);
        }
    }

}
