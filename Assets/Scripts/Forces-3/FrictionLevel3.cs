using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionLevel3 : MonoBehaviour
{

    private float coFriction;
    public Rigidbody2D Block1;
    public float maxFriction;
    public bool running;
    public float appliedForce;
    // Start is called before the first frame update
    void Start()
    {
        coFriction = 0.74f;
        maxFriction = coFriction * Block1.mass * 9.8f;


    }


    private void Update()
    {
        if (running)
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            Block1.gravityScale = 1;

            if (appliedForce <= maxFriction)
            {
                Block1.GetComponentInParent<ConstantForce2D>().force = new Vector2(appliedForce, this.gameObject.GetComponentInParent<ConstantForce2D>().force.y);
            } else
            {
                Block1.GetComponentInParent<ConstantForce2D>().force = new Vector2(appliedForce-maxFriction, this.gameObject.GetComponentInParent<ConstantForce2D>().force.y);
            }
            if (this.gameObject.transform.position.x > 3)
            {
                running = false;
                if (Block1.gameObject.transform.position.x > 3)
                {
                    Debug.Log("Win");
                } else
                {
                    Debug.Log("lose");
                }
            }
        }
    }
}
