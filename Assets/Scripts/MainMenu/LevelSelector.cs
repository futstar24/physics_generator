using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    public GameObject TopicsList;
    public List<GameObject> LevelsList;

    public GameObject levelType;

    private string currentTopic;
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
        Application.Quit();
    }

    public void SetListActive(string topic)
    {
        currentTopic = topic;
        GameObject list = null;
        foreach (GameObject obj in LevelsList)
        {
            if (obj.name == new string(topic + "Levels"))
            {
                obj.SetActive(true);
                list = obj;
            }
            else
            {
                obj.SetActive(false);
            }
        }
        GameObject content = list.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        Debug.Log(content.name);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject level = content.transform.GetChild(i).gameObject;
            int levelNum = int.Parse(level.name.Substring(level.name.Length - 1));
            Debug.Log(topic + levelNum);
            if (PlayerPrefs.GetInt(new string(topic + levelNum)) == 0){
                level.GetComponent<Button>().interactable = false;
               // level.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                level.GetComponent<Button>().interactable = true;
                //level.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

    }

    public void OnLevelSelect(string Scene)
    {
        SceneManager.LoadScene(Scene);
        //SceneManager.LoadScene("Kinematics-1");
    }

}
