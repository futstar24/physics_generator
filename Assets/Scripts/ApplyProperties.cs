using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplyProperties : MonoBehaviour
{
    public TextMeshPro inputText;

    public void applyProperties(string[][] properties, Rigidbody2D obj,string levelName)
    {

        if (levelName == "Kinematics-1")
        {
            obj.gameObject.GetComponent<Timer>().t = Time.realtimeSinceStartup;
            obj.gameObject.GetComponent<Timer>().timing = true;
            obj.gravityScale = 1;
        }
        else if (levelName == "Kinematics-2" || levelName == "Kinematics-3")
        {
            obj.gravityScale = 1;
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
                        obj.velocity = new Vector2(float.Parse(property[1]), obj.velocity.y);
                    }
                    else
                    {
                        obj.velocity = new Vector2(obj.velocity.x, float.Parse(property[1]));
                    }

                }
                if (property[0] == "mass")
                {
                    obj.mass = float.Parse(property[1]);
                    Debug.Log("set mass");
                }
                if (property[0] == "acceleration")
                {
                    if (property[2] == "x")
                    {
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(float.Parse(property[1]) * obj.mass, obj.GetComponentInParent<ConstantForce2D>().force.y);
                    }
                    else
                    {
                        obj.gravityScale = 0;
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(obj.GetComponentInParent<ConstantForce2D>().force.x, float.Parse(property[1]) * obj.mass);
                    }

                }
            }

        }

        StartCoroutine(DelayThenFall(0.05f,levelName,obj));
    

    
        
    }

IEnumerator DelayThenFall(float delay,string levelName, Rigidbody2D obj)
{
    yield return new WaitForSeconds(delay);
    if (levelName == "Momentum-3" && obj.name == "SpaceRock")
    {
        obj.GetComponent<CalculateVelocitiesMomentum3>().calculate();
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
