using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Objectives : MonoBehaviour {

    public int collectedNotes;
    public int totalNotes;
    public string[] objectiveString;
    public int objectiveID;
    bool usedSuitcase;
    public GameObject[] objectiveTriggers;
    public Animator suitcaseAnim;
    public Animator generatorAnim;
    public bool hasJerrycan;
    public bool hasFuel;
    public GameObject[] lights;
    public Material lightMaterial;

    public Text objectiveText;

    void Start()
    {
        FirstObjective();
        LightMaterial(2);
    }

    public void FirstObjective()
    {
        SetObjective();
    }

    void LightMaterial(int i)
    {
        Color baseColor = Color.yellow;
        Color finalColor = baseColor * i;
        lightMaterial.SetColor("_EmissionColor", finalColor);
    }

    public void CompletedObjective(int i)
    {
        switch (i)
        {
            case 0:
                //Entered House
                objectiveTriggers[0].SetActive(false);
                objectiveID++;
                SetObjective();
                break;
            case 1:
                //Found Suitcase
                if (!usedSuitcase)
                {
                    usedSuitcase = true;
                    suitcaseAnim.SetTrigger("Open");
                    objectiveID++;
                    SetObjective();
                    generatorAnim.SetBool("Off", true);
                    LightMaterial(0);
                    for(int l = 0; l < lights.Length; l++)
                    {
                        lights[l].SetActive(false);
                    }
                }
                break;
            case 2:
                //Checked Generator
                objectiveID++;
                SetObjective();
                break;
            case 3:
                //Got Fuel
                hasFuel = true;
                objectiveID++;
                SetObjective();
                break;
            case 4:
                //Refilled Generator
                objectiveID++;
                SetObjective();
                generatorAnim.SetBool("Off", false);
                hasFuel = false;
                hasJerrycan = false;
                for (int l = 0; l < lights.Length; l++)
                {
                    lights[l].SetActive(true);
                }
                LightMaterial(2);
                break;
        }
    }

    public void FoundNote()
    {
        collectedNotes++;
        SetObjective();
    }

    void SetObjective()
    {
        if(objectiveID == 5)
        {
            objectiveText.text = objectiveString[objectiveID] + " " + collectedNotes.ToString() + "/" + totalNotes.ToString();
            return;
        }
        objectiveText.text = objectiveString[objectiveID];
    }
}
