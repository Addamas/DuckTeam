using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvent : MonoBehaviour {

    public Transform startPos;
    public Transform endPos;
    public float speed;
    float startTime;
    float journeyLength;
    GameObject player;
    public enum EventType { movingObject, flickeringLight, other};
    public EventType eventType;
    float changeTime;
    public float timeOn;
    public float timeOff;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos.position, endPos.position);
    }

    public void Action()
    {
        switch (eventType)
        {
            case EventType.movingObject:
                StartCoroutine("StartMoving");
                break;
            case EventType.flickeringLight:
                break;
            case EventType.other:
                break;
        }
    }

    public IEnumerator StartMoving()
    {
        while(Vector3.Distance(transform.position, endPos.position) > 0.001f)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);

            yield return new WaitForEndOfFrame();
        }
        transform.position = endPos.position;
        yield break;
    }

    public void Update()
    {
        if (GetComponent<Light>())
        {
            if (Time.time > changeTime)
            {
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
                if (GetComponent<Light>().enabled)
                {
                    changeTime = Time.time + timeOn;
                }
                else
                {
                    changeTime = Time.time + timeOff;
                }
            }
        }   
    }
}
