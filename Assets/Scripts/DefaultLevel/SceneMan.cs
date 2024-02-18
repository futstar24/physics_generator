using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchScene(string str)
    {
        SceneManager.LoadScene(str);
    }
    public void LevelUp()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Contains("1"))
        {
            SceneManager.LoadScene(sceneName.Replace("1", "2"));
            string[] str = sceneName.Split("-");
            PlayerPrefs.SetInt(str[0] + "1", 1);
        }
        else if (sceneName.Contains("2"))
        {
            SceneManager.LoadScene(sceneName.Replace("2", "3"));
            string[] str = sceneName.Split("-");
            PlayerPrefs.SetInt(str[0] + "2", 1);
        }
        else
        {
            if (sceneName.Contains("Kinematics"))
            {
                PlayerPrefs.SetInt("Kinematics3", 1);
                SceneManager.LoadScene("Forces-1");
            }
            else if (sceneName.Contains("Force"))
            {
                PlayerPrefs.SetInt("Force3", 1);
                SceneManager.LoadScene("Momentum-1");
            }
            else if (sceneName.Contains("Momentum"))
            {
                PlayerPrefs.SetInt("Momentum3", 1);
                SceneManager.LoadScene("LevelSelector");
            }
        }

    }
}
