using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Awp {
    public class Enemy : MonoBehaviour {

        public AwpManager AwpManager;

        private void OnMouseDown() {
            AwpManager.DidShootEnemy(gameObject);
        }
    }
}