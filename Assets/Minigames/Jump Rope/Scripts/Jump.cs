using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpRope
{
  public class Jump : MonoBehaviour
  {

    public float JumpStrength;

    Rigidbody rb;
    int downFrameCount = 0;
    bool anyKeyLast = false;

    private void Awake()
    {
      rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
      if (Input.anyKeyDown)
      {
        downFrameCount = 0;
      }

      if (Input.anyKey)
      {
        downFrameCount += 1;
      }

      // anyKey Up
      if (!Input.anyKey && anyKeyLast)
      {
        float holdMultiplier = 1f;

        if (downFrameCount <= 2)
        {
          holdMultiplier = 0.8f;
        }
        else if (downFrameCount > 2 && downFrameCount <= 5)
        {
          holdMultiplier = 1f;
        }
        else if (downFrameCount > 5 && downFrameCount <= 10)
        {
          holdMultiplier = 1.15f;
        }
        else
        {
          holdMultiplier = 1.25f;
        }

        DoJump(holdMultiplier);
      }

      anyKeyLast = Input.anyKey;
    }

    public void DoJump(float multiplier = 1f)
    {
      rb.AddForce(Vector3.up * JumpStrength * multiplier, ForceMode.VelocityChange);
    }
  }
}