using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartParking
{
  public class ParkedKartSpawner : MonoBehaviour
  {
    public GameObject ParkedKartPrefab;
    public GameObject ArrowPrefab;
    public GameObject ParkedKartHighlight;

    float xOffset = 1.55f;
    float zOffset = 2.385f;
    float rowOffset = 7f;

    void Start()
    {
      // Spawn parked karts to make a parking lot
      List<GameObject> SpawnedKarts = new List<GameObject>();
      for (var row = 0; row < 3; row++)
      {
        for (var x = 0; x < 8; x++)
        {
          var a = Instantiate(ParkedKartPrefab, new Vector3(xOffset * x, 0f, 0f + rowOffset * row), Quaternion.identity);
          a.name = "Parked Kart A row:" + row + " col:" + x;
          SpawnedKarts.Add(a);
          a.transform.SetParent(transform);
          a.tag = "Collision Matters";

          var b = Instantiate(ParkedKartPrefab, new Vector3(xOffset * x, 0f, zOffset + rowOffset * row), Quaternion.Euler(0f, 180f, 0f));
          b.name = "Parked Kart B row:" + row + " col:" + x;
          SpawnedKarts.Add(b);
          b.transform.SetParent(transform);
          b.tag = "Collision Matters";
        }
      }

      // Hide a few karts so there's places to park
      SpawnedKarts = Utilities.Shuffle(SpawnedKarts);
      for (var y = 0; y < 3; y++)
      {
        foreach (Transform child in SpawnedKarts[y].transform)
        {
          if (!child.name.Contains("Line"))
          {
            child.gameObject.SetActive(false);
          }
          BoxCollider[] colliders = SpawnedKarts[y].GetComponents<BoxCollider>();
          foreach (BoxCollider c in colliders)
          {
            c.enabled = false;
          }
          SpawnedKarts[y].AddComponent<ParkedKartDistanceToPlayer>();
        }

        GameObject arrow = Instantiate(ArrowPrefab, SpawnedKarts[y].transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
        arrow.tag = "Arrow";
        arrow.transform.SetParent(transform);

        var highlight = Instantiate(ParkedKartHighlight, Vector3.zero, Quaternion.identity);
        highlight.transform.SetParent(SpawnedKarts[y].transform);
        highlight.transform.localPosition = new Vector3(0f, 0f, 0.04f);
        highlight.transform.localRotation = Quaternion.identity;
      }
    }
  }
}