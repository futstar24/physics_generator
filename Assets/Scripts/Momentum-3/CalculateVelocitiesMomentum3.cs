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
    public float goalVF;
    public GameObject winScreen;
    public GameObject loseScreen;

    public void calculate()
    {
        rockVI = astronaut.velocity.x;
        astronautVI = astronaut.velocity.x;
        Debug.Log(astronaut.mass);
        Debug.Log(rock.mass);
        Vector3 astronautVF = new Vector3(((rock.mass+astronaut.mass)*rockVI-rock.mass*rock.velocity.x)/astronaut.mass,0,0);
        astronaut.velocity = astronautVF;
        //resultText.text = "The astronaut's final velocity: " + Math.Round(astronaut.velocity.x,3);
        StartCoroutine(display(3f, astronaut.velocity.x));
        
    }

    IEnumerator display(float delay, float vel)
    {
        yield return new WaitForSeconds(delay);
        if (Math.Round(vel, 3) == 2.5)
        {
            Debug.Log("win");
            winScreen.SetActive(true);
            PlayerPrefs.SetInt("Momentum3", 1);
        }
        else
        {
            loseScreen.SetActive(true);

            PlayerPrefs.SetInt("Momentum3", 0);
        }
    }
}
