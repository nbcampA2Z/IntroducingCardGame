using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;
    public AudioClip clip;
    bool PitchFlag = false; // 피치값 조절 동작 체크 변수


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.clip;
        audioSource.Play();
    }

    // BGM의 피치값을 조절하기 위해 임의로 PitchFlag 함수를 만듬

    void Update()
    {
        //남은 시간이 N초 미만일때 , 피치 플래그 함수가 false일대 AudioSorce에서 피치값을 1.3배로 조절 (지금은 10초로 설정)
        //Updatd 문이라 프레임마다 1.3배가 되지 않으려면 PitchFlag 체크로 한번만 동작해야 함
        if (GameManager.Instance.time <= GameManager.Instance.timeBomb && PitchFlag == false)
        {
            GetComponent<AudioSource>().pitch = audioSource.pitch * 1.3f;
            PitchFlag = true;
        }

        // 타임오버가 되거나 카드를 다 맞춰서 승리하면 bgm 종료
        // 타임오버시에는 추후에 다른 bgm을 추가 삽입할 수도 있음

        if(GameManager.Instance.time <= 0.0f || GameManager.Instance.cardCount == 0)
        {
            Destroy(gameObject );
        }
    }
}
