using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class textInputSubmit : MonoBehaviour
{

    public TMP_InputField input;
    public Rigidbody2D[] levelObjects;
    public String[] levelObjectNames;
    public string[] propertyKeyWords = { "mass", "velocity", "acceleration","angle","degrees"};
    public GameObject changeLevel;
    public TMP_Text propertiesText;
    public List<string[]> properties = new List<string[]>();


    private void Start()
    {
        propertiesText.text = "";

    }


    public void submitText()
    {

        string[] words = input.text.Split(" ");

        string property = "";
        string obj = "";
        string direction = "";
        Nullable<int> numVal = null;
        Debug.Log(levelObjects.Length);
        int propertyCount = 0;
        int objCount = 0;
        int directionCount = 0;
        int numCount = 0;
        foreach (string word in words)
        {
            if (propertyKeyWords.Contains(word)) {
                property = word;
                propertyCount++;
            }
            if (levelObjectNames.Contains(word))
            {
                obj = word;
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
                numVal = Int32.Parse(word);
                numCount++;
            }
            catch
            {
                continue;
            }
        }
        Debug.Log(levelObjects.Length);
        Debug.Log(propertyKeyWords.Length);

        if (obj != "" || levelObjects.Length == 1 && property != "" && numVal != null && (direction != "" || property == "angle" || property == "degrees" || property == "mass") && directionCount <= 1 && objCount <= 1 && propertyCount <= 1 && numCount <= 1)
        {
            if (obj == "")
            {
                obj = levelObjectNames[0];
            }
            Debug.Log(obj + " " + property + " " + numVal + " " + direction);

            beautifyText(obj, property, numVal.ToString(),direction);

            string[] myProperty = { property, numVal.ToString(),direction};
            Debug.Log(myProperty[0]);
            properties.Add(myProperty);
            Debug.Log(properties[0]);
            propertiesText.text += input.text + "\n";
            input.text = "";
        }
        else
        {
            Debug.Log("fail");
        }
    }

    public void beautifyText(string obj, string property, string numVal, string direction)
    {
        string text = input.text;
        text += " ";
        text = Regex.Replace(text, "<[^>]*>", "", RegexOptions.Singleline);
        text = Regex.Replace(text, obj, "<color=#ff00ff>"+obj+"</color>", RegexOptions.Singleline);
        text = Regex.Replace(text, property, "<color=#ff0000>" + property + "</color>", RegexOptions.Singleline);
        text = Regex.Replace(text, numVal, "<color=#0000ff>" + numVal + "</color>", RegexOptions.Singleline);
        if (direction != "")
        {
            direction = " " + direction + " ";
            text = Regex.Replace(text, direction, "<color=#00ddff>" + direction + "</color>", RegexOptions.Singleline);
        }
        input.text = text;
 
    }

    public void addProperties()
    {
        GameObject apply = new GameObject();
        apply.AddComponent<ApplyProperties>();
        foreach (Rigidbody2D levelObject in levelObjects)
        {
            apply.GetComponent<ApplyProperties>().applyProperties(properties.ToArray(),levelObject);
        }
    }

}
