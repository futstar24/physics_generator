using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CalculateVelocitiesMomentum3 : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rock;
    public Rigidbody2D astronaut;
    private float rockVI;
    private float astronautVI;
    public TMP_Text resultText;
    public float goalVF;

    public void calculate()
    {
        rockVI = astronaut.velocity.x;
        astronautVI = astronaut.velocity.x;
        Debug.Log(astronaut.mass);
        Debug.Log(rock.mass);
        Vector3 astronautVF = new Vector3(((rock.mass+astronaut.mass)*rockVI-rock.mass*rock.velocity.x)/astronaut.mass,0,0);
        astronaut.velocity = astronautVF;
        resultText.text = "The astronaut's final velocity: " + Math.Round(astronaut.velocity.x,3);
        if (Math.Round(astronaut.velocity.x, 3) == goalVF)
        {
            Debug.Log("win");
        } 
    }
}
