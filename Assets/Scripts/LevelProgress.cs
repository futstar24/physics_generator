using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelProgress : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Kinematics1", 1);
        PlayerPrefs.SetInt("Kinematics2", 1);
        PlayerPrefs.SetInt("Kinematics3", 0);
        PlayerPrefs.SetInt("Projectiles1", 1);
        PlayerPrefs.SetInt("Projectiles2", 0);
        PlayerPrefs.SetInt("Projectiles3", 0);
        PlayerPrefs.SetInt("Angles1", 1);
        PlayerPrefs.SetInt("Angles2", 1);
        PlayerPrefs.SetInt("Angles3", 1);
        PlayerPrefs.SetInt("Momentum1", 1);
        PlayerPrefs.SetInt("Momentum2", 0);
        PlayerPrefs.SetInt("Momentum3", 0);
    }
    void Update()
    {

    }
}
