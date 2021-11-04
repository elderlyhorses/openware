using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Awp {
    public class Headshot : MonoBehaviour {

        public AudioClip BoomHeadshotClip;
        public AwpManager AwpManager;

        SoundManager SoundManager;

        private void Awake() {
            SoundManager = FindObjectOfType<SoundManager>();
        }

        private void OnMouseDown() {
            SoundManager.PlayOneShot(BoomHeadshotClip);
            AwpManager.DidShootEnemy(transform.parent.gameObject);
        }
    }
}