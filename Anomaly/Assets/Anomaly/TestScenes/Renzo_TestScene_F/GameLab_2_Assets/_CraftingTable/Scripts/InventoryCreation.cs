using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryCreation : MonoBehaviour {

    public int invHeight;
    public int invWidth;

    public int heightOffset;
    public int widthOffset;

    bool activeSwitch = true;
    public Transform invSlot;
    public Transform invParent;
    public List<Transform> listOfSlots = new List<Transform>();

    // Use this for initialization
    void Start () {//hoogte van de row's
        for (int y = 0; y < invHeight; y++)
        {//breedte van de row's
            for (int x = 0; x < invWidth; x++)
            {  //instantiate een prefab als een tijdelijke var en maar die parent van een manager(GO) in de canvas.
                Transform temp = Instantiate(invSlot, transform.position + new Vector3(x * widthOffset, -y * heightOffset, 0), Quaternion.identity) as Transform;
                temp.SetParent(invParent);
                listOfSlots.Add(temp);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Inventory"))
        {
            invParent.gameObject.SetActive(activeSwitch = !activeSwitch);
        }
	}
}
