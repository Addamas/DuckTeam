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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos.position, endPos.position);
    }

    public void Action()
    {
        StartCoroutine("StartEvent");
    }

    public IEnumerator StartEvent()
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
}
