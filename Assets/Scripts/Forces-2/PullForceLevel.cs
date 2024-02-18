using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PullForceLevel : MonoBehaviour
{
    private bool result = false;
    public GameObject win;
    public GameObject lose;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x > 3 && !result)
        {

            Debug.Log("updatin");
            result = true;
            if (Math.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x-15) <= 0.5)
            {
                win.SetActive(true);

                PlayerPrefs.SetInt("Forces2", 1);
            } else
            {
                lose.SetActive(true);
                PlayerPrefs.SetInt("Forces2", 0);
            }

        }
    }
}
