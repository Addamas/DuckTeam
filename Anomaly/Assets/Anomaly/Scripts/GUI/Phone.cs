using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Phone : MonoBehaviour
{

    public LocationService location;

    #region Battery
    private bool dead;
    [Range(0f, 100f)]
    public float battery = 100f;
    public float batteryDrain = 0.25f;
    public float normalBatteryDrain = 0.25f;
    public float fastBatteryDrain = 1f;
    public Image batteryWarning;
    public Image batteryFill;
    public bool firstWarning;
    public bool secondWarning;
    public Text batteryCount;
    public bool goFaster;
    public bool empty;
    public float chargeSpeed;
    public bool inHand;
    public GameObject phoneCase;
    public Canvas phoneCanvas;

    public void BatteryDrain()
    {
        if (goFaster)
        {
            batteryDrain = fastBatteryDrain;
        }
        else
        {
            batteryDrain = normalBatteryDrain;
        }
        if (!empty)
        {
            battery -= batteryDrain * Time.deltaTime;
            float dr = 1;
            dr = battery / 100;
            batteryFill.fillAmount = dr;
            int b = (int)battery;
            batteryCount.text = b.ToString() + "%";
            BatteryWarning();
        }
    }

    public void BatteryWarning()
    {
        if (battery <= 0f)
        {
            if (!empty)
            {
                battery = 0f;
                print("BATTERY DEAD");
                empty = true;
                phoneScreen.SetActive(false);
                return;
            }
        }
        if (battery > 0f && empty)
        {
            dead = false;
            empty = false;
            phoneScreen.SetActive(true);
        }
        if (battery < 25f)
        {
            if (!firstWarning)
            {
                firstWarning = true;
                StartCoroutine(Warn());
            }
            else if (battery < 10 && !secondWarning)
            {
                secondWarning = true;
                StartCoroutine(Warn());
            }
        }
    }

    public void Charge()
    {
        print("Charging");
        if(battery >= 100)
        {
            battery = 100;
            return;
        }
        battery += chargeSpeed * Time.deltaTime;
        if (secondWarning)
        {
            if(battery > 10)
            {
                secondWarning = false;
            }
        }
        if (firstWarning)
        {
            if (battery > 25)
            {
                firstWarning = false;
            }
        }
    }
    IEnumerator Warn()
    {
        batteryWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        batteryWarning.gameObject.SetActive(false);
    }
    #endregion
    #region Insanity
    public float insanity = 0f;
    public float bgFill;
    public bool[] insaneChecks;
    public float insaneBoost;
    public Image insaneFill;

    public void InsanityRaise()
    {
        insanity += insaneBoost * Time.deltaTime;
        bgFill -= insaneBoost * Time.deltaTime;
        float dr = 1;
        dr = bgFill / 100;
        insaneFill.fillAmount = dr;
        InsanityCheck();
    }

    public void InsanityCheck()
    {
        if (insanity >= 100)
        {
            insaneChecks[3] = true;
            //print("DEAD");
            return;
        }
        if (insanity >= 75)
        {
            print("Insanity Check2");
            insaneChecks[2] = true;
            anims[3].SetBool("Insane", true);
            anims[4].SetBool("Insane", true);
            anims[7].SetBool("Insane", true);
            return;
        }
        if (insanity >= 50)
        {
            print("Insanity Check1");
            insaneChecks[1] = true;
            anims[1].SetBool("Insane", true);
            anims[5].SetBool("Insane", true);
            anims[6].SetBool("Insane", true);
            return;
        }
        if (insanity >= 25)
        {
            print("Insanity Check0");
            insaneChecks[0] = true;
            anims[0].SetBool("Insane", true);
            anims[2].SetBool("Insane", true);
            return;
        }
        else
        {
            for (int i = 0; i < anims.Length; i++)
            {
                anims[i].SetBool("Insane", false);
            }
        }
    }
    #endregion
    #region Messages
    public GameObject[] messages;
    public GameObject[] messagePages;
    public Text[] messageText;
    int openMessagePage;

    public void PrepareMessages()
    {
        messageText[0].text = "Please... " + playerName + " I know you know, ... Letter you must";
    }
    public void OpenMessage(int i)
    {
        pages[0].SetActive(false);
        messagePages[i].SetActive(true);
        openMessagePage = i;
    }
    #endregion


    public Text time;
    public GameObject[] pages; //0 = Messages, 1 = Notes, 2 = Map, 3 = Gallery, 4 = Camera, 5 = Flashlight, 6 = MiniGame, 7 = Insanity, 8 = Options, 9 = Homescreen
    public Animator[] anims; //0 = Messages, 1 = Notes, 2 = Options, 3 = Flashlight, 4 = Camera, 5 = Map, 6 = Gallery, 7  = Inventory
    public GameObject fLight;
    int openScreen;
    public GameObject phoneScreen;
    public string playerName;

    public void Start()
    {
        playerName = Environment.UserName;
        batteryWarning.gameObject.SetActive(false);
        time.text = System.DateTime.Now.TimeOfDay.Hours.ToString() + ":" + System.DateTime.Now.TimeOfDay.Minutes.ToString();
        StartCoroutine("TimeCheck", 5f);
        PrepareMessages();
    }

    public void Update()
    {
        BatteryDrain();
        InsanityRaise();
        Hand();
        /*
        if (!dead)
        {
            BatteryDrain();
            InsanityRaise();
        }
        */
    }

    public void Hand()
    {
        if (Input.GetButtonDown("Tab"))
        {
            if (inHand)
            {
                inHand = false;
                phoneCanvas.GetComponent<Canvas>().enabled = false;
                phoneCase.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                inHand = true;
                phoneCanvas.GetComponent<Canvas>().enabled = true;
                phoneCase.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
    

    IEnumerator TimeCheck (float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if(System.DateTime.Now.TimeOfDay.Minutes.ToString().Length < 2)
            {
                //This prevents the time from appearing like 11:7 instead of 11:07
                time.text = System.DateTime.Now.TimeOfDay.Hours.ToString() + ":" + "0" + System.DateTime.Now.TimeOfDay.Minutes.ToString();
            }
            else
            {
                time.text = System.DateTime.Now.TimeOfDay.Hours.ToString() + ":" + System.DateTime.Now.TimeOfDay.Minutes.ToString();
            }
        }
    }

    public void ButtonClick(int i)
    {
        if(i == 9)
        {
            //Back to homescreen
            pages[i].SetActive(true);
            pages[openScreen].SetActive(false);
            messagePages[openMessagePage].SetActive(false);
            goFaster = false;
        }
        else if(i == 5)
        {
            if (anims[3].GetBool("On"))
            {
                anims[3].SetBool("On", false);
                fLight.SetActive(false);
                goFaster = false;
            }
            else
            {
                anims[3].SetBool("On", true);
                fLight.SetActive(true);
                goFaster = true;
            }
        }
        else
        {
            pages[9].SetActive(false);
            pages[i].SetActive(true);
            openScreen = i;
            if(i == 4)
            {
                goFaster = true;
            }
        }
    }
}
