using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dash
{
    public class DashManager : MonoBehaviour
    {
        public GameObject Target;
        public GameObject Runner1;
        
        private Dictionary<GameObject, Animator> AnimatorForRunner = new Dictionary<GameObject, Animator>();
        
        private void Awake()
        {
            AnimatorForRunner[Runner1] = Runner1.GetComponent<Animator>();
            StartCoroutine("Run");
        }

        IEnumerator Run()
        {
            yield return new WaitForSeconds(0.5f);
            
            AnimatorForRunner[Runner1].SetTrigger("Get set");
            
            yield return new WaitForSeconds(0.5f);
            
            AnimatorForRunner[Runner1].SetTrigger("Go");

            Runner1.transform.DOMoveX(Target.transform.position.x, 25f);
        }
    }
}

