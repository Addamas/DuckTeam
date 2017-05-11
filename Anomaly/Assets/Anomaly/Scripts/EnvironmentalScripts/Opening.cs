using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour {

    public float alpha;
    public float endalpha;
    public Color color;

	// Use this for initialization
	void Start () {
        color = GameObject.Find("BlackBG").GetComponent<Image>().color;
        endalpha = 0;
        StartCoroutine("FadeFromBlack");
        Destroy(gameObject, 15);
    }

	// Update is called once per frame
	public IEnumerator FadeFromBlack () {
        while (color.a != endalpha)
        {
            print(color.a);
            color.a = Mathf.Lerp(color.a, 0, .3f * Time.deltaTime);
            GameObject.Find("BlackBG").GetComponent<Image>().color = color;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}