using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;

    void Start()
    {
        // �ε��� ���� ����ȭ
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 }; //ī�尡 20���� �����Ƿ� �´� ¦��ŭ arr�� ������Ŵ
        arr = arr.OrderBy(x => Random.Range(0f, 10f)).ToArray();

        // ���忡 ī�� ��ġ
        for (int i = 0; i < arr.Length; i++) //ī�尹���� 20���� �߰����� i < 20 ��  arr.Length�� �ٲ����� �Ź� �ٲ��ֱ� �����ؼ�
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.3f - 1.9f; //ī�尡 �����Կ� ���� ī��ũ�⸦ ���� �۰� ���������
            float y = (i / 4) * 1.3f - 3.2f; //ī�尡 �����Կ� ���� ī��ũ�⸦ ���� �۰� ���������

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        // ���� ī�� �� ����
        GameManager.Instance.cardCount = arr.Length;
    }
}
