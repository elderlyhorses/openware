using UnityEngine;

public class SceneLoaded : MonoBehaviour {
    void Start() {
        FindObjectOfType<SceneCoordinator.SceneCoordinator>().SceneLoaded();
    }
}