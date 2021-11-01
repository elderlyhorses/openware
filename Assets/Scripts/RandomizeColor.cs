using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeColor : MonoBehaviour
{
    void Awake() {
        GetComponent<Image>().color = Colors.RandomColor();
    }
}
