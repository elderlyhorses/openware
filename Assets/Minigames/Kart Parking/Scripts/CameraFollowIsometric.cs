using UnityEngine;
using System.Collections;

namespace KartParking
{
  public class CameraFollowIsometric : MonoBehaviour
  {
    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    void Start()
    {
      offset = transform.position - target.position;
    }

    void LateUpdate()
    {
      if (!target)
      {
        return;
      }
      Vector3 targetCamPos = target.position + offset;
      transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
  }
}