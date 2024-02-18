using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using UnityEngine.UI;

public class MometumLevel2 : MonoBehaviour
{

    public TMP_InputField input;
    public List<Rigidbody2D> levelObjects;
    public List<string> levelObjectNames;
    public List<string> propertyKeyWords;
    public TMP_Text propertiesText;
    public List<string[]> properties = new List<string[]>();
    public int levelHeight;
    public List<string> inputs;
    public TMP_Text goalText;
    public Rigidbody2D block1;
    public Rigidbody2D block2;
    public Button practiceButton;


    private void Start()
    {
        levelMode();
        propertiesText.text = "";
        levelHeight = 7;

        goalText.text = "Goal V2: 2 m/s";
        inputs = new List<string>();
        foreach (Rigidbody2D obj in levelObjects)
        {
            obj.gravityScale = 0;
            obj.gameObject.GetComponent<calculateNewVelocities>().goalV2 = 2;
        }


    }

    public void practiceMode()
    {
        if (practiceButton.GetComponentInChildren<TextMeshProUGUI>().text == "Practice")
        {
            propertyKeyWords = new List<string>(new string[] { "velocity", "mass" });
            levelObjectNames = new List<string>();
            levelObjects = new List<Rigidbody2D>(new Rigidbody2D[] { block1, block2 });
            foreach (Rigidbody2D rb in levelObjects)
            {
                levelObjectNames.Add(rb.name);
            }
            practiceButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back to Level";
        } else
        {
            levelMode();
        }

    }

    public void levelMode()
    {
        propertyKeyWords = new List<string>(new string[] { "velocity"});
        levelObjectNames = new List<string>();
        levelObjects = new List<Rigidbody2D>(new Rigidbody2D[] { block1});
        foreach (Rigidbody2D rb in levelObjects)
        {
            levelObjectNames.Add(rb.name);
        }
        practiceButton.GetComponentInChildren<TextMeshProUGUI>().text = "Practice";
    }

    public void submitText()
    {

        List<string> words = new List<string>(input.text.Split(new string[] { " ", "'", "," }, StringSplitOptions.RemoveEmptyEntries));
        Debug.Log("Words:" + string.Join(" ", words));
        Rigidbody2D obj = null;
        string property = "";
        string objName = "";
        string direction = "";
        Nullable<float> numVal = null;
        int propertyCount = 0;
        int objCount = 0;
        int directionCount = 0;
        int numCount = 0;
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
                for (int i = 0; i < levelObjectNames.Count; i++)
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
                foreach (string wwww in words)
                {
                    Debug.Log("word:"+wwww);
                }
                numVal = (float)Math.Round(float.Parse(word), 3);
                Debug.Log("num val" + numVal.ToString());
                numCount++;
            }
            catch
            {
                continue;
            }
        }


        if ((objName != "" || levelObjects.Count == 1) && property != "" && numVal != null && (direction != "" || property == "angle" || property == "degrees" || property == "mass" || property == "height") && directionCount <= 1 && objCount <= 1 && propertyCount <= 1 && numCount <= 1)
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

                string[] myProperty = { property, numVal.ToString(), direction , objName };
                Debug.Log(myProperty[0]);
                foreach (string[] prop in properties)
                {
                    if (prop[0] == property && prop[1] == numVal.ToString() && prop[2] == direction && prop[3] == objName)
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

    public string beautifyText(List<string> words, string objName2, string property, string numVal, string direction)
    {
        string text = string.Join(" ", words);
        Debug.Log("before beautify "+text);
        text += " ";
        text = " " + text;
        Debug.Log("obj"+objName2);
        text = Regex.Replace(text, "<[^>]*>", "", RegexOptions.Singleline);
        text = Regex.Replace(text, objName2, "<color=#ff00ff>" + objName2 + "</color>", RegexOptions.Singleline);
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
        if (properties.ToArray().Length == 0) { return; }
        GameObject apply = new GameObject();
        apply.AddComponent<ApplyProperties>();

        foreach (Rigidbody2D levelObject in levelObjects)
        {
            apply.GetComponent<ApplyProperties>().applyProperties(properties.ToArray(), levelObject, "Momentum2");
        }
    }

}

