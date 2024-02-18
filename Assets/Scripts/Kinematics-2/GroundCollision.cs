using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GroundCollision : MonoBehaviour
{

    private bool resultGiven = false;
    public TMP_Text resultText;
    public GameObject BallFrame;

    private float leaveGroundX;


    public void OnCollisionExit2D(Collision2D collision)
    {
        leaveGroundX = this.gameObject.transform.position.x;
        Debug.Log("left"+leaveGroundX);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "badGround" && !resultGiven)
        {
            Debug.Log("Lose");
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            resultGiven = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            resultText.text = "Distance Traveled:" + Math.Round((this.gameObject.transform.position.x - leaveGroundX-0.2),1);
        }
        if (collision.gameObject.tag == "winGround" && !resultGiven)
        {
            Debug.Log("Win");
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            resultGiven = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            resultText.text = "Distance Traveled:" + Math.Round((this.gameObject.transform.position.x - leaveGroundX-0.2), 1);
        }
    }

}
