using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GroundCollision : MonoBehaviour
{

    private bool resultGiven = false;

    public GameObject winScreen;
    public GameObject loseScreen;
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
            loseScreen.SetActive(true);
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            resultGiven = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
        }
        if (collision.gameObject.tag == "winGround" && !resultGiven)
        {
            Debug.Log("Win");
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            resultGiven = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            winScreen.SetActive(true);
        }
    }

}
