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
            else if (propertyName.ToLower().Contains("force"))
            {
                this.units = "N";
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
        propertyInfo.Add("Forces-3", new string[][] { new string[] { "force" }, new string[] { "force", "mass" } });
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
        int j = 0;
        foreach (Rigidbody2D obj in levelObjects)
        {
            obj.GetComponentInParent<ConstantForce2D>().force = Vector2.zero;
            obj.gravityScale = 0;
            obj.velocity = Vector2.zero;
            obj.gameObject.transform.position = new Vector3(initialPosLevel[j].x, initialPosLevel[j].y, -5);
            j++;
        }
        inPracticeMode = true;
        practiceMode();
        clearProperties();
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
            inPracticeMode = true;
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

        inPracticeMode = false;
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
        int counter = 0;
        foreach (string word2 in words)
        {
            counter += 1;
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
                numVal = (float)Math.Round(float.Parse(word), 3);
                numCount++;
            }
            catch
            {
                continue;
            }
        }
        Debug.Log("Counter" + counter);

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
                //Debug.Log(myProperty[0]);
                //bool exists = false;
                //foreach (string[] prop in properties)
                //{
                //    if (prop[0] == property && prop[1] == numVal.ToString() && prop[2] == direction && prop[3] == objName)
                //    {
                //        int removeI = properties.IndexOf(prop);
                //        properties.Remove(prop);
                //        inputs.RemoveAt(removeI);
                //        exists = true;
                //        break;
                //    }
                //}
                //properties.Add(myProperty);

                myProperty[0] = direction + property;
                Debug.Log(myProperty[0]);
                bool exists = false;

                try
                {
                    foreach (currentProperty cP in currentProperties[levelObjectNames.IndexOf(myProperty[3])])
                    {
                        Debug.Log("name " + cP.propertyName.ToLower());
                        if (cP.propertyName.ToLower() == myProperty[0].ToLower())
                        {
                            currentProperties[levelObjectNames.IndexOf(myProperty[3])].Remove(cP);
                            Debug.Log("im over here");
                            cP.assignValues(myProperty[0], float.Parse(myProperty[1]));
                            Debug.Log(cP.value);
                            currentProperties[levelObjectNames.IndexOf(myProperty[3])].Add(cP);
                            exists = true;
                        }
                    }
                }
                catch
                { }
                if (!exists)
                {
                    Debug.Log("no im here");
                    currentProperty cP = new currentProperty();
                    cP.assignValues(myProperty[0], float.Parse(myProperty[1]));
                    currentProperties[levelObjectNames.IndexOf(myProperty[3])].Add(cP);
                    Debug.Log(currentProperties[levelObjectNames.IndexOf(myProperty[3])].Count);
                }
                DrawProperties();
                Debug.Log("b");


                if (property == "height")
                {
                    levelObjects[levelObjectNames.IndexOf(objName)].gameObject.transform.position = new Vector3(0, (float) numVal - 2, 0);
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

        GameObject property = Instantiate(propertyUI, new Vector3(levelObject.transform.position.x + 0.6841426f, levelObject.transform.position.y - 0.9841181f, 5), Quaternion.identity);
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
        Debug.Log("running");
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
                if (cP.propertyName.ToLower().Contains("force"))
                {
                    if (cP.propertyName.ToLower().Contains("x"))
                    {
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(cP.value, obj.GetComponentInParent<ConstantForce2D>().force.y);
                    }
                    else
                    {
                        obj.gravityScale = 0;
                        obj.GetComponentInParent<ConstantForce2D>().force = new Vector2(obj.GetComponentInParent<ConstantForce2D>().force.x, cP.value);
                    }
                    if (sceneName == "Forces-3")
                    {
                        levelObjects[0].GetComponent<FrictionLevel3>().running = true;
                        levelObjects[0].GetComponent<FrictionLevel3>().appliedForce = cP.value;
                    }


                }
            }
            StartCoroutine(DelayThenFall(0.05f, sceneName, obj));
        }


        if (sceneName == "Forces-1")
        {
            levelObjects[0].GetComponent<FrictionLevel1>().setFriction();
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
        currentProperties.Clear();

        for (int index = 0; index < levelObjects.Count; index++)
        {
            
            foreach (baseProperty bP in baseProperties)
            {
                if (bP.enabled)
                {
                    currentProperty currentProperty = new currentProperty();
                    currentProperty.assignValues(bP.propertyName, bP.returnValue(index));
                    if (currentProperties.ContainsKey(index))
                    {
                        currentProperties[index].Add(currentProperty);
                    }
                    else
                    {
                        currentProperties[index] = new List<currentProperty>();
                        currentProperties[index].Add(currentProperty);
                    }

                }
            }
            Debug.Log(currentProperties.Count);
        }
        DrawProperties();
    }
private void DrawProperties()
    {
        for (int index = 0; index < currentProperties.Count; index++)
        {
            
            List<currentProperty> entry =currentProperties[index];
            GameObject propertyText = levelObjects[index].transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
            propertyText.GetComponent<TMP_Text>().text = "";
            foreach (currentProperty cP in entry)
            {
                Debug.Log(cP.ToString());
                propertyText.GetComponent<TMP_Text>().text += cP.ToString() + "\n";
            }
        }
    }
}
