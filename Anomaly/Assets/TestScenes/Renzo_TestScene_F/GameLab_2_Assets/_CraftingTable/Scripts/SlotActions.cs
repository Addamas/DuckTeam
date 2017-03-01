using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotActions : MonoBehaviour {

    public Transform mouseFollowr;
    public Sprite mouseSprite;
    public Sprite slotSprite;
    // Use this for initialization
    void Start () {
        mouseFollowr = GameObject.Find("MouseFollowr").transform;
    }

    public void OnClick()
    { //is de muis niet leeg, en
        if (mouseFollowr.GetComponent<Image>().sprite != null)
        {   // is de slot zelf wel leeg, maak van de slot zen sprite en muis sprite en maak de muis leeg en inactive; 
            if (GetComponent <Image>().sprite == null)
            {
                GetComponent<Image>().sprite = mouseFollowr.GetComponent<Image>().sprite;
                mouseFollowr.GetComponent<Image>().sprite = null;
                mouseFollowr.GetComponent<Image>().enabled = false;
            }// is de slot niet leeg, doe de slot sprite in een tijdelijke var, de slot sprite wordt die van de muis, en de muis krijgt de sprite van de tijdelijke var;
            else if (GetComponent<Image>().sprite != null)
            {
                Sprite temp = GetComponent<Image>().sprite;
                GetComponent<Image>().sprite = mouseFollowr.GetComponent<Image>().sprite;
                mouseFollowr.GetComponent<Image>().sprite = temp;
                mouseFollowr.GetComponent<Image>().enabled = true;
            }
        }//is de muis zelf leeg, en de slot niet dan krijgt de muis de sprite van de slot en die wordt leeg dan;
        else if (mouseFollowr.GetComponent<Image>().sprite == null)
        {
            if (GetComponent<Image>().sprite != null)
            {
                mouseFollowr.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
                GetComponent<Image>().sprite = null;
                mouseFollowr.GetComponent<Image>().enabled = true;
            }
        }
    }
}

