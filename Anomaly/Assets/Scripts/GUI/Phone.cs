using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{

    public LocationService location;

    #region Battery
        private bool dead;
        [Range(0f,100f)]
        public float battery = 100f;
        public float batteryDrain = 0.25f;
        public Image batteryWarning;
        public bool firstWarning;
        public bool secondWarning;

        public void BatteryDrain()
        {
            battery -= batteryDrain * Time.deltaTime;
            BatteryWarning();
        }

        public void BatteryWarning()
        {
            if (battery <= 0f)
            {
                dead = true;
                battery = 0f;
                return;
            }

            if (battery > 9.5f && battery < 10.5f)
            {
                if (!secondWarning)
                {
                    batteryWarning.gameObject.SetActive(true);
                    secondWarning = true;
                }
            }

            else if (battery > 19.5f && battery < 20.5f)
            {
                if (!firstWarning)
                {
                    batteryWarning.gameObject.SetActive(true);
                    firstWarning = true;
                }
            }
        }
    #endregion

    public Text time;
    public GameObject[] pages; //0 = Messages, 1 = Notes, 2 = Map, 3 = Gallery, 4 = Camera, 5 = Flashlight, 6 = MiniGame, 7 = Insanity, 8 = Options, 9 = Homescreen
    int openScreen;

    public void Start()
    {
        batteryWarning.gameObject.SetActive(false);
        time.text = System.DateTime.Now.TimeOfDay.Hours.ToString() + ":" + System.DateTime.Now.TimeOfDay.Minutes.ToString();
        StartCoroutine("TimeCheck", 5f);
    }

    public void Update()
    {
        if(!dead)
            BatteryDrain();
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
        }
        else
        {
            pages[9].SetActive(false);
            pages[i].SetActive(true);
            openScreen = i;
        }
        /*
        switch (i)
        {
            case 0:
                //Messages
                break;
            case 1:
                //Notes
                break;
            case 2:
                //Map
                break;
            case 3:
                //Gallery
                break;
            case 4:
                //Camera
                break;
            case 5:
                //FlashLight
                break;
            case 6:
                //Minigame
                break;
            case 7:
                //Insanity
                break;
            case 8:
                //Options
                break;
        }
        */
    }
}
