using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour {

    public float alpha;
    public float endalpha;
    public Color color;
    public float timer;

	void Start () {
        color = GameObject.Find("BlackBG").GetComponent<Image>().color;
        endalpha = 0;
        timer = 82;
    }

    void Update()
    {
        if (timer <= 0)
        {
            StartCoroutine("FadeFromBlack");
            Destroy(gameObject, 30);
        }
        timer -= Time.deltaTime;
    }

	public IEnumerator FadeFromBlack () {
        while (color.a != endalpha)
        {
            color.a = Mathf.Lerp(color.a, 0, .3f * Time.deltaTime);
            GameObject.Find("BlackBG").GetComponent<Image>().color = color;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}