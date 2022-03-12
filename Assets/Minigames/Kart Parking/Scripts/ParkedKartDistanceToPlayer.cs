using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartParking
{
  public class ParkedKartDistanceToPlayer : MonoBehaviour
  {
    KartParkingGameManager gameManager;
    GameObject player;
    bool isOver = false;

    void Awake()
    {
      player = GameObject.Find("Player");
      gameManager = GameObject.Find("Game Manager").GetComponent<KartParkingGameManager>();
    }

    void Update()
    {
      if (isOver)
      {
        return;
      }

      if (!player)
      {
        isOver = true;
        return;
      }

      float d = Vector3.Distance(player.transform.position, transform.position);
      if (d < 0.7)
      {
        gameManager.DidSuccessfullyPark();
      }
    }
  }
}