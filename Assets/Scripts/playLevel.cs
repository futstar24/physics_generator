using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{

    public TMP_InputField input;
    private List<Rigidbody2D> levelObjects;
    private List<string> levelObjectNames;
    private List<string> propertyKeyWords;
    public TMP_Text propertiesText;
    public List<string[]> properties = new List<string[]>();
    public int levelHeight;
    private List<string> inputs;
    private Dictionary<string, string[][]> propertyInfo = new Dictionary<string, string[][]>();
    public List<Rigidbody2D> mainLevelObjects;
    public List<Rigidbody2D> sandboxLevelObjects;
    public float goalValue;
    private string sceneName;

    public bool inPracticeMode;

    public GameObject propertyUI;


    public Vector3[] initialPosLevel;

    [Serializable]
    public struct baseProperty
    {
        public string propertyName;
        public bool enabled;
        public float[] initialValues;
        public string units;

        public string ToString(int objectNum)
        {
            try
            {
                return propertyName + ": " + initialValues[objectNum] + units;
            }
            catch
            {
                return propertyName + ": " + 0 + units;
            }

        }

        public float returnValue(int objectNum)
        {
            try
            {
                return initialValues[objectNum];
            }
            catch
            {
                return 0;
            }
        }
    }

    private struct currentProperty
    {
        public string propertyName;
        public float value;
        public string units;

        public override string ToString()
        {
            return propertyName + ": " + value + units;
        }

        public void assignValues(string propertyName, float value)
        {
            this.propertyName = propertyName;
            this.value = value;
            if (propertyName.ToLower().Contains("velocity"))
            {
                this.units = "m/s";
            }
            else if (propertyName.ToLower().Contains("mass"))
            {
                this.units = "kg";
            }
            else if (propertyName.ToLower().Contains("acceleration"))
            {
                this.units = "m/s^2";
            }
            else if (propertyName.ToLower().Contains("height"))
            {
                this.units = "m";
            }

        }
    }

    public baseProperty[] baseProperties = { };
    private Dictionary<int, List<currentProperty>> currentProperties = new Dictionary<int, List<currentProperty>>();


    private void Start()
    {
        inPracticeMode = false;
        sceneName = SceneManager.GetActiveScene().name;
        propertyInfo.Add("DefaultLevel", new string[][] { new string[] { "velocity" }, new string[] { "height", "velocity", "acceleration" } });
        propertyInfo.Add("Kinematics-1", new string[][] { new string[] { "height" }, new string[] { "height", "velocity", "acceleration" } });
        propertyInfo.Add("Momentum-1", new string[][] { new string[] { "velocity" }, new string[] { "velocity", "mass" } });
        propertyInfo.Add("Momentum-2", new string[][] { new string[] { "velocity" }, new string[] { "velocity", "mass" } });
        propertyInfo.Add("Kinematics-2", new string[][] { new string[] { "velocity" }, new string[] { "velocity", "acceleration" } });
        propertyInfo.Add("Kinematics-3", new string[][] { new string[] { "velocity" }, new string[] { "velocity", "acceleration" } });
        propertyInfo.Add("Momentum-3", new string[][] { new string[] { "velocity" }, new string[] { "velocity", "mass" } });
        propertyInfo.Add("Forces-1", new string[][] { new string[] { "force" }, new string[] { "force", "mass" } });
        propertyInfo.Add("Forces-2", new string[][] { new string[] { "force" }, new string[] { "force", "mass" } });
        levelMode();
        levelHeight = 7;

        initialPosLevel = new Vector3[levelObjects.Count];

        inputs = new List<string>();
        int i = 0;
        foreach (Rigidbody2D obj in levelObjects)
        {
            obj.gravityScale = 0;
            if (sceneName == "Momentum-2")
            {
                obj.gameObject.GetComponent<calculateNewVelocities>().goalV2 = 2;
            }
            else if (sceneName == "Momentum-1")
            {
                obj.gameObject.GetComponent<CheckVelocity>().velocityGoal = goalValue;
            }
            else if (sceneName == "Momentum-3" && obj.name == "SpaceRock")
            {
                obj.gameObject.GetComponent<CalculateVelocitiesMomentum3>().goalVF = goalValue;
            }
            createProperties(obj.gameObject, i);

            initialPosLevel[i] = obj.gameObject.transform.position;
            Debug.Log(i);
            Debug.Log(initialPosLevel[i]);
            
            i++;

        }



    }

    public void ResetLevel()
    {
        currentProperties.Clear();
        int j = 0;
        foreach (Rigidbody2D obj in levelObjects)
        {
            obj.velocity = Vector2.zero;
            obj.gameObject.transform.position = new Vector3(initialPosLevel[j].x, initialPosLevel[j].y, -5);
            j++;
        }
        DrawProperties();
    }

    public void practiceMode()
    {
        if (!inPracticeMode)
        {
            propertyKeyWords = new List<string>(propertyInfo[SceneManager.GetActiveScene().name][1]);
            levelObjectNames = new List<string>();
            levelObjects = sandboxLevelObjects;
            foreach (Rigidbody2D rb in levelObjects)
            {
                levelObjectNames.Add(rb.name);
            }
        } else
        {
            levelMode();
        }

    }

    public void levelMode()
    {
        propertyKeyWords = new List<string>(propertyInfo[SceneManager.GetActiveScene().name][0]);
        levelObjectNames = new List<string>();
        levelObjects = mainLevelObjects;
        foreach (Rigidbody2D rb in levelObjects)
        {
            levelObjectNames.Add(rb.name);
        }
    }

    public void SubmitText()
    {

        List<string> words = new List<string>(input.text.Split(new string[] { " ", "'", "," }, StringSplitOptions.RemoveEmptyEntries));
        Debug.Log("Words:" + string.Join(" ", words));
        Rigidbody2D obj = null;
        string property = "";
        string objName = "";
        string direction = "";
        Nullable<float> numVal = null;
        int propertyCount = 0;
        int objCount = 0;
        int directionCount = 0;
        int numCount = 0;
        foreach (string word2 in words)
        {
            string word = word2.ToLower();
            Debug.Log(string.Join(" ", propertyKeyWords));
            Debug.Log(word);
            if (propertyKeyWords.Contains(word))
            {
                Debug.Log("here");
                property = word;
                propertyCount++;
            }
            if (levelObjectNames.Contains(word))
            {
                objName = word;
                for (int i = 0; i < levelObjectNames.Count; i++)
                {
                    if (levelObjectNames[i] == objName)
                    {
                        obj = levelObjects[i];
                    }
                }
                objCount++;
            }

            if (word == "x")
            {
                direction = "x";
                directionCount++;
            }
            if (word == "y")
            {
                direction = "y";
                directionCount++;
            }

            try
            {
                foreach (string wwww in words)
                {
                    Debug.Log("word:" + wwww);
                }
                numVal = (float)Math.Round(float.Parse(word), 3);
                Debug.Log("num val" + numVal.ToString());
                numCount++;
            }
            catch
            {
                continue;
            }
        }

        Debug.Log(property + numVal + direction);
        if ((objName != "" || levelObjects.Count == 1) && property != "" && numVal != null && (direction != "" || property == "angle" || property == "degrees" || property == "mass" || property == "height") && directionCount <= 1 && objCount <= 1 && propertyCount <= 1 && numCount <= 1)
        {
            if (!(property == "height" && (numVal > levelHeight || numVal <= 0)))
            {
                if (objName == "")
                {
                    objName = levelObjectNames[0];
                    obj = levelObjects[0];

                }
                Debug.Log(objName + " " + property + " " + numVal + " " + direction);

                string[] myProperty = { property, numVal.ToString(), direction, objName };
                Debug.Log(myProperty[0]);
                bool exists = false;
                foreach (string[] prop in properties)
                {
                    if (prop[0] == property && prop[1] == numVal.ToString() && prop[2] == direction && prop[3] == objName)
                    {
                        int removeI = properties.IndexOf(prop);
                        properties.Remove(prop);
                        inputs.RemoveAt(removeI);
                        exists = true;
                        break;
                    }
                }
                properties.Add(myProperty);

                myProperty[0] = direction + property;

                if (exists)
                {
                    foreach (currentProperty cP in currentProperties[levelObjectNames.IndexOf(myProperty[3])])
                    {
                        if (cP.propertyName == myProperty[0])
                        {
                            cP.assignValues(myProperty[0], float.Parse(myProperty[1]));
                        }
                        Debug.Log("x");
                    }
                } else
                {
                    Debug.Log("c");
                    currentProperty cP = new currentProperty();
                    cP.assignValues(myProperty[0], float.Parse(myProperty[1]));
                    currentProperties[levelObjectNames.IndexOf(myProperty[3])].Add(cP);
                    Debug.Log(currentProperties[levelObjectNames.IndexOf(myProperty[3])].Count);
                }
                DrawProperties();
                Debug.Log("b");


                if (property == "height")
                {
                    obj.gameObject.transform.parent.gameObject.transform.position = new Vector3(0, (float)numVal - 3, 0);
                }


            }
        }
        else
        {
            Debug.Log(property + "fail");
        }
    }
    private void createProperties(GameObject levelObject, int objectNum)
    {

        GameObject property = Instantiate(propertyUI, new Vector3(levelObject.transform.position.x + 0.6841426f, levelObject.transform.position.y - 0.9841181f, -5), Quaternion.identity);
        property.transform.parent = levelObject.transform;

        foreach (baseProperty bP in baseProperties)
        {
            if (bP.enabled)
            {
                currentProperty currentProperty = new currentProperty();
                currentProperty.assignValues(bP.propertyName, bP.returnValue(objectNum));
                if (currentProperties.ContainsKey(objectNum))
                {
                    currentProperties[objectNum].Add(currentProperty);
                }
                else
                {
                    currentProperties[objectNum] = new List<currentProperty>();
                    currentProperties[objectNum].Add(currentProperty);
                }

                GameObject propertyText = property.transform.GetChild(0).gameObject.transform.GetChild(0).transform.gameObject;
                propertyText.GetComponent<TMP_Text>().text += bP.ToString(objectNum) + "\n";
            }
        }

    }

    public void RunSim()
    {
        bool check = false;
        for (int index = 0; index < currentProperties.Count; index++)
        {
            KeyValuePair<int, List<currentProperty>> entry = currentProperties.ElementAt(index);

            Rigidbody2D obj = levelObjects[index];
            if (!check) {
                if (sceneName == "Kinematics-1")
                {
                    obj.gameObject.GetComponent<Timer>().t = Time.realtimeSinceStartup;
                    obj.gameObject.GetComponent<Timer>().timing = true;
                    obj.gravityScale = 1;
                }
                else if (sceneName == "Kinematics-2" || sceneName == "Kinematics-3")
                {
                    obj.gravityScale = 1;
                }
                check = true;
            }
            foreach (currentProperty cP in entry.Value)
            {

                if (cP.propertyName.ToLower().Contains("velocity"))
                {
                    if (cP.propertyName.ToLower().Contains("x"))
                    {
                        obj.velocity = new Vector2(cP.value, obj.velocity.y);
                    }
                    else
                    {
                        obj.velocity = new Vector2(obj.velocity.x, cP.value);
                    }

                }
                if (cP.propertyName.ToLower().Contains("mass"))
                {
                    obj.mass = cP.value;
                    Debug.Log("set mass");
                }
                if (cP.propertyName.ToLower().Contains("acceleration"))
                {
                    if (cP.propertyName.ToLower().Contains("x"))
                    {
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(cP.value * obj.mass, obj.GetComponentInParent<ConstantForce2D>().force.y);
                    }
                    else
                    {
                        obj.gravityScale = 0;
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(obj.GetComponentInParent<ConstantForce2D>().force.x, cP.value * obj.mass);
                    }

                }
            }
            StartCoroutine(DelayThenFall(0.05f, sceneName, obj));
        }
    }
    IEnumerator DelayThenFall(float delay, string levelName, Rigidbody2D obj)
    {
        yield return new WaitForSeconds(delay);
        if (levelName == "Momentum-3" && obj.name == "SpaceRock")
        {
            obj.GetComponent<CalculateVelocitiesMomentum3>().calculate();
        }
    }
    private void clearProperties()
    {
        for (int index = 0; index < currentProperties.Count; index++)
        {
            KeyValuePair<int, List<currentProperty>> entry = currentProperties.ElementAt(index);


        }
    }
private void DrawProperties()
    {
        for (int index = 0; index < currentProperties.Count; index++)
        {
            KeyValuePair<int, List<currentProperty>> entry = currentProperties.ElementAt(index);
            GameObject propertyText = levelObjects[index].transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
            propertyText.GetComponent<TMP_Text>().text = "";
            foreach (currentProperty cP in entry.Value)
            {
                Debug.Log(cP.ToString());
                propertyText.GetComponent<TMP_Text>().text += cP.ToString() + "\n";
            }
        }
    }
}
