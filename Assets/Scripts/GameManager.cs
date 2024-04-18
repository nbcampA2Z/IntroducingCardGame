using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject reductionTime; // 1초 감소 프리팹 받아오기

    public Card firstCard;  // 처음 오픈한 카드
    public Card secondCard; // 두 번째 오픈한 카드
    public Text timeTxt;    // 남은 시간 텍스트
    public Text nameTxt;    // 이름 텍스트
    public GameObject endTxt;   // 게임종료 문구
   
    AudioSource audioSource;
    public AudioClip clip;  // 성공 시 출력될 소리
    public AudioClip notMatched; // 실패 시 출력될 소리
    public AudioClip Victory; // 카드 다 맞추면 출력될 소리
    public AudioClip Fail;    // 게임오버시 출력될 소리

    bool isFail = false;

    public Animator timeAnim; // 시간이 촉박할 시 애니메이션
    bool playTimeAnim = false; // 애니메이션 동작 불리언 변수로 체크
    public float timeBomb = 5.0f; // 애니메이션 시작 시간
    public float time = 30.0f;      // 남은 시간 
                                    // AudioManger에서 접근해야해서 public으로 수정

    public int cardCount = 0;   // 보드에 남은 카드 수

    public int flapCnt;     // 시도 횟수(카드를 오픈한 횟수)
    public Text flapcntTxt; // 시도 횟수 텍스트
    public float timeOut;   // 카드 오픈 후 시간 카운트
    

    public Text scoreTxt; // 게임 점수
    int score; //점수 초기값 // float -> int
    public GameObject board;// 보드 게임오브젝트

    public GameObject scoreEffect; // 점수 이펙트 프리팹
    public bool isPlus = false; // 플러스 or 마이너스
    public int plusScore = 10; // 플러스 점수
    public int minusScore = 1; // 마이너스 점수
    public Color textColor = Color.green;
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
        time -= Time.deltaTime; // 시간 프레임 단위로 카운트 다운 하고 time변수에 넣기
        timeTxt.text = time.ToString("N2"); // time변수에 넣은 실수를 문자형으로 바꿔서 Text에다 넣기
        // 시간이 설정 시간 이하이면 애니메이션 동작
        // playTimeAnim 을 체크하는 이유: 업데이트문이므로 반복적으로 실행 방지
        if (time <= timeBomb && playTimeAnim == false) 
        {
            playTimeAnim = true; // true 로 바꿔줌으로써 반복 실행 방지
            timeAnim.SetBool("startBomb", true); // 애니메이션 실행
        }
        if (score <= 0)
        {
            score = 0;
        }

        // 0초가 되면 게임 종료
        if (time <= 0.0f)
        {
            //update 문이라 한번만 출력해야해서 boolen값으로 체크해줌
            if (!isFail)
            {
                //실패 브금 출력 및 볼륨 0.3배로 조절
                audioSource.PlayOneShot(Fail);
                GetComponent<AudioSource>().volume = audioSource.volume * 0.3f;
                isFail = true;
            }
            time = 0.0f; // 오차 제거
            Time.timeScale = 0.0f;
            endTxt.SetActive(true);
            board.SetActive(false);
            nameTxt.gameObject.SetActive(false);
            flapcntTxt.text = flapCnt.ToString();
        }

        // 첫 카드 오픈 후 5초 경과 시 다시 엎어놓음
        if (firstCard != null)
        {
            timeOut -= Time.deltaTime;
            if (timeOut <= 0f)
            {
                firstCard.CloseCardInvoke();    // 첫 카드 다시 엎어놓음
                CountTry();                     // 시도 횟수 1 증가
                firstCard = null;
            }
        }
        else
        {
            timeOut = 3f;
        }
        scoreTxt.text = score.ToString(); // 점수 표기
     
    }
    /* Matched 함수
     * 2장의 카드를 오픈했을 때 서로 일치하는지(성공) 불일치하는지(실패) 판별함
     */
    public void Matched()
    {
        // 일치할 경우(성공)
        if (firstCard.idx == secondCard.idx)
        {
            ShowName(true); // 이름 출력
            CountTry(); // 시도횟수 1 증가
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            score += plusScore; // 성공시 플러스 10점해주기
            
            isPlus = true;
            Instantiate(scoreEffect, scoreTxt.transform);

            cardCount -= 2;
            // 마지막 카드일 경우 게임 종료
            if (cardCount == 18)
            {           
                // 남은카드 0장(승리)시 오디오 출력
                GetComponent<AudioSource>().volume = audioSource.volume * 0.3f;
                audioSource.PlayOneShot(Victory);
                Time.timeScale = 0.0f;
                flapcntTxt.text = flapCnt.ToString();
                //폰트그대로 wid 550 색깔 초록
                endTxt.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 550f);
                endTxt.GetComponent<Text>().text = "클리어!";
                endTxt.GetComponent<Text>().color = textColor;

                // endTxt.GetComponent<Text>().fontSize = 100; 폰트사이즈 바꾸는 방법
                // endTxt.GetComponent<Text>().color = new Color(0f, 1f, 0f); // rgb값으로 색깔 바꾸는 방법
                endTxt.SetActive(true);
                board.SetActive(false);
                nameTxt.gameObject.SetActive(false);
            }
        }
        // 불일치할 경우(실패)
        else
        {
            ShowName(false); // "실패" 문구 출력

            if (firstCard.flipped == true || secondCard.flipped == true) // 뒤집힌 카드 확인
            {
                audioSource.PlayOneShot(notMatched);// 땡소리 출력
                time -= 1f; // 실패시 시간추가 카운트다운 일시 마이너스로 바꿔주면됨
                ShowName(false); // "실패" 문구 출력
                score -= minusScore; // 실패시 점수 마이너스 1점하기

                isPlus = false;
                Instantiate(scoreEffect, scoreTxt.transform);

                Instantiate(reductionTime, timeTxt.gameObject.transform); // 1초 감소 프리팹 생성, 부모 위치 기준으로
            }


            CountTry(); // 시도횟수 1 증가
            firstCard.CloseCard();
            secondCard.CloseCard();
            firstCard.ColorChange(); //닫힌 후 색 변경
            secondCard.ColorChange(); //닫힌 후 색 변경2
            firstCard.flipped = true; // 뒤집힌 카드
            secondCard.flipped = true; // 뒤집힌 카드2
        }
        // 초기화
        firstCard = null;
        secondCard = null;
    }

    /* ShowName 함수
     * Matched 함수에 의해 판별된 매칭 성공 여부가
     * 성공일 경우 이름, 실패일 경우 '실패'를 NameTxt에 띄워줌
     * 매칭 성공 여부를 Boolean 변수 isAnswer에 인자로 받음 
     * isAnswer가 true일 경우 첫 카드 이름을 출력
     * isAnswer가 false일 경우 "실패" 출력
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

    /* CountTry 함수
     * 카드를 오픈했을 때 시도 횟수를 1 증가시켜줌
     */
    public void CountTry()
    {
        flapCnt += 1;
        flapcntTxt.text = flapCnt.ToString();
    }
}
//푸시 확인용 주석입니다


