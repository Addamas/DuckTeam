using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ontrigenter, get player.enterPosition, if player.pos.distance > xAmount, player.pos = enterposition.
//( increase fog based on distance to field center, moet ook opletten dat de xAmount wordt berekend tot de diepte van de field, en niet wordt getriggerd als je toch terug loopt de map in.
//      Keep player.lookrotation in mind

//haal de player transform van de gamehandler??

public class Field_Reposition : MonoBehaviour {

    Vector3 enterPos;
    public Transform playerB;
    public Transform playerTracker;
    public Transform trckr;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider onEnter)
    {
        if (onEnter.transform.tag == "Player")
        {
            enterPos = playerB.position;
            trckr = Instantiate(playerTracker, playerB.position, Quaternion.identity, this.transform);
            Debug.Log("Player entered field");
        }
    }

    void OnTriggerStay(Collider onStay)
    {
        if (onStay.transform.tag == "Player")
        {
            trckr.transform.position = new Vector3(enterPos.x, enterPos.y, playerB.position.z);
            float dist = Vector3.Distance(playerB.position, trckr.position);
            if (dist >= 10)
            {
                playerB.transform.position = trckr.transform.position;
                
            }
        }
    }

    void OnTriggerExit(Collider onExit)
    {
        if (onExit.transform.tag == "Player")
        {
            Destroy(trckr.gameObject);
            trckr = null;
            Debug.Log("Player exited field");
        }
    }
}
