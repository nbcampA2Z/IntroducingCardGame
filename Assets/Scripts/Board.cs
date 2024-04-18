using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Board : MonoBehaviour
{
    public GameObject card;

    int[] arr;

    void Start()
    {
        // 인덱스 순서 랜덤화
        if (GameManager.Instance.level == 2)
        {
            arr = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 }; //카드 20개로 늘리기
            arr = arr.OrderBy(x => Random.Range(0f, 10f)).ToArray();
        }
        else
        {
            arr = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
            arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();
        }
        
        // 보드에 카드 배치
        for (int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.3f - 1.9f;
            float y = (i / 4) * 1.3f - 3.2f; 

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        // 남은 카드 수 설정
        GameManager.Instance.cardCount = arr.Length;
    }
}