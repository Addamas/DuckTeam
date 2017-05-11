using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour {

    public GameObject sprite;
    float alpha;

	// Use this for initialization
	void Start () {
        sprite = GameObject.Find("BlackBG");
        FadeFromBlack();
	}
	
	// Update is called once per frame
	void FadeFromBlack () {
        //sprite.GetComponent<Image>().GetComponent<Color>().a = alpha;
        alpha = Mathf.Lerp(1, 0, 1 * Time.deltaTime);

    }
}
