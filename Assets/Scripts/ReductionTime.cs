using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReductionTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyReductionTime", 0.5f); // 0.5초 뒤 파괴
    }

    // Update is called once per frame
    void Update()
    {
        float gmaeTime = GameManager.Instance.time;
        if(gmaeTime <= 0)
        {
            Destroy(gameObject); // 게임이 끝나고 남아있는 효과 없애주기
        }
    }

    void DestroyReductionTime()
    {
        Destroy(gameObject);
    }
}
