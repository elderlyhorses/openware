using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace KartParking
{
  public class KartParkingGameManager : MonoBehaviour
  {
    public MinigameCompletionHandler MinigameCompletionHandler;
    public GameObject CollisionKartPrefab;
    public RectTransform CrashedRT;
    public GameObject CrashedCircle;
    public RectTransform WinRT;
    public TextMeshProUGUI CountdownText;
    public RectTransform CountdownTextRT;
    public RectTransform OutOfTimeRT;
    public GameObject Instructions;
    public GameObject SpawnContainer;

    GameObject activeKart;
    bool isOver = false;
    Vector3 finishedCamPos;
    int secondsRemaining;

    void Awake()
    {
      secondsRemaining = 16;
      activeKart = GameObject.Find("Player");
      CrashedRT.gameObject.SetActive(false);
      CrashedCircle.SetActive(false);
      WinRT.gameObject.SetActive(false);
      OutOfTimeRT.gameObject.SetActive(false);
      CountdownText.gameObject.SetActive(true);
      Instructions.SetActive(true);

      InvokeRepeating("countdown", 0, 1);
    }

    public void DidSuccessfullyPark()
    {
      if (isOver)
      {
        return;
      }
      isOver = true;
      StartCoroutine("handleSuccess");
    }

    IEnumerator handleSuccess()
    {
      WinRT.gameObject.SetActive(true);
      WinRT.DOScale(CrashedRT.localScale * 1.3f, 0.2f);
      yield return new WaitForSeconds(2f);
      MinigameCompletionHandler.WinCallback.Invoke();
    }

    public void DidCollideWithCar(GameObject other)
    {
      if (other.tag != "Collision Matters")
      {
        return;
      }

      if (isOver) { return; }
      isOver = true;
      Instructions.SetActive(false);

      CrashedRT.gameObject.SetActive(true);
      CrashedRT.DOScale(CrashedRT.localScale * 1.3f, 0.2f);
      StartCoroutine("hideCrashedRT");
      finishedCamPos = Vector3.Lerp(activeKart.transform.position, other.transform.position, 0.5f);

      GameObject k = Instantiate(CollisionKartPrefab, activeKart.transform.position, activeKart.transform.rotation);
      k.transform.SetParent(SpawnContainer.transform);

      Destroy(activeKart);
    }

    IEnumerator hideCrashedRT()
    {
      yield return new WaitForSeconds(1f);
      CrashedRT.gameObject.SetActive(false);

      GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrow");
      foreach (GameObject arrow in arrows)
      {
        arrow.SetActive(false);
      }

      Camera.main.GetComponent<CameraFollowIsometric>().enabled = false;

      finishedCamPos.y = 15f;
      Camera.main.transform.position = finishedCamPos;
      Camera.main.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

      Camera.main.orthographicSize = 2.4f;

      CrashedCircle.SetActive(true);

      yield return new WaitForSeconds(1f);
      MinigameCompletionHandler.LoseCallback.Invoke();
    }

    void countdown()
    {
      if (isOver)
      {
        return;
      }

      secondsRemaining--;
      CountdownText.text = "" + secondsRemaining;

      if (secondsRemaining <= 0)
      {
        isOver = true;
        Instructions.SetActive(false);

        OutOfTimeRT.gameObject.SetActive(true);
        OutOfTimeRT.DOScale(CrashedRT.localScale * 1.3f, 0.2f);
        GameObject k = Instantiate(CollisionKartPrefab, activeKart.transform.position, activeKart.transform.rotation);
        k.transform.SetParent(SpawnContainer.transform);
        Destroy(activeKart);

        StartCoroutine("loseAfterDelay");
      }
    }

    IEnumerator loseAfterDelay()
    {
      yield return new WaitForSeconds(1f);
      MinigameCompletionHandler.LoseCallback.Invoke();
    }
  }
}