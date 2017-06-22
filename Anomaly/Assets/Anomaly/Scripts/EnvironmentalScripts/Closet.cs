using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour {

    public GameObject phone;
    public GameObject enemyPos;
    Animator anim;
    bool isOpen;
    public float spawnChance;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Use()
    {
        if (isOpen)
        {
            isOpen = false;
            anim.SetTrigger("Close");
            return;
        }
        else
        {
            isOpen = true;
            float randomInt = Random.Range(0, 100);
            print(randomInt.ToString());
            if(randomInt <= spawnChance)
            {
                print("Put enemy in closet");
                //Put enemy in closet
                enemyPos.SetActive(true);
                phone.GetComponent<Phone>().InsanityBoost(20);
                StartCoroutine(EnemyDisapear());
            }
            anim.SetTrigger("Open");
        }
    }
    IEnumerator EnemyDisapear()
    {
        yield return new WaitForSeconds(1.5f);
        //Do particle shit
        enemyPos.SetActive(false);
    }
}
