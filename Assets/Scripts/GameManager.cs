using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;
    public Text timeTxt;
    public Text nameTxt;
    public GameObject endTxt;
    AudioSource audioSource;
    public AudioClip clip;

    float time = 0.0f;
    public int cardCount = 0;

    public Text flapcntTxt; // ī�带 ���������� �ؽ�Ʈ ����
    public int flapCnt; // ī�� ������ ������ Ƚ��
    public float timeOut; // ī�带 �ٽ� �������� ����ϴ� ī��Ʈ�ٿ�

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        
        // 30�� ����� ���� ����
        if(time >= 30.0f)
        {
            Time.timeScale = 0.0f;
            endTxt.SetActive(true);
        }

        // ù ī�� ���� �� 5�� ��� �� �ٽ� ��Ʈ
        if (firstCard != null)
        {
            timeOut -= Time.deltaTime;
            if (timeOut <= 0f)
            {
                firstCard.CloseCardInvoke();    // ù ī�� �ٽ� ��Ʈ
                CountTry();                     // �õ� Ƚ�� ī��Ʈ
                firstCard = null;
            }
        }
        else
        {
            timeOut = 5f;
        }

        // Debug.Log(timeOut);
    }

    /* */

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            ShowName(true);
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            CountTry(); // �õ�Ƚ�� �����Լ�
            if (cardCount == 0)
            {
                Time.timeScale = 0.0f;
                endTxt.SetActive(true);
            }
        }
        else
        {
            ShowName(false);
            firstCard.CloseCard();
            secondCard.CloseCard();
            CountTry(); // �õ�Ƚ�� �����Լ�
            time += 1f; // ���н� �ð��߰� ī��Ʈ�ٿ� �Ͻ� ���̳ʽ��� �ٲ��ָ��
        }

        firstCard = null;
        secondCard = null;
    }

    /*
     * ī�带 �������� �� ������ ��� �̸�, ������ ��� '����'�� NameTxt�� ����ִ� �Լ�
     * ī�� ���� ���θ� Boolean ���� isAnswer�� ���ڷ� ���� 
     * isAnswer�� true�� ��� ù��° ī���� �̸��� ���
     * isAnswer�� false�� ��� string "����"�� ���
    */
    public void ShowName(bool isAnswer)
    {
        if (isAnswer)
        {
            nameTxt.text = firstCard.name;
        }
        else
        {
            nameTxt.text = "����";
        }
        nameTxt.gameObject.SetActive(true);
    }

    public void CountTry() // �õ�Ƚ�� �����Լ�
    {
        flapCnt += 1;
        flapcntTxt.text = flapCnt.ToString();
    }
}