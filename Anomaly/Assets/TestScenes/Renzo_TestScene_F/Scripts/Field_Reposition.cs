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

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider onCol)
    {
        if (onCol.transform.tag == "Player")
        {
            enterPos = playerB.position;
            Transform trckr =  Instantiate(playerTracker, playerB.position, Quaternion.identity, this.transform);

            //trckr.transform.position = playerB.position;
            Debug.Log("Player entered field");
            print(playerB.position);
        }
    }
}
