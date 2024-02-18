using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject TopicsList;
    public List<GameObject> LevelsList;
    public void OnPlay()
    {
        TopicsList.SetActive(true);
    }
    public void OnSettings()
    {
        TopicsList.SetActive(false);
        SetListActive("");
    }
    public void OnExit()
    {
        TopicsList.SetActive(false);
        SetListActive("");
    }

    public void SetListActive(string topic)
    {
        foreach (GameObject obj in LevelsList)
        {
            if (obj.name == new string(topic + "Levels"))
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }

}
