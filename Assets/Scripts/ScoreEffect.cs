using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour
{
    public Animator anim;
    public Text txt;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.isPlus == true) // 플러스 or 마이너스 애니메이션
        {
            anim.SetBool("isPlus", true);
            txt.text = $"+{GameManager.Instance.plusScore}점"; // 텍스트에 직접 입력
        }
        else
        {
            anim.SetBool("isPlus", false);
            txt.text = $"-{GameManager.Instance.minusScore}점"; // 텍스트에 직접 입력
        }

        Invoke("DestroyScoreEffect", 0.5f); // 0.5초 뒤 파괴
    }

    // Update is called once per frame
    void Update()
    {
        float gmaeTime = GameManager.Instance.time;
        if (gmaeTime <= 0)
        {
            Destroy(gameObject); // 게임이 끝나고 남아있는 효과 없애주기
        }
    }

    void DestroyScoreEffect()
    {
        Destroy(gameObject);
    }
}
