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

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        
        // 30초 경과시 게임 종료
        if(time >= 30.0f)
        {
            Time.timeScale = 0.0f;
            endTxt.SetActive(true);
        }

        // 첫 카드 오픈 후 5초 경과 시 다시 세트
        if (firstCard != null)
        {
            timeOut -= Time.deltaTime;
            if (timeOut <= 0f)
            {
                firstCard.CloseCardInvoke();    // 첫 카드 다시 세트
                CountTry();                     // 시도 횟수 카운트
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

    /*
     * 카드를 뒤집었을 때 성공일 경우 이름, 실패일 경우 '실패'를 NameTxt에 띄워주는 함수
     * 카드 성공 여부를 Boolean 변수 isAnswer에 인자로 받음 
     * isAnswer가 true일 경우 첫번째 카드의 이름을 출력
     * isAnswer가 false일 경우 string "실패"를 출력
    */
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