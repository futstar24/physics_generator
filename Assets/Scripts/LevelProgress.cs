using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelProgress : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Kinematics1", 1);
        PlayerPrefs.SetInt("Kinematics2", 0);
        PlayerPrefs.SetInt("Kinematics3", 0);
        PlayerPrefs.SetInt("Energy1", 1);
        PlayerPrefs.SetInt("Energy2", 0);
        PlayerPrefs.SetInt("Energy3", 0);
        PlayerPrefs.SetInt("Forces1", 1);
        PlayerPrefs.SetInt("Forces2", 0);
        PlayerPrefs.SetInt("Forces3", 0);
        PlayerPrefs.SetInt("Momentum1", 1);
        PlayerPrefs.SetInt("Momentum2", 0);
        PlayerPrefs.SetInt("Momentum3", 0);
    }
    void Update()
    {

    }
}
