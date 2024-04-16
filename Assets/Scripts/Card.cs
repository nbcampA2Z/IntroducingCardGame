using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;         // ī�� ��ȣ
    public GameObject front;    // ī�� �ո� (����)
    public GameObject back;     // ī�� �޸� (����ǥ)
    public Animator anim;       // ī�� �ִϸ�����
    public SpriteRenderer frontImage;
    AudioSource audioSource;
    public AudioClip clip;      // ī�� ������ �Ҹ�
    public string name;         // �̸�
    string[] nameArr = { "�տ���", "������", "������", "�ڽ���", "�����", "�տ���", "������", "������", "�ڽ���", "�����" };

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /* Setting �Լ�
     * ī�� �ε����� ���� ������ �̸��� ��������
     * ī�� �ε����� int�� ���� number�� ����
     */
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"{idx}");
        name = nameArr[idx];
    }

    /* OpenCard �Լ�
     * ī�带 �����ϰ� �ִϸ��̼ǰ� �Ҹ��� �����
     * �� ��°�� ������ ī���� ��� ���� ��ġ�ϴ� ī������
     * ���ӸŴ����� Matched() �Լ��� ���� �˻���
     */
    public void OpenCard()
    {
        if (GameManager.Instance.secondCard != null) return;

        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if(GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }

    /* DestroyCard �Լ�
     * �ش� ī�带 ���忡�� ������
     * ������ �� ī�尡 ��ġ�� ��� �� �Լ��� ȣ���
     */
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    /* CloseCard �Լ�
    * �ش� ī�带 ���忡�� �ٽ� ���� ����
    * ������ �� ī�尡 �ٸ� ��� �� �Լ��� ȣ���
    */
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
