using UnityEngine;

public class SceneLoaded : MonoBehaviour {
    void Start() {
        FindObjectOfType<SuperManager.SuperManager>().SceneLoaded();
    }
}