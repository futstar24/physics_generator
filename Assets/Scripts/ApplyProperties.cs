using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplyProperties : MonoBehaviour
{
    public TextMeshPro inputText;

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

    public void OnChanged(string str)
    {
        string[] keywords = str.Split(" ");
        string input = new string("");
        for (int i = 0; i < keywords.Length; i++)
        {
            if (keywords[i] == "height")
            {
                input += "<color=blue>height</color>";
            }
            else if (keywords[i] == "acceleration")
            {

            }
            else if (keywords[i] == "mass")
            {

            }
            else if (keywords[i] == "velocity")
            {

            }
            else if (keywords[i] == "angle")
            {

            }
        }
        //add more
    }

}
