using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
  public List<Material> Materials;

  void Start()
  {
    var ind = Random.Range(0, Materials.Count);
    GetComponent<SkinnedMeshRenderer>().material = Materials[ind];
  }
}
