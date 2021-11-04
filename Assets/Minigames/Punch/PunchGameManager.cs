using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PunchGameManager : MonoBehaviour
{
    public Animator HeroAnimator;
    public Animator ButtonAnimator;
    public float LawyerSpeed;
    public Animator LawyerAnimator;
    public GameObject Lawyer;
    public List<GameObject> LawyerPositionObjs;
    public GameObject LawyerStart;
    public TextMeshProUGUI WinText;
    public GameObject ReadyText;
    public GameObject LoseText;
    public MinigameCompletionHandler MinigameCompletionHandler;

    Vector3 lawyerOrigin;
    Vector3 lawyerTarget;
    int lawyerStep = 0;
    float lawyerDuration = 1f;
    float lawyerElapsed = 0f;
    bool gameOver = false;

    void Awake()
    {
        lawyerOrigin = LawyerStart.transform.position;
        Lawyer.transform.position = lawyerOrigin;
        lawyerTarget = RandomLawyerPosDestructive();
        WinText.gameObject.SetActive(false);
        ReadyText.SetActive(true);
        ReadyText.GetComponent<RectTransform>().DOPunchScale(Vector3.one / 10f, 0.5f);
        LoseText.SetActive(false);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad < 2f)
        {
            return;
        }

        if (ReadyText.activeInHierarchy)
        {
            ReadyText.SetActive(false);
        }

        if (!gameOver && Input.anyKey)
        {
            ButtonAnimator.SetBool("Down", true);
            HeroAnimator.SetTrigger("Punch");
        }
        else if (ButtonAnimator.GetBool("Down"))
        {
            ButtonAnimator.SetBool("Down", false);
        }

        if (!gameOver && Input.anyKeyDown)
        {
            Punch();
        }

        MoveLawyer();
    }

    void Punch()
    {
        float dist = Vector3.Distance(HeroAnimator.gameObject.transform.position, Lawyer.transform.position);
        if (dist < 0.5f)
        {
            StartCoroutine("Win");
        }
        else
        {
            StartCoroutine("Lose");
        }
    }

    Vector3 RandomLawyerPosDestructive()
    {
        int ind = Random.Range(0, LawyerPositionObjs.Count);
        Vector3 pos = LawyerPositionObjs[ind].transform.position;
        if (pos.x < Lawyer.transform.position.x)
        {
            Lawyer.transform.localScale = Vector3.one;
        }
        else
        {
            Lawyer.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        LawyerPositionObjs.RemoveAt(ind);
        return pos;
    }

    void MoveLawyer()
    {
        if (gameOver)
        {
            return;
        }

        if (MoveLawyer(lawyerOrigin, lawyerTarget))
        {
            lawyerOrigin = Lawyer.transform.position;
            lawyerElapsed = 0f;
            lawyerTarget = RandomLawyerPosDestructive();
        }
    }

    bool MoveLawyer(Vector3 start, Vector3 end)
    {
        lawyerElapsed += Time.deltaTime;
        float progress = Mathf.Min(lawyerElapsed / lawyerDuration, 1f);
        Lawyer.transform.position = Vector3.Lerp(start, end, progress);
        return progress >= 1f && LawyerPositionObjs.Count > 0;
    }

    IEnumerator Win()
    {
        LawyerAnimator.SetTrigger("Die");
        WinText.gameObject.SetActive(true);
        WinText.rectTransform.DOPunchScale(Vector3.one * 2f, 0.25f);
        gameOver = true;

        yield return new WaitForSeconds(2f);
        MinigameCompletionHandler.WinCallback.Invoke();
    }

    IEnumerator Lose()
    {
        LoseText.SetActive(true);
        LoseText.GetComponent<RectTransform>().DOPunchScale(Vector3.one, 0.5f);
        gameOver = true;

        yield return new WaitForSeconds(2f);
        MinigameCompletionHandler.LoseCallback.Invoke();
    }
}
