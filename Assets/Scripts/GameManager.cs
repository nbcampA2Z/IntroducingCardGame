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
            if(cardCount == 0)
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
            nameTxt.text = "½ÇÆÐ";
        }
        nameTxt.gameObject.SetActive(true);
    }
}