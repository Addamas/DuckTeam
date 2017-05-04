using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour {

    public Text pickUpText;
    public Transform mouseFollowr;
    public Sprite itemSprite;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            pickUpText.gameObject.SetActive(true);
            pickUpText.text = "Press 'E' to pick up item";
            if (Input.GetButtonDown("Use"))
            {
                mouseFollowr.GetComponent<Image>().enabled = true;
                mouseFollowr.GetComponent<Image>().sprite = itemSprite;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pickUpText.gameObject.SetActive(false);
        }
    }
}

