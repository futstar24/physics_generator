using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class MometumLevel1 : MonoBehaviour
{

    public TMP_InputField input;
    public Rigidbody2D[] levelObjects;
    public String[] levelObjectNames;
    public string[] propertyKeyWords;
    public TMP_Text propertiesText;
    public List<string[]> properties = new List<string[]>();
    public int levelHeight;
    public List<String> inputs;

    private void Start()
    {
        propertiesText.text = "";
        levelHeight = 7;
        double[] possibleFinalVelocities = { 1.15 };
        System.Random random = new System.Random();
        int randomIndex = random.Next(possibleFinalVelocities.Length);
        inputs = new List<string>();
        foreach (Rigidbody2D obj in levelObjects)
        {
            obj.gravityScale = 0;
            if (obj.gameObject.GetComponent<Timer>() != null)
            {
                obj.gameObject.GetComponent<Timer>().timeGoal = possibleFinalVelocities[randomIndex];
            }
        }


    }


    public void submitText()
    {

        List<string> words = new List<string>(input.text.Split(new string[] { " ", "'", "," }, StringSplitOptions.RemoveEmptyEntries));
        Debug.Log(words.ToString());
        Rigidbody2D obj = null;
        string property = "";
        string objName = "";
        string direction = "";
        Nullable<float> numVal = null;
        int propertyCount = 0;
        int objCount = 0;
        int directionCount = 0;
        int numCount = 0;
        int changeI = 0;
        foreach (string word in words)
        {
            if (propertyKeyWords.Contains(word))
            {
                property = word;
                propertyCount++;
            }
            if (levelObjectNames.Contains(word))
            {
                objName = word;
                for (int i = 0; i < levelObjectNames.Length; i++)
                {
                    if (levelObjectNames[i] == objName)
                    {
                        obj = levelObjects[i];
                    }
                }
                objCount++;
            }

            if (word == "x")
            {
                direction = "x";
                directionCount++;
            }
            if (word == "y")
            {
                direction = "y";
                directionCount++;
            }

            try
            {

                changeI = words.IndexOf(word);
                numVal = (float)Math.Round(float.Parse(word), 3);
                Debug.Log("num val" + numVal.ToString());
                numCount++;
            }
            catch
            {
                continue;
            }
        }

        if (numVal != null)
        {
            words[changeI] = numVal.ToString();
        }


        if ((objName != "" || levelObjects.Length == 1) && property != "" && numVal != null && (direction != "" || property == "angle" || property == "degrees" || property == "mass" || property == "height") && directionCount <= 1 && objCount <= 1 && propertyCount <= 1 && numCount <= 1)
        {
            if (!(property == "height" && (numVal > levelHeight || numVal < 0)))
            {
                if (objName == "")
                {
                    objName = levelObjectNames[0];
                    obj = levelObjects[0];

                }
                Debug.Log(objName + " " + property + " " + numVal + " " + direction);

                string newInputText = beautifyText(words, objName, property, numVal.ToString(), direction);
                if (property == "height")
                {
                    Debug.Log(obj.gameObject.transform.parent.gameObject.name);
                    obj.gameObject.transform.parent.gameObject.transform.position = new Vector3(0, (float)numVal - 3, 0);
                }

                string[] myProperty = { property, numVal.ToString(), direction };
                Debug.Log(myProperty[0]);
                foreach (string[] prop in properties)
                {
                    if (prop[0] == property)
                    {
                        int removeI = properties.IndexOf(prop);
                        properties.Remove(prop);
                        inputs.RemoveAt(removeI);
                        break;
                    }
                }
                properties.Add(myProperty);
                inputs.Add(newInputText);
                propertiesText.text = "";
                foreach (string t in inputs)
                {
                    propertiesText.text += t + "\n";
                }
                input.text = "";
            }
        }
        else
        {
            Debug.Log(property + "fail");
        }
    }

    public string beautifyText(List<string> words, string obj, string property, string numVal, string direction)
    {
        string text = String.Join(" ", words);
        text += " ";
        Debug.Log("text" + text);
        text = Regex.Replace(text, "<[^>]*>", "", RegexOptions.Singleline);
        text = Regex.Replace(text, obj, "<color=#ff00ff>" + obj + "</color>", RegexOptions.Singleline);
        text = Regex.Replace(text, property, "<color=#ff0000>" + property + "</color>", RegexOptions.Singleline);
        text = Regex.Replace(text, numVal, "<color=#0000ff>" + numVal + "</color>", RegexOptions.Singleline);
        if (direction != "")
        {
            direction = " " + direction + " ";
            text = Regex.Replace(text, direction, "<color=#00ddff>" + direction + "</color>", RegexOptions.Singleline);
        }
        Debug.Log(text);
        return text;

    }

    public void addProperties()
    {
        GameObject apply = new GameObject();
        apply.AddComponent<ApplyProperties>();
        foreach (Rigidbody2D levelObject in levelObjects)
        {
            apply.GetComponent<ApplyProperties>().applyProperties(properties.ToArray(), levelObject);
        }
    }

}

