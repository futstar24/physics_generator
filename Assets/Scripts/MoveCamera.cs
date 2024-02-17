using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        { //wheel goes up
            if (Camera.main.transform.position.y >= 0)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, -10);
            }
            else
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 0, Camera.main.transform.position.y + 0.1f, -10);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 0, Camera.main.transform.position.y +  - 0.1f, -10);
        }
    }
}
