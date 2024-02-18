using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class calculateNewVelocities : MonoBehaviour
{
    public Rigidbody2D block1;
    public Rigidbody2D block2;
    public Vector3 oldV1;
    public Vector3 oldV2;
    public float goalV2;

    public GameObject win;
    public GameObject lose;

    private void Update()
    {
        oldV1 = block1.velocity;
        oldV2 = block2.velocity;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Block2")
        {
            Vector3 newV1 = new Vector3((block1.mass * oldV1.x - block2.mass * oldV1.x + 2 * block2.mass * oldV2.x) / (block1.mass + block2.mass),0,0);
            Debug.Log(newV1);
            Vector3 newV2 = oldV1 + newV1 - oldV2;
            Debug.Log(newV2);
            block1.velocity = newV1;
            block2.velocity = newV2;
            float vel = (float) Math.Round(newV2.x, 3);
            Debug.Log(newV2.x + " "+goalV2);
            StartCoroutine(display(3f, newV2.x));
            
           
        }
        IEnumerator display(float delay, float vel)
        {
            yield return new WaitForSeconds(delay);
            if (Math.Round(vel, 3) == 1.25)
            {
                win.SetActive(true);

                PlayerPrefs.SetInt("Momentum2", 1);
            }
            else
            {
                lose.SetActive(true);
                PlayerPrefs.SetInt("Momentum2", 1);
            }
        }
    }
}
