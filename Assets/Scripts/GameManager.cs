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
    float time = 0.0f;

    public GameObject endTxt;
    public int cardCount = 0;

    AudioSource audioSource;
    public AudioClip clip;

    public Text nameTxt;

    public Text flapcntTxt; // 카드를 뒤집기위한 텍스트 공간
    public int flapCnt; // 카드 두장을 뒤집은 횟수
    public float timeOut; // 카드를 다시 뒤집을때 사용하는 카운트다운

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        
        if(time >= 30.0f)
        {
            Time.timeScale = 0.0f;
            endTxt.SetActive(true);
        }
        if (firstCard != null)
        {
            timeOut -= Time.deltaTime;
            if (timeOut <= 0f)
            {
                firstCard.ClosedCardInvoke();//첫번째 카드 다시 뒤집기
                CountTry();// 시도횟수세기
                firstCard = null;
            }
        }
        else
        {
            timeOut = 5f;
        }
        Debug.Log(timeOut);// 카운트다운 확인용
    }
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            ShowName(true);
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            CountTry(); // 시도횟수 세는함수
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
            CountTry(); // 시도횟수 세는함수
            time += 1f; // 실패시 시간추가 카운트다운 일시 마이너스로 바꿔주면됨
        }

        firstCard = null;
        secondCard = null;
    }

    public void ShowName(bool isAnswer)
    {
        if (isAnswer)
        {
            nameTxt.text = firstCard.name;
        }
        else
        {
            nameTxt.text = "실패";
        }
        nameTxt.gameObject.SetActive(true);
    }
    public void CountTry() // 시도횟수 세는함수
    {
        flapCnt += 1;
        flapcntTxt.text = flapCnt.ToString();
    }
}