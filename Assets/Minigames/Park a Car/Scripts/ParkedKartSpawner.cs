using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkedKartSpawner : MonoBehaviour
{
  public GameObject ParkedKartPrefab;

  float xOffset = 1.55f;
  float zOffset = 2.385f;
  float rowOffset = 7f;

  void Start()
  {
    for (var row = 0; row < 3; row++)
    {
      for (var x = 0; x < 8; x++)
      {
        var a = Instantiate(ParkedKartPrefab, new Vector3(xOffset * x, 0f, 0f + rowOffset * row), Quaternion.identity);
        a.name = "A";
        var b = Instantiate(ParkedKartPrefab, new Vector3(xOffset * x, 0f, zOffset + rowOffset * row), Quaternion.Euler(0f, 180f, 0f));
        b.name = "B";
      }
    }
  }
}
