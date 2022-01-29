using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
  public GameObject Target;

  void Update()
  {
    transform.LookAt(Target.transform);
  }
}
