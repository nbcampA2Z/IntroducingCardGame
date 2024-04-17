using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReductionTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyReductionTime", 0.5f);// 0.5ÃÊ µÚ ÆÄ±«
    }

    // Update is called once per frame
    void Update()
    {
        float gmaeTime = GameManager.Instance.time;
        if(gmaeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyReductionTime()
    {
        Destroy(gameObject);
    }
}
