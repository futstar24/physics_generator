using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyProperties : MonoBehaviour
{


    public void applyProperties(string[][] properties, Rigidbody2D obj,string levelName)
    {
     
        if (levelName == "Kinematics1")
        {
            obj.gameObject.GetComponent<Timer>().t = Time.realtimeSinceStartup;
            obj.gameObject.GetComponent<Timer>().timing = true;
        }
        Debug.Log(properties[0]);
        foreach (string[] property in properties)
        {
            if (property[3] == obj.name)
            {
                Debug.Log(properties.ToString());
                if (property[0] == "velocity")
                {
                    if (property[2] == "x")
                    {
                        obj.velocity = new Vector2(float.Parse(property[1]), 0);
                    }
                    else
                    {
                        obj.velocity = new Vector2(0, float.Parse(property[1]));
                    }

                }
                if (property[0] == "mass")
                {
                    obj.mass = float.Parse(property[1]);
                }
                if (property[0] == "acceleration")
                {
                    if (property[2] == "x")
                    {
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(float.Parse(property[1]) * obj.mass, 0);
                    }
                    else
                    {
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(0, float.Parse(property[1]) * obj.mass);
                    }

                }
                if (property[0] == "acceleration")
                {
                    if (property[2] == "x")
                    {
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(float.Parse(property[1]) * obj.mass, 0);
                    }
                    else
                    {
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(0, float.Parse(property[1]) * obj.mass);
                    }

                }
            }

        }
    }

}
