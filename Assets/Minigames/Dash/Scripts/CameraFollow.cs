using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dash
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothTime = 0.3F;
        public Vector3 offset;
        private Vector3 velocity = Vector3.zero;

        void Update()
        {
            if (transform.position.x > 20f)
            {
                return;
            }
            
            var t = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
            transform.position = Vector3.SmoothDamp(transform.position, t, ref velocity, smoothTime);
        }
    }
}


