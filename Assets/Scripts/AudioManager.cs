using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;
    public AudioClip clip;
    bool PitchFlag = false; // ��ġ�� ���� ���� üũ ����


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

    // BGM�� ��ġ���� �����ϱ� ���� ���Ƿ� PitchFlag �Լ��� ����

    void Update()
    {
        //���� �ð��� N�� �̸��϶� , ��ġ �÷��� �Լ��� false�ϴ� AudioSorce���� ��ġ���� 1.3��� ���� (������ 10�ʷ� ����)
        //Updatd ���̶� �����Ӹ��� 1.3�谡 ���� �������� PitchFlag üũ�� �ѹ��� �����ؾ� ��
        if (GameManager.Instance.time < 10.0f && PitchFlag == false)
        {
            GetComponent<AudioSource>().pitch = audioSource.pitch * 1.3f;
            PitchFlag = true;
        }

        // Ÿ�ӿ����� �ǰų� ī�带 �� ���缭 �¸��ϸ� bgm ����
        // Ÿ�ӿ����ÿ��� ���Ŀ� �ٸ� bgm�� �߰� ������ ���� ����

        if(GameManager.Instance.time <= 0.0f || GameManager.Instance.cardCount == 0)
        {
            Destroy(gameObject );
        }
    }
}
