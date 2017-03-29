using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CraftingTable : MonoBehaviour {
    public PlayerControllr playerScript;
    public CamControllr camScript;

    private Transform mouseFollowr;
    public Text textUse;
    public Transform tableManager;
    public Transform craftButton;
    public Transform[] craftSlots;
    public Transform resultSlot;
    bool hasResult;
    public Sprite[] pickAxeRecipe;
    public Sprite pickAxeResult;
    public Sprite[] axeRecipe;
    public Sprite axeResult;
    
    void Start () {
        mouseFollowr = GameObject.Find("MouseFollowr").transform;
        craftButton.GetChild(0).GetComponent<Text>().text = "Check for recipe's";
    }

    #region OnTriggers
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            textUse.gameObject.SetActive(true);
            textUse.text = "Press 'E' to use";
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Use"))
            {
                tableManager.gameObject.SetActive(true);
                textUse.gameObject.SetActive(false);
                playerScript.mayLook = false;
                camScript.mayLook = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            textUse.gameObject.SetActive(false);
            tableManager.gameObject.SetActive(false);
            playerScript.mayLook = true;
            camScript.mayLook = true;
        }
    }
    #endregion

    public void OnClick()
    {//functie voor de craft button;
        if (hasResult == true)
        {//als de recipe's een result hebben gevonden, wordt die gecraft en op je muis gezet.
            mouseFollowr.GetComponent<Image>().sprite = resultSlot.GetComponent<Image>().sprite;
            mouseFollowr.GetComponent<Image>().enabled = true;
            resultSlot.GetComponent<Image>().sprite = null;
            for (int i = 0; i < craftSlots.Length; i++)
            {
                craftSlots[i].GetComponent<Image>().sprite = null;
            }
        }
        Recipes();
    }

    void Recipes()//dit zou wellicht via een IEnumerator kunnen
    {
        //PickAxeRecipe
        for (int i = 0; i < craftSlots.Length; i++)
        {//checkd de sprites van de craftingslots of die overeenkomen met de sprites in de recipe list;
            if (craftSlots[i].GetComponent<Image>().sprite == pickAxeRecipe[i])
            {
                print("A pickaxe");
                if (i == craftSlots.Length -1)
                {//als ze tot de laatste allemaal overeenkomen gaat de result boolean op true en laat ie een sprite zien van de recipe die gemaakt kan worden;
                    print("can be made");
                    hasResult = true;
                    resultSlot.GetComponent<Image>().sprite = pickAxeResult;
                    craftButton.GetChild(0).GetComponent<Text>().text = "Craft item";
                    return;
                }//bij return, stop'd het met verder zoeken naar recipe's aangezien het al een match heeft.
            }
            else
            {//komt een sprite niet overeen, stop met deze recipe te checken en ga naar de volgende.
                print("cannot be made");
                hasResult = false;
                break;
            }
        }

        //AxeRecipe
        for (int i = 0; i < craftSlots.Length; i++)
        {
            if (craftSlots[i].GetComponent<Image>().sprite == axeRecipe[i])
            {
                print("A axe");
                if (i == craftSlots.Length - 1)
                {
                    print("can be made");
                    hasResult = true;
                    resultSlot.GetComponent<Image>().sprite = axeResult;
                    craftButton.GetChild(0).GetComponent<Text>().text = "Craft item";
                    return;
                }
            }
            else
            {
                print("cannot be made");
                hasResult = false;
                break;
            }
        }
    }
}
