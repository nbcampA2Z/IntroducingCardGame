using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;

    void Start()
    {
        // 인덱스 순서 랜덤화
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 }; //카드가 20개가 됐으므로 맞는 짝만큼 arr를 증가시킴
        arr = arr.OrderBy(x => Random.Range(0f, 10f)).ToArray();

        // 보드에 카드 배치
        for (int i = 0; i < arr.Length; i++) //카드갯수를 20개로 추가해줌 i < 20 을  arr.Length로 바꿔줬음 매번 바꿔주기 불편해서
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.3f - 1.9f; //카드가 증가함에 따라 카드크기를 조금 작게 만들어줬음
            float y = (i / 4) * 1.3f - 3.2f; //카드가 증가함에 따라 카드크기를 조금 작게 만들어줬음

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        // 남은 카드 수 설정
        GameManager.Instance.cardCount = arr.Length;
    }
}
