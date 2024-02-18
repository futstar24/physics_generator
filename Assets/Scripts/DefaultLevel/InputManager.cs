using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public TMP_InputField inputField;
    private void Update()
    {
        inputField.MoveToEndOfLine(false, false);
    }

    public void ClearInput(TMP_InputField inputField)
    {
        Debug.Log("d");
        if (inputField.text == "")
        {
            return;
        }
        inputField.text = "";
    }
    public void OnChanged(TMP_InputField inputField)
    {
        Debug.Log("f");
        bool num = false;
        string[] keywords = inputField.text.Split(" ");
        string input = new string("");
        for (int i = 0; i < keywords.Length; i++)
        {
            Debug.Log(keywords[i]);
            
            if (keywords[i].ToLower() == "height")
            {
                input += " " + "<color=yellow>height</color>";
                
            }
            else if (keywords[i].ToLower() == "acceleration")
            {
                input += " " + "<color=red>acceleration</color>";
            }
            else if (keywords[i].ToLower() == "mass")
            {
                input += " " + "<color=blue>mass</color>";
            }
            else if (keywords[i].ToLower() == "velocity")
            {
                input += " " + "<color=green>velocity</color>";
            }
            else if (keywords[i].ToLower() == "angle")
            {
                input += " " + "<color=purple>angle</color>";
            }
          
            else
            {
                int value;
                if (int.TryParse(keywords[i], out value) && !num)
                {
                    input += " " + "<color=purple>" + value + "</color>";
                    num = true;
                }
                else
                {
                    input += " " + keywords[i];
                }
                
            }
        }
        inputField.text = input;
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        //add more
    }
}
