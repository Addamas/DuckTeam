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

    public Text objectiveText;

    void Start()
    {
        FirstObjective();
    }

    public void FirstObjective()
    {
        SetObjective();
    }

    public void Suitcase()
    {
        if (!usedSuitcase)
        {
            usedSuitcase = true;
            objectiveID++;
            SetObjective();
        }
    }

    public void FoundNote()
    {
        collectedNotes++;
        SetObjective();
    }

    void SetObjective()
    {
        if(objectiveID == 1)
        {
            objectiveText.text = objectiveString[objectiveID] + " " + collectedNotes.ToString() + "/" + totalNotes.ToString();
            return;
        }
        objectiveText.text = objectiveString[objectiveID];
    }
}
