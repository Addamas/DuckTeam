using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region goal-ish
// ontriggenter, get player.enterPosition, if player.pos.distance > xAmount, player.pos = enterposition.
//( increase fog based on distance to field center, moet ook opletten dat de xAmount wordt berekend tot de diepte van de field, en niet wordt getriggerd als je toch terug loopt de map in.
//      Keep player.lookrotation in mind
#endregion

public class EdgePortal : MonoBehaviour {

    Vector3 enterPos;
    public Transform playerB;
    public Transform playerTracker;
    public Transform trckr;
    public int distReq = 10;

    public ParticleSystem smokePFX;

    private void Start()
    { 
        if (smokePFX.isPlaying)
        {
            smokePFX.Stop();
        }
    }
    void OnTriggerEnter(Collider onEnter)
    {
        if (onEnter.transform.tag == "Player")
        {
            enterPos = playerB.position;
            trckr = Instantiate(playerTracker, playerB.position, playerB.rotation, transform);
            Transform t = smokePFX.transform;
            t.SetParent(playerB);
            t.position = playerB.position;
            t.rotation = playerB.rotation;
            smokePFX.startColor = new Color(0.5f, 0.5f, 0.5f, 0.75f);
            smokePFX.Play();
            //Debug.Log("Player entered field");
        }
    }

    void OnTriggerStay(Collider onStay)
    {
        if (onStay.transform.tag == "Player")
        {
            trckr.position = new Vector3(enterPos.x, enterPos.y, enterPos.z);
            float dist = Vector3.Distance(playerB.position, trckr.position);
            if (dist >= distReq)
            {
                playerB.position = trckr.position;
                playerB.rotation = Quaternion.Inverse(trckr.rotation);
                smokePFX.startColor = new Color(0.5f, 0.5f, 0.5f, 0.25f);
            }
        }
    }

    void OnTriggerExit(Collider onExit)
    {
        if (onExit.transform.tag == "Player")
        {
            Destroy(trckr.gameObject);
            trckr = null;
            smokePFX.Stop();
            smokePFX.transform.parent = null;
            //Debug.Log("Player exited field");
        }
    }
}
