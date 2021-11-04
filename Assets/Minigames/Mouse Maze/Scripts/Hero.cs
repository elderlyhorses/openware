using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MouseMaze {
    public class Hero : MonoBehaviour
    {
        public MouseMazeManager MouseMazeManager;

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.name.StartsWith("Walls")) {
                MouseMazeManager.DidCollideWithWall();
            }
        }
    }
}
