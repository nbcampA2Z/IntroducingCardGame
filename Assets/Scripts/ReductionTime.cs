using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReductionTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyReductionTime", 0.5f);// 0.5�� �� �ı�
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyReductionTime()
    {
        Destroy(gameObject);
    }
}