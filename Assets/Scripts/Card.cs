using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;         // 카드 번호
    public bool flipped;        // 뒤집기 확인
    public GameObject front;    // 카드 앞면 (사진)
    public GameObject back;     // 카드 뒷면 (물음표)
    public Animator anim;       // 카드 애니메이터
    public SpriteRenderer frontImage;
    public SpriteRenderer backImage;
    AudioSource audioSource;
    public AudioClip clip;      // 카드 뒤집는 소리
    public string name;         // 이름
    string[] nameArr = { "손영주", "김재혁", "임재훈", "박신후", "신재원", "손영주", "김재혁", "임재훈", "박신후", "신재원" };

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        backImage = back.GetComponent<SpriteRenderer>(); //뒤집기 후 색 변경을 위한 GetComponent
    }

    /* Setting 함수
     * 카드 인덱스에 따라 사진과 이름을 설정해줌
     * 카드 인덱스를 int형 변수 number로 받음
     */
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"{idx}");
        name = nameArr[idx];
    }

    /* OpenCard 함수
     * 카드를 오픈하고 애니메이션과 소리를 출력함
     * 두 번째로 뒤집은 카드일 경우 서로 일치하는 카드인지
     * 게임매니저의 Matched() 함수를 통해 검사함
     */
    public void OpenCard()
    {
        if (GameManager.Instance.secondCard != null) return;

        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);

        //따로 빼둔 FlipCardInvoke 함수를 invoke 함수로 지연시켜 불러옴
        front.SetActive(true);
        back.SetActive(false);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }

    //카드의 back과 front를 켜고 끄는 기능을 invoke 시키기 위해
    //FlipCardInvoke 함수로 따로 빼놓음

    /* DestroyCard 함수
     * 해당 카드를 보드에서 제외함
     * 오픈한 두 카드가 일치할 경우 이 함수가 호출됨
     */
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    /* CloseCard 함수
    * 해당 카드를 보드에서 다시 엎어 놓음
    * 오픈한 두 카드가 다를 경우 이 함수가 호출됨
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

    /*public void Zed()
    {
        anim.SetBool("isZed", true);
    }*/

    public void ColorChange() //색 변경 함수 불러오기
    {
        Invoke("ColorChangeInvoke", 1.0f);
    }

    void ColorChangeInvoke() //색 변경 함수
    {
        backImage.color = Color.grey;
    }

}
