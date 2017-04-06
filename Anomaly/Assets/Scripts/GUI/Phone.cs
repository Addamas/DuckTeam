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
            time.text = System.DateTime.Now.TimeOfDay.Hours.ToString() + ":" + System.DateTime.Now.TimeOfDay.Minutes.ToString();
        }
    }
}
