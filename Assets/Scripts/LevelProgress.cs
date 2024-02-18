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
        PlayerPrefs.SetInt("ProjectileMovement1", 1);
        PlayerPrefs.SetInt("ProjectileMovement2", 0);
        PlayerPrefs.SetInt("ProjectileMovement3", 0);
        PlayerPrefs.SetInt("AngularMovement1", 1);
        PlayerPrefs.SetInt("AngularMovement2", 0);
        PlayerPrefs.SetInt("AngularMovement3", 0);
        PlayerPrefs.SetInt("Momentum1", 1);
        PlayerPrefs.SetInt("Momentum2", 0);
        PlayerPrefs.SetInt("Momentum3", 0);
    }
    void Update()
    {
        
    }
}
