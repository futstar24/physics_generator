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
    public string[] levelSubjects = {"ball"};
    public string[] propertyKeyWords = { "mass", "velocity", "acceleration" };
    public GameObject changeLevel;
    public TMP_Text propertiesText;

    private void Start()
    {
        propertiesText.text = "";
    }

    public void submitText()
    {

        string[] words = input.text.Split(" ");

        string property = "";
        string obj = "";
        Nullable<int> numVal = null;
        Debug.Log(levelSubjects.Length);
        foreach (string word in words)
        {
            if (propertyKeyWords.Contains(word)) {
                property = word;
            }
            if (levelSubjects.Contains(word))
            {
                obj = word;
            }
            
            try
            {
                numVal = Int32.Parse(word);
            }
            catch
            {
                continue;
            }
        }
        Debug.Log(levelSubjects.Length);
        Debug.Log(propertyKeyWords.Length);

        if (obj != "" || levelSubjects.Length == 1 && property != "" && numVal != null)
        {
            if (obj == "")
            {
                obj = levelSubjects[0];
            }
            Debug.Log(obj + " " + property + " " + numVal);

            beautifyText(obj, property, numVal.ToString());

            propertiesText.text += input.text + "\n";
            input.text = "";
        }
        else
        {
            Debug.Log("fail");
        }
    }

    public void beautifyText(string obj, string property, string numVal)
    {
        string text = input.text;
        text = Regex.Replace(text, "<[^>]*>", "", RegexOptions.Singleline);
        text = Regex.Replace(text, obj, "<color=#ff00ff>"+obj+"</color>", RegexOptions.Singleline);
        text = Regex.Replace(text, property, "<color=#ff0000>" + property + "</color>", RegexOptions.Singleline);
        text = Regex.Replace(text, numVal, "<color=#0000ff>" + numVal + "</color>", RegexOptions.Singleline);
        input.text = text;

        
    }
}
