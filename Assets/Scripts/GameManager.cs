using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject reductionTime; // 1초 감소 프리팹 받아오기
    public GameObject canvas; // 캔버스 위치 받기 위해

    public Card firstCard;  // 처음 오픈한 카드
    public Card secondCard; // 두 번째 오픈한 카드
    public Text timeTxt;    // 남은 시간 텍스트
    public Text nameTxt;    // 이름 텍스트
    public GameObject endTxt;   // 게임종료 문구
    AudioSource audioSource;
    public AudioClip clip;  // 성공 시 출력될 소리
    public AudioClip notMatched; // 실패 시 출력될 소리
    public AudioClip Victory; // 카드 다 맞추면 출력될 소리

<<<<<<< HEAD
    float time = 30.0f;      // ���� �ð�
    public int cardCount = 0;   // ���忡 ���� ī�� ��

    public int flapCnt;     // �õ� Ƚ��(ī�带 ������ Ƚ��)
    public Text flapcntTxt; // �õ� Ƚ�� �ؽ�Ʈ
    public float timeOut;   // ī�� ���� �� �ð� ī��Ʈ
    public Text scoreTxt; // ������ ����üũ
    float score;
=======
    public Animator timeAnim; // 시간이 촉박할 시 애니메이션
    bool playTimeAnim = false; // 애니메이션 동작 불리언 변수로 체크
    float timeBomb = 5.0f; // 애니메이션 시작 시간
    public float time = 30.0f;      // 남은 시간 
                                    // AudioManger에서 접근해야해서 public으로 고쳤어요
    
    public int cardCount = 0;   // 보드에 남은 카드 수

    public int flapCnt;     // 시도 횟수(카드를 오픈한 횟수)
    public Text flapcntTxt; // 시도 횟수 텍스트
    public float timeOut;   // 카드 오픈 후 시간 카운트

>>>>>>> 846f9b0265e47268ad690e682b9bb073c2ccac47
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
<<<<<<< HEAD
        time -= Time.deltaTime; // �ð� ������ ������ ī��Ʈ �ٿ� �ϰ� time������ �ֱ�
        timeTxt.text = time.ToString("N2"); // time������ ���� �Ǽ��� ���������� �ٲ㼭 Text���� �ֱ�
        score = time - flapCnt;  // ������ ��Ÿ���� ���� ���� �ð����� ����� Ƚ���� ���ְ� score������ �־��ֱ�
        if (score < 0.0f)
        {
            score = 0.0f;
        }

        // 30�� ����� ���� ����
        if (time <= 0.0f)
=======
        time -= Time.deltaTime; // 남은 시간 감소
        timeTxt.text = time.ToString("N2");

        // 시간이 설정 시간 이하이면 애니메이션 동작  // playTimeAnim 을 체크하는 이유: 업데이트문이므로 반복적으로 실행 방지
        if (time <= timeBomb && playTimeAnim == false)
>>>>>>> 846f9b0265e47268ad690e682b9bb073c2ccac47
        {
            playTimeAnim = true; // true 로 바꿔줌으로써 반복 실행 방지
            timeAnim.SetBool("startBomb", true); // 애니메이션 실행
        }

        // 0초가 되면 게임 종료
        if (time <= 0.0f)
        {
            time = 0.0f; // 오차 제거
            Time.timeScale = 0.0f;
            endTxt.SetActive(true);

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
            timeOut = 5f;
        }
        scoreTxt.text = score.ToString("N0");// score������ ���� �Ǽ��� ���������� �ٲ㼭 Text���� �ֱ�

    }

    /* Matched 함수
     * 2장의 카드를 오픈했을 때 서로 일치하는지(성공) 불일치하는지(실패) 판별함
     */
    public void Matched()
    {
<<<<<<< HEAD
        // ��ġ�� ���(����)
        if (firstCard.idx == secondCard.idx)
=======
        // 일치할 경우(성공)
        if(firstCard.idx == secondCard.idx)
>>>>>>> 846f9b0265e47268ad690e682b9bb073c2ccac47
        {
            ShowName(true); // 이름 출력
            CountTry(); // 시도횟수 1 증가
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
<<<<<<< HEAD
            score += 1f; // ī�� ���⶧���� ���� 1���߰�
            // ������ ī���� ��� ���� ����
=======
            // 마지막 카드일 경우 게임 종료
>>>>>>> 846f9b0265e47268ad690e682b9bb073c2ccac47
            if (cardCount == 0)
            {
                // 남은카드 0장(승리)시 오디오 출력
                audioSource.PlayOneShot(Victory);
                Time.timeScale = 0.0f;
                endTxt.SetActive(true);
            }
        }
        // 불일치할 경우(실패)
        else
        {
            //틀렸을때 땡 소리 출력
            audioSource.PlayOneShot(notMatched);
            ShowName(false); // "실패" 문구 출력
            CountTry(); // 시도횟수 1 증가
            firstCard.CloseCard();
            secondCard.CloseCard();
<<<<<<< HEAD
            time -= 1f; // ���н� �ð��߰� ī��Ʈ�ٿ� �Ͻ� ���̳ʽ��� �ٲ��ָ��
=======
            time -= 1f; // 실패시 시간추가, 카운트다운 일시 마이너스로 바꿔주면됨

            Instantiate(reductionTime, canvas.transform); // 1초 감소 프리팹 생성, 부모 위치 기준으로
>>>>>>> 846f9b0265e47268ad690e682b9bb073c2ccac47
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