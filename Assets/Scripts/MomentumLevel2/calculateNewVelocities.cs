using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class calculateNewVelocities : MonoBehaviour
{
    public Rigidbody2D movingBlock;
    public Rigidbody2D stationaryBlock;
    public Vector3 oldV1;
    public Vector3 oldV2;
    public float goalV2;

    private void Update()
    {
        oldV1 = movingBlock.velocity;
        oldV2 = stationaryBlock.velocity;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Block2")
        {
            Vector3 newV1 = new Vector3((movingBlock.mass * oldV1.x - stationaryBlock.mass * oldV1.x + 2 * stationaryBlock.mass * oldV2.x) / (movingBlock.mass + stationaryBlock.mass),0,0);
            Debug.Log(newV1);
            Vector3 newV2 = oldV1 + newV1 - oldV2;
            Debug.Log(newV2);
            movingBlock.velocity = newV1;
            stationaryBlock.velocity = newV2;
            Debug.Log(newV2.x + " "+goalV2);
            if (Math.Round(newV2.x,3) == goalV2)
            {
                Debug.Log("Win");
            }
           
        }
    }
}
