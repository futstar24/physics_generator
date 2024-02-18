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
        }
        else if (sceneName.Contains("2"))
        {
            SceneManager.LoadScene(sceneName.Replace("2", "3"));
        }
        else
        {
            if (sceneName.Contains("Kinematics"))
            {
                SceneManager.LoadScene("Force-1");
            }
            else if (sceneName.Contains("Force"))
            {
                SceneManager.LoadScene("Energy-1");
            }
            else if (sceneName.Contains("Energy"))
            {
                SceneManager.LoadScene("Force-1");
            }
            else if (sceneName.Contains("Momentum"))
            {
                SceneManager.LoadScene("LevelSelector");
            }
        }

    }
}
