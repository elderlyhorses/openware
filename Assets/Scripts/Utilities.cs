using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class Utilities {
    public static List<string> MinigameScenes() {
        List<string> sceneNames = new List<string>();
        // Skip `SceneCoordinator`, `Main Menu`, `Minigame Menu`, `Score and lives`, and `Transition`
        for (int x = 5; x < SceneManager.sceneCountInBuildSettings; x++) {
            string name = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(x));
            sceneNames.Add(name);
        }
        sceneNames.Sort();
        return sceneNames;
    }
    
    // From https://stackoverflow.com/a/58599209
    public static List<T> Shuffle<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }

        return _list;
    }
}