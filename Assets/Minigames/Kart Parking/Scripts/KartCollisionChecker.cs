using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartParking
{
  public class KartCollisionChecker : MonoBehaviour
  {
    KartParkingGameManager gameManager;

    void Awake()
    {
      gameManager = GameObject.Find("Game Manager").GetComponent<KartParkingGameManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
      print("OnCollisionEnter with " + collision.gameObject.name);
      gameManager.DidCollideWithCar(collision.gameObject);
    }
  }
}