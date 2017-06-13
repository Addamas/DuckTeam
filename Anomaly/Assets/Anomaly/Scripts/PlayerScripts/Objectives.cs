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

    public Text objectiveText;

    void Start()
    {
        FirstObjective();
    }

    public void FirstObjective()
    {
        SetObjective();
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
                }
                break;
            case 2:
                //Checked Generator
                objectiveID++;
                SetObjective();
                break;
            case 3:
                //Got Fuel
                objectiveID++;
                SetObjective();
                break;
            case 4:
                //Refilled Generator
                objectiveID++;
                SetObjective();
                generatorAnim.SetBool("Off", false);
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
