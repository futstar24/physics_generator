using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrictionLevel1 : MonoBehaviour
{
    // Start is called before the first frame update

    private float prevX;
    private float prevY;
    public double angle;
    public float coefficientOfFriction;

    public void Start()
    {
        this.gameObject.GetComponentInParent<Rigidbody2D>().rotation = 42.8f;
        angle = (42.8)*(Math.PI/180);
        coefficientOfFriction = 0.74f;
    }

    public void setFriction()
    {
        this.gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = 1;
        prevX = this.gameObject.transform.position.x;
        prevY = this.gameObject.transform.position.y;
        Debug.Log(((float)Math.Cos(angle)));
        this.gameObject.GetComponentInParent<ConstantForce2D>().force = new Vector2(coefficientOfFriction*this.gameObject.GetComponentInParent<Rigidbody2D>().mass*9.8f* ((float)Math.Cos(angle))* ((float)Math.Cos(angle)) + this.gameObject.GetComponentInParent<ConstantForce2D>().force.x, coefficientOfFriction * this.gameObject.GetComponentInParent<Rigidbody2D>().mass * 9.8f * ((float)Math.Cos(angle))*((float)Math.Sin(angle)) + this.gameObject.GetComponentInParent<ConstantForce2D>().force.y);

        StartCoroutine(WaitForFall(3f));
    }

    IEnumerator WaitForFall (float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Math.Abs(this.gameObject.transform.position.x-prevX)<0.1 && Math.Abs(this.gameObject.transform.position.y - prevY) < 0.1)
        {
            Debug.Log("win");
        } else
        {
            Debug.Log("Lose");
        }
    }
}
