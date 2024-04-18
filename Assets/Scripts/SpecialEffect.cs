using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpecialEffect : MonoBehaviour
{
    public float speed;
    public bool isZed = false;
    GameObject square;
    SpriteRenderer sr;
    private GameObject go;

    private void Start()
    {
        sr = go.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isZed == false && sr.material.color.a > 0)
        {
            //square.SetActive(false);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0f, 0f), Time.deltaTime * speed);
            StartCoroutine(FadeOut());
            isZed = false;
            square.SetActive(true);
        }
    }

    IEnumerator FadeOut()
    {
        float f = 1;
        while (f > 0)
        {
            f -= 0.1f;
            Color ColorAlpha = sr.material.color;
            ColorAlpha.a = f;
            sr.material.color = ColorAlpha;
            yield return new WaitForSeconds(0.02f);
        }
        /*for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }*/
    }
}
