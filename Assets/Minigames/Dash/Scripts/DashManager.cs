using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dash
{
  public class DashManager : MonoBehaviour
  {
    public MinigameCompletionHandler MinigameCompletionHandler;
    public GameObject Target;
    public List<Animator> ComputerRunners;
    public Animator StarterPistolAnim;
    public RectTransform OnYourMarkRT;
    public RectTransform GetSetRT;
    public RectTransform GoRT;
    public RectTransform FalseStartRT;
    public RectTransform WonRaceRT;
    public RectTransform LostRaceRT;
    public GameObject HumanRunner;
    public GameObject Camera;

    private Dictionary<GameObject, Animator> AnimatorForRunner = new Dictionary<GameObject, Animator>();
    private enum GameState { waiting, running, done }
    private GameState currentState = GameState.waiting;
    private bool isOver = false;
    private Animator HumanAnim;
    private float humanSpeedCurrentPercent = 0.2f;
    private float humanSpeedTargetPercent = 0.2f;
    private float humanTopSpeed = 0.25f;
    private Vector3 punchScale = Vector3.one / 80f;
    private float punchDuration = 0.15f;

    private void Awake()
    {
      HideAllText();
      HumanAnim = HumanRunner.GetComponent<Animator>();
      StartCoroutine("Run");
    }

    private void Update()
    {
      if (isOver)
      {
        if (currentState == GameState.done)
        {
          HumanRunner.transform.Translate(-Vector3.left * humanTopSpeed * humanSpeedCurrentPercent);
        }
        return;
      }

      HandleUserInput();
      CheckRaceComplete();
    }

    void HideAllText()
    {
      WonRaceRT.gameObject.SetActive(false);
      LostRaceRT.gameObject.SetActive(false);
      FalseStartRT.gameObject.SetActive(false);
      OnYourMarkRT.gameObject.SetActive(false);
      GetSetRT.gameObject.SetActive(false);
      GoRT.gameObject.SetActive(false);
    }

    void HandleUserInput()
    {
      if (Input.anyKeyDown && currentState == GameState.waiting)
      {
        StartCoroutine("FalseStart");
        return;
      }

      if (Input.anyKeyDown && currentState == GameState.running && humanSpeedCurrentPercent < 1f)
      {
        humanSpeedTargetPercent += 50f * Time.deltaTime;
      }
      else if (!Input.anyKeyDown && currentState == GameState.running && humanSpeedCurrentPercent > 0.2f)
      {
        humanSpeedTargetPercent -= 5f * Time.deltaTime;
      }

      if (currentState == GameState.running)
      {
        // Adjust current percent, but limit the change
        var changeLimit = 0.2f;
        var diff = humanSpeedTargetPercent - humanSpeedCurrentPercent;
        var sign = diff > 0 ? 1 : -1;
        humanSpeedCurrentPercent += Mathf.Min(Mathf.Abs(diff), changeLimit) * sign;
        HumanAnim.speed = humanSpeedCurrentPercent;
        HumanRunner.transform.Translate(-Vector3.left * humanTopSpeed * humanSpeedCurrentPercent);
      }
    }

    void CheckRaceComplete()
    {
      var end = 20f;

      // Check if any of the computers are done
      foreach (Animator runner in ComputerRunners)
      {
        if (runner.gameObject.transform.position.x > end)
        {
          StartCoroutine("LostRace");
          currentState = GameState.done;
          return;
        }
      }

      if (HumanRunner.transform.position.x > end)
      {
        StartCoroutine("WonRace");
        currentState = GameState.done;
      }
    }

    IEnumerator Run()
    {
      yield return new WaitForSeconds(0.5f);
      if (isOver) { yield break; }

      OnYourMarkRT.gameObject.SetActive(true);
      OnYourMarkRT.DOPunchScale(punchScale, punchDuration);

      yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
      if (isOver) { yield break; }

      OnYourMarkRT.gameObject.SetActive(false);
      GetSetRT.gameObject.SetActive(true);
      GetSetRT.DOPunchScale(punchScale, punchDuration);

      yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.25f));
      if (isOver) { yield break; }

      Utilities.Shuffle(ComputerRunners);
      foreach (var runner in ComputerRunners)
      {
        runner.SetTrigger("Get set");
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.1f));
      }

      HumanAnim.SetTrigger("Get set");

      if (isOver) { yield break; }

      yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));

      if (isOver) { yield break; }

      GetSetRT.gameObject.SetActive(false);
      GoRT.gameObject.SetActive(true);
      GoRT.DOPunchScale(punchScale, punchDuration);
      StarterPistolAnim.SetTrigger("Fire");

      yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.025f));

      if (isOver) { yield break; }

      HumanAnim.SetTrigger("Go");
      HumanAnim.speed = humanSpeedCurrentPercent;
      currentState = GameState.running;

      Utilities.Shuffle(ComputerRunners);
      foreach (var runner in ComputerRunners)
      {
        runner.SetTrigger("Go");
        var speed = UnityEngine.Random.Range(14f, 16f);
        runner.gameObject.transform.DOMoveX(Target.transform.position.x, speed);
        runner.speed = (speed - 14f) / 2f;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.1f));
      }

      if (isOver) { yield break; }

      yield return new WaitForSeconds(1f);

      if (isOver) { yield break; }

      GoRT.gameObject.SetActive(false);
    }

    IEnumerator LostRace()
    {
      isOver = true;
      HideAllText();
      LostRaceRT.gameObject.SetActive(true);
      LostRaceRT.DOPunchScale(punchScale, punchDuration);
      yield return new WaitForSeconds(1f);
      MinigameCompletionHandler.LoseCallback.Invoke();
    }

    IEnumerator FalseStart()
    {
      isOver = true;

      HideAllText();

      HumanAnim.SetTrigger("Go");
      HumanAnim.speed = 0.5f;
      HumanRunner.gameObject.transform.DOMoveX(Target.transform.position.x, 10f);

      Camera.GetComponent<CameraFollow>().enabled = false;
      FalseStartRT.gameObject.SetActive(true);
      FalseStartRT.DOPunchScale(punchScale, punchDuration);

      yield return new WaitForSeconds(1.5f);
      MinigameCompletionHandler.LoseCallback.Invoke();
    }

    IEnumerator WonRace()
    {
      isOver = true;
      HideAllText();
      WonRaceRT.gameObject.SetActive(true);
      WonRaceRT.DOPunchScale(punchScale, punchDuration);
      yield return new WaitForSeconds(1f);
      MinigameCompletionHandler.WinCallback.Invoke();
    }
  }
}

