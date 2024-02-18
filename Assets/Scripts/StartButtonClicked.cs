using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void SwitchScene()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
