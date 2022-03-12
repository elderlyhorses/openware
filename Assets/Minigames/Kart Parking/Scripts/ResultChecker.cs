using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartParking
{
  public class ResultChecker : MonoBehaviour
  {
    public GameObject CrashKartPrefab;
    bool gameOver = false;

    void OnCollisionEnter(Collision col)
    {
      if (gameOver)
      {
        return;
      }

      if (col.gameObject.name == "Parked Kart")
      {
        gameOver = true;
        GameObject.Instantiate(CrashKartPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
      }
    }
  }
}