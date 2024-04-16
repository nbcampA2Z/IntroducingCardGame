using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;  // ó�� ������ ī��
    public Card secondCard; // �� ��° ������ ī��
    public Text timeTxt;    // ���� �ð� �ؽ�Ʈ
    public Text nameTxt;    // �̸� �ؽ�Ʈ
    public GameObject endTxt;   // �������� ����
    AudioSource audioSource;
    public AudioClip clip;  // ���� �� ��µ� �Ҹ�

    public Animator timeAnim; // �ð��� �˹��� �� �ִϸ��̼�
    bool playTimeAnim = false; // �ִϸ��̼� ���� �Ҹ��� ������ üũ
    public float timeBomb = 5.0f; // �ִϸ��̼� ���� �ð�
    public float time = 30.0f;      // ���� �ð� 
                                    // AudioManger���� �����ؾ��ؼ� public���� ���ƾ��

    public int cardCount = 0;   // ���忡 ���� ī�� ��

    public int flapCnt;     // �õ� Ƚ��(ī�带 ������ Ƚ��)
    public Text flapcntTxt; // �õ� Ƚ�� �ؽ�Ʈ
    public float timeOut;   // ī�� ���� �� �ð� ī��Ʈ

    public Text scoreTxt; // ���� ����
    float score;

    

private void Awake()
    {
        if (Instance == null)
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
        time -= Time.deltaTime; // �ð� ������ ������ ī��Ʈ �ٿ� �ϰ� time������ �ֱ�
        timeTxt.text = time.ToString("N2"); // time������ ���� �Ǽ��� ���������� �ٲ㼭 Text���� �ֱ�
        score = time - flapCnt;  // ������ ��Ÿ���� ���� ���� �ð����� ����� Ƚ���� ���ְ� score������ �־��ֱ�
        if (score < 0.0f)
        {
            score = 0.0f;
        }

        // 30�� ����� ���� ����
        if (time <= 0.0f)
        {
            Time.timeScale = 0.0f;
            endTxt.SetActive(true);

        }

        // ù ī�� ���� �� 5�� ��� �� �ٽ� �������
        if (firstCard != null)
        {
            timeOut -= Time.deltaTime;
            if (timeOut <= 0f)
            {
                firstCard.CloseCardInvoke();    // ù ī�� �ٽ� �������
                CountTry();                     // �õ� Ƚ�� 1 ����
                firstCard = null;
            }
        }
        else
        {
            timeOut = 5f;
        }
        scoreTxt.text = score.ToString("N0");// score������ ���� �Ǽ��� ���������� �ٲ㼭 Text���� �ֱ�

    }

    /* Matched �Լ�
     * 2���� ī�带 �������� �� ���� ��ġ�ϴ���(����) ����ġ�ϴ���(����) �Ǻ���
     */
    public void Matched()
    {
        // ��ġ�� ���(����)
        if (firstCard.idx == secondCard.idx)
        {
            ShowName(true); // �̸� ���
            CountTry(); // �õ�Ƚ�� 1 ����
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            score += 1f; // ������ �÷��� 1�����ֱ�
            cardCount -= 2;
            // ������ ī���� ��� ���� ����
            if (cardCount == 0)
            {
                // ����ī�� 0��(�¸�)�� ����� ���
                
                Time.timeScale = 0.0f;
                endTxt.SetActive(true);
            }
        }
        // ����ġ�� ���(����)
        else
        {
            ShowName(false); // "����" ���� ���
            CountTry(); // �õ�Ƚ�� 1 ����
            firstCard.CloseCard();
            secondCard.CloseCard();
            time -= 1f; // ���н� �ð��߰� ī��Ʈ�ٿ� �Ͻ� ���̳ʽ��� �ٲ��ָ��
        }
        // �ʱ�ȭ
        firstCard = null;
        secondCard = null;
    }

    /* ShowName �Լ�
     * Matched �Լ��� ���� �Ǻ��� ��Ī ���� ���ΰ�
     * ������ ��� �̸�, ������ ��� '����'�� NameTxt�� �����
     * ��Ī ���� ���θ� Boolean ���� isAnswer�� ���ڷ� ���� 
     * isAnswer�� true�� ��� ù ī�� �̸��� ���
     * isAnswer�� false�� ��� "����" ���
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

    /* CountTry �Լ�
     * ī�带 �������� �� �õ� Ƚ���� 1 ����������
     */
    public void CountTry()
    {
        flapCnt += 1;
        flapcntTxt.text = flapCnt.ToString();
    }

}